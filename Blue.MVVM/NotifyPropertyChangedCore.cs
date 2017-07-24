using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;

namespace Blue.MVVM {

    /// <summary>
    /// core infrastructure
    /// </summary>
    public abstract class NotifyPropertyChangedCore : IPropertyInterceptor {

        internal static object Self = new object();

        private readonly IPropertyInterceptor _ThisInterceptor;

        private readonly object _Sender;

        /// <summary>
        /// initializes a new instance of <see cref="NotifyPropertyChangedCore"/>
        /// </summary>
        /// <param name="sender">the sender to be used when firing events</param>
        public NotifyPropertyChangedCore(object sender) {
            _ThisInterceptor    = this;
            _Sender             = sender;
            if (sender == Self)
                _Sender = this;
        }

        /// <summary>
        /// Sets the specified value and triggers all registered pre- or PostInterceptors
        /// </summary>
        /// <typeparam name="T">type of the value to be set</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="newValue">The value to be set</param>
        /// <param name="propertyExpression">An expression of the property that has changed</param>
        /// <returns></returns>
        protected bool          Set<T>(ref T target,  T newValue, Expression<Func<T>> propertyExpression) {

            return Set(ref target, newValue, () => propertyExpression.ExtractMemberName());
        }

        /// <summary>
        /// Sets the specified value and triggers all registered pre- or PostInterceptors
        /// </summary>
        /// <typeparam name="T">type of the value to be set</typeparam>
        /// <param name="target">A reference to the propertie´s backing-field</param>
        /// <param name="newValue">The value to be set</param>
        /// <param name="propertyName">Name of the property, for which the PropertyChanged-event should be raised/></param>
        /// <returns></returns>
        protected bool          Set<T>(ref T target, T newValue, [CallerMemberName] string propertyName = "") {
            var settingEventArgs = new PropertySettingEventArgs<T>(target, newValue, propertyName);

            OnPreSet<T>(settingEventArgs);

            if (settingEventArgs.Cancel)
                return false;

            target = newValue;
            
            OnPostSet(settingEventArgs.ToSetEventArgs());

            return true;
        }

        private bool            Set<T>(ref T target, T newValue, Func<string> propertyNameAccessor) {
            return Set(ref target, newValue, propertyNameAccessor());
        }

        private void            OnPreSet<T>(PropertySettingEventArgs e) {
            var handler = _PreInterceptors + _PreSet;
            handler?.Invoke(_Sender, e);
        }

        private void            OnPostSet(PropertySetEventArgs e) {
            var handler = _PostInterceptors + _PostSet;
            handler?.Invoke(_Sender, e);
        }

        event EventHandler<PropertySettingEventArgs> IPropertyInterceptor.PreSet {
            add     { _PreSet += value; }
            remove  { _PreSet -= value; }
        }
        private EventHandler<PropertySettingEventArgs> _PreSet;

        event EventHandler<PropertySetEventArgs> IPropertyInterceptor.PostSet {
            add     { _PostSet += value; }
            remove  { _PostSet -= value; }
        }
        private EventHandler<PropertySetEventArgs> _PostSet;


        private static ObjectPipeLine<EventHandler<PropertySettingEventArgs>, EventHandler<PropertySetEventArgs>> _Interceptors 
            = new ObjectPipeLine<EventHandler<PropertySettingEventArgs>, EventHandler<PropertySetEventArgs>>();

        /// <summary>
        /// registers an interceptor that gets called before setting properties
        /// </summary>
        /// <param name="preInterceptor">the interceptor</param>
        /// <param name="priority">the priority with which this interceptor will be executed among all other PreInterceptors</param>
        public static void RegisterPreInterceptor (EventHandler<PropertySettingEventArgs> preInterceptor, int priority = 100000) {
            if (preInterceptor == null)
                throw new ArgumentNullException( nameof(preInterceptor));

            _Interceptors.AddEnterPipe(priority, preInterceptor);

            _PreInterceptors = _Interceptors.EnteringPipeLine.Combine();
        }

        private static EventHandler<PropertySettingEventArgs> _PreInterceptors;

        /// <summary>
        /// registers an interceptor that gets called after setting properties
        /// </summary>
        /// <param name="postInterceptor">the interceptor</param>
        /// <param name="priority">the priority with which this interceptor will be executed among all other PostInterceptors</param>
        public static void RegisterPostInterceptor (EventHandler<PropertySetEventArgs> postInterceptor, int priority = 100000) {
            if (postInterceptor == null)
                throw new ArgumentNullException(nameof(postInterceptor));

            _Interceptors.AddLeavePipe(priority, postInterceptor);

            _PostInterceptors = _Interceptors.LeavingPipeLine.Combine();
        }

        private static EventHandler<PropertySetEventArgs> _PostInterceptors;

        internal static void ClearInterceptors () { 
            _Interceptors.Clear();
            _PreInterceptors = null;
            _PostInterceptors = null;
        }
    }
}
