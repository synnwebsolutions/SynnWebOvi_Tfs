using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynnCore.Controls
{
    public class ListItem
    {
        public ListItem()
        {

        }

        public override string ToString()
        {
            return Text;
        }
        public string Text { get; set; }
        public string Value { get; set; }
    }
}
