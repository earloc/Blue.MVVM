using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation.Notifications {
    public interface INotifyNavigatingTo {
        Task OnNavigatingTo(NavigateToEventArgs e);
    }

    internal static class INotifyNavigatingToExtensions {
        internal static async Task TryNotifyNavigatingTo(this INotifyNavigatingTo source, NavigateToEventArgs e) {
            if (source != null) await source.OnNavigatingTo(e);
        }
    }
}
