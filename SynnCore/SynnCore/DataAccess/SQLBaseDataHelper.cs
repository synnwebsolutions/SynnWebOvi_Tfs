using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynnCore.DataAccess
{
    public class SQLBaseDataHelper : IDatabaseHelper
    {
        static string connstr = null;
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;
        SqlDataAdapter adapter;

        public SQLBaseDataHelper(string connetionString)
        {
            connstr = connetionString;
        }

        public bool Delete(string iquery)
        {
            return ExecuteNonQuery(iquery);
        }

        private bool ExecuteNonQuery(string iquery)
        {
            bool result = false;
            using (connection = new SqlConnection(connstr))
            using (command = new SqlCommand(iquery, connection))
            {
                connection.Open();
                result = Convert.ToBoolean(command.ExecuteNonQuery());
                connection.Close();
            }
            return result;
        }

        public IList Get(string SelectCommand, Type ListType, Type ItemType)
        {
            if (!ItemType.IsAssignableFrom(typeof(IDatabaseLoadable)))
                throw new Exception("Item must implement IDatabaseLoadable");

            IList lst = (IList)Activator.CreateInstance(ListType, null);
            using (connection = new SqlConnection(connstr))
            {
                connection.Open();
                using (command = new SqlCommand(SelectCommand, connection))
                {
                    using (reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = (IDatabaseLoadable)Activator.CreateInstance(ItemType, null);
                            item.Load(reader);
                            lst.Add(item);
                        }
                    }
                }
            }
            return lst;
        }

        public DataSet GetTableData(string iquery)
        {
            string query = iquery;
            DataSet data = new DataSet();
            using (connection = new SqlConnection(connstr))
            using (adapter = new SqlDataAdapter(query, connection))
            {
                adapter.Fill(data);
            }
            return data;
        }

        public bool Insert(string iquery)
        {
            return ExecuteNonQuery(iquery);
        }

        public bool Update(string iquery)
        {
            return ExecuteNonQuery(iquery);
        }

    }
}
