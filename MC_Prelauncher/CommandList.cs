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
                                if (mod != "buffer") Console.WriteLine("{0} not found, skipping", mod);
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

        public void help(string arg)
        {
            if (arg == null)
            {
                // do some shit over here
            }
            else
            {
                switch (arg)
                {
                    case "setmodfolder":
                        Console.WriteLine("Usage - setmodfolder (folder) - folder has to be registered in root/settings/folderconfigs\n"); // gay explanation, remove after installer done
                        break;

                    case "help":
                        Console.WriteLine("you're not funny\n");
                        break;

                    case "launch":
                        Console.WriteLine("Usage - launch (args) - launches the launcher, args do nothing rn");
                        break;
                }
            }
        }
    }
}
