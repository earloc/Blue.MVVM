using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Blue.MVVM {
    /// <summary>
    /// base class for notification-proxies
    /// </summary>
  public class NotifyPropertyChangedProxyBase : NotifyPropertyChangedCore {


        private readonly Func<PropertyChangedEventHandler> _HandlerProvider;

        private readonly object _Sender;
        /// <summary>
        /// initializes a new isntance of <see cref="NotifyPropertyChangedProxyBase"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="handlerProvider"></param>
        public NotifyPropertyChangedProxyBase(INotifyPropertyChanged sender, Func<PropertyChangedEventHandler> handlerProvider) : base (sender) {
            if (sender == null)
                throw new ArgumentNullException(nameof(sender));

            _Sender = sender;

            if (handlerProvider == null)
                throw new ArgumentNullException(nameof(handlerProvider));

            _HandlerProvider    = handlerProvider;

            var thisInterceptor         = (IPropertyInterceptor)this;
            thisInterceptor.PostSet    += PostSet;
        }

        private void PostSet(object sender, PropertySetEventArgs e) {
            OnPropertyChanged(e.PropertyName);
        }

        /// <summary>
        /// raises the PropertyChanged event.
        /// </summary>
        /// <typeparam name="T">type of the changed property</typeparam>
        /// <param name="propertyExpression">An expression of the property that has changed</param>
        /// <exception cref="System.ArgumentNullException">propertyExpression</exception>
        protected void          OnPropertyChanged<T>(Expression<Func<T>> propertyExpression) {
            if (propertyExpression == null)
                throw new ArgumentNullException(nameof(propertyExpression));

            OnPropertyChanged(propertyExpression.ExtractMemberName());
        }

        /// <summary>
        /// raises the PropertyChangedevent.
        /// </summary>
        /// <param name="propertyName">Name of the property or <see cref="string.Empty"/>, indicating that all properties have been changed.</param>
        protected void          OnPropertyChanged(string propertyName) {

            _HandlerProvider()?.Invoke(_Sender, new PropertyChangedEventArgs(propertyName));
        }

        [Obsolete("parameter notifyOnChangeOnly is no longer used")]
        public new bool Set<T>(ref T target, T newValue, Expression<Func<T>> propertyExpression, bool notifyOnChangeOnly = false) {
            return base.Set<T>(ref target, newValue, propertyExpression); 
        }

        [Obsolete("parameter notifyOnChangeOnly is no longer used")]
        public new bool Set<T>(ref T target, T newValue, string propertyName, bool notifyOnChangeOnly = false) {
            return base.Set<T>(ref target, newValue, propertyName); 
        }

        public new bool Set<T>(ref T target, T newValue, Expression<Func<T>> propertyExpression) {
            return base.Set<T>(ref target, newValue, propertyExpression); 
        }

        public new bool Set<T>(ref T target, T newValue, string propertyName) {
            return base.Set<T>(ref target, newValue, propertyName); 
        }
    }   
}
