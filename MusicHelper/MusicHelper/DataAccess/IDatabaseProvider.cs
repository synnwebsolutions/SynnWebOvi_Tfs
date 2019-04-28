using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHelper
{
    public interface IDatabaseProvider
    {
        bool Match(MusicItem ti);
        void AddMusicItem(MusicItem ti);
    }
}
