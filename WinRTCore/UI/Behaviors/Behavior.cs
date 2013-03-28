using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace WinRTCore.UI
{
    public abstract class Behavior : FrameworkElement
    {
        private FrameworkElement associatedObject;

        /// <summary>
        /// The associated object
        /// </summary>
        public FrameworkElement AssociatedObject
        {
            get
            {
                return associatedObject;
            }
            set
            {
                if (associatedObject != null)
                {
                    OnDetaching();
                }
                DataContext = null;

                associatedObject = value;
                if (associatedObject != null)
                {
                    OnAttached();
                }
            }
        }

        protected virtual void OnAttached()
        {
            AssociatedObject.Unloaded += AssociatedObjectUnloaded;
            AssociatedObject.Loaded += AssociatedObjectLoaded;
        }

        protected virtual void OnDetaching()
        {
            AssociatedObject.Unloaded -= AssociatedObjectUnloaded;
            AssociatedObject.Loaded -= AssociatedObjectLoaded;
        }

        private void AssociatedObjectLoaded(object sender, RoutedEventArgs e)
        {
            // Binds the DataContext of the Associated Object to the Behavior DataContext
            SetBinding(
                     DataContextProperty,
                     new Binding
                     {
                         Path = new PropertyPath("DataContext"),
                         Source = associatedObject
                     });
        }

        private void AssociatedObjectUnloaded(object sender, RoutedEventArgs e)
        {
            OnDetaching();
        }
    }

}
