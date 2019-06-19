using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicHelper
{
    public class IRow : DataGridViewRow
    {

        public IRow()
        {
         
        }
        public override string ToString()
        {
            return "base.ToString()";
        }
    }
}
