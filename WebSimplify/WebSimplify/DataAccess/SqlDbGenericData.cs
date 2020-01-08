using SynnCore.DataAccess;
using SynnWebOvi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using WebSimplify.Data;

namespace WebSimplify.DataAccess
{
    public class SqlDbGenericData : SqlDbController, IDbGenericData
    {
        public SqlDbGenericData(string _connectionString) : base(new SynnSqlDataProvider(_connectionString))
        {
        }

        public List<T> GetGenericData<T>(GenericDataSearchParameters sp)
        {
            sp.FromType = typeof(T);
            return GetGenericData(sp) as List<T>;
        }

        private Type GetListType(Type sp)
        {
            Type listGenericType = typeof(List<>);
            return  listGenericType.MakeGenericType(sp);
        }

        public void Add(GenericData u)
        {
            u.CreationDate = DateTime.Now;
            u.UpdateDate = DateTime.Now;
            SqlItemList sqlItems = Get(u);
            SetInsertIntoSql(u.GetType().Name, sqlItems);
            ExecuteSql();
        }

        private static SqlItemList Get(GenericData u)
        {
            var sqlItems = new SqlItemList();

            sqlItems.Add(new SqlItem("Active", u.Active));
            sqlItems.Add(new SqlItem("CreationDate", u.CreationDate));
            sqlItems.Add(new SqlItem("Description", u.Description));
            sqlItems.Add(new SqlItem("UpdateDate", u.UpdateDate));

            var props = u.GetType().GetProperties();
            foreach (var pinfo in props)
            {
                var genericDataField = ((GenericDataFieldAttribute[])pinfo.GetCustomAttributes(typeof(GenericDataFieldAttribute), true)).FirstOrDefault();
                if (genericDataField != null)
                {
                    var val = pinfo.GetValue(u);
                    sqlItems.Add(new SqlItem(genericDataField.FieldName, val ?? string.Empty));
                }
            }

            return sqlItems;
        }

        public void Update(GenericData g)
        {
            g.UpdateDate = DateTime.Now;
            SqlItemList sqlItems = Get(g);

            SetUpdateSql(g.GetType().Name, sqlItems, new SqlItemList { new SqlItem { FieldName = "Id", FieldValue = g.Id } });
            ExecuteSql();
        }

        public IEnumerable GetGenericData(GenericDataSearchParameters sp)
        {
            if (sp.FromType == null)
                throw new Exception("No Generic Type Detected");
            var type = sp.FromType;

            SetSqlFormat("select * from {0}", type.Name);
            ClearParameters();

            sp.AppendExtraFieldsValues();
            foreach (var item in sp.Filters)
                AddSqlWhereField(item.FieldName, item.Value);

            IList lst = (IList)Activator.CreateInstance(GetListType(type));
            FillList(lst, type);
            return lst;
        }

        public object GetSingleGenericData(GenericDataSearchParameters genericDataSearchParameters)
        {
            return GetGenericData(genericDataSearchParameters).OfType<object>().FirstOrDefault(); 
        }
    }

    public class GenericDataDbFilter
    {
        public GenericDataDbFilter(string fName, object val)
        {
            this.FieldName = fName;
            this.Value = val;
        }

        public string FieldName { get; set; }
        public object Value { get; set; }
    }
}