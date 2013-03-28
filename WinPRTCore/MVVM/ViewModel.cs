using System.ComponentModel;
using System.Runtime.Serialization;


namespace WinRTCore.MVVM
{
    /// <summary>
    /// A view model that implement the INotifyPropertyChanged interface
    /// </summary>
    [DataContract]
    public class ViewModel : INotifyPropertyChanged
    {

        protected void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
