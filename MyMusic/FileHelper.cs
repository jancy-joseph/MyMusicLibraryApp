using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace MyMusic
{
    public static class FileHelper
    {
        public async static void WriteTextFileAsync(string FileName,string content)
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var textFile =  await localFolder.CreateFileAsync(FileName,CreationCollisionOption.OpenIfExists);
            var textStream = await textFile.OpenAsync(FileAccessMode.ReadWrite);
            var textWriter = new DataWriter(textStream);
            textWriter.WriteString(content);
            await textWriter.StoreAsync();
        }
        public static async Task<string> ReadTextFileAsync(string FileName)
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var textFile = await localFolder.GetFileAsync(FileName);
            var textStream = await textFile.OpenReadAsync();
            var textReader = new DataReader(textStream);
            var textLength = textStream.Size;
            await textReader.LoadAsync((uint)textLength);
            return textReader.ReadString((uint)textLength);


        }
    }
}
