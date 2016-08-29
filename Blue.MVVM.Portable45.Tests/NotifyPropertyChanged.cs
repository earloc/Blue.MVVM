using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Net45.Tests {
    public class NotifyPropertyChanged : NotifyPropertyChangedBase {
        protected new bool Set<T>(ref T target, T value, [CallerMemberName] string propertyName = "") {
            return base.Set(ref target, value, propertyName);
        }

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
