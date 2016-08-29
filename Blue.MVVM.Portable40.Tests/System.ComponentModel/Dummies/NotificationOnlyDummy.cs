using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Tests.Dummies {
    public class NotificationOnlyDummy : NotifyPropertyChangedBase {


        private int _NotificationOnlyProperty = 0;
        public int NotificationOnlyProperty {
            get {
                return _NotificationOnlyProperty;
            }
            set {
                Set(ref _NotificationOnlyProperty, value, "NotificationOnlyProperty");
            }
        }
    }
}
