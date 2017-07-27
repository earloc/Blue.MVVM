using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation {
    public abstract class Navigator<TView> : INavigator<TView>, IModalNavigator<TView>
        where TView : class {

        public abstract Task PopAsync();

        public abstract Task PopModalAsync();

        public Task PushAsync<TViewModel>(Action<TViewModel> initializer)
            where TViewModel : class {
            if (initializer == null)
                throw new ArgumentNullException(nameof(initializer), "must not be null");

            return PushAsync<TViewModel>(async x => {
                await Task.Yield();
                initializer(x);
            });
        }

        public Task PushAsync<TViewModel>(Func<TViewModel, Task> asyncInitializer)
           where TViewModel : class {
            if (asyncInitializer == null)
                throw new ArgumentNullException(nameof(asyncInitializer), "must not be null");

            return PushAsyncCore(null, asyncInitializer);
        }

        public Task PushAsync<TViewModel>(TViewModel viewModel = null)
            where TViewModel : class {
            return PushAsyncCore(viewModel, null);
        }

        public abstract Task PushAsync<TViewModel>(TViewModel viewModel, TView view)
            where TViewModel : class
        ;
        protected abstract Task PushAsyncCore<TViewModel>(TViewModel maybeNullViewModel, Func<TViewModel, Task> asyncInitializer)
            where TViewModel : class
        ;

        public Task PushModalAsync<TViewModel>(Action<TViewModel> initializer) where TViewModel : class {
            if (initializer == null)
                throw new ArgumentNullException(nameof(initializer), "must not be null");

            return PushModalAsync<TViewModel>(async x => {
                await Task.Yield();
                initializer(x);
            });
        }

        public Task PushModalAsync<TViewModel>(Func<TViewModel, Task> asyncInitializer) where TViewModel : class {
            if (asyncInitializer == null)
                throw new ArgumentNullException(nameof(asyncInitializer), "must not be null");

            return PushModalAsyncCore(null, asyncInitializer);
        }

        public Task PushModalAsync<TViewModel>(TViewModel viewModel = null) where TViewModel : class {
            return PushModalAsyncCore(viewModel, null);
        }

        protected abstract Task PushModalAsyncCore<TViewModel>(TViewModel maybeNullViewModel, Func<TViewModel, Task> asyncInitializer)
            where TViewModel : class
        ;

        public abstract Task PushModalAsync<TViewModel>(TViewModel viewModel, TView view)
            where TViewModel : class
        ;



        
    }

}
