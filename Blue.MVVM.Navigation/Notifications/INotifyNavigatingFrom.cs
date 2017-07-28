using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation.Notifications {
    public interface INotifyNavigatingFrom {
        void OnNavigatedFrom(NavigateFromEventArgs e);
    }
}
