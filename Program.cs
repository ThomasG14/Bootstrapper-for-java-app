using System.Diagnostics;
using System.Formats.Tar;
using System.Net;

namespace Bootstrapper2
{
    internal class Program
    {
        static string app = "app.jar";
        static string[] files = new string[] {"http://localhost/" + app};
        static string gameDir = "/.Test";
        static string gamePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + gameDir;
        static string javaExecutable = gamePath + app;
        static void Main(string[] args)
        {
            if (!Directory.Exists(gamePath))
            {
                Directory.CreateDirectory(gamePath);
                {
                }
            }
            Console.WriteLine("Checking...");
            foreach (Process process in Process.GetProcessesByName("launcherDl"))
            {
                process.Kill();
            }

            Console.WriteLine("Downloading update...");
            foreach (string file in files)
            {
                string name = file.Substring(file.LastIndexOf("/") + 1);
                try
                {
                    new WebClient().DownloadFile(new Uri(file), gamePath + name);

                    Process process = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                            
                        FileName = javaExecutable,
                        Arguments = $"java -jar {name}",
                        RedirectStandardOutput = false,
                        UseShellExecute = true,
                        CreateNoWindow = true
                    };
                    process.StartInfo = startInfo;
                    Console.WriteLine("Starting Launcher...");
                    process.Start();
                }
                catch (IOException ex)
                {

                }
            }
            Environment.Exit(0);
            
        }
    }
}
