using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic
{
    class UserPlaylist
    {
        public string playlistName { get; set; }
        public string playlistUserName { get; set; }
        /// <summary>
        /// Lists of  Music Files in a given Playlist created by user.
        /// </summary>
        public List<string> MusicFLists = new List<string>();
        

        public void LoadMyMusic()
        {

        }
        public void AddSongToPlaylistTOcheckbox()
        {

        }
        public void DeleteSongsFromPlaylist()
        {

        }
        public void PlayMyMusic()
        {

        }
        public static async Task<UserPlaylist> DeserelizeDataFromJson(string fileName)
        {

            try
            {
                var DeserializedJsonPlayLst = new UserPlaylist();
                var Folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                var file = await Folder.GetFileAsync(fileName + ".json");
                var data = await file.OpenReadAsync();

                using (StreamReader r = new StreamReader(data.AsStream()))
                {
                    string text = r.ReadToEnd();
                    DeserializedJsonPlayLst = JsonConvert.DeserializeObject<UserPlaylist>(text);
                    //foreach (var i in p)
                    //{
                    //    persons.UserPlaylists.Add(i);
                    // }
                }
                return DeserializedJsonPlayLst;
            }
            catch (Exception e)
            {
                throw e;
            }
        }   

        public static async Task<string> SerelizeDataToJson(UserPlaylist MyListPlay, string filename)
        {
            try
            {
                var Folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                var file = await Folder.CreateFileAsync(filename + ".json", Windows.Storage.CreationCollisionOption.ReplaceExisting);
                var data = await file.OpenStreamForWriteAsync();

                using (StreamWriter r = new StreamWriter(data))
                {
                    var serelizedfile = JsonConvert.SerializeObject(MyListPlay);
                    r.Write(serelizedfile);

                }
                return filename;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /*
        public static  Task<ICollection<LibraryUser>> GetPlaylists()
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
    
        public static void WritePlaylistsToFile(LibraryUser myUser)
        {
            
            var LibUserData = $"{myUser.UserName},{myUser.USerPassword}\n";
            FileHelper.WriteTextFileAsync(LOGIN_FILE_NAME, LibUserData);
            
        }

    
        public static Task<bool> FindPlaylistsbyName(LibraryUser myUser)
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
                if (LibUser.Equals(myUser))
                {
                    return true;
                }
            }
            return false;
          
        }
       */

    }
}
