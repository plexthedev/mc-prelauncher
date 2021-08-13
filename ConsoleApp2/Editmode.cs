using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp2
{
    class Editmode
    {
        Functions Functions = new Functions();

        public void editmodeInit()
        {
            string pathToListing = $@"C:\Users\{Environment.UserName}\AppData\Roaming\.minecraft\Prelauncher\configs";
            string modFolder = $@"C:\Users\{Environment.UserName}\AppData\Roaming\.minecraft\mods";
            string unusedModFolder = $@"C:\Users\{Environment.UserName}\AppData\Roaming\.minecraft\Prelauncher\mods";

            string selected = null;

            Console.Clear();
            Console.Title = "Minecraft Prelauncher - editmode";

            while (true)
            {
                Console.Write("\nEDITMODE > "); string cmd = Console.ReadLine();
                string[] commandArgs = cmd.Split(' ');

                switch (commandArgs[0])
                {
                    case "select":
                        if (commandArgs.Length <= 1) { Functions.ThrowWinErrorMsg("NoModfolderGiven"); break; }
                        if (File.Exists($"{pathToListing}\\{commandArgs[1]}"))
                        {
                            selected = commandArgs[1];
                            Console.WriteLine("Selected {0}", selected);
                        }
                        else
                        {
                            Console.WriteLine("Modfolder {0} not found, use the LIST command to see all available mod folders", commandArgs[1]);
                        }
                        break;

                    case "add":
                        if (commandArgs.Length <= 1) { Functions.ThrowWinErrorMsg("NoModGiven"); break; }
                        if (selected == null) { Functions.ThrowWinErrorMsg("ModfolderNotSelected"); break; }
                        // reminder: use selected
                        string[] mods = File.ReadAllLines($"{pathToListing}\\{selected}");
                        if (!File.Exists($"{modFolder}\\{commandArgs[1]}") && !File.Exists($"{unusedModFolder}\\{commandArgs[1]}"))
                        {
                            Functions.ThrowWinErrorMsg("ModNotFound");
                        }
                        else
                        {
                            if (!mods.Contains(commandArgs[1]))
                            {
                                List<string> modslist = new List<string>(mods);

                                try
                                {
                                    if (commandArgs[1].Split('.').Last() == "jar")
                                    {
                                        modslist.Add(commandArgs[1]);
                                        mods = modslist.ToArray();
                                        File.WriteAllLines($"{pathToListing}\\{selected}", mods);
                                        Console.WriteLine("{0} has been successfully added to {1}", commandArgs[1], selected);
                                    }
                                    else
                                    {
                                        Console.Write("WARNING: This file is not a .jar file, continue? (y/n) > ");
                                        ConsoleKeyInfo ans = Console.ReadKey();
                                        Console.WriteLine();

                                        if (ans.Key == ConsoleKey.Y)
                                        {
                                            modslist.Add(commandArgs[1]);
                                            mods = modslist.ToArray();
                                            File.WriteAllLines($"{pathToListing}\\{selected}", mods);
                                            Console.WriteLine("{0} has been successfully added to {1}", commandArgs[1], selected);
                                        }
                                    }
                                }
                                catch (IndexOutOfRangeException)
                                {
                                    Console.WriteLine("The given file name is invalid");
                                }
                            }
                            else
                            {
                                Functions.ThrowWinErrorMsg("ModAlreadyExists");
                            }
                        }
                        break;

                    case "remove":
                        if (commandArgs.Length <= 1) { Functions.ThrowWinErrorMsg("NoModGiven"); break; }
                        if (selected == null) { Functions.ThrowWinErrorMsg("ModfolderNotSelected"); break; }
                        string[] _mods = File.ReadAllLines($"{pathToListing}\\{selected}");
                        List<string> _temp = new List<string>(_mods);
                        int tempint = -1;

                        if (!_mods.Contains(commandArgs[1]))
                        {
                            Functions.ThrowWinErrorMsg("ModNotFound");
                        }
                        else
                        {
                            for (int i = 0; i < _mods.Length; i++)
                            {
                                if (_mods[i] == commandArgs[1])
                                {
                                    tempint = i;
                                    break;
                                }
                            }

                            _temp.RemoveAt(tempint);
                            _mods = _temp.ToArray();
                            File.WriteAllLines($"{pathToListing}\\{selected}", _mods);
                            Console.WriteLine("{0} has been successfully removed from {1}", commandArgs[1], selected);

                        }
                        break;

                    case "modlist":
                        if (selected == null) { Functions.ThrowWinErrorMsg("ModfolderNotSelected"); break; }
                        string[] modlist = File.ReadAllLines($"{pathToListing}\\{selected}");
                        foreach (string mod in modlist)
                        {
                            Console.WriteLine(mod);
                        }
                        break; 

                    case "list":
                        string[] mdflist = Functions.ModfolderList();
                        foreach (string mdf in mdflist)
                        {
                            if (mdf.Split('.')[1] == "mdf") Console.WriteLine(mdf);
                        }
                        break;
                        
                    case "clean":
                        if (selected == null) { Functions.ThrowWinErrorMsg("ModfolderNotSelected"); break; }
                        if (File.Exists($"{pathToListing}\\{selected}"))
                        {
                            Console.Write("WARNING: this will clear the entire mdf file, continue? (y/n) > ");
                            ConsoleKeyInfo x = Console.ReadKey();

                            if (x.Key == ConsoleKey.Y)
                            {
                                try
                                {
                                    File.Delete($"{pathToListing}\\{selected}");
                                    var temp = File.Create($"{pathToListing}\\{selected}");
                                    temp.Close();
                                    Console.WriteLine("Successfully cleared {0}", selected);
                                }
                                catch (Exception ex)
                                {
                                    Console.Write(ex.Message);
                                }
                            }
                            else break;
                        }
                        break;

                    case "create":
                        if (commandArgs.Length <= 1) { Functions.ThrowWinErrorMsg("NoModfolderGiven"); break; }
                        if (!File.Exists(pathToListing + '\\' + commandArgs[1] + ".mdf"))
                        {
                            try
                            {
                                var temp = File.Create($"{pathToListing}\\{commandArgs[1]}.mdf");
                                temp.Close();
                                Console.WriteLine("Modfolder {0} successfully created", commandArgs[1]);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        else Functions.ThrowWinErrorMsg("ModfolderExists");
                        break;

                    case "exit":
                        return; // yes, this is literally everything you need to do to exit editmode, wow
                        
                }
            }
        }
    }
}
