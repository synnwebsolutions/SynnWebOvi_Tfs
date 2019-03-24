using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public class LottoHandler
    {
        const LottoWin minWinState = LottoWin.Three;
        private static Random Rnd = new Random();
        private static readonly object syncLock = new object();
        internal static bool Match(LottoPole pole, LottoRow row)
        {
            row.PoleKey = pole.PoleKey;
            row.PoleDestinationDate = pole.PoleActionDate;

            LottoWin state = LottoWin.None;
            List<int> polenums = pole.GetNumbers();
            List<int> trynums = row.GetNumbers();
            var res = polenums.Intersect(trynums).ToList().Count;
            if (pole.SpecialNumber == row.SpecialNumber)
                res += 10;

            state = (LottoWin)res;

            row.Wins.Add(state);
            if (state >= minWinState)
                pole.Wins.Add(state);
            return state >= minWinState;
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
                r.Add(ri);
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

        private static Dictionary<int, bool> GenerateFlags(bool specialNumber = false)
        {
            var d = new Dictionary<int, bool>();
            int maxV = specialNumber ? 8 : 38;

            for (int i = 1; i < maxV; i++)
                d.Add(i, false);
            return d;
        }
    }
}