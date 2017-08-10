using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Blue.MVVM {
    public interface INotificationCommand : ICommand {
        event EventHandler Executing;
        event EventHandler Executed;

        void NotifyCanExecuteChanged();
    }
}
