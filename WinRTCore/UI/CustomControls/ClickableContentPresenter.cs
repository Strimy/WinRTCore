using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace WinRTCore.UI.CustomControls
{
    public class ClickableContentPresenter : ContentControl
    {
        #region [ Constructor ]
        public ClickableContentPresenter()
            : base()
        {
            this.PointerPressed += OnPointerPressed;
            this.PointerReleased += OnPointerReleased;
            this.PointerEntered += ClickableContentPresenter_PointerEntered;
            this.PointerExited += ClickableContentPresenter_PointerExited;
        }
        #endregion [ Constructor ]


        #region [ Events ]

        private void OnPointerPressed(object sender, PointerRoutedEventArgs pointerRoutedEventArgs)
        {
            VisualStateManager.GoToState(this, "Pressed", true);
        }

        private void OnPointerReleased(object sender, PointerRoutedEventArgs pointerRoutedEventArgs)
        {
            VisualStateManager.GoToState(this, "Normal", true);
        }

        private void ClickableContentPresenter_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "PointerOver", true);
        }

        private void ClickableContentPresenter_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "Normal", true);
        }

        #endregion [ Events ]
    }
}
