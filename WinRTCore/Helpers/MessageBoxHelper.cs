using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Popups;

namespace WinRTCore.Helpers
{
    public class MessageBox
    {
        /// <summary>
        /// Display a MessageBox with only one button
        /// </summary>
        /// <param name="messageBoxText">Content of the MessageBox</param>
        /// <param name="title">Title of the MessageBox</param>
        /// <param name="button">Button to interact with the MessageBox</param>
        /// <returns></returns>
        public static async Task<object> ShowAsync(string messageBoxText,
                                                             string title,
                                                             MessageBoxButton button)
        {
            MessageDialog md = new MessageDialog(messageBoxText, title);
            object result = null;

            //An exception could be raised if there is two MessageBox in the same time
            try
            {
                await DispatcherHelper.RunAsync(CoreDispatcherPriority.Normal, async delegate
                {
                    md.Commands.Add(
                        new UICommand(button.Text,
                                      new UICommandInvokedHandler
                                          ((cmd) =>
                                           result =
                                           button.Result)));
                    try
                    {
                        await md.ShowAsync();
                    }
                    catch (Exception)
                    {

                    }

                });
            }
            catch (Exception)
            {

            }

            return result;
        }

        /// <summary>
        /// Display a MessageBox with some buttons
        /// </summary>
        /// <param name="messageBoxText">Content of the MessageBox</param>
        /// <param name="title">Title of the MessageBox</param>
        /// <param name="buttons">List of buttons to interact with the MessageBox</param>
        /// <returns></returns>
        public static async Task<object> ShowAsync(string messageBoxText,
                                                             string title,
                                                             List<MessageBoxButton> buttons)
        {

            MessageDialog md = new MessageDialog(messageBoxText, title);
            object result = null;

            foreach (var button in buttons)
            {

                md.Commands.Add(new UICommand(button.Text,
                                              new UICommandInvokedHandler((cmd) => result = button.Result)));


            }
            try
            {
                await md.ShowAsync();
            }
            catch (Exception)
            {
            }
            return result;
        }

    }

    public class MessageBoxButton
    {
        public string Text { get; set; }
        public object Result { get; set; }

        /// <summary>
        /// Create a new MessageBox button to interact with a MessageBox
        /// </summary>
        /// <param name="text">Button's content</param>
        /// <param name="result">Object returned by the click on the button</param>
        public MessageBoxButton(string text, object result)
        {
            this.Text = text;
            this.Result = result;

        }
    }
}
