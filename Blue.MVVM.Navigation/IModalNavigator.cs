using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation {
    public interface IModalNavigator {
        Task PushModalAsync<TViewModel>(Func<TViewModel, Task> asyncConfig, bool? animated = null) where TViewModel : class;
        Task PushModalAsync<TViewModel>(Action<TViewModel> config, bool? animated = null) where TViewModel : class;
        Task PushModalAsync<TViewModel>(TViewModel viewModel, bool? animated = null) where TViewModel : class;
        Task PushModalAsync<TViewModel>(bool? animated = null) where TViewModel : class;

        Task PopModalAsync(bool? animated = null);
    }
}
