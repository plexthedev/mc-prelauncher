using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        break;
                }
            }
        }
    }
}
