using Rubix;
using System;
using OpenTK;
using RubixLIB;

namespace TestGame._Scenes
{
    class TestScene : Scene
    {

        public override void OnDraw(float timeElapsed)
        {

        }

        public override void OnLoad()
        {
            AddLayer();
            Sprite test = new Sprite(
                new Vector4(0, 0, 0, 0),
                new Vector4(0, 0, 0, 1),
                30, 30
                );
            AddToLayer(0, test);
        }

        public override void OnUpdate() // Handle User Input and AI and Interactions
        {
        }
    }
}
