using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SynnCore.Generics;
using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebSimplify.Helpers;

namespace WebSimplify.Ws
{
    /// <summary>
    /// Summary description for WebSimplifyExternalServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebSimplifyExternalServices : System.Web.Services.WebService
    {
        private static IDatabaseProvider DBController = SynnDataProvider.DbProvider;

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public ServiceActionResult HandleDeposits(string appToken, string depositsJson = null, string depositsXml = null)
        {
            if (depositsJson != null || depositsXml != null)
            {
                try
                {
                    List<DepositTransaction> depositTransactions = null;
                    if (depositsJson.NotEmpty())
                        depositTransactions = JsonConvert.DeserializeObject<List<DepositTransaction>>(depositsJson);
                    else if (depositsXml.NotEmpty())
                        depositTransactions = XmlHelper.CreateFromXml<List<DepositTransaction>>(depositsXml);

                    var depositList = new List<UserDeposit>();
                    foreach (var exTran in depositTransactions)
                    {
                        depositList.Add(new UserDeposit
                        {
                            IDate = exTran.TranDate,
                            PaymentReferenceNumber = exTran.Reffrerence,
                            Description = exTran.Description,
                            Amount = (int)exTran.Credit,
                            UserId = DetectPaymentUser (exTran)
                        });
                    }

                    foreach (var idp in depositList.Where(x => x.Amount > 0).ToList())
                    {
                        var exsistingI = DBController.DbGenericData.GetGenericData<UserDeposit>(new UserDepositSearchParameters { UserId = idp.UserId });
                        if(!exsistingI.Any(x => x.PaymentReferenceNumber == idp.PaymentReferenceNumber && x.IDate == idp.IDate))
                            DBController.DbGenericData.Add(idp);
                    }
                    return ServiceActionResult.Success;
                }
                catch (Exception ex)
                {
                    Logger.Instance.Error(ex);
                }
            }
            return ServiceActionResult.Error;
        }

        private int DetectPaymentUser(DepositTransaction exTran)
        {
            if (exTran.Description.Contains("טילהון"))
            {
                return 9;
            }
            if (exTran.Description.Contains("אמבט"))
            {
                return 4;
            }
            if (exTran.Description.Contains("פרדה"))
            {
                return 8;
            }
            if (exTran.Description.Contains("יטמניו"))
            {
                return 7;
            }
            if (exTran.Description.Contains("סימצ"))
            {
                return 1;
            }
            if (exTran.Description.Contains("נחום"))
            {
                return 3;
            }
            if (exTran.Description.Contains("אברהם"))
            {
                return 11;
            }
            if (exTran.Description.Contains("מהרט"))
            {
                return 6;
            }
            if (exTran.Description.Contains("אלמו"))
            {
                return 5;
            }
            if (exTran.Description.Contains("רחל"))
            {
                return 10;
            }

            return 0;
        }

    }

    public enum ServiceActionResult
    {
        Success,
        Error
    }

    public class DepositTransaction
    {
        public DateTime TranDate { get; set; }
        public DateTime TranValueDate { get; set; }
        public string Reffrerence { get; set; }
        public string Description { get; set; }
        public double Credit { get; set; }
        public double Debit { get; set; }
        public double Balance { get; set; }
        public int UserId { get; set; }
    }
}
