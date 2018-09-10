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
        private const string LOGIN_FILE_NAME = "LoginConfig.txt";
        /// <summary>
        /// User ID of the Music Library
        /// </summary>
        public string UserName { get; set; }
        public string USerPassword { get; set; }
        /// <summary>
        /// Lists of Playlists created by user
        /// </summary>
        public List<UserPlaylist> LibUserPlaylist = new List<UserPlaylist>();
        /// <summary>
        /// Hold the List of Playlists and Location of Json File.Not sure if needed??
        /// </summary>
         public static Dictionary<string, string> LibUserPlaylistDictList = new Dictionary<string, string>();


        public static  async Task<ICollection<LibraryUser>> GetLibraryUsers()
        {
            var LibUsersList = new List<LibraryUser>();
            var filecontent = await FileHelper.ReadTextFileAsync(LOGIN_FILE_NAME);
            var lines = filecontent.Split(new char[] { '\r', '\n' });
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                var lineParts = line.Split(',');
                var LibUser = new LibraryUser()
                {
                    UserName = lineParts[0],
                    USerPassword = lineParts[1]
                };
                LibUsersList.Add(LibUser);
            }
            return LibUsersList;
        }
         public  static void WriteLibraryUserToFile(LibraryUser myUser)
        {
            var LibUserData = $"{myUser.UserName},{myUser.USerPassword}\n";
             FileHelper.WriteTextFileAsync(LOGIN_FILE_NAME, LibUserData);
          
        }
        public static  async Task<bool> ValidateLibraryUser(LibraryUser myUser)
        {
            
            var filecontent = await FileHelper.ReadTextFileAsync(LOGIN_FILE_NAME);
            var lines = filecontent.Split(new char[] { '\r', '\n' });
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                var lineParts = line.Split(',');
                var LibUser = new LibraryUser()
                {
                    UserName = lineParts[0],
                    USerPassword = lineParts[1]
                };
                // Checking if the user object is in the Stored Login config file.
                if((LibUser.UserName ==myUser.UserName)&& (LibUser.USerPassword ==myUser.USerPassword))
                {
                    return true;
                }
            }
            return false;

        }
        
    }
}
