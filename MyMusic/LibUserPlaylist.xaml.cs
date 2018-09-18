using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MyMusic
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LibUserPlaylist : Page
    {
        MediaPlayer player;
        UserPlaylist playlistObj;
        private ObservableCollection<MusicFile> PlaylistCollection;
        public LibUserPlaylist()
        {
            this.InitializeComponent();
            PlaylistCollection = new ObservableCollection<MusicFile>();
            player = new MediaPlayer();
        }

        protected  override void OnNavigatedTo(NavigationEventArgs e)
        {

            base.OnNavigatedTo(e);
            playlistObj = (UserPlaylist)e.Parameter;
            PlaylistManager.GetAllPlaylistSongs(PlaylistCollection, playlistObj);
            txtPlaylistName.Text = playlistObj.playlistName;

            // parameters.Name
            // parameters.Text
            // ...
            //LoadLibUserPlaylist(Userobject.UserName);
            // var MusicFileListOnload = MusicFile.LoadMyMusicCollection();
            // DataContext = MusicFile.LoadMyMusicCollection();
            //MusicList = await MusicManager.LoadMyMusicCollection();

            // MusicListToDisplay = await MusicManager.PickUsersMusicCollection();
            // MusicListToDisplay = await MusicManager.PickUsersMusicCollection();
            // DataContext= await MusicManager.PickUsersMusicCollection();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            var song = (MusicFile)PlaylistView.SelectedItem;
            var songName = song.MFileName;
            player.AutoPlay = false;
            foreach (KeyValuePair<string, StorageFile> Music in MusicManager.MyMusicDictList)
            {
                if (Music.Key == songName)
                {
                    player.Source = MediaSource.CreateFromStorageFile(Music.Value);
                    player.Play();

                }


            }

        }
        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            player.Pause();
        }
    }
}
