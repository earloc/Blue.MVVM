using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Blue.MVVM.Commands {

    public class Command<TIn> : Function<TIn, object> {
        public Command(Func<TIn, Task> executeAsync, Func<TIn, bool> canExecute = null) 
            : base(AsAsyncFuncOfT(executeAsync), canExecute) {
        }

        public new Task ExecuteAsync(TIn p) {
            return base.ExecuteAsync(p);
        }

        protected static Func<TIn, Task<object>> AsAsyncFuncOfT(Func<Task> executeAsync) {
            return async p => {
                await executeAsync();
                return null;
            };
        }

        protected static Func<TIn, Task<object>> AsAsyncFuncOfT(Func<TIn, Task> executeAsync) {
            return async p => {
                await executeAsync(p);
                return null;
            };
        }
    }

    /// <summary>
    /// non generic command implementation for passing the execution logic as <see cref="Action"/> / <see cref="System.Func{T}"/>s
    /// </summary>
    public class Command : Command<object> {

        public Command(Action execute, Func<bool> canExecute = null)
            : base(AsAsyncFunc(execute), AsParameterized(canExecute)) {
        }

        public Command(Func<Task> executeAsync, Func<bool> canExecute = null)
            : base(AsAsyncFuncOfT(executeAsync), AsParameterized(canExecute)) {
        }

        public static bool BlockRecursionByDefault { get; set; } = true;
    }
}
