using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Tests.Dummies {
    public class MinimalisticInterecptableHost : NotifyPropertyChangedCore {

        public MinimalisticInterecptableHost() : base (NotifyPropertyChangedCore.Self) {

        }

        private int _MinimalisticInterceptableProperty = 0;
        public int MinimalisticInterceptableProperty {
            get {
                return _MinimalisticInterceptableProperty;
            }
            set {
                Set(ref _MinimalisticInterceptableProperty, value, "MinimalisticInterceptableProperty");
            }
        }

        private object _Reference = null;
        public object Reference {
            get {
                return _Reference;
            }
            set {
                Set(ref _Reference, value, "Reference");
            }
        }
    }
}
