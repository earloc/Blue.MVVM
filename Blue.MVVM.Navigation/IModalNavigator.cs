using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation {
    public interface IModalNavigator<TView> : IModalNavigator
        where TView : class {
        Task PushModalAsync<TViewModel>(TViewModel viewModel, TView view)
            where TViewModel : class
        ;
    }

    public interface IModalNavigator {

        Task PushModalAsync<TViewModel>(Action<TViewModel> initializer)
            where TViewModel : class
        ;
        Task PushModalAsync<TViewModel>(Func<TViewModel, Task> asyncInitializer)
            where TViewModel : class
        ;
        Task PushModalAsync<TViewModel>(TViewModel viewModel = null)
            where TViewModel : class
        ;

        Task PopModalAsync();
    }
}
