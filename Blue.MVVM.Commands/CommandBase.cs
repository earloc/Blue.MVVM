using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Blue.MVVM.Commands {
    /// <summary>
    /// non-generic command implementation. Logic for Execute and CanExecute has to be implemented in derived types
    /// </summary>
    public abstract class CommandBase : CommandBase<object> {

        /// <summary>
        /// gets or sets a value indicating wheter newly created Commands support reentrany on execution. The default value is false
        /// </summary>
        public static bool IsReentranceEnabledByDefault { get; set; }

        static CommandBase() {
            IsReentranceEnabledByDefault = false;
        }

    }
}
