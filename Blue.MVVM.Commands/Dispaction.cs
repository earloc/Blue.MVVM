using System;
using System.Collections.Generic;
using System.Text;

namespace Blue.MVVM.Commands {
    public class Dispaction : IDisposable {

        private readonly Action _OnDisposing;
        public Dispaction (Action onDisposing) {
            _OnDisposing = onDisposing ?? throw new ArgumentNullException(nameof(onDisposing), "must not be null");
        }

        public void Dispose() {
            Dispose(true);
        }

        private bool _Disposed;
        public void Dispose(bool disposing) {
            if (_Disposed)
                return;

            _Disposed = true;

            if (disposing)
                _OnDisposing();
        }
    }
}
