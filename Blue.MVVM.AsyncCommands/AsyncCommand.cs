using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Commands {
    public class AsyncCommand : AsyncCommand<object> {
        public AsyncCommand(Func<object, Task> execute, Func<object, bool> canExecute = null) 
            : base (execute, canExecute) {
        }

        public AsyncCommand(Func<Task> execute, Func<bool> canExecute = null) 
            : base(execute, canExecute) { 
        }
    }
}

