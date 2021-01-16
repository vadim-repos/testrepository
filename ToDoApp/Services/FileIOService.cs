using Newtonsoft.Json;
using System.ComponentModel;
using System.IO;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    class FileIOService
    {
        private readonly string PATH;

        public FileIOService(string path)
        {
            PATH = path;
        }

        public BindingList<ToDoModel> LoadData()
        {
            var fileExists = File.Exists(PATH);

            if(!fileExists)
            {
                File.CreateText(PATH).Dispose();
                return new BindingList<ToDoModel>();
            }
            using (var reader = File.OpenText(PATH))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<ToDoModel>>(fileText);
            }
        }

        public void SaveData(object list)
        {
            using (StreamWriter streamWriter = File.CreateText(PATH))
            {
                string output = JsonConvert.SerializeObject(list);
                streamWriter.Write(output);
            }
        }
    }
}
