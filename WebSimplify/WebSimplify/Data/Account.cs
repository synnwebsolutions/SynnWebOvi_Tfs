using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public enum DepositTypeEnum
    {
        FixedAmount,
        MonthlyPayment,
    }

    public class Account : GenericData
    {
        public int AmountForPerson { get; set; }
        [GenericDataField("AmountForPersonText", "AmountForPerson")]
        public string AmountForPersonText
        {
            get { return AmountForPerson.ToString(); }
            set { AmountForPerson = value.ToInteger(); }
        }

        public DepositTypeEnum DepositType { get; set; }
        [GenericDataField("DepositTypeText", "DepositType")]
        public string DepositTypeText
        {
            get { return ((int)DepositType).ToString(); }
            set { DepositType = value.ToEnum<DepositTypeEnum>(); }
        }

        [GenericDataField("DepositName", "DepositName")]
        public string DepositName { get; set; }

        public DateTime StartDate { get; internal set; }
        [GenericDataField("StartDateText", "StartDate")]
        public string StartDateText
        {
            get { return StartDate.ToString(); }
            set { StartDate = value.ToDateTime(); }
        }

        internal int GetMyBalnace(List<UserDeposit> myDeposits, IDatabaseProvider dBController)
        {
            var toBepaid = GetTotalToPay();
            var totalPaid = myDeposits.Where(x => x.AccountId == this.Id).Sum(x => x.Amount);
            return toBepaid - totalPaid;
        }

        internal int GetTotalToPay()
        {
            var toBepaid = 0;

            if (DepositType == DepositTypeEnum.MonthlyPayment)
            {
                var numberOfPayments = (((DateTime.Now.Year - StartDate.Year) * 12) + DateTime.Now.Month - StartDate.Month);
                toBepaid = AmountForPerson * numberOfPayments;
            }
            else
            {
                toBepaid = AmountForPerson;
            }
            return toBepaid;
        }

        internal override string FormatedGenericValue(string valueToFormat, GenericDataFieldAttribute genericFieldInfo, IDatabaseProvider db)
        {
            if (genericFieldInfo.PropertyName == "DepositTypeText")
            {
                var jobStatus = valueToFormat.ToEnum<DepositTypeEnum>();
                return jobStatus.GetDescription();
            }
            return base.FormatedGenericValue(valueToFormat, genericFieldInfo, db);
        }

        public Account(IDataReader data)
        {
            Load(data);
        }

        public Account()
        {

        }
    }
}