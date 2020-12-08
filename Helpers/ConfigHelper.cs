using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDataManagement.Helpers
{
    public class ConfigHelper
    {
        public static string ProjectFolder = Directory.GetCurrentDirectory();
        public static string SettingFolder = "Setting";
        public static string FileHistory = "file_history.json";
        public static string FileData = "file_data.json";

        public static List<string> GetFileHistory()
        {
            var list = new List<string>();

            try
            {
                // Read from file
                list = FileHelper.ReadJsonFromFile<List<string>>($"{ProjectFolder}\\{SettingFolder}\\{FileHistory}");
            }
            catch
            {
                // Do nothing
            }

            return list;
        }

        public static void AddNewFileHistory(string path)
        {
            var list = GetFileHistory();
            if (list.Any(x => x.ToLower().Equals(path.ToLower()))) return;
            list.Add(path);
            if (!Directory.Exists($"{ProjectFolder}\\{SettingFolder}"))
            {
                Directory.CreateDirectory($"{ProjectFolder}\\{SettingFolder}");
            }                
            FileHelper.SaveJson($"{ProjectFolder}\\{SettingFolder}\\{FileHistory}", list);
        }

        public static void RemoveFileHistory(string path)
        {
            var list = GetFileHistory();
            var elemet = list.Where(x => x.ToLower().Equals(path.ToLower())).FirstOrDefault();
            if (elemet is null) return;
            list.Remove(elemet);
            FileHelper.SaveJson($"{ProjectFolder}\\{SettingFolder}\\{FileHistory}", list);
        }
    }
}
