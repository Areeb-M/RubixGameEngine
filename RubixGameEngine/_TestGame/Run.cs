using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubix.TestGame
{
    class Run
    {
        public static void Main(string[] args)
        {
            RubixGame TestGame = new TestGame(args);
            TestGame.Run();
        }
    }
}
