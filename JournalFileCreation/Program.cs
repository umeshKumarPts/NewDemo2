using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalFileCreation
{
    class Program
    {
        static void Main(string[] args)
        {

            String value = Environment.GetEnvironmentVariable("3DIQBatchMode");
           // value = value.Remove(0, 1);
          //  value = value.Remove(value.Length-1, 1);
            if (value != null)
            {


                List<String> list = new List<string>();
                using (StreamReader reader = new StreamReader(value))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Do something with the line.
                        list.Add(line);
                        
                    }
                }
                 if (list.Count < 7)
                    return;

                 String path = list.ElementAt(0);
                 String addInPath = list.ElementAt(1);
                 String outPath = list.ElementAt(2);
                 String revitExePath = list.ElementAt(3);
                 String idealPatPath = list.ElementAt(4);
                 String toTestPatPath = list.ElementAt(5);
                 String roundOffSettings = list.ElementAt(6);
                 String PateComparisonExe = list.ElementAt(7);
                 String resultFileName = list.ElementAt(8);
                 //Example.txt";
                 
                 String timeStamp = "Revit"+string.Format("{0:yyyy_MM_dd_hh_mm_ss_tt}", DateTime.Now);
                 String timeStampoutPath = outPath + timeStamp+@"\";
                 DirectoryInfo info = Directory.CreateDirectory(timeStampoutPath);
                 using (var writer = new StreamWriter(value))
                 {
                     writer.WriteLine(path);
                     writer.WriteLine(addInPath);
                     writer.WriteLine(outPath);
                     writer.WriteLine(revitExePath);
                     writer.WriteLine(idealPatPath);
                     writer.WriteLine(toTestPatPath);
                     writer.WriteLine(roundOffSettings );
                     writer.WriteLine(PateComparisonExe);
                     writer.WriteLine(resultFileName);
                     writer.WriteLine(timeStampoutPath);
                 }

                 
                 string[] filesList = Directory.GetFiles(path);
                 JournalCommand jnl = new JournalCommand(filesList, timeStampoutPath);
                 List<String> genralFile  = jnl.GetCommand();
                 using (var writer = new StreamWriter(addInPath+"Example.txt"))
                 {
                   foreach (String item in genralFile)
                   {
                     writer.WriteLine(item);
                   }
                 }
                 Process process = Process.Start(revitExePath, "  \"" + addInPath + "Example.txt"+ "\"");
                 process.WaitForExit();

                 //string command1 //= @"D:\BatchModPatFileComp\PatFileComparison\PatFileComparison\bin\Debug\PatFileComparison.exe";
                 var process2 = Process.Start(PateComparisonExe, idealPatPath + " " + toTestPatPath + " " + roundOffSettings);
                 process2.WaitForExit();

                 Process process1 = Process.Start(revitExePath, "  \"" + addInPath + "Reflect.txt" + "\"");
                 process1.WaitForExit();
            }
        }

   
    }
}
