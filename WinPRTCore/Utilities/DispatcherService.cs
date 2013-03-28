using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Windows.UI.Core;

namespace WinRTCore.Utilities
{
    public static class DispatcherService
    {
        static private Dispatcher m_refDispatcher = null;

        /// <summary>
        /// The Dispatcher
        /// </summary>
        static public Dispatcher Dispatcher
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


        public static void Init(Dispatcher dispatcher)
        {
            Dispatcher = dispatcher;
        }

        /// <summary>
        /// Runs the given delegate on the Dispatcher (with the optional priority)
        /// </summary>
        /// <param name="method">The method to run on the Dispatcher</param>
        /// <param name="priority">The Dispatcher priority</param>
        public static void RunOnDispatcher(Action method, CoreDispatcherPriority priority = CoreDispatcherPriority.Normal)
        {
            if (m_refDispatcher == null)
            {
                throw new InvalidOperationException("The service has NOT been initialized");
            }

            Dispatcher.BeginInvoke(method);
        }
    }
}
