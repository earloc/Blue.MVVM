using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation.Notifications {
    public interface INotifyNavigatingFrom {
        Task OnNavigatedFrom(NavigateFromEventArgs e);
    }

    internal static class INotifyNavigatingFromExtensions {
        internal static async Task TryNotifyNavigatingFrom(this INotifyNavigatingFrom source, NavigateFromEventArgs e) {
            if (source != null) await source.OnNavigatedFrom(e);
        }
    }
}
