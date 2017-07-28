using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation.Notifications {
    public interface INotifyNavigatedTo {
        Task OnNavigatedTo(NavigateToEventArgs e);
    }

    internal static class INotifyNavigatedToExtensions {
        internal static async Task TryINotifyNavigatedTo(this INotifyNavigatedTo source, NavigateToEventArgs e) {
            if (source != null) await source.OnNavigatedTo(e);
        }
    }
}
