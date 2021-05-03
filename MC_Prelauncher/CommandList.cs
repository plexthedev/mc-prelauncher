using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MC_Prelauncher
{
    class CommandList
    {
        public void setModFolder(string modFolderName)
        {
            string ignoreListPath = $"C:\\Users\\{Environment.UserName}\\AppData\\Roaming\\.minecraft\\Prelauncher\\settings\\ignore.dat";
            string unusedStoragePath = $"C:\\Users\\{Environment.UserName}\\AppData\\Roaming\\.minecraft\\Prelauncher\\mods";
            string configPath = $@"C:\Users\{Environment.UserName}\AppData\Roaming\.minecraft\Prelauncher\settings\folderconfigs";
            string modsPath = $"C:\\Users\\{Environment.UserName}\\AppData\\Roaming\\.minecraft\\mods";

            try
            {
                foreach (string file in Directory.GetFiles(configPath))
                {
                    if (file.Split('\\').Last() == modFolderName)
                    {
                        string[] allmods = File.ReadAllText(file).Split(';');

                        foreach (string mod in Directory.GetFiles(modsPath))
                        {
                            string filenameraw = mod.Split('\\').Last();
                            string[] ignorelist = File.ReadAllText(ignoreListPath).Split(';');

                            if (!ignorelist.Contains(filenameraw) && filenameraw != "buffer")
                            {
                                File.Move(mod, unusedStoragePath + "\\" + filenameraw);
                            }
                        }
                        foreach (string mod in allmods)
                        {
                            if (File.Exists(unusedStoragePath + "\\" + mod) && mod != "buffer")
                            {
                                File.Move(unusedStoragePath + "\\" + mod, modsPath + "\\" + mod);
                            }
                            else
                            {
                                if (mod != "buffer")
                                {
                                    Console.Write("{0} not found, skipping (Enter to continue)", mod);
                                    Console.ReadLine();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }

        public void help(string[] args)
        {
            if (args.Length <= 1)
            {
                Console.WriteLine(
                    "setmodfolder/smf - sets the modfolder\n" +
                    "editmode - enters editmode for a modfolder\n" +
                    "listmods - lists mods in current modfolder\n" +
                    "modfolders - lists all modfolders\n" +
                    "help [command] - gives more info about a command\n" +
                    "launch - starts Minecraft Launcher\n" +
                    "exit - closes the prelauncher\n"
                    );
            }
            else
            {
                switch (args[1])
                {
                    case "setmodfolder":
                        Console.WriteLine("Usage - setmodfolder (folder) - Sets your modfolder (to create a new modfolder, use the editmode command with an unused modfolder name)\nAliases: smf\n");
                        break;

                    case "smf":
                        Console.WriteLine("Usage - setmodfolder (folder) - Sets your modfolder (to create a new modfolder, use the editmode command with an unused modfolder name)\nAliases: smf\n");
                        break;

                    case "editmode":
                        Console.WriteLine("Usage - editmode (folder) - Enters editmode for the specified modfolder (and creates a new one if it doesnt exist)\nNOTE: Instructions for editmode are in the README.md file that came with this program\n");
                        break;

                    case "listmods":
                        Console.WriteLine("Usage - listmods (folder) - Lists all the mods in a specified modfolder");
                        break;

                    case "modfolders":
                        Console.WriteLine("Usage - modfolders - Lists all existing modfolders");
                        break;

                    case "help":
                        Console.WriteLine("you're not funny\n");
                        break;

                    case "launch":
                        Console.WriteLine("Usage - launch - starts the regular Minecraft Launcher");
                        break;

                    case "exit":
                        Console.WriteLine("Usage - exit - closes the prelauncher, does not start the regular launcher");
                        break;
                }
            }
        }
    }
}
