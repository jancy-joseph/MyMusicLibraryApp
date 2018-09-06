﻿using System;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MyMusic
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyMusicCollection : Page
    {
        MediaPlayer MyPlayer;
        StorageFile file;

        public MyMusicCollection()
        {
            this.InitializeComponent();
            MyPlayer = new MediaPlayer();
            LoadMyFullCollection();


        }
        public async void LoadMyFullCollection()
        {
            var MusicFileList = new List<MusicFile>();
            //var jObj = new JsonObject();
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.MusicLibrary;

            picker.FileTypeFilter.Add(".mp3");
            picker.FileTypeFilter.Add(".wav");
            picker.FileTypeFilter.Add(".wma");
            picker.FileTypeFilter.Add(".ogg");

            IReadOnlyList<StorageFile> FilesDiscovered = await picker.PickMultipleFilesAsync();
            
            if (FilesDiscovered.Count > 0)
            {
                //StringBuilder output = new StringBuilder("Picked files:\n");

                // Application now has read/write access to the picked file(s)
                foreach (Windows.Storage.StorageFile fileToAdd in FilesDiscovered)
                {
                    MusicProperties musicProperties = await fileToAdd.Properties.GetMusicPropertiesAsync();
                    //MyPlayer.AutoPlay = false;
                    //MyPlayer.Source = MediaSource.CreateFromStorageFile(fileToAdd);

                    var mymusic = new MusicFile()
                    {
                        MFileName = fileToAdd.Name,
                        MFile = fileToAdd,
                        MAlbum = musicProperties.Album,
                        MArtist = musicProperties.Artist,
                        MTitle= musicProperties.Title

                    };
                    MusicFileList.Add(mymusic);
                   //this.ChoosePlaylist1.Items.Add(fileToAdd.Path);
                    // output.Append(file.Name + "\n");
                }
                //  this.textBlock.Text = output.ToString();
            }.
            else
            {
                //this.ChoosePlaylist1.Items.Add("Operation cancelled.");
            }

        }
        public static void AddMusicToFullCollectiontocheckbox()
        {
        }
        public static void RemoveMusicFromColelction()
        {

        }
        public static void playMusic()
        {

        }


    }
}
