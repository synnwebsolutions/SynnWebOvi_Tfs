using SynnCore.DataAccess;
using SynnWebOvi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSimplify.DataAccess
{
    public class SqlDbGenericData : SqlDbController, IDbGenericData
    {
        public SqlDbGenericData(string _connectionString) : base(new SynnSqlDataProvider(_connectionString))
        {
        }

        public List<T> GetGenericData<T>(GenericDataSearchParameters sp)
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.GenericData);
            ClearParameters();

            AddSqlWhereField("GenericDataEnum", (int)sp.GenericDataEnum);

            if (sp.Id.HasValue)
                AddSqlWhereField("Id", sp.Id);

            IList lst = (IList)Activator.CreateInstance(GetListType(typeof(T)));
            FillList(lst, typeof(T));
            return (List<T>)lst;
        }

        private Type GetListType(Type sp)
        {
            Type listGenericType = typeof(List<>);
            return  listGenericType.MakeGenericType(sp);
        }
        

        public void Add(GenericData u)
        {
            SqlItemList sqlItems = Get(u);
            SetInsertIntoSql(SynnDataProvider.TableNames.GenericData, sqlItems);
            ExecuteSql();
        }

        private static SqlItemList Get(GenericData u)
        {
            var sqlItems = new SqlItemList();

            sqlItems.Add(new SqlItem("Active", u.Active));
            sqlItems.Add(new SqlItem("CreationDate", u.CreationDate));
            sqlItems.Add(new SqlItem("Description", u.Description));
            sqlItems.Add(new SqlItem("GenericDataType", u.GenericDataType));
            sqlItems.Add(new SqlItem("UpdateDate", u.UpdateDate));

            return sqlItems;
        }
    }
}