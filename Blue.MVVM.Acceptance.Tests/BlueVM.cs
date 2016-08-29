using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Acceptance.Tests {
    internal class BlueVM : NotifyPropertyChanged {

        public int IntProperty45 {
            get {
                return _IntProperty45;
            }
            set {
                this.Set(ref _IntProperty45, value);
            }
        }
        private int _IntProperty45 = default(int);

        public int IntProperty40 {
            get {
                return _IntProperty40;
            }
            set {
                this.Set(ref _IntProperty40, value, () => IntProperty40);
            }
        }
        private int _IntProperty40 = default(int);
    }
}
