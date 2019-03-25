using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public class LottoStatsHelper
    {
        public static IDatabaseProvider DBController = SynnDataProvider.DbProvider;

        public static List<LottoPole> Poles { get; private set; }
        public static List<LottoRow> Results { get; private set; }

        internal static List<LottoRow> Generate(int numOfRows, DateTime newDate, LottoStatItem lsi)
        {
            Results = new List<LottoRow>();
            Poles = DBController.DbLotto.Get(new LottoPolesSearchParameters { }).OrderByDescending(x => x.PoleActionDate).ToList();
            switch (lsi)
            {
                case LottoStatItem.MostApperancesPole:
                     GetMostApperancesPole();
                    break;
                case LottoStatItem.MostWiningNumbers:
                     GetMostWiningNumbers();
                    break;
                case LottoStatItem.StrongestSpecialNumber:
                     GetStrongestSpecialNumber();
                    break;
                default:
                    break;
            }
            return Results;
        }

        private static void GetMostWiningNumbers()
        {
           
            var dataFlags = LottoHandler.GenerateCountFlags();
            foreach (var pole in Poles)
                foreach (var poleNum in pole.GetNumbers())
                    if (poleNum <= 37)
                    {
                        if (dataFlags.ContainsKey(poleNum))
                            dataFlags[poleNum] += 1;
                        else
                            dataFlags.Add(poleNum, 0);
                    }

        }

        private static void GetStrongestSpecialNumber()
        {
            throw new NotImplementedException();
        }

        private static void GetMostApperancesPole()
        {
            throw new NotImplementedException();
        }
    }

    public enum LottoStatItem
    {
        [Description("הגרלה עם הכי הרבה מופעים")]
        MostApperancesPole,
        [Description("מספרים עם הכי הרבה זכיות")]
        MostWiningNumbers,
        [Description("מספר חזק עם הכי הרבה מופעים")]
        StrongestSpecialNumber,
    }
}