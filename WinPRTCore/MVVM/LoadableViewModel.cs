using System.Windows.Threading;
using WinRTCore.Utilities;

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

                    if (DispatcherService.Dispatcher != null)
                    {
                        DispatcherService.Dispatcher.BeginInvoke(delegate
                        {
                            NotifyPropertyChanged("IsLoading");
                            HandleIsLoadingChanged(value);
                        });
                    }
                    else
                    {
                        NotifyPropertyChanged("IsLoading");
                        HandleIsLoadingChanged(value);
                    }
                }
            }
        }

        protected virtual void HandleIsLoadingChanged(bool newValue)
        {
        }
    }
}
