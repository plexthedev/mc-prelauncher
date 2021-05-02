using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MC_Prelauncher
{
    class Editmode
    {
        public void editmode(string modfolder)
        {
            Console.Clear();
            Console.Title = $"Minecraft Prelauncher: editmode {modfolder}";

            while (true)
            {
                Console.Write("editmode > "); string x = Console.ReadLine();
                string[] cmdArgs = x.Split(' ');

                switch (cmdArgs[0])
                {
                    case "add": // add mod name to array if it exists in any of the 2 folders (ignore items listed in ignore.dat)
                        break;

                    case "remove": // remove any entry from array that matched cmdArgs[1], unless it is named 'buffer'
                        if (cmdArgs.Length <= 1) break;
                        if (cmdArgs[1].ToLower() == "buffer") { Console.WriteLine("You cannot remove the buffer element from a mod folder!"); break; }
                        string[] arr = File.ReadAllText($@"C:\Users\Admin\AppData\Local\Prelauncher\settings\folderconfigs\{modfolder}").Split(';');
                        var list = new List<string>(arr);
                        
                        if (arr.Contains(cmdArgs[1]))
                        {
                            list.Remove(cmdArgs[1]);
                            string temp = "";
                            foreach (string mod in list)
                            {
                                temp += $";{mod}";
                            }

                            var listchar = new List<char>(temp.ToCharArray());
                            listchar.RemoveAt(0);

                            string final = "";

                            foreach (char c in listchar)
                            {
                                final += c;
                            }

                            File.WriteAllText($@"C:\Users\Admin\AppData\Local\Prelauncher\settings\folderconfigs\{modfolder}", final);
                            
                            Console.WriteLine("Successfully removed {0} from this mod folder\n", cmdArgs[1]);
                        }
                        else
                        {
                            Console.WriteLine("Mod \"{0}\" was not found in this mod folder\n", cmdArgs[1]);
                        }
                        break;

                    case "list": // list all mods in current modfolder (obviously)
                        foreach (string modname in File.ReadAllText($@"C:\Users\Admin\AppData\Roaming\.minecraft\Prelauncher\settings\folderconfigs\{modfolder}").Split(';'))
                        {
                            if (modname.ToLower() != "buffer") Console.WriteLine(modname);
                        }
                        Console.WriteLine();
                        break;

                    case "exit":
                        Console.Clear();
                        ModFolderDetect GetModFolder = new ModFolderDetect();
                        string currentModFolder = GetModFolder.CompareCurrent();
                        Console.WriteLine("Minecraft Pre-launcher by Trollsta_");
                        Console.WriteLine("Current mod folder: {0}", currentModFolder);
                        return;
                }
            }
        }
    }
}
