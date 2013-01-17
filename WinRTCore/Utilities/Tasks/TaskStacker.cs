using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WinRTCore.Utilities.Tasks
{
    public static class TaskStacker
    {
        private static Queue<Action> m_lstTask = new Queue<Action>();
        private static ManualResetEventSlim m_resetEvent = new ManualResetEventSlim();


        static TaskStacker()
        {
            Task.Run(delegate
            {
                Action currentTask = null;
                while (true)
                {
                    lock(m_lstTask)
                    {
                        if (m_lstTask.Count != 0)
                        {
                            m_resetEvent.Set();
                        }
                    }
                    m_resetEvent.Wait();
                    
                    lock (m_lstTask)
                    {
                        currentTask = m_lstTask.Dequeue();
                    }

                    currentTask.Invoke();
                    m_resetEvent.Reset();
                }
            });
        }

        public static void AddTask(Action action)
        {
            lock (m_lstTask)
            {
                m_lstTask.Enqueue(action);
            }
            m_resetEvent.Set();
        }
    }
}
