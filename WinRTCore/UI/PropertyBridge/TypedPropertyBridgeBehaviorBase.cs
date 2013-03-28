using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace WinRTCore.UI
{
    /// <summary>
    /// The typed base PropertyBridge Behavior that gives gives the method to update the bridge
    /// </summary>
    public class PropertyBridgeBehaviorBase<T> : Behavior<T> where T : FrameworkElement
    {
        protected void UpdateBridgeBinding()
        {
            if (AssociatedObject == null)
                return;

            PropertyBridge bridge = PropertyBridge.GetBridge(AssociatedObject);
            if (bridge != null)
            {
                bridge.UpdateModelProperty();
            }

#if DEBUG
            else
                Debug.WriteLine("WARNING : BridgeElement without bound Bridge");
#endif
        }
    }
}
