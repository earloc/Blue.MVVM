using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Commands {
    public class AsyncCommand<T> : AsyncCommandBase<T> {


        public AsyncCommand(Func<T, Task> execute, Func<T, bool> canExecute = null) {
            _Execute = execute ?? throw new ArgumentNullException(nameof(execute), "must not be null");
            _CanExecute = canExecute;

        }

        public AsyncCommand(Func<Task> execute, Func<bool> canExecute = null) {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute), "must not be null");

            _Execute = new Func<T, Task>(async p => await execute());

            if (canExecute != null)
                _CanExecute = new Func<T, bool>(p => canExecute());
        }

        public override async Task ExecuteAsync(T parameter) {
            await _Execute(parameter);
        }

        public override bool CanExecute(T parameter) {
            if (_CanExecute == null)
                return base.CanExecute(parameter);
            return _CanExecute(parameter);
        }

        private readonly Func<T, Task> _Execute;
        private readonly Func<T, bool> _CanExecute;
    }
}
