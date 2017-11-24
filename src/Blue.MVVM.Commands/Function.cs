using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Blue.MVVM.Commands {

    public class Function<TIn, TOut> : ICommand<TIn, TOut> {

        private readonly Func<TIn, Task<TOut>> _ExecuteAsync;
        private readonly Func<TIn, bool> _CanExecute;

        public Function(Func<TIn, Task<TOut>> executeAsync, Func<TIn, bool> canExecute = null) {
            _ExecuteAsync = executeAsync ?? throw new ArgumentNullException(nameof(executeAsync), "must not be null");
            _CanExecute = canExecute;

            BlocksRecursions = Command.BlockRecursionByDefault;
        }

        protected static Func<TIn, Task<TOut>> AsAsyncFunc(Action execute) {
            return AsAsyncFunc(p => execute());
        }

        protected static Func<TIn, Task<TOut>> AsAsyncFunc(Action<TIn> execute) {
            if (execute == null)
                return null;

            return p => {
                execute(p);
                return Task.FromResult(default(TOut));
            };
        }

        protected static Func<TIn, bool> AsParameterized(Func<bool> canExecute) {
            if (canExecute == null)
                return null;

            return p => canExecute();
        }

        bool System.Windows.Input.ICommand.CanExecute(object parameter) {
            var p = EnsureParameter(parameter);
            return CanExecute(p);
        }

        public virtual bool CanExecute(TIn parameter) {
            if (_RecursionGuard.ShouldBlock)
                return false;

            if (_CanExecute == null)
                return true;
            return _CanExecute(parameter);

        }

        async void System.Windows.Input.ICommand.Execute(object parameter) {
            var p = EnsureParameter(parameter);
            await ExecuteAsync(p);
        }

        private TIn EnsureParameter(object untyped) {
            if (untyped == null)
                return default(TIn);

            try {
                return (TIn)untyped;
            }
            catch (InvalidCastException ex) {
                var expected = typeof(TIn);
                var actual = untyped.GetType();

                throw new InvalidCastException($"expected parameter of type '{expected.FullName}', but was '{actual.FullName}'", ex);
            }
        }

        public async Task<TOut> ExecuteAsync(TIn p) {
            if (!CanExecute(p))
                return await RecursionBlockedResult;

            using (_RecursionGuard.Enter(out bool isRecursingCall)) {
                if (isRecursingCall && BlocksRecursions)
                    return await RecursionBlockedResult;

                await OnExecutingAsync(p);
                var result = await _ExecuteAsync(p);
                await OnExecutedAsync(p, result);

                return result;
            }
        }
        private Task<TOut> RecursionBlockedResult => Task.FromResult(default(TOut));

        private async Task OnExecutingAsync(TIn p) {
            var e = new ExecutingEventArgs<TIn>(p);
            OnExecuting(e);
            await Task.WhenAll(e.PendingDeferrals);
        }

        private async Task OnExecutedAsync(TIn p, TOut result) {
            var e = new ExecutedEventArgs<TIn, TOut>(p, result);
            OnExecuted(e);
            await Task.WhenAll(e.PendingDeferrals);
        }

        public bool BlocksRecursions {
            get { return _RecursionGuard.IsEnabled; }
            set { _RecursionGuard.IsEnabled = value; }
        }

        private readonly SlimLock _RecursionGuard = new SlimLock();

        public void NotifyCanExecuteChanged() => OnCanExecuteChanged();
        protected void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        public event EventHandler CanExecuteChanged;

        protected void OnExecuting(ExecutingEventArgs<TIn> e) => Executing?.Invoke(this, e);
        public event EventHandler<ExecutingEventArgs<TIn>> Executing;

        protected void OnExecuted(ExecutedEventArgs<TIn, TOut> e) => Executed?.Invoke(this, e);
        public event EventHandler<ExecutedEventArgs<TIn, TOut>> Executed;
    }
}
