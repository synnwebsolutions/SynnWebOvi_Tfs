using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSimplify.Data
{
    public interface IMarkAble
    {
        string MarkableDescription { get;  }
        string MarkableName { get;  }
        string MarkableType { get;  }
    }
}
