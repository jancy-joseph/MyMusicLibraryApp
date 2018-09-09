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

        public MyMusicCollection()
        {
            this.InitializeComponent();
            player = new MediaPlayer();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            LibUserObject = (LibraryUser)e.Parameter;
            TxtUSER.Text = LibUserObject.UserName;
            // parameters.Name
            // parameters.Text
            // ...
            //LoadLibUserPlaylist(Userobject.UserName);
            DataContext = MusicFile.LoadMyMusicCollection();

        }
        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            player.Pause();
        }

        private async void PickFileToMusicCollection_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<string> dataList = new ObservableCollection<string>();
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.MusicLibrary;

            picker.FileTypeFilter.Add(".mp3");
            picker.FileTypeFilter.Add(".wav");
            picker.FileTypeFilter.Add(".wma");
            picker.FileTypeFilter.Add(".ogg");

            IReadOnlyList<StorageFile> files = await picker.PickMultipleFilesAsync();
            if (files.Count > 0)
            {
                //StringBuilder output = new StringBuilder("Picked files:\n");

                // Application now has read/write access to the picked file(s)
                foreach (Windows.Storage.StorageFile fileToAdd in files)
                {
                    if (MusicFile.MyMusicDictList.ContainsKey(fileToAdd.Name))
                        continue;
                    MusicProperties musicProperties = await fileToAdd.Properties.GetMusicPropertiesAsync();

                    var mymusic = new MusicFile()
                    {
                        MFileName = fileToAdd.Name,
                        MFile = fileToAdd,
                        MAlbum = musicProperties.Album,
                        MArtist = musicProperties.Artist,
                        MTitle = musicProperties.Title
                    };
                     MusicFile.MyMusicDictList.Add(mymusic.MFileName, mymusic);
                    dataList.Add(mymusic.MFileName);

                }
                this.MyViewlist.ItemsSource = dataList;
                foreach (KeyValuePair<string, MusicFile> Music in MusicFile.MyMusicDictList)
                {
                    Debug.WriteLine("Music List");
                    Debug.WriteLine("Key = {0}, Value = {1}", Music.Key, Music.Value);
                }
            }
            else
            {
                TxtUSER.Text = "Operation cancelled.";
            }
        }

        private void MyViewlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            player.AutoPlay = false;
            Debug.WriteLine(MyViewlist.SelectedItem);
            foreach (KeyValuePair<string, MusicFile> Music in MusicFile.MyMusicDictList)
            {
                if (Music.Key == (string)MyViewlist.SelectedItem)
                {
                    player.Source = MediaSource.CreateFromStorageFile(Music.Value.MFile);
                    player.Play();

                }


            }
        }

        private void CreatePlaylist_Click(object sender, RoutedEventArgs e)
        {
            var PLaylist1 = new UserPlaylist()
            {
                playlistName = txtPlaylistName.Text,
                playlistUserName = LibUserObject.UserName
            };
            LibUserObject.LibUserPlaylist.Add(PLaylist1);
            foreach (string Filename in this.MyViewlist.SelectedItems)
            {
                PLaylist1.LibUserPlaylist.Add(Filename);
                this.PlaylistView.Items.Add(Filename);

            }
            foreach (string filetoplay in PLaylist1.LibUserPlaylist)
            {
                FindndPLayMusic(filetoplay);
            }

            this.Frame.Navigate(typeof(LibUserPlaylist), LibUserObject);
        }
    }
}
