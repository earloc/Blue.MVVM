using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace Blue.MVVM.Acceptance.Tests {
    internal class CaliburnVM : PropertyChangedBase {

        private int _IntProperty40;

        public int IntProperty40 {
            get { return _IntProperty40; }
            set { 
                _IntProperty40 = value; 
                NotifyOfPropertyChange( () => IntProperty40);
            }
        }

        private int _IntProperty45;

        public int IntProperty45 {
            get { return _IntProperty45; }
            set { 
                _IntProperty45 = value; 
                NotifyOfPropertyChange();
            }
        }


    }
}
