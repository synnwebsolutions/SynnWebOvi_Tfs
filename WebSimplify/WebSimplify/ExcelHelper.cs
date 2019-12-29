using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SynnWebOvi;
using System.IO;

namespace WebSimplify
{
    public class ExcelHelper
    {
        const string excPa = @"C:\Users\adelasm\Desktop\SYNN Data\ltt.csv";
        internal static void Perform(IDatabaseProvider dB)
        {
            if (File.Exists(excPa))
            {
                var lines = File.ReadAllLines(excPa);
                var lis = new List<LottoPole>();
                foreach (var line in lines)
                {
                    var vals = line.Split(',').Where(x => x.Length > 0).ToList();
                    if (vals.Count == 9)
                    {
                        var lp = new LottoPole
                        {
                            PoleKey = vals[0],
                            PoleActionDate = Convert.ToDateTime(vals[1]),
                            N1 = Convert.ToInt32(vals[2]),
                            N2 = Convert.ToInt32(vals[3]),
                            N3 = Convert.ToInt32(vals[4]),
                            N4 = Convert.ToInt32(vals[5]),
                            N5 = Convert.ToInt32(vals[6]),
                            N6 = Convert.ToInt32(vals[7]),
                            SpecialNumber = Convert.ToInt32(vals[8]),
                        };
                        lis.Add(lp);
                        //dB.DbLotto.AddLottoPole(lp);
                    }
                }

                var ttlcnt = lis.Count;
            }
        }
    }
}