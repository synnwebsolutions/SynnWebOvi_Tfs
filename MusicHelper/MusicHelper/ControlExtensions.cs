using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicHelper
{
    public static class ControlExtensions
    {
        public static bool IsEmpty(this TextBox tx)
        {
            return string.IsNullOrEmpty(tx.Text);
        }

        public static bool NotEmpty(this TextBox tx)
        {
            return !tx.IsEmpty();
        }
    }
}
