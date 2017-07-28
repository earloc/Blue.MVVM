using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Blue.MVVM.Navigation.Notifications;

namespace Blue.MVVM.Navigation {
    public abstract class Navigator<TView> : INavigator<TView>, IModalNavigator<TView>
        where TView : class {

        private readonly Stack<object> _NavigationStack = new Stack<object>();
        private readonly Stack<object> _ModalStack = new Stack<object>();
        public Task PopAsync() => InvokePopAsync(_NavigationStack, () => PopAsyncCore());
        
        protected abstract Task PopAsyncCore();

        public Task PopModalAsync() => InvokePopAsync(_ModalStack, () => PopModalAsyncCore());
        

        protected abstract Task PopModalAsyncCore();

        public async Task InvokePopAsync(Stack<object> stack, Func<Task> popAction) {
            var source = stack.Pop();
            var target = stack.Peek();

            await NavigateAsync(source, target, popAction);
        }

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
        private Task PushAsyncCore<TViewModel>(TViewModel maybeNullViewModel, Func<TViewModel, Task> asyncInitializer)
            where TViewModel : class => InvokePushAsync(maybeNullViewModel, asyncInitializer, _NavigationStack, (vm, v) => PushAsync(vm, v));

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

        private Task PushModalAsyncCore<TViewModel>(TViewModel maybeNullViewModel, Func<TViewModel, Task> asyncInitializer)
            where TViewModel : class => InvokePushAsync(maybeNullViewModel, asyncInitializer, _ModalStack, (vm, v) => PushModalAsync(vm, v));

        private async Task InvokePushAsync<TViewModel>(TViewModel maybeNullViewModel, Func<TViewModel, Task> asyncInitializer, Stack<object> stack, Func<TViewModel, TView, Task> pushAction)
            where TViewModel : class {
            var target = maybeNullViewModel ?? await ResolveViewModelAsync<TViewModel>();

            if (asyncInitializer != null)
                await asyncInitializer(target);

            var view = await ResolveViewAsync(target);

            var source = stack.Peek();
            await NavigateAsync(source, target, () => pushAction(target, view));
            stack.Push(target);
        }

        private static async Task NavigateAsync(object source, object target, Func<Task> pushOrPop) {
            var sourceEventArgs = new NavigateFromEventArgs(target);
            var navigatingFromSource = source as INotifyNavigatingFrom;
            var navigatedFromSource = source as INotifyNavigatingFrom;

            var targetEventArgs = new NavigateToEventArgs(source);
            var navigatingToTarget = target as INotifyNavigatingTo;
            var navigatedToTarget = target as INotifyNavigatedTo;

            await navigatingFromSource.TryNotifyNavigatingFrom(sourceEventArgs);
            await navigatingToTarget.TryNotifyNavigatingTo(targetEventArgs);

            await pushOrPop();

            await navigatedFromSource.TryNotifyNavigatingFrom(sourceEventArgs);
            await navigatedToTarget.TryINotifyNavigatedTo(targetEventArgs);
        }

        public abstract Task PushModalAsync<TViewModel>(TViewModel viewModel, TView view)
            where TViewModel : class
        ;
        protected abstract Task<TViewModel> ResolveViewModelAsync<TViewModel>()
            where TViewModel : class
        ;

        protected abstract Task<TView> ResolveViewAsync<TViewModel>(TViewModel viewModel)
            where TViewModel : class
        ;
    }

}
