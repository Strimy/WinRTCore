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
    /// The base PropertyBridge Behavior that gives gives the method to update the bridge
    /// </summary>
    public abstract class PropertyBridgeBehaviorBase : Behavior<FrameworkElement>
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
