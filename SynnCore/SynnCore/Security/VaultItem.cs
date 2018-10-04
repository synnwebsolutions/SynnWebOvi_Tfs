using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynnCore.Security
{
    public class VaultItem
    {
        private string sName;
        private string uName;
        private string pass;
        private string extra;
        private bool open;
        public const string passPhrase = "*682";

        public void UNblock(string PassPhrase)
        {
            if (PassPhrase == passPhrase)
                open = true;
        }

        public string SystemName
        {
            get
            {
                return sName;
            }
            set
            {
                sName = value;
            }
        }

        public string UsermName
        {
            get
            {
                if (!open)
                    return uName;
                return XCipher.Decrypt(uName, passPhrase);
            }

            set
            {
                uName = value;
            }
        }

        public string Password
        {
            get
            {
                if (!open)
                    return pass;
                return XCipher.Decrypt(pass, passPhrase);
            }

            set
            {
                pass = value;
            }
        }

        public VaultItem()
        {

        }
        //public VaultItem(SqlDataReader data)
        //{
        //    Load(data);
        //}

        public string ExtraDetail
        {
            get
            {
                if (!open)
                    return extra;
                return XCipher.Decrypt(extra, passPhrase);
            }

            set
            {
                extra = value;
            }
        }

        //public void Load(SqlDataReader reader)
        //{
        //    sName = Biu_Utility.LoadNullable<string>(reader, "SystemName");
        //    uName = Biu_Utility.LoadNullable<string>(reader, "UsermName");
        //    pass = Biu_Utility.LoadNullable<string>(reader, "Password");
        //    extra = Biu_Utility.LoadNullable<string>(reader, "ExtraDetail");
        //}

        internal void Close()
        {
            open = false;
        }

        internal void Init(string systemName, string username, string password, string iextra)
        {
            open = false;
            sName = systemName;
            uName = XCipher.Encrypt(username, passPhrase);
            pass = XCipher.Encrypt(password, passPhrase);
            extra = XCipher.Encrypt(iextra, passPhrase);
        }
    }


}
