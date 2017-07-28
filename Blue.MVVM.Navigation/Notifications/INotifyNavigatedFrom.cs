using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation.Notifications {
    public interface INotifyNavigatedFrom {
        Task OnNavigatedFrom(NavigateFromEventArgs e);
    }

    internal static class INotifyNavigatedFromExtensions {
        internal static async Task TryNotifyNavigatedFrom(this INotifyNavigatedFrom source, NavigateFromEventArgs e) {
            if (source != null) await source.OnNavigatedFrom(e);
        }
    }
}
