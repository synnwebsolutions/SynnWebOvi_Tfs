using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynnCore.DataAccess
{
    public class MySQLBaseDataHelper : IDatabaseHelper
    {

        static string connstr = null;
        MySqlConnection connection;
        MySqlCommand command;
        MySqlDataReader reader;
        MySqlDataAdapter adapter;


        private bool ExecuteNonQuery(string iquery)
        {
            bool result = false;
            using (connection = new MySqlConnection(connstr))
            using (command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = iquery;
                int res = command.ExecuteNonQuery();
            }
            return result;
        }

        public bool Delete(string iquery)
        {
            return ExecuteNonQuery(iquery);
        }

        public IList Get(string SelectCommand, Type ListType, Type ItemType)
        {
            if (!ItemType.IsAssignableFrom(typeof(IDatabaseLoadable)))
                throw new Exception("Item must implement IDatabaseLoadable");

            IList lst = (IList)Activator.CreateInstance(ListType, null);
            using (connection = new MySqlConnection(connstr))
            {
                connection.Open();
                using (command = connection.CreateCommand())
                {
                    command.CommandText = SelectCommand;
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
            using (connection = new MySqlConnection(connstr))
            using (adapter = new MySqlDataAdapter(query, connection))
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
