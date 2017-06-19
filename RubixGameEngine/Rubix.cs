using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubix
{
    public class RubixGame
    {
        public string executablePath = System.IO.Directory.GetCurrentDirectory();
        public RubixGame(string[] args)
        {
            string configPath = "options.config";
            if (args.Contains("config"))
                configPath = args.ElementAt(Array.IndexOf(args, "config") + 1);

            Config.Load(configPath);
            TaskManager.Initialize();

            if (args.Contains("debug"))
                Debug.Initialize();
        }

        public void Run()
        {

        }

        public void Shutdown()
        {
            Config.Save();
            TaskManager.Shutdown();
        }
    }
}
