using SynnCore.DataAccess;
using SynnCore.Generics;
using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebSimplify.Data;

namespace WebSimplify
{
    public class SqlDbShifts : SqlDbController, IDbShifts
    {
        public SqlDbShifts(string _connectionString) : base(new SynnSqlDataProvider(_connectionString))
        {

        }

        public UserShiftsContainer GetShiftsData(ShiftsSearchParameters sp)
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.ShiftsData);
            ClearParameters();
            
            SetPermissions(sp);

            UserShiftsContainer prefs = new UserShiftsContainer();
            using (IDataReader data = DoSelect())
            {
                if (data.Read())
                    prefs = XmlHelper.CreateFromXml<UserShiftsContainer>(data["ShiftData"].ToString());
                return prefs;
            }
        }

        private void SetPermissions(ShiftsSearchParameters sp)
        {
            if (sp.RequirePrivateKeyOnly)
                AddSqlWhereField("UserId", sp.CurrentUser.Id.ToString());
            else
                AddSqlWhereField("UserGroupId", sp.UserGroupId.ToString());
        }

        public void Save(ShiftsSearchParameters sp)
        {
            string prefs = XmlHelper.ToXml(sp.ItemForAction);
            DeleteExsisting(sp);
            SetSqlFormat("insert  into {0} ( ShiftData, UserId,UserGroupId) values ( ?,?,? )", SynnDataProvider.TableNames.ShiftsData);
            ClearParameters();
            SetParameters(prefs, sp.CurrentUser.Id.ToString(), sp.UserGroupId.ToString());
            ExecuteSql();
        }

        private void DeleteExsisting(ShiftsSearchParameters sp)
        {
            SetSqlFormat("delete {0}", SynnDataProvider.TableNames.ShiftsData);
            ClearParameters();
            SetPermissions(sp);
            ExecuteSql();
        }
    }
}