using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    internal class JsonDS
    {
        private static string PathJsonFiles;
        public static void Serialize<T>(string NameJsonFile, T list)
        {
            File.WriteAllText(PathJsonFiles + "\\" + NameJsonFile, JsonConvert.SerializeObject(list));
        }
        public static T? Deserialize<T>(string NameJsonFile)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(PathJsonFiles+"\\"+NameJsonFile));
        }
        public static void SeartchPathJsonFiles()
        { 
            string a = Environment.CurrentDirectory;
            for (int i = a.Length - 1; i != 0; i--)
            {
                if (a[i].ToString() == @"\")
                {
                    foreach (var element in Directory.GetDirectories(a))
                    {
                        if (Path.GetFileName(element) == "JsonFiles")
                        {
                            PathJsonFiles = element;
                            i = 1;
                            break;
                        }
                    }
                }
                else
                {
                    a = a.Remove(i);
                }
            }
        }
    }
}
