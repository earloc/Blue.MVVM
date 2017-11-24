using Blue.MVVM.Navigation.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation {
    public class NavigationManager {

        public async Task NavigateAsync(object source, object target, Func<Task> pushOrPop) {
            var sourceEventArgs = new NavigateFromEventArgs(target);
            var previewNavigateFromSource = source as INotifyPreviewNavigatedFrom;
            var navigatedFromSource = source as INotifyNavigatedFrom;

            var targetEventArgs = new NavigateToEventArgs(source);
            var previewNavigatingToTarget = target as INotifyPreviewNavigateTo;
            var navigatedToTarget = target as INotifyNavigatedTo;

            previewNavigateFromSource?.OnPreviewNavigateFrom(sourceEventArgs);
            previewNavigatingToTarget?.OnPreviewNavigateTo(targetEventArgs);

            await pushOrPop();

            navigatedToTarget?.OnNavigatedTo(targetEventArgs);
            navigatedFromSource?.OnNavigatedFrom(sourceEventArgs);
        }

    }
}
