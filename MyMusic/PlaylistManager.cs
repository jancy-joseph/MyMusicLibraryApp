using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic
{
    class PlaylistManager
    {
        public static Dictionary<string, UserPlaylist> PlaylistCollectionDictSongs = new Dictionary<string, UserPlaylist>();
        public static ObservableCollection<MusicFile> GetAllPlaylistSongs(ObservableCollection<MusicFile> PlaylistCollection, UserPlaylist Playlistobj)
        {
            PlaylistCollection.Clear();
            List<MusicFile> musiccollection = Playlistobj.MusicFLists;
            //List<MusicFile> musiccollection = await LoadMyMusicCollection();
            PlaylistCollection.Clear();
            //Checking for condition when playlist is first created
           foreach (MusicFile mf in musiccollection)
           {
                    PlaylistCollection.Add(mf);
           }
           
            return PlaylistCollection;
        }
        /*
        public static List<MusicFile> LoadMyPlaylist(UserPlaylist Playlistobj)
        {
            foreach (MusicFile song in Playlistobj.MusicFLists)
            {
               
                this.PlaylistView.Items.Add(myFilename);

            }
        }*/
      }  
}
