using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using Windows.Storage.Search;


namespace MyMusic
{
    public class MusicManager
    {
        /// <summary>
        /// Creating a Dictionary of all Music Files discovered so that it be easy to play using Media Player
        /// </summary>
        public static Dictionary<string, StorageFile> MyMusicDictList = new Dictionary<string, StorageFile>();

      //  public static ObservableCollection<MusicFile> musicFileList = new ObservableCollection<MusicFile>();
      

        // public static ObservableCollection<MusicFile> MusicFileList { get => musicFileList; set => musicFileList = value; }

       public static async Task<List<MusicFile>> GetAllMusic(ObservableCollection<MusicFile> MusicCollection)
        {
            MyMusicDictList.Clear();
            List<MusicFile> musiccollection = await PickUsersMusicCollection();
            //List<MusicFile> musiccollection = await LoadMyMusicCollection();
            MusicCollection.Clear();
            foreach(MusicFile mf in musiccollection)
            {
                MusicCollection.Add(mf);
            }
            return musiccollection;
       }
        public static async Task<List<MusicFile>> GetMusicfromDictionary(ObservableCollection<MusicFile> MusicCollection)
        {
            MyMusicDictList.Clear();
            List<MusicFile> musiccollection = await PickUsersMusicCollection();
            //List<MusicFile> musiccollection = await LoadMyMusicCollection();
            MusicCollection.Clear();
            foreach (MusicFile mf in musiccollection)
            {
                MusicCollection.Add(mf);
            }
            return musiccollection;
        }


        // <summary>
        /// Function to Discover all Music Files from
        /// </summary>
        /// <returns>Collection of Songs Discovered from Known Folders</returns>
        public static async Task<List<MusicFile>> LoadMyMusicCollection()
        {

           var MusicList = new List<MusicFile>();
           FolderPicker picker = new FolderPicker() { SuggestedStartLocation = PickerLocationId.MusicLibrary };

           picker.FileTypeFilter.Add(".mp3");
           StorageFolder folder = await picker.PickSingleFolderAsync();
           if (folder != null)
           {
               string[] files = { };
              var filelist = Directory.GetFiles(folder.Path, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".mp3") || s.EndsWith(".wav"));
                foreach (var x in filelist)
                {
                    StorageFile file = await StorageFile.GetFileFromPathAsync(x);

                    if (MusicManager.MyMusicDictList.ContainsKey(file.Name))
                        continue;
                    MusicProperties musicProperties = await file.Properties.GetMusicPropertiesAsync();

                    MusicFile mymusic = new MusicFile(file.Name, musicProperties.Title, musicProperties.Album, musicProperties.Artist);
                    MusicList.Add(mymusic);
                    MyMusicDictList.Add(mymusic.MFileName, file);
                    // musicFileList.Add(mymusic);
                    //Trying to Bind an object to XAML didnot work so keeping for next time
                    // dataList.Add(mymusic.MFileName);    

                    //MyViewlist.ItemsSource = dataList
                }
                   foreach (KeyValuePair<string, StorageFile> Music in MusicManager.MyMusicDictList)
                   {
                    Debug.WriteLine("Music List");
                    Debug.WriteLine("Key = {0}, Value = {1}", Music.Key, Music.Value.Path);
                    }
            }
          
            return MusicList;
            
        }
            

            public static async Task<List<MusicFile>> PickUsersMusicCollection()
            {

            // ObservableCollection<MusicFile> MusicFileList = new ObservableCollection<MusicFile>();
            var MusicList = new List<MusicFile>();
            //ObservableCollection<MusicFile> MusicFileList = new ObservableCollection<MusicFile>();
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
                // Looping through all files picked by user to create our Music Metafile
                foreach (Windows.Storage.StorageFile fileToAdd in files)
                {
                    //If already found in the discovered dictionary there is no need for a metafile
                    if (MusicManager.MyMusicDictList.ContainsKey(fileToAdd.Name))
                        continue;
                    MusicProperties musicProperties = await fileToAdd.Properties.GetMusicPropertiesAsync();
                    /*
                    var mymusic = new MusicFile()
                    {
                        MFileName = fileToAdd.Name,
                        MAlbum = musicProperties.Album,
                        MArtist = musicProperties.Artist,
                        MTitle = musicProperties.Title
                    };*/
                    MusicFile mymusic = new MusicFile(fileToAdd.Name, musicProperties.Title, musicProperties.Album, musicProperties.Artist);
                    MyMusicDictList.Add(mymusic.MFileName, fileToAdd);
                    // dataList.Add(mymusic.MFileName);
                    MusicList.Add(mymusic);
                }
                foreach (KeyValuePair<string, StorageFile> Music in MusicManager.MyMusicDictList)
                {
                    Debug.WriteLine("Music List");
                    Debug.WriteLine("Key = {0}, Value = {1}", Music.Key, Music.Value.Path);
                }

            }
            else
            {
                // TxtUSER.Text = "Operation cancelled.";
                Debug.WriteLine("Discovery from Known Folders didnt work .Operation cancelled!!");
            }
            return MusicList;
        }

    }
}

