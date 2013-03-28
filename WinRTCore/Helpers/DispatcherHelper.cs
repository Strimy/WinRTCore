using System;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace WinRTCore.Helpers
{
    public static class DispatcherHelper
    {
        public static CoreDispatcher Dispatcher { get; set; }

        /// <summary>
        /// Runs the event dispatcher on UI thread
        /// </summary>
        /// <param name="coreDispatcherPriority"></param>
        /// <param name="agileCallBack"></param>
        public async static Task RunAsync(CoreDispatcherPriority coreDispatcherPriority, DispatchedHandler agileCallBack)
        {
            //If there is no dispatcher
            if (Dispatcher == null)
                Dispatcher = Window.Current.Dispatcher;

            //If it's already on UI Thread
            if (Dispatcher.HasThreadAccess)
                agileCallBack.Invoke();
            else
                await DispatcherHelper.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, agileCallBack);
        }
    }
}
