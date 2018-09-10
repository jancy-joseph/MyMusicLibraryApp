using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    class LoadMusic
    {
     public ObservableCollection<MusicFile> MusicFiles { get { return this.musicFiles; } }


    public LoadMusic()
    {
            this.musicFiles = MusicFile.LoadMyMusicCollection;
    } 
         
    
    }
}
