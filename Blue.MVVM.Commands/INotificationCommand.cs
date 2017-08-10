using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Blue.MVVM.Commands {
    public interface INotificationCommand : ICommand {
        event EventHandler Executing;
        event EventHandler Executed;

        void NotifyCanExecuteChanged();
    }
}
