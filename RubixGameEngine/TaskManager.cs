using System;
using System.Collections.Generic;
using System.Threading;

namespace Rubix
{

    class Task
    {
        public const int NOW = 0;
        public const int HIGH = 1;
        public const int NORMAL = 2;
        public const int LOW = 3;

        static private Dictionary<int, ThreadPriority> priorityConversion = new Dictionary<int, ThreadPriority>()
        {
            {NOW, ThreadPriority.Highest},
            {HIGH, ThreadPriority.AboveNormal},
            {NORMAL, ThreadPriority.Normal},
            {LOW, ThreadPriority.BelowNormal}
        };

        private ThreadStart task;
        private Thread parent;
        private Thread self;

        public int priority;

        public Task(ThreadStart task, int priority=NORMAL)
        {
            this.task = task;
            this.priority = priority;
            this.parent = Thread.CurrentThread;
            this.self = new Thread(this.task);
            this.self.Priority = priorityConversion[priority];
        }

        public Thread GetThread()
        {
            return self;
        }
    }

    class TaskManager
    {
        static List<Task> tasks;
        static TaskQueue queue;
        static Thread self;
        static bool initialized = false;

        public static void Initialize()
        {
            tasks = new List<Task>();
            queue = new TaskQueue();

            ThreadStart startTaskManager = new ThreadStart(Run);
            self = new Thread(startTaskManager);
            self.Priority = ThreadPriority.Highest;
            self.Start();

            initialized = true;
        }

        public static void Shutdown()
        {
            self.Abort();
            foreach (Task task in tasks)
            {
                task.GetThread().Abort(); 
            }
            tasks.Clear();
        #warning Add shutdown sequence to every task
        }

        public static void AddTask(Task task)
        {
            if (initialized)
                queue.AddTask(task);
            else
                throw new Exception("Task Manager is not initialized!");
        }

        private static void Run()
        {
            while (true)
            {
                lock (tasks)
                {
                    Task task = null;
                    do
                    {
                        task = queue.GetTask();
                        if (task != null)
                        {
                            task.GetThread().Start();
                            tasks.Add(task);
                        }
                    } while (task != null);

                    for (int i = tasks.Count - 1; i > -1; i--)
                    {
                        if (tasks[i].GetThread().IsAlive)
                            continue;
                        tasks.RemoveAt(i);
                    }
                }

            }

        }
    }

    class TaskQueue
    {
        private List<List<Task>> tasks;

        public TaskQueue()
        {
            this.tasks = new List<List<Task>>();
            this.tasks.Add(new List<Task>());
            this.tasks.Add(new List<Task>());
            this.tasks.Add(new List<Task>());
            this.tasks.Add(new List<Task>());
        }

        public void AddTask(Task task)
        {
            lock (this)
            {
                tasks[task.priority].Add(task);
            }
        }

        public Task GetTask()
        {
            lock (this)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (tasks[i].Count > 0)
                    {
                        Task t = tasks[i][0];
                        tasks[i].RemoveAt(0);
                        return t;
                    }
                }
                return null;
            }
        }
    }
}
