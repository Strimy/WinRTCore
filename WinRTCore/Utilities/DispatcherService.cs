using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace WinRTCore.Utilities
{
    public static class DispatcherService
    {
        public static CoreDispatcher Dispatcher { get; set; }

        public static void Init()
        {
            if (Dispatcher == null)
            {
                Dispatcher = Windows.UI.Core.CoreWindow.GetForCurrentThread().Dispatcher;
            }
        }
    }
}
