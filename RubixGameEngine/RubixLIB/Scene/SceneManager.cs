using Rubix;
using System;
using OpenTK;
using RubixLIB.Graphics;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace RubixLIB
{
    public class SceneManager
    {
        static List<Scene> scenes;
        static int sceneCount = 0;

        static RubixGame game;
        static Window window;

        // OpenGL
        private int _vertexArray;

        public static void Initialize(RubixGame _game, Scene defaultScene)
        {
            game = _game;

            int width = 0;
            int height = 0;
            int windowFlag = 0;
            GameWindowFlags mode = GameWindowFlags.Default;
            bool exists;

            #region Checking Config for Screen Resolution and Window Mode
            exists = Config.Exists("SCREEN_RESOLUTION");
            if (!exists)
                Config.SetOption("SCREEN_RESOLUTION", new string[] { "1280", "720" });

            if (!int.TryParse(Config.GetOption("SCREEN_RESOLUTION")[0], out width) || !int.TryParse(Config.GetOption("SCREEN_RESOLUTION")[1], out height))
            {
                Debug.Log("SCREEN_RESOLUTION Config Setting was unreadable. Resetting to 1280x720.");
                Config.SetOption("SCREEN_RESOLUTION", new string[] { "1280", "720" });
                width = 1280;
                height = 720;
            }

            exists = Config.Exists("WINDOW_MODE");
            if (!exists)
                Config.SetOption("WINDOW_MODE", new string[] { "0" });

            if (!int.TryParse(Config.GetOption("WINDOW_MODE")[0], out windowFlag) && windowFlag < 2)
            {
                Debug.Log("SCREEN_RESOLUTION Config Setting was unreadable. Resetting to windowed(0)");
                Config.SetOption("SCREEN_RESOLUTION", new string[] { "0" });
                windowFlag = 0;
            }

            if (windowFlag == 0)
            {
                mode = GameWindowFlags.Default;
            } else if (windowFlag == 1)
            {
                mode = GameWindowFlags.Fullscreen;
            }
            #endregion


            window = new Window(width, height, game.GetWindowTitle(), mode);
            window.Show();
            Debug.Log("Window Initialization Successful!");

            scenes = new List<Scene>();
            LoadScene(defaultScene);
            Debug.Log("Loaded Default Scene!");
        }

        public static void LoadScene(Scene addition, bool pausePrevious=true, bool hidePrevious=false)
        {
            if (sceneCount > 0)
            {
                scenes[sceneCount - 1].Pause(pausePrevious);
                scenes[sceneCount - 1].Hide(hidePrevious);
            }

            scenes.Add(addition);
            addition.Load();
            sceneCount++;
            Debug.Log("Scene Added!");
        }

        public static void RemoveScene(Scene removal)
        {
            removal.Shutdown();
            scenes.Remove(removal);
            sceneCount--;
            Debug.Log("Scene Removed!");
        }

        public static void FixedUpdate(float timeElapsed) // Perform any physics calculations here
        {
            foreach (Scene s in scenes)
            {
                s.FixedUpdate(timeElapsed);
            }
        }

        public static void Update() // Handle User Input and AI
        {
            foreach (Scene s in scenes)
            {
                s.Update();
            }
        }

        public static void Draw(float timeElapsed)
        {
            if (window.CanBeginRender())
            {
                foreach (Scene s in scenes)
                {
                    s.Draw(timeElapsed);
                }
            } else
            {
                Debug.Log("Window forced exit!");
                Shutdown();
            }
            window.EndRender();
        }

        public static void Shutdown()
        {
            foreach (Scene s in scenes)
                s.Shutdown();
            game.Shutdown();
        }
    }
}
