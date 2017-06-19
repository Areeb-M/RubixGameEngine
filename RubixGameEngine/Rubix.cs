using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using RubixLIB;

namespace Rubix
{
    public abstract class RubixGame
    {
        
        public string executablePath = Directory.GetCurrentDirectory();
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

        public abstract Scene GetDefaultScene();

        public void Run()
        {
            SceneManager.Initialize(GetDefaultScene());
            Debug.Log("Initialized SceneManager!");

            bool exists;
            int FPS, FixedTimeStep;
            double timePerFixedLoop, timePerLoop, timeElapsed, lag;
            Stopwatch timer;

            // Checking for Configuration -------------------------------------
            exists = Config.Exists("FPS");
            if (!exists)
                Config.SetOption("FPS", new string[] { "60" });
            FPS = 0;
            if (!int.TryParse(Config.GetOption("FPS")[0], out FPS))
            {
                Debug.Log("FPS Config Setting was unreadable. Resetting to 60.");
                Config.SetOption("FPS", new string[] { "60" });
                FPS = 60;
            }

            exists = Config.Exists("FixedTimeStep");
            if (!exists)
                Config.SetOption("FixedTimeStep", new string[] { "20" });
            FixedTimeStep = 0;
            if (!int.TryParse(Config.GetOption("FixedTimeStep")[0], out FixedTimeStep))
            {
                Debug.Log("FixedTimeStep Config Setting was unreadable. Resetting to 20.");
                Config.SetOption("FixedTimeStep", new string[] { "20" });
                FixedTimeStep = 20;
            }
            // -----------------------------------------------------------------

            lag = 0;
            timeElapsed = 0;
            timePerLoop = 1000 / (double)FPS;
            timePerFixedLoop = 1000 / (double)FixedTimeStep;
            timer = new Stopwatch();

            Debug.Log("Starting Game Loop!");
            while (true)
            {
                timer.Stop();
                timeElapsed = timer.Elapsed.TotalMilliseconds;
                lag += timeElapsed;

                while (lag >= timePerFixedLoop)
                {
                    SceneManager.FixedUpdate(timePerFixedLoop);
                    lag -= timePerFixedLoop;
                }

                SceneManager.Update();
                SceneManager.Draw(lag / timePerFixedLoop);
                Thread.Sleep((int)(timePerLoop - timeElapsed));

                timer.Start();
            }
        }

        public void Shutdown()
        {
            SceneManager.Shutdown();
            Debug.Log("Shutdown SceneManger!");
            Config.Save();
            Debug.Log("Saved Config to file!");
            TaskManager.Shutdown();
            Thread.CurrentThread.Abort();
        }
    }
}
