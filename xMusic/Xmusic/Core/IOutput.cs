using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xmusic
{
    public interface IOutput
    {
        void Write(float[] sampleBuffer, int v);
    }
}
