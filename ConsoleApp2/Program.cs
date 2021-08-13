using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace ConsoleApp2
{
    class Program
    {
        /// <summary>
        /// All of the "UI" stuff is gonna be inside this class, anything else should be in other classes :)
        /// </summary>

        static string appName = "Minecraft Prelauncher";
        static string devName = "PlexIsLucky";
        static string version = "Beta";
        static void Main()
        {
            while (true)
            {

                Functions Functions = new Functions();
                ModfolderDetect Modfolder = new ModfolderDetect();
                string _modfolder = Modfolder.CompareCurrent();
                
                Console.Title = appName;

                Console.Clear();
                Console.WriteLine($"{appName} {version} by {devName}\n[Esc] to exit\n");
                Console.WriteLine($"Current modfolder: {_modfolder}\n");
                Console.WriteLine(
                    "[1] Launch\n" +
                    "[2] Switch Mod Folder\n" +
                    "[3] Mod Folder Editor");

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;

                    case ConsoleKey.D1:
                        Functions.StartLauncher();
                        Environment.Exit(0);
                        break;

                    case ConsoleKey.D2:
                        string[] modfolders = Functions.ModfolderList();
                        Functions.ModfolderChoice(modfolders);
                        break;

                    case ConsoleKey.D3:
                        Editmode Editmode = new Editmode();
                        Editmode.editmodeInit();
                        break;


                }
            }
        }
    }
}
