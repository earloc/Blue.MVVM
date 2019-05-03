using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Blue.MVVM.Commands
{

    public class Function<TIn, TOut> : FunctionBase<TIn, TOut> {

        private readonly Func<TIn, Task<TOut>> _ExecuteAsync;
        private readonly Func<TIn, bool> _CanExecute;

        public Function(Func<TIn, Task<TOut>> executeAsync, Func<TIn, bool> canExecute = null) {
            _ExecuteAsync = executeAsync ?? throw new ArgumentNullException(nameof(executeAsync), "must not be null");
            _CanExecute = canExecute;
        }

        protected override bool OnCanExecute(TIn parameter)
        {
            if (_CanExecute == null)
                return true;
            return _CanExecute(parameter);
        }

        protected override Task<TOut> OnExecuteAsync(TIn parameter) => _ExecuteAsync(parameter);
    }
}
