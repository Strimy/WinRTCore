﻿using WinRTCore.Utilities;

namespace WinRTCore.MVVM
{
    /// <summary>
    /// A ViewModel that has a loading indicator
    /// </summary>
    public class LoadableViewModel : ViewModel
    {
        private bool m_refIsLoading;

        /// <summary>
        /// Indicates if the ViewModel is loading.
        /// This is thread safe, and can be changed from any thread (if the DispatcherService is initialized)
        /// </summary>
        public bool IsLoading
        {
            get 
            { 
                return m_refIsLoading; 
            }
            set 
            {
                if (value != m_refIsLoading)
                {
                    m_refIsLoading = value;

                    // Get the current thread, and compare the dispatcher
                    var coreWin = Windows.UI.Core.CoreWindow.GetForCurrentThread();
                    if (coreWin != null && DispatcherService.Dispatcher == coreWin.Dispatcher)
                    {
                        // We are already on the UI Thread, just throw the notification directly
                        NotifyPropertyChanged("IsLoading");
                        HandleIsLoadingChanged(value);
                    }
                    else
                    {
                        // Ensure the changes of the loading state can be done from any thread
                        DispatcherService.RunOnDispatcher(delegate
                        {
                            NotifyPropertyChanged("IsLoading");
                            HandleIsLoadingChanged(value);
                        });
                    }
                }
            }
        }

        protected virtual void HandleIsLoadingChanged(bool newValue)
        {
        }
    }
}
