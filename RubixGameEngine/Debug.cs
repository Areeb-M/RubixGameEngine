using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Collections.Generic;

namespace Rubix
{

    class Debug
    {
        static Debugger debugger;
        static MessageQueue queue;
        static bool initialized = false;

        public static void Initialize()
        {
            queue = new MessageQueue();
            debugger = new Debugger(queue);

            Task startDebugger = new Task(debugger.runref, Task.LOW);
            TaskManager.AddTask(startDebugger);
            initialized = true;
            Log("Successfully initialized Task Manager!");
            Log("Successfully initialized Logger!");
        }

        public static void Log(Object message)
        {
            if (initialized)
                queue.AddMessage(message);
        }

    }

    class MessageQueue
    {
        List<String> queue;

        public MessageQueue()
        {
            queue = new List<String>();
        }

        public void AddMessage(Object message)
        {
            lock(this)
            {
                string time = DateTime.Now.ToString("h:mm:ss");
                queue.Add("[" + time + "]" + message);
            }
        }

        public String GetMessage()
        {
            lock(this)
            {
                if (queue.Count > 0)
                {
                    string message = queue[0];
                    queue.RemoveAt(0);
                    return message;
                } else
                {
                    return null;
                }
            }
        }

    }

    class Debugger
    {
        public MessageQueue queue;
        public ThreadStart runref;
        private StreamWriter log;

        public Debugger(MessageQueue queue)
        {
            this.queue = queue;
            this.runref = new ThreadStart(Run);
            
            if (!Directory.Exists("\\logs"))
                Directory.CreateDirectory("\\logs");
            
            string path = "\\logs\\" + DateTime.Today.ToString("d").Replace(" ", "").Replace("/",".").Replace(":",",") + DateTime.Now.ToString("h:mm:ss").Replace(":", ",") + ".txt";
            File.WriteAllText(path, "");
            this.log = new StreamWriter(path);
        }

        private void Run()
        {
            while (true)
            {
                string message = queue.GetMessage();
                if (message == null)
                    Thread.Sleep(100);
                else
                    Log(message);
            }
        }

        private void Log(Object msg)
        {
            string message = msg.ToString();
            Console.WriteLine(message);
            log.WriteLine(message);
        }
    }
}
