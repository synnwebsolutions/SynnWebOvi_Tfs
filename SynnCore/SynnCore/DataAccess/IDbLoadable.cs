using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynnCore.DataAccess
{
    public interface IDbLoadable
    {
        void Load(System.Data.IDataReader reader);
    }
}
