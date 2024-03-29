﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace WinRTCore.Utilities
{
    public static class DispatcherService
    {
        static private CoreDispatcher m_refDispatcher = null;

        /// <summary>
        /// The Dispatcher
        /// </summary>
        static public CoreDispatcher Dispatcher
        {
            get
            {
                return m_refDispatcher;
            }
            private set
            {
                m_refDispatcher = value;
            }
        }

        /// <summary>
        /// Initialization method that must be called from the UI thread
        /// </summary>
        public static void Init()
        {
            if (m_refDispatcher == null)
            {
                Dispatcher = Windows.UI.Core.CoreWindow.GetForCurrentThread().Dispatcher;
            }
        }

        /// <summary>
        /// Runs the given delegate on the Dispatcher (with the optional priority)
        /// </summary>
        /// <param name="method">The method to run on the Dispatcher</param>
        /// <param name="priority">The Dispatcher priority</param>
        public static async void RunOnDispatcher(DispatchedHandler method, CoreDispatcherPriority priority = CoreDispatcherPriority.Normal)
        {
            if (m_refDispatcher == null)
            {
                throw new InvalidOperationException("The service has NOT been initialized");
            }

            await Dispatcher.RunAsync(priority, method);
        }
    }
}
