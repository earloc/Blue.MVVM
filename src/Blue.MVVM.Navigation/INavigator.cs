using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation {
    public interface INavigator<TView> : INavigator
    where TView : class {
        Task<TViewModel> PushAsync<TViewModel>(TViewModel viewModel, TView view)
            where TViewModel : class
        ;
    }

    public interface INavigator {

        Task<TViewModel> PushAsync<TViewModel>(Action<TViewModel> initializer)
            where TViewModel : class
        ;
        Task<TViewModel> PushAsync<TViewModel>(Func<TViewModel, Task> asyncInitializer)
            where TViewModel : class
        ;
        Task<TViewModel> PushAsync<TViewModel>(TViewModel viewModel = null)
            where TViewModel : class
        ;

        Task PopAsync();
    }
}
