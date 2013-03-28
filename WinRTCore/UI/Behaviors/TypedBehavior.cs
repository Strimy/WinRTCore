using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace WinRTCore.UI
{
    /// <summary>
    /// Typed Base class for behaviors
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Behavior<T> : Behavior where T : FrameworkElement
    {
        protected Behavior()
        {
        }

        public new T AssociatedObject
        {
            get
            {
                return (T)base.AssociatedObject;
            }
            set
            {
                base.AssociatedObject = value;
            }
        }
    }

}
