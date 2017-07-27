using Blue.MVVM.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Blue.MVVM.Commands {
    public static class ICommandExtensions {

        public static async Task ExecuteAsync(this ICommand source, object parameter) {
            var asyncCommand = source as IAsyncCommand;
            if (asyncCommand != null)
                await asyncCommand.ExecuteAsync(parameter);

            source.Execute(parameter);
        }

        public static async Task ExecuteAsync<T>(this ICommand<T> source, T parameter) {
            var asyncCommand = source as IAsyncCommand<T>;
            if (asyncCommand != null)
                await asyncCommand.ExecuteAsync(parameter);

            source.Execute(parameter);
        }
    }
}
