using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp2
{
    class ModfolderDetect
    {
        string pathToListing = $@"C:\Users\{Environment.UserName}\AppData\Roaming\.minecraft\Prelauncher\configs";
        string modFolder = $@"C:\Users\{Environment.UserName}\AppData\Roaming\.minecraft\mods";
        public bool GetSimilarityListing(string file, int arraysize)
        {
            int I_HATE_THE_GAYS = 0;
            if (File.Exists(pathToListing + @"\" + file))
            {
                string[] modsIndex = File.ReadAllLines(pathToListing + @"\" + file);

                foreach (string modfile in Directory.GetFiles(modFolder))
                {
                    string modFileEnd = modfile.Split('\\').Last();
                    if (modsIndex.Contains(modFileEnd)) I_HATE_THE_GAYS++;
                }

                if (I_HATE_THE_GAYS == arraysize) return true;
            }
            else return false;
            return false;
        }

        public string CompareCurrent()
        {
            string[] allmodconfigs = GetDirectoryListing();

            foreach (string file in allmodconfigs)
            {
                string[] FUCKING_ASS = File.ReadAllLines(pathToListing + '\\' + file);
                bool isFolderRight = GetSimilarityListing(file, FUCKING_ASS.Length);
                if (isFolderRight)
                {
                    return file.Split('.')[0];
                }
            }
            return "unrecognised";
        }

        public string[] GetDirectoryListing()
        {
            List<string> gay = new List<string> { };


            foreach (string file in Directory.GetFiles(pathToListing))
            {
                gay.Add(file.Split('\\').Last());
            }

            return gay.ToArray();
        }
    }
}
