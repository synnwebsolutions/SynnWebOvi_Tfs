using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHelper
{
    public class SqlDatabaseProvider : SqlDbController, IDatabaseProvider
    {
        public SqlDatabaseProvider(SynnDataProviderBase d) : base(d)
        {
        }
    }
}
