using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Blue.MVVM.Commands {
    public interface IAsyncCommand<T> : ICommand<T> {
        Task ExecuteAsync(T parameter);
    }
}
