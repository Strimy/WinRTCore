using System;
using System.Threading.Tasks;

namespace WinRTCore.Utilities
{
    /// <summary>
    /// Allows an application to launch the email application with a new message displayed. Use this to allow users to send email from your application.
    /// </summary>
    public class EmailComposeTask
    {
        /// <summary>
        /// Gets or sets the subject of the new email message.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the body of the new email message.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the recipients (separate with a comma) on the To line of the new email message.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the recipients (separate with a comma) on the Cc line of the new email message.
        /// </summary>
        public string Cc { get; set; }

        /// <summary>
        /// Gets or sets the recipients (separate with a comma) on the Bcc line of the new email message.
        /// </summary>
        public string Bcc { get; set; }

        /// <summary>
        /// Shows the email application with a new message displayed.
        /// </summary>
        /// <returns></returns>
        public async Task Show()
        {
            var mailto = new Uri(String.Format("mailto:?subject={0}&body={1}&to={2}&cc={3}&bcc={4}", Subject, Body, To, Cc, Bcc));
            await Windows.System.Launcher.LaunchUriAsync(mailto);
        }
    }
}
