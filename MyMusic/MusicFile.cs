
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
    public class MusicFile
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
        //ObservableCollection<MusicFile> MusicFilestoDisplay = new ObservableCollection<MusicFile>();
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

        public MusicFile(string FileName, string Title, string Album, string Artist)
        {
            if (!string.IsNullOrEmpty(FileName))
                MFileName = FileName;
            else
                MFileName = "Unknown Title";
            if (!string.IsNullOrEmpty(MTitle))
                MTitle = Title;
            else
                MTitle = "Unknown Title";
            if(!string.IsNullOrEmpty(Album))
                MAlbum = Album;
            else
                MTitle = "Unknown Title";
            if (!string.IsNullOrEmpty(Artist))
                MArtist = Artist;
            else
                MTitle = "Unknown Artist";
            McoverImage = String.Format("Assets/cd-654603_1280.png");
        }



       



    }
}




