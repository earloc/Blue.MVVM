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
