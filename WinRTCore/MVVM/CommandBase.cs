using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WinRTCore
{
    public abstract class CommandBase : ICommand
    {
        /// <summary>
        /// Determines whether this command can execute.
        /// </summary>
        /// <param name="parameter">
        /// The command parameter.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the command can execute, otherwise <see langword="false"/>.
        /// </returns>
        public abstract bool CanExecute(object parameter);

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">
        /// The command parameter.
        /// </param>
        public abstract void Execute(object parameter);


        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}
