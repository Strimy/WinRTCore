using System.ComponentModel;


namespace WinRTCore
{
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
