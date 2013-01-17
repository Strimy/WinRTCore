﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.System.Threading;

namespace WinRTCore.Utilities
{
    /// <summary>
    /// A class to execute a task at specified interval
    /// </summary>
    public class Timer
    {
        ManualResetEventSlim m_refEvent = new ManualResetEventSlim();
        (
        private bool m_bIsRunning = false;

        public bool IsRunning
        {
            get
            {
                return m_bIsRunning;
            }
        }

        private TimeSpan m_refTime;
        
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
        public event EventHandler Tick;

        public Timer()
        {

        }

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

        public void Stop()
        {
            m_bIsRunning = false;
            m_refEvent.Set();
        }
    }
}
