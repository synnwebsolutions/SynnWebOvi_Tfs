using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public class MoneyTransactionTemplate : IDbLoadable
    {
        public MoneyTransactionTemplate()
        {

        }

        public MoneyTransactionTemplate(IDataReader data)
        {
            Load(data);
        }

        public void Load(IDataReader reader)
        {
            TransactionType = (MonthlyTransactionType)DataAccessUtility.LoadInt32(reader, "TransactionType");
            Amount = DataAccessUtility.LoadNullable<int>(reader, "Amount");
            Name = DataAccessUtility.LoadNullable<string>(reader, "Name");
            Id = DataAccessUtility.LoadInt32(reader, "Id");
            Active = DataAccessUtility.LoadNullable<bool>(reader, "Active");
            Auto = DataAccessUtility.LoadNullable<bool>(reader, "Auto");
            UserGroupId = DataAccessUtility.LoadInt32(reader, "UserGroupId");
        }

        public MonthlyTransactionType TransactionType { get; set; }
        public int UserGroupId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public bool Auto { get; set; }
        public int Amount { get; set; }
    }
    public class MonthlyMoneyTransaction : IDbLoadable
    {
        public MonthlyMoneyTransaction()
        {

        }

        public MonthlyMoneyTransaction(IDataReader data)
        {
            Load(data);
        }

      
        public int Amount { get; set; }
        public DateTime Month { get; set; }
        public int Id { get; set; }
        public int TemplateId { get; set; }
       
        public int UserGroupId { get; set; }

        public void Load(IDataReader reader)
        {
            Amount = DataAccessUtility.LoadNullable<int>(reader, "Amount");
            Id = DataAccessUtility.LoadInt32(reader, "Id");
            TemplateId = DataAccessUtility.LoadInt32(reader, "TemplateId");
            Month = DataAccessUtility.LoadNullable<DateTime>(reader, "Month");
            UserGroupId = DataAccessUtility.LoadInt32(reader, "UserGroupId");
        }
    }

    public enum MonthlyTransactionType
    {
        [Description("חובה")]
        Debit,
        [Description("זכות")]
        Credit
    }
}