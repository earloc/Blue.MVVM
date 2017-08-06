using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Commands {
    public class AsyncEventArgs<T> : EventArgs {

        private class AutoCompleteTask : IDisposable {

            private TaskCompletionSource<bool> _Source = new TaskCompletionSource<bool>();

            public Task Task => _Source.Task;

            public void Dispose() {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            public void Dispose(bool disposing) {
                if (disposing)
                    _Source.SetResult(true);
            }
        }

        private List<Task> _Tasks = new List<Task>();

        public IDisposable RequestDeferral () {
            var autoCompleteTask = new AutoCompleteTask();
            _Tasks.Add(autoCompleteTask.Task);
            return autoCompleteTask;
        }

        internal async Task WaitForDeferralsAsync() {
            await Task.Factory.StartNew(() => Task.WaitAll());
        }

        public AsyncEventArgs(T commandParameter) {
            CommandParameter = commandParameter;
        }

        public T CommandParameter { get; }

    }
}
