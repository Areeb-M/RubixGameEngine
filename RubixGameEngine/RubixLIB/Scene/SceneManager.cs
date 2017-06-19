using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubixLIB
{
    class SceneManager
    {
        static List<Scene> scenes;
        static int sceneCount = 0;

        public static void Initialize(Scene defaultScene)
        {
            scenes = new List<Scene>();
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
