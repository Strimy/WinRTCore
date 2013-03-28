using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WinRTCore.Utilities
{
    /// <summary>
    /// A class to execute a task at specified interval
    /// </summary>
    public class Timer
    {
        // Field to simulate the timer
        ManualResetEventSlim m_refEvent = new ManualResetEventSlim();
        
        private bool m_bIsRunning = false;

        /// <summary>
        /// Defines if the Timer is running
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return m_bIsRunning;
            }
        }

        private TimeSpan m_refTime;
        
        /// <summary>
        /// The interval between each tick.
        /// </summary>
        public TimeSpan Interval 
        {
            get
            {
                return m_refTime;
            }
            set
            {
                m_refTime = value;
            }
        }

        /// <summary>
        /// Event raised each time the interval has passed. 
        /// This event is blocking for the timer : it can only be raised one time, and the timer will only restart at the end of the work done on this event.
        /// </summary>
        public event EventHandler Tick;

        /// <summary>
        /// Starts the timer
        /// </summary>
        public void Start()
        {
            if (Interval.TotalMilliseconds == 0)
                throw new InvalidOperationException("Cannot start a timer without a valid interval");
            if (m_bIsRunning)
                throw new InvalidOperationException("The timer is already running");

            Task.Run(delegate
            {
                DateTime startTime = DateTime.Now;
                m_bIsRunning = true;
                while (!m_refEvent.Wait(Interval))
                {
                    //if (DateTime.Now - startTime > Interval)
                    //{
                        if (Tick != null)
                        {
                            Tick(this, new EventArgs());
                        }
                        startTime = DateTime.Now;
                    //}
                }
                m_refEvent.Reset();
                m_bIsRunning = false;
            });
        }

        /// <summary>
        /// Stop the timer
        /// </summary>
        public void Stop()
        {
            m_bIsRunning = false;
            m_refEvent.Set();
        }
    }
}
