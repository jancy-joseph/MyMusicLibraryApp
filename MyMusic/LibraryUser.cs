using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic
{
    /// <summary>
    /// This is the blueprint for a user of the Music Library
    /// </summary>
    class LibraryUser
    {
        /// <summary>
        /// User ID of the Music Library
        /// </summary>
        public string UserName { get; set; }
        public string USerPassword { get; set; }
        /// <summary>
        /// Hold the List of Music playlists of the Playlists class
        /// </summary>
     
       // public MusicPlaylist[] UserPlaylist { get; set; }
       
        public static  LibraryUser GetLibraryUser()
        {
            var libuser = new LibraryUser()
            {
                UserName = "Jancy",
                USerPassword = "Jan!23"
            };
            return libuser;
        }
         public static void WriteLibraryUser(LibraryUser myUser)
        {
            var LibUserData = $"{myUser.UserName},{myUser.USerPassword}";
             FileHelper.WriteTextFileAsync("LoginConfig.txt", LibUserData);
        }
        
    }
}
