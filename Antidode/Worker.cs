using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antidode
{
    class Worker
    {
        public static Worker Instance(string EXT) => new Worker(EXT);
        public string EXT;
        public Worker(string EXT)
        {
            this.EXT = EXT;
        }
        public void Run(string VICTIM_PRIVATE_KEY, Action<string> LOGGER, string[] EXCLUDE_DIRS = null)
        {
            if(VICTIM_PRIVATE_KEY == null || VICTIM_PRIVATE_KEY.Trim() == "")
            {
                LOGGER("\n*Oops! No Antidode Key Given");
                return;
            }
#if (DEBUG)
            FindTargets(Directory.GetCurrentDirectory(), (TARGET) =>
            {
                Utils.Heal(TARGET, EXT, VICTIM_PRIVATE_KEY, LOGGER);
                string README_PATH = Path.Combine(Path.GetDirectoryName(TARGET), $"README[{EXT}].txt");
                if (File.Exists(README_PATH))
                {
                    System.IO.File.Delete(README_PATH);
                }
            },
            EXCLUDE_DIRS);
#else
#endif
        }

        private void FindTargets(string PATH, Action<string> TARGET, string[] EXCLUDE_DIRS = null)
        {
            try
            {
                foreach (string EACH_NODE in Directory.GetFiles(PATH).Where(elem => {
                    if (elem.EndsWith(this.EXT))
                    {
                        if (elem.Substring(0, elem.Length - EXT.Length) == System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
                        {
                            return false;
                        }
                        return true;
                    }
                    return false;
                }))
                {
                    TARGET(EACH_NODE);
                }
            }
            catch { }

            try
            {
                foreach (string EACH_NODE in Directory.GetDirectories(PATH).Where(elem => EXCLUDE_DIRS != null ? !EXCLUDE_DIRS.Contains(elem) : true))
                {
                    FindTargets(EACH_NODE, TARGET, EXCLUDE_DIRS);
                }
            }
            catch { }
        }
    }
}
