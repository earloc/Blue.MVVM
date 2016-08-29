using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Blue.MVVM {
    internal interface IHandlerProvider {
        void ProvideHandler(Func<PropertyChangedEventHandler> func);
        void ProvideSender(Func<INotifyPropertyChanged> func);
    }
}
