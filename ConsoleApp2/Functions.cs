using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ConsoleApp2
{
    class Functions
    {
        public void StartLauncher()
        {
            string path = $@"C:\Program Files (x86)\Minecraft Launcher\MinecraftLauncher.exe";
            Process.Start(path);
        }

        public void SwitchModfolders(string modfolder)
        {
            string modsPath = $@"C:\Users\{Environment.UserName}\AppData\Roaming\.minecraft\mods";
            string unusedStoragePath = $@"C:\Users\{Environment.UserName}\AppData\Roaming\.minecraft\Prelauncher\mods";
            string modFolderPath = $@"C:\Users\{Environment.UserName}\AppData\Roaming\.minecraft\Prelauncher\configs\";

            foreach (string mod in Directory.GetFiles(modsPath))
            {
                string filenameraw = mod.Split('\\').Last();

                File.Move(mod, unusedStoragePath + "\\" + filenameraw);
            }

            string[] allmods = File.ReadAllLines(modFolderPath+modfolder);
            int s = 0;
            int f = 0;
            foreach (string mod in allmods)
            {
                if (File.Exists(unusedStoragePath + "\\" + mod))
                {
                    s++;
                    File.Move(unusedStoragePath + "\\" + mod, modsPath + "\\" + mod);
                }
                else f++;
            }
            Console.WriteLine($"{s} mods moved, {f} failed.");
            Console.Write("Any key to continue..."); Console.ReadKey(true);
        }
        public void ModfolderChoice(string[] modfolders)
        {
            Console.Clear();
            Console.WriteLine("Available modfolders:");

            foreach (string mod in modfolders)
            {
                Console.WriteLine(mod);
            }

            Console.Write("\nModfolder name: ");
            string modnameGiven = Console.ReadLine();

            if (modfolders.Contains(modnameGiven))
            {
                SwitchModfolders(modnameGiven);
            }
            else
            {
                ThrowWinErrorMsg("ModfolderNotFound");
            }
        }
        public string[] ModfolderList()
        {
            List<string> modfolderlist = new List<string> { };
            string modfolderspath = $@"C:\Users\{Environment.UserName}\AppData\Roaming\.minecraft\Prelauncher\configs";
            foreach (var file in Directory.GetFiles(modfolderspath))
            {
                string modfolder = file.Split('\\')[file.Split('\\').Length - 1];
                modfolderlist.Add(modfolder);
            }

            return modfolderlist.ToArray();
        }

        public void ThrowWinErrorMsg(string errorName)
        {
            string errorMessage = "";
            switch (errorName)
            {
                case "ModfolderNotFound":
                    errorMessage = "The given modfolder was not found, please check your existing list of modfolders!";
                    break;

                case "ModfolderExists":
                    errorMessage = "The given modfolder name already exists!";
                    break;

                case "ModNotFound":
                    errorMessage = "The given mod was not found in the minecraft mods or prelauncher mods directory, please put your mod in one of those directories and try again!";
                    break;

                case "ModAlreadyExists":
                    errorMessage = "This mod already exists in the given modfolder!";
                    break;

                case "ModfolderNotSelected":
                    errorMessage = "You have not selected a modfolder, please do so using the SELECT command";
                    break;

                case "NoModGiven":
                    errorMessage = "No mod name was given";
                    break;

                case "NoModfolderGiven":
                    errorMessage = "No modfolder name was given";
                    break;

                default:
                    errorMessage = "Unknown exception occured";
                    break;
            }

            MessageBox.Show(errorMessage, errorName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
