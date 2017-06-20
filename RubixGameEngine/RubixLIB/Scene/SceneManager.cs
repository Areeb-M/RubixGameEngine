using System;
using System.Collections.Generic;
using Rubix;
using RubixLIB.Graphics;

namespace RubixLIB
{
    class SceneManager
    {
        static List<Scene> scenes;
        static int sceneCount = 0;

        public static void Initialize(Scene defaultScene)
        {
            scenes = new List<Scene>();

            int width = 0;
            int height = 0;
            bool exists;

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
            
            LoadScene(defaultScene);            
        }

        public static void LoadScene(Scene addition, bool pausePrevious=true, bool hidePrevious=false)
        {
            sceneCount++;
        }

        public static void FixedUpdate(double timeElapsed) // Perform any physics calculations here
        {

        }

        public static void Update() // Handle User Input
        {

        }

        public static void Draw(double timeElapsed)
        {

        }

        public static void Shutdown()
        {

        }
    }
}
