using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Commands {
    public abstract class AsyncCommandBase<T> : CommandBase<T>, IAsyncNotificationCommand<T> {

        public async override void Execute(T parameter) {
            await ExecuteAsyncCore(parameter);
        }

        public async Task ExecuteAsyncCore(T parameter) {
            var executingArgs = new AsyncEventArgs<T>(parameter);
            ExecutingAsync?.Invoke(this, executingArgs);
            await executingArgs.WaitForDeferralsAsync();

            await ExecuteAsync(parameter);

            var executedArgs = new AsyncEventArgs<T>(parameter);
            ExecutedAsync?.Invoke(this, executedArgs);
            await executedArgs.WaitForDeferralsAsync();
        }

        public event EventHandler<AsyncEventArgs<T>> ExecutingAsync;
        public event EventHandler<AsyncEventArgs<T>> ExecutedAsync;

        public abstract Task ExecuteAsync(T parameter);
    }
}
