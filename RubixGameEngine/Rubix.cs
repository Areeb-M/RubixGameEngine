using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubix
{
    public class Rubix
    {
        public static void Main(string[] args)
        {
            TaskManager.Initialize();
            Debug.Initialize();

            for(int i = 0; i < 100000000; i++)
            {
                if (i % 2500 == 0)
                    Debug.Log(i);
                if (i % 250000 == 0)
                    Console.WriteLine("Main Thread is at " + i);
            }

            Console.ReadLine();

            TaskManager.Shutdown();
        }
    }
}
