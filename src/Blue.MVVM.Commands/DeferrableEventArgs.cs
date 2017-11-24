using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blue.MVVM.Commands {
    public class DeferrableEventArgs<T> : EventArgs {

        public IDisposable RequestDeferral() {
            var deferral = new TaskCompletionSource<bool>();
            var disposable = new Dispaction(() => deferral.SetResult(true));
            _RequestedDeferrals.Add(deferral.Task);
            return disposable;
        }

        public IEnumerable<Task> PendingDeferrals => _RequestedDeferrals.ToArray();
        private List<Task> _RequestedDeferrals = new List<Task>();


    }
}
