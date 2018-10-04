using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SynnCore.Forms;

namespace SynnCore
{
    public interface IGridCustom
    {
        Type GetColumnsInfoType(string controlName);

    }
}
