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

        public static TimeSpan? GetTimeSpan(this TextBox tx)
        {
            TimeSpan? sp = null;
            if (tx.NotEmpty())
            {
                var ttlSeconds = Convert.ToInt32(tx.Text);
                var minutes = ttlSeconds / 60;
                var seconds = ttlSeconds % 60;
                sp = new TimeSpan(0,minutes,seconds);
            }
            return sp;
        }

        public static bool NotEmpty(this TextBox tx)
        {
            return !tx.IsEmpty();
        }
    }
}
