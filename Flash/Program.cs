using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Flash
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\Public\Documents\Output.txt";
            FileStream fstream = null;
            try
            {
                fstream = new FileStream(path, FileMode.OpenOrCreate);
                using (StreamWriter sw = new StreamWriter(fstream))
                {
                    foreach (string str in GetFiles(GetDrive()))
                    {
                        sw.WriteLine(str);
                    }
                }
                    
            }
            catch(Exception E)
            {
                Console.WriteLine(E.Message);
            }
            finally
            {
                fstream?.Close();
            }
        }

        public static string GetDrive()
        {
            DriveInfo[] allDriveInfo = DriveInfo.GetDrives();
            Console.WriteLine("Все диски:");
            int count = 0;
            foreach ( DriveInfo drive in allDriveInfo)
            {
                Console.WriteLine($"{++count}. {drive}");
            }
            Console.Write("Выберите нужный диск: ");
            bool flag = false;
            int input = 1;
            while (!flag)
            {
                if (!Int32.TryParse(Console.ReadLine(), out input))
                    Console.WriteLine("Введите число!");
                else
                    if (input > count || input < 1)
                    {
                        Console.WriteLine("Выберите существующий диск!");
                    }
                    else flag = true;
            }
            return $@"{allDriveInfo[--input]}";
        }
        public static List<String> GetFiles(string path, string tabs = "")
        {
            List<String> ls = new List<String>();
            DirectoryInfo di = new DirectoryInfo(path);
            try
            {
                DirectoryInfo[] dirList = di.GetDirectories();
                foreach (DirectoryInfo dir in dirList)
                {
                    ls.Add($"{tabs}{dir.Name}");
                    ls.AddRange(GetFiles(dir.FullName, tabs+"\t"));
                }
                
                FileSystemInfo[] filesList = di.GetFileSystemInfos(); ;
                
                foreach (FileSystemInfo file in filesList)
                {
                    ls.Add($"{tabs}{file.ToString()}");
                }
            }
            catch(Exception E)
            {
                Console.WriteLine(E.Message);
            }
            return ls;
        }
    }
}
