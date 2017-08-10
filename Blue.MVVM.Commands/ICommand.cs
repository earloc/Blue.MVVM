using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Blue.MVVM {
    public interface ICommand<TIn, TOut> : ICommand<TIn> {
        Task<TOut> ExecuteAsync(TIn parameter);
    }

    public interface ICommand<TIn> : ICommand {
        //Task ExecuteAsync(TIn parameter);
        bool CanExecute(TIn parameter);
    }
}
