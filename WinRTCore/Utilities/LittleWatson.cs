using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WinRTCore.Helpers;

namespace WinRTCore.Utilities
{
    public static class LittleWatson
    {
        const string filename = "LittleWatson.txt";

        /// <summary>
        /// Save the new catched exception and delete the old one
        /// </summary>
        /// <param name="ex">The exception</param>
        /// <param name="extra">Custom message</param>
        public async static Task ReportException(Exception ex, string extra)
        {
            try
            {
                string text = extra + "\n" + ex.Message + "\n\n" + ex.StackTrace;
                await StorageService.SaveAsync(filename, text);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Verify if there is an exception saved
        /// </summary>
        public async static Task CheckForPreviousException()
        {
            try
            {
                string contents = null;
                bool isFileExist = await StorageService.IsFileExist(filename);
                if (isFileExist)
                {

                    contents = (await StorageService.LoadAsync<object>(filename)) as String;

                    await SafeDeleteFile();
                }

                if (contents != null)
                {
                    List<MessageBoxButton> Buttons = new List<MessageBoxButton>();
                    Buttons.Add(new MessageBoxButton("Ok", true));
                    Buttons.Add(new MessageBoxButton("No", false));
                    bool result =
                        (bool)
                        await
                        MessageBox.ShowAsync(
                            "A problem occurred the last time you ran this application. Would you like to send an email to report it?",
                            "Problem Report",
                            Buttons);
                    if (result)
                    {
                        EmailComposeTask email = new EmailComposeTask();
                        email.To = "support@clevlab.fr";
                        email.Subject = "[Error] Instant TV - Windows 8 - Bêta";
                        email.Body = contents;
                        await SafeDeleteFile();
                        await email.Show();
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                Task.Run(() => SafeDeleteFile());
            }
        }

        /// <summary>
        /// Delete old exception
        /// </summary>
        /// <returns></returns>
        private static async Task SafeDeleteFile()
        {
            try
            {
                await StorageService.DeleteFileAsync(filename);
            }
            catch (Exception)
            {
            }
        }
    }
}
