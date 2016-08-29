using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Tests.Dummies {
    internal class NotifyPropertyChangedProxyDummy : NotifyPropertyChangedProxyBase {
        public void OnPropertyChangedSurrogate<T> (Expression<Func<T>> propertyExpression) {
            base.OnPropertyChanged(propertyExpression);
        }

        public NotifyPropertyChangedProxyDummy(INotifyPropertyChanged sender, Func<PropertyChangedEventHandler> handlerProvider)
            : base(sender, handlerProvider) {
        }
    }

    internal class NotifyPropertyChangedDummyUsingProxy : INotifyPropertyChangedDummy {

        public event PropertyChangedEventHandler PropertyChanged;

        private int _MyProperty;
        public int MyProperty {
            get {
                return _MyProperty;
            }
            set {
                _Proxy.Set(ref _MyProperty, value, () => MyProperty);
            }
        }

        public void OnPropertyChangedSurrogate<T>(Expression<Func<T>> propertyExpression) {
            _Proxy.OnPropertyChangedSurrogate(propertyExpression);
        }

        private ReferenceType _ReferenceProperty;
        public ReferenceType ReferenceProperty {
            get {
                return _ReferenceProperty;
            }
            set {
                _Proxy.Set(ref _ReferenceProperty, value, () => ReferenceProperty);
            }
        }

        public bool SetSurrogate<T>(ref T target, T value, Expression<Func<T>> propertyExpression, bool notifyOnChangeOnly) {
            return _Proxy.Set(ref target, value, propertyExpression);
        }

        public NotifyPropertyChangedDummyUsingProxy() {
            _Proxy = new NotifyPropertyChangedProxyDummy(this, () => PropertyChanged);
        }

        private readonly NotifyPropertyChangedProxyDummy _Proxy;



        public int PlainProperty { get; set; }


        public int ChangeAgnosticNotificationProperty {
            get {
                return _ChangeAgnosticNotificationProperty;
            }
            set {
                _Proxy.Set(ref _ChangeAgnosticNotificationProperty, value, "ChangeAgnosticNotificationProperty");
            }
        }
        private int _ChangeAgnosticNotificationProperty = default(int);


        public int ChangeAwareNotificationProperty {
            get {
                return _ChangeAwareNotificationProperty;
            }
            set {
                _Proxy.Set(ref _ChangeAwareNotificationProperty, value, "ChangeAgnosticNotificationProperty"); //, true);
            }
        }
        private int _ChangeAwareNotificationProperty = default(int);

        private int _InlineProperty;

        public int InlineProperty {
            get { return _InlineProperty; }
            set { 
                _InlineProperty = value; 

                OnProeprtyChanged ( () => this, () => PropertyChanged, "InlineProperty");

            }
        }

        private void OnProeprtyChanged(Func<object> getSender, Func<PropertyChangedEventHandler> getHandler, string name) {
            Raise.Event(getSender(), getHandler(), name );
        }
    }
}
