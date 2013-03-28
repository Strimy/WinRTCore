using Windows.UI.Xaml;

namespace WinRTCore.UI
{
    /// <summary>
    /// Bridge behavior that will trigger the changes in the bridge when the capture is lost on the attached control
    /// </summary>
    public class OnCaptureLostPropertyBridgeBehavior : PropertyBridgeBehaviorBase<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.PointerCaptureLost += OnAssociatedObject_PointerCaptureLost;
        }

        void OnAssociatedObject_PointerCaptureLost(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            UpdateBridgeBinding();
        }
    }
}
