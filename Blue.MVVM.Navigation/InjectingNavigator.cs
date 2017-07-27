using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation {
    public abstract class InjectingNavigator<TView> : Navigator<TView> 
        where TView : class {

        public InjectingNavigator(IServiceProvider serviceProvider, IViewTypeResolver viewTypeResolver) {
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider), "must not be null");
            ViewTypeResolver = viewTypeResolver ?? throw new ArgumentNullException(nameof(viewTypeResolver), "must not be null");
        }

        public IServiceProvider ServiceProvider { get; }

        public IViewTypeResolver ViewTypeResolver { get; }


        protected override Task PushAsyncCore<TViewModel>(TViewModel maybeNullViewModel, Func<TViewModel, Task> asyncInitializer) {
            return InvokeAsyncCore(maybeNullViewModel, asyncInitializer, (vm, v) => PushAsync(vm, v));
        }

        protected override Task PushModalAsyncCore<TViewModel>(TViewModel maybeNullViewModel, Func<TViewModel, Task> asyncInitializer) {
            return InvokeAsyncCore(maybeNullViewModel, asyncInitializer, (vm, v) => PushModalAsync(vm, v));
        }
        private async Task InvokeAsyncCore<TViewModel>(TViewModel maybeNullViewModel, Func<TViewModel, Task> asyncInitializer, Func<TViewModel, TView, Task> targetInvocation)
            where TViewModel : class {
            var viewModel = maybeNullViewModel ?? await ServiceProvider.GetAsync(asyncInitializer);

            var viewType = await ViewTypeResolver.GetViewTypeAsync(typeof(TViewModel), viewModel);
            var view = await ServiceProvider.GetAsAsync<TView>(viewType);

            await targetInvocation(viewModel, view);
        }
    }
}
