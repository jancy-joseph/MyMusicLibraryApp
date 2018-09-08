
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
        /*
        public string Album { get; set; }
        public string AlbumArtist { get; set; }
        public string Artist { get; set; }
        public uint Bitrate { get; }
      //  public IList<string> Composers { get; }
      //  public IList<string> Conductors { get; }
        public TimeSpan Duration { get; }
        public IList<string> Genre { get; }
       // public IList<string> Producers { get; }
       // public string Publisher { get; set; }
        //public uint Rating { get; set; }
        public string Subtitle { get; set; }
        public string Title { get; set; }
        public uint TrackNumber { get; set; }
       // public IList<string> Writers { get; }
        public uint Year { get; set; }

    */
    /// <summary>
    /// Source of the file to play using Mediaplayer
    /// </summary>
        public StorageFile MFile { get; set; }
        /// <summary>
        /// Name of the File to be played
        /// </summary>
        public string MFileName { get; set; }
        /// <summary>
        /// Adding a cover Image property
        /// </summary>
        public string McoverImage { get; set; } = "Assets/cd-654603_1280.png";
        /// <summary>
        /// Name of the Albumn
        /// </summary>
        public string MAlbum { get; set; }
        /// <summary>
        /// Name of the  Artist
        /// </summary>
        public string MArtist { get; set; }
        /// <summary>
        /// Title of the SOng
        /// </summary>
        public string MTitle { get; set; }

        public static Dictionary<string, MusicFile> MyMusicDictList = new Dictionary<string, MusicFile>();
        
        public  static async Task<ICollection<MusicFile>> LoadMyMusicCollection()
        { 
           ObservableCollection<string> dataList = new ObservableCollection<string>();
            var MusicFileList = new List<MusicFile>();
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
                    if (MyMusicDictList.ContainsKey(fileToAdd.Name))
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
                    MyMusicDictList.Add(mymusic.MFileName, mymusic);
                    MusicFileList.Add(mymusic);
                    dataList.Add(mymusic.MFileName);                                              
              }
              //this.MyViewlist.ItemsSource = dataList;
              foreach (KeyValuePair<string, MusicFile> Music in MyMusicDictList)
              {
                        Debug.WriteLine("Music List");
                        Debug.WriteLine("Key = {0}, Value = {1}", Music.Key, Music.Value);
              }
           }
           else
           {
                //MyMusicCollection.TxtUSER.txt= "Operation cancelled.";
                Debug.WriteLine("Operation cancelled in Music File");
           }
           return MusicFileList;
        }
       
    }
}
