using WinRTCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinRTCore.MVVM
{
    public class LoadableViewModel : ViewModel
    {
        private bool m_refIsLoading;

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

                    var coreWin = Windows.UI.Core.CoreWindow.GetForCurrentThread();
                    if (coreWin != null && DispatcherService.Dispatcher == coreWin.Dispatcher)
                    {
                        NotifyPropertyChanged("IsLoading");
                        HandleIsLoadingChanged(value);
                    }
                    else
                    {
                        // Ensure the changes of the loading state can be done from any thread
                        DispatcherService.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, delegate
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

        static LoadableViewModel()
        {
            DispatcherService.Init();
        }
    }
}
