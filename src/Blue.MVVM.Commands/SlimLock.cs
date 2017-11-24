using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Blue.MVVM.Commands {
    internal class SlimLock {

        private int _PendingCalls = 0;

        public IDisposable Enter(out bool shouldBlock) {
            var callers = Interlocked.Increment(ref _PendingCalls);
            shouldBlock = ShouldBlockCore(callers, 1);

            return new Dispaction(() => Leave());
        }

        public IDisposable Enter() {
            return Enter(out bool shouldBlock);
        }

        private void Leave() {
            Interlocked.Decrement(ref _PendingCalls);
        }

        private bool ShouldBlockCore (int pendingCalls, int allowedCalls) {
            return IsEnabled? pendingCalls > allowedCalls : false;
        }

        public bool ShouldBlock => ShouldBlockCore(_PendingCalls, 0);

        public bool IsEnabled { get; internal set; }
    }
}
