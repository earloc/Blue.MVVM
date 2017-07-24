using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;

namespace Blue.MVVM {

    /// <summary>
    /// base class for propertychange-notification enabled classes
    /// </summary>
    public class NotifyPropertyChanged : NotifyPropertyChangedCore, INotifyPropertyChanged {

        /// <summary>
        /// create a new instance of <see cref="NotifyPropertyChangedBase"/>
        /// </summary>
        public NotifyPropertyChanged() : base(NotifyPropertyChangedCore.Self) {
            var thisInterceptor = (IPropertyInterceptor)this;
            thisInterceptor.PostSet += PostSet;
        }

        private void PostSet(object sender, PropertySetEventArgs e) {
            OnPropertyChanged(e.PropertyName);
        }

        ///// <summary>
        ///// returns true, if the corresponding EqualityComparer determines equality, otherwise false <see cref="EqualityComparer&lt;T&gt;"/>
        ///// maybe overridden when custom logic for determining equality is needed
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="oldValue">the old value that will be overwritten</param>
        ///// <param name="newValue">the new value to be set</param>
        ///// <returns></returns>
        //protected virtual bool AreEqual<T> (T oldValue, T newValue) {
        //    return EqualityComparer<T>.Default.Equals(oldValue, newValue);
        //}

        /// <summary>
        /// raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <typeparam name="T">type of the changed property</typeparam>
        /// <param name="propertyExpression">An expression of the property that has changed</param>
        /// <exception cref="System.ArgumentNullException">propertyExpression</exception>
        protected void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression) {
            if (propertyExpression == null)
                throw new ArgumentNullException(nameof(propertyExpression));

            OnPropertyChanged(propertyExpression.ExtractMemberName());
        }

        /// <summary>
        /// raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">Name of the property or <see cref="string.Empty"/>, indicating that all properties have been changed.</param>
        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Occurs when a property changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected Func<PropertyChangedEventHandler> PropertyChangedHandler => () => PropertyChanged;
    }


}
