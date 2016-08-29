using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace Blue.MVVM.Acceptance.Tests {
    internal class LightVM : ViewModelBase {

        public int IntProperty {
            get {
                return _IntProperty;
            }
            set {
                this.Set(() => IntProperty, ref _IntProperty, value);
            }
        }
        private int _IntProperty = default(int);
    }
}
