using System;
using Rubix;
using RubixLIB;
using TestGame._Scenes;

namespace TestGame
{
    class Test : RubixGame
    {
        public Test(string[] args) : base(args)
        {

        }

        public override Scene GetDefaultScene()
        {
            return new TestScene();
        }

        public override string GetWindowTitle()
        {
            return "Test Game v1.0.0";
        }
    }
}
