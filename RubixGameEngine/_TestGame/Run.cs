using Rubix;

namespace TestGame
{
    public class Run
    {
        public static RubixGame TestGame;
        public static void Main(string[] args)
        {
            TestGame = new Test(args);
            TestGame.Run();
        }
    }
}
