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
            FileInfo f = new FileInfo(@"C:\Users\d3n1s\Desktop\Output.txt");
            List<String> ls = GetFiles(GetDrive());

            /*if (!f.Exists)
            {
                var fs = f.Create();
                fs.Close();
            }
            else
            {
                using (StreamWriter sw = f.AppendText())
                {
                    sw.WriteLine(GetFiles(GetDrive()));
                }
            }*/
            foreach (string s in ls)
                Console.WriteLine(s.ToString());

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
            return @"C:\temp";
        }
        public static List<String> GetFiles(string path, int levelCount = 1)
        {
            
            List<String> ls = new List<String>();
            DirectoryInfo di = new DirectoryInfo(path);
            string tabs = "";
            try
            {
                DirectoryInfo[] dirList = di.GetDirectories();
                foreach (DirectoryInfo dir in dirList)
                {
                    for (int i = 0; i < levelCount; i++)
                        tabs += "\t";
                    ls.Add($"{tabs}{dir.Name}");
                    
                    
                    ls.AddRange(GetFiles(dir.FullName));
                    


                }

                FileInfo[] filesList = di.GetFiles();
                for (int i = 0; i < levelCount--; i++)
                    tabs += "\t";
                
                foreach (FileInfo file in filesList)
                {
                    
                    ls.Add($"{tabs}{file.Name}");
                    levelCount++;
                    
                    
                }


            }
            catch(Exception E)
            {
                
            }
            return ls;
        }
    }
}
