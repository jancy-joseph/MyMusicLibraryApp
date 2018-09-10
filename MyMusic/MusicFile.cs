
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Search;

namespace MyMusic
{
    class MusicFile
    {
        //These are properties that can be got from a music file

        /*
        public string Album { get; set; }
        public string AlbumArtist { get; set; }
        public string Artist { get; set; }
        public uint Bitrate { get; }
        public IList<string> Composers { get; }
        public IList<string> Conductors { get; }
        public TimeSpan Duration { get; }
        public IList<string> Genre { get; }
        public IList<string> Producers { get; }
        public string Publisher { get; set; }
        public uint Rating { get; set; }
        public string Subtitle { get; set; }
        public string Title { get; set; }
        public uint TrackNumber { get; set; }
        public IList<string> Writers { get; }
        public uint Year { get; set; }

    */
        ObservableCollection<MusicFile> MusicFilestoDisplay = new ObservableCollection<MusicFile>();
        /// <summary>
        /// Name of the Music File
        /// </summary>
        public string MFileName { get; set; }
        /// <summary>
        /// Adding a cover Image property as I didnt find it from system class - MusicProperties
        /// </summary>
        public string McoverImage { get; set; } = "Assets/cd-654603_1280.png";
        /// <summary>
        /// Name of the Music Albumn
        /// </summary>
        public string MAlbum { get; set; }
        /// <summary>
        /// Name of the  Artist
        /// </summary>
        public string MArtist { get; set; }
        /// <summary>
        /// Title of the Song
        /// </summary>
        public string MTitle { get; set; }

        /// <summary>
        /// Creating a Dictionary of all Music Files discovered so that it be easy to play using Media Player
        /// </summary>

        public static Dictionary<string, StorageFile> MyMusicDictList = new Dictionary<string, StorageFile>();

        // <summary>
        /// Function to Discover all Music Files from
        /// </summary>
        /// <returns>Collection of Songs Discovered from Known Folders</returns>
        public static async Task<ICollection<MusicFile>> LoadMyMusicCollection()
        {
            ObservableCollection<MusicFile> MusicFileList = new ObservableCollection<MusicFile>();
            QueryOptions queryOption = new QueryOptions
                              (CommonFileQuery.OrderByTitle, new string[] { ".mp3", ".mp4", ".wma", ".ogg" });

            queryOption.FolderDepth = FolderDepth.Deep;

            Queue<IStorageFolder> folders = new Queue<IStorageFolder>();

            var files = await KnownFolders.MusicLibrary.CreateFileQueryWithOptions
                       (queryOption).GetFilesAsync();
            if (files.Count > 0)
            {
                foreach (var fileToAdd in files)
                {
                    if (MusicFile.MyMusicDictList.ContainsKey(fileToAdd.Name))
                        continue;
                    MusicProperties musicProperties = await fileToAdd.Properties.GetMusicPropertiesAsync();

                    var mymusic = new MusicFile()
                    {
                        MFileName = fileToAdd.Name,
                        MAlbum = musicProperties.Album,
                        MArtist = musicProperties.Artist,
                        MTitle = musicProperties.Title
                    };
                    MusicFile.MyMusicDictList.Add(mymusic.MFileName, fileToAdd);
                    MusicFileList.Add(mymusic);
                      //Trying to Bind an object to XAML didnot work so keeping for next time
                    // dataList.Add(mymusic.MFileName);    

                }
                //MyViewlist.ItemsSource = dataList;
                foreach (KeyValuePair<string, StorageFile> Music in MusicFile.MyMusicDictList)
                {
                    Debug.WriteLine("Music List");
                    Debug.WriteLine("Key = {0}, Value = {1}", Music.Key, Music.Value.Path);
                }
            }
            else
            {
                //MyMusicCollection.TxtUSER.txt= "Operation cancelled.";
                Debug.WriteLine("Discovery from Known Folders didnt work .Operation cancelled!!");
            }

            return MusicFileList;

        }

    }

}
