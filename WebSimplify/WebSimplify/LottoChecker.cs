using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SynnWebOvi;

namespace WebSimplify
{
    public class LottoHandler
    {
        const LottoWin minWinState = LottoWin.Three;
        private static Random Rnd = new Random();
        private static readonly object syncLock = new object();
        private static bool Match(LottoPole pole, LottoRow row)
        {
            row.PoleKey = pole.PoleKey;
            row.PoleDestinationDate = pole.PoleActionDate;

            LottoWin state = GetWinState(pole, row);

            row.Wins.Add(state);
            if (state != LottoWin.OnePlus && state != LottoWin.TwoPlus && state != LottoWin.NonePlus && state >= minWinState)
                pole.Wins.Add(state);
            return state >= minWinState;
        }

        private static LottoWin GetWinState(LottoPole pole, LottoRow row)
        {
            LottoWin state = LottoWin.None;
            List<int> polenums = pole.GetNumbers();
            List<int> trynums = row.GetNumbers();
            var res = polenums.Intersect(trynums).ToList().Count;

            state = Calculate(res,pole,row);
            return state;
        }

        private static LottoWin Calculate(int intersection, LottoPole pole, LottoRow row)
        {
            switch (intersection)
            {
                case 1:
                    return SetSpecialEffect(LottoWin.One, LottoWin.OnePlus, pole, row);
                case 2:
                    return SetSpecialEffect(LottoWin.Two, LottoWin.TwoPlus, pole, row);
                case 3:
                    return SetSpecialEffect(LottoWin.Three, LottoWin.ThreePlus, pole, row);
                case 4:
                    return SetSpecialEffect(LottoWin.Four, LottoWin.FourPlus, pole, row);
                case 5:
                    return SetSpecialEffect(LottoWin.Five, LottoWin.FivePlus, pole, row);
                case 6:
                    return SetSpecialEffect(LottoWin.Six, LottoWin.JackPot, pole, row);
                default:
                    return SetSpecialEffect(LottoWin.None, LottoWin.NonePlus, pole, row);
            }
        }

        private static LottoWin SetSpecialEffect(LottoWin cState,LottoWin altWinstate, LottoPole pole, LottoRow row)
        {
            if (pole.SpecialNumber == row.SpecialNumber)
                return altWinstate;
            return cState;
        }

        internal static List<LottoRow> Generate(int numOfRows, DateTime destinationDate)
        {
            var r = new List<LottoRow>();
            for (int i = 0; i < numOfRows; i++)
            {
                var ri = new LottoRow();
                Dictionary<int, bool> flags = GenerateFlags();
                Dictionary<int, bool> speFlags = GenerateFlags(true);
                ri.CreationDate = DateTime.Now;
                ri.PoleDestinationDate = destinationDate;
                ri.N1 = GenerateNumber(flags);
                ri.N2 = GenerateNumber(flags);
                ri.N3 = GenerateNumber(flags);
                ri.N4 = GenerateNumber(flags);
                ri.N5 = GenerateNumber(flags);
                ri.N6 = GenerateNumber(flags);
                ri.SpecialNumber = GenerateNumber(speFlags, true);

                if (!r.Any(x => x.GetCodedNumbers() == ri.GetCodedNumbers()))
                    r.Add(ri);
                else
                    i -= 1;
            }
            return r;
        }

        private static int GenerateNumber(Dictionary<int, bool> flags, bool specialNumber = false)
        {
            int res = 0;
            //Rnd = new Random();
            int maxV = specialNumber ? 8 : 38;

            do
            {
                lock (syncLock)
                { // synchronize
                    res = Rnd.Next(1, maxV);
                }
            }
            while (flags[res]);

            flags[res] = true;
            return res;
        }

        public static Dictionary<int, bool> GenerateFlags(bool specialNumber = false)
        {
            var d = new Dictionary<int, bool>();
            int maxV = specialNumber ? 8 : 38;

            for (int i = 1; i < maxV; i++)
                d.Add(i, false);
            return d;
        }

        public static Dictionary<int, int> GenerateCountFlags(bool specialNumber = false)
        {
            var d = new Dictionary<int, int>();
            int maxV = specialNumber ? 8 : 38;

            for (int i = 1; i < maxV; i++)
                d.Add(i, 0);
            return d;
        }

        internal static void FindMatches(IDatabaseProvider dBController, LottoPole cp)
        {
            if (cp != null)
            {
                List<LottoRow> rows = dBController.DbLotto.Get(new LottoRowsSearchParameters { PoleActionDate = cp.PoleActionDate, PoleKey = cp.PoleKey });
                bool match = false;
                foreach (var row in rows)
                {
                    if (Match(cp, row))
                    {
                        match = true;
                        dBController.DbLotto.Update(row);
                    }
                }
                if (match)
                    dBController.DbLotto.Update(cp);
            }
        }

        internal static LottoWin TestMatch(LottoPole pole, LottoRow trow)
        {
            return GetWinState(pole,trow);
        }
    }
}