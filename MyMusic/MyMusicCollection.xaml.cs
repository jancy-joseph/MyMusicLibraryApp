using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Json;
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
using Windows.Storage.Pickers;
using System.Diagnostics;
using System.Text;
using Windows.Media.Playlists;
using Windows.Storage.FileProperties;
using Windows.Storage.Search;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MyMusic
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyMusicCollection : Page
    {
        
        MediaPlayer player;
        LibraryUser LibUserObject;
        UserPlaylist myplaylist;
        private ObservableCollection<MusicFile> MusicCollection;

        public MyMusicCollection()
        {
            this.InitializeComponent();
            MusicCollection = new ObservableCollection<MusicFile>();
            player = new MediaPlayer();
            
        }


        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
      
            base.OnNavigatedTo(e);
            LibUserObject = (LibraryUser)e.Parameter;
            await MusicManager.GetAllMusic(MusicCollection);
            TxtUSER.Text = LibUserObject.UserName;
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
    
        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            player.Pause();          
        }
        /*
                
        private void CreatePlaylist_Click(object sender, RoutedEventArgs e)
        {
            
            var playlist = new UserPlaylist()
            {
                playlistName = txtBox.Text,
                playlistUserName = LibUserObject.UserName
            };

            LibUserObject.LibUserPlaylist.Add(playlist);
            foreach (MusicFile song in this.MyViewList.SelectedItems)
            {
                playlist.MusicFLists.Add(song);
             //  this.PlaylistView.Items.Add(myFilename);

            }
            
            foreach(MusicFile song in playlist.MusicFLists)
            {
                FindandPlayMusic(song.MFileName);
            }
            
            this.Frame.Navigate(typeof(LibUserPlaylist), playlist);
            
        }
        */
    
        public void FindandPlayMusic(string MusicFilename)
        {
            player.AutoPlay = false;
           // Debug.WriteLine(myFilename);
            foreach (KeyValuePair<string, StorageFile> Music in MusicManager.MyMusicDictList)
            {
                if (Music.Key == MusicFilename)
                {
                   player.Source = MediaSource.CreateFromStorageFile(Music.Value);
                   player.Play();

                }

            }
        }
        /*
        private void MyViewList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            player.AutoPlay = false;
            Debug.WriteLine(MyViewList.SelectedItem);                            

            foreach (KeyValuePair<string, StorageFile> Music in MusicManager.MyMusicDictList)
            {
                if (Music.Key == MyViewList.SelectedItem.s)
                {
                    player.Source = MediaSource.CreateFromStorageFile(Music.Value);
                    player.Play();

                }


            }
            
        }
        */
        private void MenuButton2_Click(object sender, RoutedEventArgs e)
        {
            // Frame.Navigate(typeof(MyMusicCollection));
        }
        private void MenuButton1_Click(object sender, RoutedEventArgs e)
        {
            //Frame.Navigate(typeof());
        }
        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            //MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
        /*
        private void MyViewList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var song = (MusicFile)e.ClickedItem;
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
        */
        private void MyViewList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private async void CreatePlaylist_Click_1(object sender, RoutedEventArgs e)
        {
            
            var dialog1 = new ContentDialog1();
            var result = await dialog1.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                var text = dialog1.Text;
                Debug.WriteLine(text);
                var playlist = new UserPlaylist()
                {
                    playlistName = text,
                    playlistUserName = LibUserObject.UserName
                };
                LibUserObject.LibUserPlaylist.Add(playlist);
                PlaylistManager.PlaylistCollectionDictSongs.Add(playlist.playlistName,playlist);
                myplaylist = playlist;
                /*
                foreach (MusicFile song in this.MyViewList.SelectedItems)
                {
                    playlist.MusicFLists.Add(song);
                    //  this.PlaylistView.Items.Add(myFilename);

                }
                
                foreach (MusicFile song in playlist.MusicFLists)
                {
                    FindandPlayMusic(song.MFileName);
                }
                */

                
            }
            /*
            else if (result == ContentDialogResult.Secondary)
            {

            }
            */
            

            

            

            
        }

        private void AddtoPlaylist_Click(object sender, RoutedEventArgs e)
        {
            if (myplaylist != null)
            { 

                foreach (MusicFile song in MyViewList.SelectedItems)
                {

                    myplaylist.MusicFLists.Add(song);
                //  this.PlaylistView.Items.Add(myFilename);

                }
                Frame.Navigate(typeof(LibUserPlaylist), myplaylist);
            }
            
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            var song = (MusicFile)MyViewList.SelectedItem;
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

        private void MyViewList_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
   
   

}
