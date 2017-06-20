using System;
using Rubix;
using RubixLIB;

namespace TestGame
{
    class Test : RubixGame
    {
        public Test(string[] args) : base(args)
        {

        }

        public override Scene GetDefaultScene()
        {
            return null;
        }

        public override string GetWindowTitle()
        {
            return "Test Game v1.0.0";
        }
    }
}
