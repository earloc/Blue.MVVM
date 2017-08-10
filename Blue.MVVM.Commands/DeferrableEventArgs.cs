using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blue.MVVM {
    public class DeferrableEventArgs<T> : EventArgs {

        private class Deferral : IDisposable {

            private TaskCompletionSource<bool> _Source = new TaskCompletionSource<bool>();

            public Task Task => _Source.Task;

            public void Dispose() {
                Dispose(true);
            }

            private bool _Disposed;
            public void Dispose(bool disposing) {
                if (_Disposed)
                    return;

                _Disposed = true;

                if (disposing)
                    _Source.SetResult(true);
            }
        }

        private List<Task> _RequestedDeferrals = new List<Task>();

        public IDisposable RequestDeferral () {
            var disposable = new Deferral();
            _RequestedDeferrals.Add(disposable.Task);
            return disposable;
        }

        public IEnumerable<Task> PendingDeferrals => _RequestedDeferrals.ToArray();

        
    }
}
