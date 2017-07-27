using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation {
    public interface INavigator<TView> : INavigator
    where TView : class {
        Task PushAsync<TViewModel>(TViewModel viewModel, TView view)
            where TViewModel : class
        ;
    }

    public interface INavigator {

        Task PushAsync<TViewModel>(Action<TViewModel> initializer)
            where TViewModel : class
        ;
        Task PushAsync<TViewModel>(Func<TViewModel, Task> asyncInitializer)
            where TViewModel : class
        ;

        Task PushAsync<TViewModel>(TViewModel viewModel = null)
            where TViewModel : class
        ;

        Task PopAsync();
    }
}
