using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Net45.Tests {
    public class NotifyPropertyChanged : Blue.MVVM.NotifyPropertyChanged {

        private int _MyProperty;
        public int MyProperty {
            get {
                return _MyProperty;
            }
            set {
                Set(ref _MyProperty, value);
            }
        }
    }

    
}
