using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WinRTCore.Utilities.Tasks
{
    /// <summary>
    /// A helper to stack task to execute one after another. Only one task is executed
    /// </summary>
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

        /// <summary>
        /// Add a task on the pool. The task will be started only after all the others have finished
        /// </summary>
        /// <param name="action"></param>
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
