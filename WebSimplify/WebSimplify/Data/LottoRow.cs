using SynnCore.DataAccess;
using SynnCore.Generics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace WebSimplify
{
    public class LottoRow
    {
        public LottoRow()
        {
            Wins = new List<LottoWin>();
        }
        public LottoRow(IDataReader data)
        {
            Load(data);
        }

        public DateTime CreationDate { get; set; }
        public int Id { get;  set; }

        internal List<int> GetNumbers()
        {
            return new List<int> { N1,N2,N3,N4,N5,N6 };
        }

        public string GetCodedNumbers()
        {
            var N = GetNumbers().OrderByDescending(x => x).ToList();
            return string.Format("{0}#{1}#{2}#{3}#{4}#{5}", N[0], N[1], N[2], N[3], N[4], N[5]);
        }

        public int N1 { get;  set; }
        public int N2 { get;  set; }
        public int N3 { get;  set; }
        public int N4 { get;  set; }
        public int N5 { get;  set; }
        public int N6 { get;  set; }
        public DateTime PoleDestinationDate { get;  set; }
        public string PoleKey { get;  set; }
        public int SpecialNumber { get;  set; }
        public List<LottoWin> Wins { get; set; }

        public void Load(IDataReader reader)
        {
            Id = DataAccessUtility.LoadInt32(reader, "Id");
            N1 = DataAccessUtility.LoadInt32(reader, "N1");
            N2 = DataAccessUtility.LoadInt32(reader, "N2");
            N3 = DataAccessUtility.LoadInt32(reader, "N3");
            N4 = DataAccessUtility.LoadInt32(reader, "N4");
            N5 = DataAccessUtility.LoadInt32(reader, "N5");
            N6 = DataAccessUtility.LoadInt32(reader, "N6");
            SpecialNumber = DataAccessUtility.LoadInt32(reader, "S");

            PoleKey = DataAccessUtility.LoadNullable<string>(reader, "PoleKey");
            PoleDestinationDate = DataAccessUtility.LoadNullable<DateTime>(reader, "PoleDestinationDate");
            CreationDate = DataAccessUtility.LoadNullable<DateTime>(reader, "CreationDate");

            string wWinsData = DataAccessUtility.LoadNullable<string>(reader, "WinsData");
            if (string.IsNullOrEmpty(wWinsData))
                Wins = new List<LottoWin>();
            else
                Wins = XmlHelper.CreateFromXml<List<LottoWin>>(wWinsData);

        }
    }


    public class LottoPole
    {
        public LottoPole()
        {
            Wins = new List<LottoWin>();
        }
        public LottoPole(IDataReader data)
        {
            Load(data);
        }

        public int Id { get; set; }
        public int N1 { get; set; }
        public int N2 { get; set; }
        public int N3 { get; set; }
        public int N4 { get; set; }
        public int N5 { get; set; }
        public int N6 { get; set; }
        public DateTime PoleActionDate { get; set; }
        public string PoleKey { get; set; }
        public int SpecialNumber { get; set; }
        public List<LottoWin> Wins { get; set; }
        public string WinsText
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (LottoWin item in Wins)
                    sb.AppendLine(item.GedDescription());
                return sb.ToString();
            }
        }

        public string GetCodedNumbers()
        {
            var N = GetNumbers().OrderByDescending(x => x).ToList();
            return string.Format("{0}#{1}#{2}#{3}#{4}#{5}", N[0], N[1], N[2], N[3], N[4], N[5]);
        }
        public void Load(IDataReader reader)
        {
            Id = DataAccessUtility.LoadInt32(reader, "Id");
            N1 = DataAccessUtility.LoadInt32(reader, "N1");
            N2 = DataAccessUtility.LoadInt32(reader, "N2");
            N3 = DataAccessUtility.LoadInt32(reader, "N3");
            N4 = DataAccessUtility.LoadInt32(reader, "N4");
            N5 = DataAccessUtility.LoadInt32(reader, "N5");
            N6 = DataAccessUtility.LoadInt32(reader, "N6");
            SpecialNumber = DataAccessUtility.LoadInt32(reader, "S");

            PoleKey = DataAccessUtility.LoadNullable<string>(reader, "PoleKey");
            PoleActionDate = DataAccessUtility.LoadNullable<DateTime>(reader, "PoleActionDate");

            string wWinsData = DataAccessUtility.LoadNullable<string>(reader, "WinsData");
            if (string.IsNullOrEmpty(wWinsData))
                Wins = new List<LottoWin>();
            else
                Wins = XmlHelper.CreateFromXml<List<LottoWin>>(wWinsData);

        }

        internal List<int> GetNumbers()
        {
            return new List<int> { N1, N2, N3, N4, N5, N6 };
        }
    }

    public enum LottoWin
    {
        [Description("1")]
        One = 2,
        [Description("1+")]
        OnePlus = 3,
        [Description("2")]
        Two = 4,
        [Description("2+")]
        TwoPlus = 5,
        [Description("3")]
        Three = 6,
        [Description("3+")]
        ThreePlus = 7,
        [Description("4")]
        Four = 8,
        [Description("4+")]
        FourPlus = 9,
        [Description("5")]
        Five = 10,
        [Description("5+")]
        FivePlus = 11,
        [Description("6")]
        Six = 12,
        [Description("$$$")]
        JackPot = 13,
        [Description("0")]
        None = 0,
        [Description("0+")]
        NonePlus = 1
    }
}