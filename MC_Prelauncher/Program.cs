using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
namespace MC_Prelauncher
{
    class Program
    {
        static void Main()
        {
            ModFolderDetect GetModFolder = new ModFolderDetect();
            string currentModFolder = GetModFolder.CompareCurrent();
            if (!Directory.Exists("C:\\Users\\Admin\\AppData\\Roaming\\.minecraft\\Prelauncher"))
            {
                Directory.CreateDirectory("C:\\Users\\Admin\\AppData\\Roaming\\.minecraft\\Prelauncher");
            }
            if (!Directory.Exists("C:\\Users\\Admin\\AppData\\Roaming\\.minecraft\\Prelauncher\\mods"))
            {
                Directory.CreateDirectory("C:\\Users\\Admin\\AppData\\Roaming\\.minecraft\\Prelauncher\\mods");
            }

            // code above is bad, but works lol

            string pathToLauncher = "C:\\Program Files (x86)\\Minecraft Launcher\\MinecraftLauncher.exe";
            Console.WriteLine("Minecraft Pre-launcher by Trollsta_");
            Console.WriteLine("Current mod folder: {0}", currentModFolder);

            while (true)
            {
                Console.Write("$ "); string x = Console.ReadLine();

                string[] y = x.Split(' ');

                CommandList Commands = new CommandList();
                switch (y[0])
                {
                    // mod folder set
                    case "smf":
                        Commands.setModFolder(y[1]);
                        Console.Clear();
                        currentModFolder = GetModFolder.CompareCurrent();
                        Console.WriteLine("Minecraft Pre-launcher by Trollsta_");
                        Console.WriteLine("Current mod folder: {0}", currentModFolder); 
                        break;

                    case "setmodfolder":
                        Commands.setModFolder(y[1]);
                        Console.Clear();
                        currentModFolder = GetModFolder.CompareCurrent();
                        Console.WriteLine("Minecraft Pre-launcher by Trollsta_");
                        Console.WriteLine("Current mod folder: {0}", currentModFolder);
                        break;

                    // other

                    case "listmods":
                        if (y.Length > 1)
                        {
                            if (File.Exists($@"C:\Users\Admin\AppData\Roaming\.minecraft\Prelauncher\settings\folderconfigs\{y[1]}"))
                            {
                                Console.WriteLine("Mods present in {0}:", y[1]);
                                foreach (string modname in File.ReadAllText($@"C:\Users\Admin\AppData\Roaming\.minecraft\Prelauncher\settings\folderconfigs\{y[1]}").Split(';'))
                                {
                                    Console.WriteLine(modname);
                                }
                                Console.WriteLine();
                            }
                            else Console.WriteLine("No such mod folder exists");
                        }
                        else
                        {
                            Console.WriteLine("Mods present in {0}:", currentModFolder);
                            foreach (string modname in File.ReadAllText($@"C:\Users\Admin\AppData\Roaming\.minecraft\Prelauncher\settings\folderconfigs\{currentModFolder}").Split(';'))
                            {
                                Console.WriteLine(modname);
                            }
                            Console.WriteLine();
                        }
                        break;

                    case "modfolders":
                        Console.Write("All present mod folders: ");
                        foreach (string file in Directory.GetFiles(@"C:\Users\Admin\AppData\Roaming\.minecraft\Prelauncher\settings\folderconfigs"))
                        {
                            Console.Write(file.Split('\\').Last());
                        }
                        Console.WriteLine();
                        break;

                    case "help":
                        Commands.help(y[1]);
                        break;

                    case "exit":
                        Environment.Exit(0);
                        break;

                    case "launch": // ignore any other parameters since they dont interfere with launch
                        Process.Start(pathToLauncher);
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
