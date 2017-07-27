using System;
using System.Collections.Generic;
using System.Text;

namespace Blue.MVVM.Commands {
    public interface INotificationCommand<T> : INotificationCommand, ICommand<T> {
    }
}
