using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MC_Prelauncher
{
    class ModFolderDetect
    {
        string pathToListing = @"C:\Users\Admin\AppData\Roaming\.minecraft\Prelauncher\settings\folderconfigs";
        string modFolder = @"C:\Users\Admin\AppData\Roaming\.minecraft\mods";
        public bool GetSimilarityListing(string file, int arraysize)
        {
            int I_HATE_THE_GAYS = 0;
            if (File.Exists(pathToListing + @"\" + file))
            {
                string[] modsIndex = File.ReadAllText(pathToListing + @"\" + file).Split(';');

                foreach (string modfile in Directory.GetFiles(modFolder))
                {
                    string modFileEnd = modfile.Split('\\').Last();     
                    if (modsIndex.Contains(modFileEnd))
                    {
                        I_HATE_THE_GAYS++;
                    }
                }

                if (I_HATE_THE_GAYS == arraysize)
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
            return false;
        }

        public string CompareCurrent()
        {
            string[] allmodconfigs = GetDirectoryListing();

            foreach (string file in allmodconfigs)
            {
                string FUCKING_ASS = File.ReadAllText(pathToListing + '\\' + file);
                bool isFolderRight = GetSimilarityListing(file, FUCKING_ASS.Split(';').Length);
                if (isFolderRight)
                {
                    return file;
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
