using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Tests.Dummies {
    internal class NotifyPropertyChangedDummy : NotifyPropertyChangedBase, INotifyPropertyChangedDummy {

        public bool SetSurrogate<T>(ref T target, T value, Expression<Func<T>> propertyExpression, bool notifyOnChangeOnly) {
            return Set(ref target, value, propertyExpression);
        }

        public void OnPropertyChangedSurrogate<T>(Expression<Func<T>> propertyExpression) {
            OnPropertyChanged(propertyExpression);
        }

        public int MyProperty {
            get;
            set;
        }

        public NotifyPropertyChangedDummy() {
        }

        public ReferenceType ReferenceProperty { get; set; }


        public int PlainProperty { get; set; }


        public int ChangeAgnosticNotificationProperty {
            get {
                return _ChangeAgnosticNotificationProperty;
            }
            set {
                this.Set(ref _ChangeAgnosticNotificationProperty, value, "ChangeAgnosticNotificationProperty");
            }
        }
        private int _ChangeAgnosticNotificationProperty = default(int);


        public int ChangeAwareNotificationProperty {
            get {
                return _ChangeAwareNotificationProperty;
            }
            set {
                this.Set(ref _ChangeAwareNotificationProperty, value, "ChangeAgnosticNotificationProperty");//, true);
            }
        }
        private int _ChangeAwareNotificationProperty = default(int);

        private int _InlineProperty;

        public int InlineProperty {
            get { return _InlineProperty; }
            set { 
                _InlineProperty = value; 
                OnPropertyChanged("InlineProperty");
            }
        }

    }
}
