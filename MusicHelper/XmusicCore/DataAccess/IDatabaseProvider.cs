using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace XmusicCore
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
        UserTheme GetUserTheme();
    }

    public class MusicSearchParameters
    {
        public bool? InPlayList { get;  set; }
        public bool? InUsbList { get;  set; }
        public string SearchText { get; set; }
    }
}
