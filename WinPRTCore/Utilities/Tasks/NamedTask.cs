using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace WinRTCore.Utilities.Tasks
{
    /// <summary>
    /// A helper to invoke tasks after a timeout, or cancel them, or reinitialize them.
    /// A NamedTask is an associtation between a name, a timeout and an action.
    /// When a NamedTask is added, the associated action will be invoked only after the timeout has expired. During this timeout,
    /// the task can be reinitialized by using the same name, with a new timeout, and another delegate. The previous task is replaced by the new one.
    /// 
    /// The goal is to invoke a method only a few times by reseting the named task.
    /// </summary>
    public static class NamedTask
    {
        private static Dictionary<string, Tuple<int, ManualResetEventSlim, Action>> m_refMapTaskNameToEvent = new Dictionary<string, Tuple<int, ManualResetEventSlim, Action>>();

        /// <summary>
        /// Create a named task that will be invoked after the timeout is reached. 
        /// If another task with the same name is called, the task is cancelled and replaced by the new one
        /// </summary>
        /// <param name="taskName">The task name</param>
        /// <param name="timeOut">The delay before the task is invoked (in milliseconds)</param>
        /// <param name="refDelegate">The action to invoke</param>
        public static void AddNamedTask(string taskName, int timeOut, Action refDelegate)
        {
            lock(m_refMapTaskNameToEvent)
            {
                ManualResetEventSlim oldEvent = null;
                if (m_refMapTaskNameToEvent.ContainsKey(taskName))
                {
                    oldEvent = m_refMapTaskNameToEvent[taskName].Item2;
                }
                
                m_refMapTaskNameToEvent[taskName] = new Tuple<int, ManualResetEventSlim, Action>(timeOut, new ManualResetEventSlim(), refDelegate);
                if (oldEvent != null)
                    oldEvent.Set();
            }

            Task.Run(delegate
            {
                // Start an infinite loop that will be broken at the end of the task or when the task doesn't exist anymore
                while (true)
                {
                    Action namedTaskAction = null;
                    int timeout = -1;
                    ManualResetEventSlim namedTaskEvent = null;
                    lock (m_refMapTaskNameToEvent)
                    {
                        // The task started and the event has already
                        if (!m_refMapTaskNameToEvent.ContainsKey(taskName))
                            return;
                        else
                        {
                            Tuple<int, ManualResetEventSlim, Action> namedTaskTuple = m_refMapTaskNameToEvent[taskName];
                            timeout = namedTaskTuple.Item1;
                            namedTaskEvent = namedTaskTuple.Item2;
                            namedTaskAction = namedTaskTuple.Item3;
                        }
                    }

                    if (namedTaskEvent.Wait(timeout))
                    {
                        // The task was reset or cancelled, restart with a the new timeout and action
                        continue;
                    }
                    else
                    {
                        // Ok, the timeout has passed, invoke the method, and remove the task from the map
                        namedTaskAction.Invoke();

                        lock (m_refMapTaskNameToEvent)
                        {
                            m_refMapTaskNameToEvent.Remove(taskName);
                        }
                        return;
                    }
                }
            });
        }

        /// <summary>
        /// Cancel a previously added named task
        /// </summary>
        /// <param name="taskName">The name of the task to cancel</param>
        /// <returns>True if the task has been cancelled, false otherwise (unexisting task/already executed)</returns>
        public static bool CancelNamedTask(string taskName)
        {
            lock (m_refMapTaskNameToEvent)
            {
                if (m_refMapTaskNameToEvent.ContainsKey(taskName))
                {
                    ManualResetEventSlim resetEvent = m_refMapTaskNameToEvent[taskName].Item2;

                    // Remove the event from the map, and set the event, 
                    // this will trigger the task to restart and checks that it doesn't exist anymore
                    m_refMapTaskNameToEvent.Remove(taskName);
                    resetEvent.Set();
                    return true;
                }
            }

            return false;
        }
    }
}
