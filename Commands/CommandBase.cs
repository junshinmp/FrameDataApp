using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace FrameDataApp.Commands
{
    /// <summary>
    /// Class <c>CommandBase</c> serves as a base blueprint for 
    /// concrete command classes.
    /// </summary>
    public abstract class CommandBase : ICommand
    {

        /// <summary>
        /// Event that WPF subscribes for behind the scenes.
        /// Attaches for commands in the child Command classes.
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// Provides default implementation, but allows overrides
        /// so that child classes can override.
        /// 
        /// WPF calls upon this method in order to determine if
        /// a certain UI button should be enabled or disable.
        /// </summary>
        /// 
        /// <param name="parameter"></param> Extra data from WPF
        /// <returns></returns> Whether it can be executing
        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Invoked by WPF when user clicks a button,
        /// child command class will define.
        /// </summary>
        /// <param name="parameter"></param> Extra data from WPF
        public abstract void Execute(object parameter);

        /// <summary>
        /// Triggers the event safely upon PropertyChanged
        /// call.
        /// </summary>
        protected void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
