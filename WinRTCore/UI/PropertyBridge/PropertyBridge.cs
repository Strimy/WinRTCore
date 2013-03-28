using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace WinRTCore.UI
{
    /// <summary>
    /// A class that allow the Model value to be dissociated from the UI
    /// It works by binding the ModelValue property to the Model, and binding the UI controls to the UIBridgeValue
    /// In this cas, the UI controls that can modify the value must have an PropertyBridgeBehavior attached to push the UI value to the model
    /// The way the value is pushed is then defined by the Behavior attached that decides when to update the model
    /// </summary>
    public class PropertyBridge : FrameworkElement
    {
        #region Dependency Properties
         #region ModelValue DP
         /// <summary>
         /// The property that is bound to the Model, VM or ViewVM or anything you want
         /// </summary>
         public object ModelValue
         {
             get { return (object)GetValue(ModelValueProperty); }
             set { SetValue(ModelValueProperty, value); }
         }
         
         /// <summary>
         /// ModelValue associated DP
         /// </summary>
         public static readonly DependencyProperty ModelValueProperty =
             DependencyProperty.Register("ModelValue", typeof(object), typeof(PropertyBridge), new PropertyMetadata(null, OnModelValueChanged));
         #endregion
         
         #region UIBridgeValue DP
         /// <summary>
         /// The property that will be bound to the View, it is the direct representation of the UI, and not the Model
         /// </summary>
         public object UIBridgeValue
         {
             get { return (object)GetValue(UIBridgeValueProperty); }
             set { SetValue(UIBridgeValueProperty, value); }
         }
         
         public static readonly DependencyProperty UIBridgeValueProperty = DependencyProperty.Register("UIBridgeValue",
                                                                                                        typeof(object),
                                                                                                        typeof(PropertyBridge),
                                                                                                        new PropertyMetadata(null));
         
         #endregion 
         #endregion
        
        #region DP Changed
         /// <summary>
         /// Callback when the ModelValue changed
         /// </summary>
         /// <param name="o"></param>
         /// <param name="e"></param>
         private static void OnModelValueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
         {
             ((PropertyBridge)o).OnModelValueChanged(e.OldValue, e.NewValue);
         }
         
         /// <summary>
         /// Local Callback when the ModelValue changed, update the UI value from the model value
         /// </summary>
         /// <param name="oldValue"></param>
         /// <param name="newValue"></param>
         private void OnModelValueChanged(object oldValue, object newValue)
         {
             UIBridgeValue = newValue;
         }
         #endregion
        
        /// <summary>
         /// Push the value in the UIBridgeValue property into the ModelValue property
         /// </summary>
        public void UpdateModelProperty()
         {
             ModelValue = UIBridgeValue;
         }
        
        
        #region Static
         /// <summary>
         /// Attached Property to defines on a control that have a Property Bridge Behavior attached
         /// </summary>
        public static readonly DependencyProperty BridgeProperty = DependencyProperty.RegisterAttached("Bridge", typeof(PropertyBridge), typeof(PropertyBridge), new PropertyMetadata(null));


        /// <summary>
        /// Called when Property is retrieved
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Always)]
        public static PropertyBridge GetBridge(DependencyObject obj)
        {
            var bridge = obj.GetValue(BridgeProperty) as PropertyBridge;

            return bridge;
        }

        /// <summary>
        /// Called when Property is retrieved
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetBridge(DependencyObject obj, PropertyBridge value)
        {
            obj.SetValue(BridgeProperty, value);
        }

        #endregion

    }

}
