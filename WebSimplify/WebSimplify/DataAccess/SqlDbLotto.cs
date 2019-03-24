using SynnCore.DataAccess;
using SynnCore.Generics;
using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public class SqlDbLotto : SqlDbController, IDbLotto
    {
        public SqlDbLotto(string _connectionString) : base(new SynnSqlDataProvider(_connectionString))
        {
        }

        public void AddLottoPole(LottoPole lp)
        {
            SqlItemList sqlItems = Get(lp);
            SetInsertIntoSql(SynnDataProvider.TableNames.LottoPoles, sqlItems);
            ExecuteSql();
        }

        private SqlItemList Get(LottoPole n)
        {
            var i = new SqlItemList();
            i.Add(new SqlItem("N1", n.N1));
            i.Add(new SqlItem("N2", n.N2));
            i.Add(new SqlItem("N3", n.N3));
            i.Add(new SqlItem("N4", n.N4));
            i.Add(new SqlItem("N5", n.N5));
            i.Add(new SqlItem("N6", n.N6));
            i.Add(new SqlItem("S", n.SpecialNumber));

            i.Add(new SqlItem("PoleKey", n.PoleKey));
            i.Add(new SqlItem("PoleActionDate", n.PoleActionDate));
            i.Add(new SqlItem("WinsData", XmlHelper.ToXml(n.Wins)));
            return i;
        }

        private SqlItemList Get(LottoRow n)
        {
            var i = new SqlItemList();
            i.Add(new SqlItem("N1", n.N1));
            i.Add(new SqlItem("N2", n.N2));
            i.Add(new SqlItem("N3", n.N3));
            i.Add(new SqlItem("N4", n.N4));
            i.Add(new SqlItem("N5", n.N5));
            i.Add(new SqlItem("N6", n.N6));
            i.Add(new SqlItem("S", n.SpecialNumber));

            i.Add(new SqlItem("PoleKey", n.PoleKey));
            i.Add(new SqlItem("PoleDestinationDate", n.PoleDestinationDate));
            i.Add(new SqlItem("CreationDate", n.CreationDate));
            i.Add(new SqlItem("WinsData", XmlHelper.ToXml(n.Wins)));
            return i;
        }

        public void AddLottoRow(LottoRow lr)
        {
            SqlItemList sqlItems = Get(lr);
            SetInsertIntoSql(SynnDataProvider.TableNames.LottoRows, sqlItems);
            ExecuteSql();
        }

        public List<LottoRow> Get(LottoRowsSearchParameters mp)
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.LottoRows);
            ClearParameters();
            if (mp.Id.HasValue)
                AddSqlWhereField("Id", mp.Id.Value);

            if (mp.PoleActionDate.HasValue)
                AddSqlWhereField("PoleDestinationDate", mp.PoleActionDate.Value);
            else if (!string.IsNullOrEmpty(mp.PoleKey))
                AddSqlWhereField("PoleKey", mp.PoleKey);

            var lst = new List<LottoRow>();
            FillList(lst, typeof(LottoRow));
            return lst;
        }

        public List<LottoPole> Get(LottoPolesSearchParameters mp)
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.LottoPoles);
            ClearParameters();
            if (mp.Id.HasValue)
                AddSqlWhereField("Id", mp.Id.Value);

            var lst = new List<LottoPole>();
            FillList(lst, typeof(LottoPole));
            return lst.OrderByDescending(x => x.PoleActionDate).ToList();
        }

        public void Update(LottoRow u)
        {
            SqlItemList sqlItems = Get(u);
            var wItems = new SqlItemList { new SqlItem("Id", u.Id) };
            SetUpdateSql(SynnDataProvider.TableNames.LottoRows, sqlItems, wItems);
            ExecuteSql();
        }

        public void Update(LottoPole u)
        {
            SqlItemList sqlItems = Get(u);
            var wItems = new SqlItemList { new SqlItem("Id", u.Id) };
            SetUpdateSql(SynnDataProvider.TableNames.LottoPoles, sqlItems, wItems);
            ExecuteSql();
        }
    }
}