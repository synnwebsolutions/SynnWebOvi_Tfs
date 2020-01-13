using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public class UserDeposit : GenericData
    {

        public int UserId { get; set; }
        [GenericDataField("UserIdText", "UserId")]
        public string UserIdText
        {
            get { return UserId.ToString(); }
            set { UserId = value.ToInteger(); }
        }

        public int AccountId { get; set; }
        [GenericDataField("AccountText", "AccountId")]
        public string AccountText
        {
            get { return AccountId.ToString(); }
            set { AccountId = value.ToInteger(); }
        }

        public DateTime IDate { get; set; }
        [GenericDataField("IDateText", "IDate")]
        public string IDateText
        {
            get { return IDate.ToString(); }
            set { IDate = value.ToDateTime(); }
        }

        public int Amount { get; set; }
        [GenericDataField("AmountText", "Amount")]
        public string AmountText
        {
            get { return Amount.ToString(); }
            set { Amount = value.ToInteger(); }
        }


        internal override string FormatedGenericValue(string valueToFormat, GenericDataFieldAttribute genericFieldInfo, IDatabaseProvider db)
        {
            if (genericFieldInfo.PropertyName == "UserIdText")
            {
                if (valueToFormat.IsInteger())
                {
                    var u = db.DbAuth.GetUser(valueToFormat.ToInteger());
                    return u.DisplayName;
                }
            }
            if (genericFieldInfo.PropertyName == "DepositIdText")
            {
                if (valueToFormat.IsInteger())
                {
                    var u = db.DbGenericData.GetSingleGenericData(new GenericDataSearchParameters { Id = valueToFormat.ToInteger(), FromType = typeof(Account) });
                    return (u as Account).DepositName;
                }
            }
            return base.FormatedGenericValue(valueToFormat, genericFieldInfo, db);
        }
        public UserDeposit(IDataReader data)
        {
            Load(data);
        }

        public UserDeposit()
        {

        }
    }
}