using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace DllReplacementUtility
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string wsPath = string.Empty; 
            StartEndProcess(false);
            if (args.Length > 0)
            {
                args[0] = args[0].Replace('?', ' ');
                wsPath = args[1].Replace('?', ' ');
                ReplaceVersion(args[0]);
            } 
            StartEndProcess(true, wsPath);
            System.Threading.Thread.Sleep(3000);
            Environment.Exit(0);
        }
        private static void StartEndProcess(bool IsStart, string workSpacePath = "")
        {
            if (IsStart)
            {
                Console.WriteLine("Starting Tosca with path: " + workSpacePath);
                Process.Start(workSpacePath);
                Console.WriteLine("Commander Started!");
            }
            else
            {
                Process[] procList = Process.GetProcesses();
                
                foreach (Process proc in procList)
                {
                    if (proc.ProcessName.Contains("Tosca"))
                    {
                        try
                        {
                            proc.Kill();
                        }
                        catch (Exception ex)
                        {
                        }
                        Console.WriteLine("Commander Closed!");
                    }
                    
                }
            }
        }
        private static void ReplaceVersion(string filePath)
        {
            TRY_AGAIN:
            try
            {
                string fileName = filePath.Substring(filePath.LastIndexOf("\\") + 1);
                string home = Environment.GetEnvironmentVariable("Tricentis_Home");
                string fileToDelete = home + "\\" + fileName;
                string fileExtension = fileName.Substring(fileName.LastIndexOf('.') + 1).ToLower();
                File.Copy(filePath, fileToDelete, true);
                Console.WriteLine("Selected Version Placed in Tosca Home Directory!");
            }
            catch (Exception ex)
            {
                System.Threading.Thread.Sleep(3000);
                goto TRY_AGAIN;            
            }            
        }
    }
}
