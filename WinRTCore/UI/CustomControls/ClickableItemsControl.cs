using System;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace WinRTCore.UI.CustomControls
{
    public class ClickableItemsControl : ItemsControl
    {
        #region [ Properties ]
        public delegate void ItemClickedEventHandler(object o, PointerRoutedEventArgs e);
        public event EventHandler<PointerRoutedEventArgs> ItemClicked;

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(ClickableItemsControl), new PropertyMetadata(null));


        public static readonly DependencyProperty ItemClickedCommandProperty =
            DependencyProperty.Register("ItemClickedCommand", typeof(ICommand), typeof(ClickableItemsControl), new PropertyMetadata(default(ICommand)));

        public ICommand ItemClickedCommand
        {
            get { return (ICommand)GetValue(ItemClickedCommandProperty); }
            set { SetValue(ItemClickedCommandProperty, value); }
        }

        #endregion [ Properties ]


        #region [ Events ]
        /// <summary>
        /// Get the selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fe_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            SelectedItem = ((FrameworkElement)sender).DataContext;
        }

        /// <summary>
        /// If the selected item is still the same we raised an ItemClicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void fe_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            try
            {
                if (SelectedItem.Equals(((FrameworkElement)sender).DataContext))
                {
                    if (ItemClicked != null)
                    {
                        ItemClicked(SelectedItem, e);
                    }
                    if (ItemClickedCommand != null && ItemClickedCommand.CanExecute(SelectedItem))
                        ItemClickedCommand.Execute(SelectedItem);
                    SelectedItem = null;
                }
            }
            catch (Exception)
            {


            }

        }
        #endregion [ Events ]


        #region [ Functions ]
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            FrameworkElement fe = element as FrameworkElement;
            fe.PointerReleased += fe_PointerReleased;
            fe.PointerPressed += fe_PointerPressed;
        }

        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            base.ClearContainerForItemOverride(element, item);
            FrameworkElement fe = element as FrameworkElement;
            fe.PointerReleased -= fe_PointerReleased;
            fe.PointerPressed -= fe_PointerPressed;
        }
        #endregion [ Functions ]

    }
}
