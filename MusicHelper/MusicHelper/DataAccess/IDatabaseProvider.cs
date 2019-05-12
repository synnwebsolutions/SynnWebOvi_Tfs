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
        List<MusicItem> GetMusicItems(MusicSearchParameters musicSearchParameters);
        void ClearData(MusicSearchParameters musicSearchParameters);
        void Update(MusicItem mclickedMusicItem);
        void AddToUsbList(MusicItem mclickedMusicItem);
        void AddToPlayList(MusicItem mclickedMusicItem);
        void ClearUsbList();
        bool ValidateUser(ref LoggedUser u);
    }

    public class MusicSearchParameters
    {
        public bool? InPlayList { get; internal set; }
        public bool? InUsbList { get; internal set; }
        public string SearchText { get; set; }
    }
}
