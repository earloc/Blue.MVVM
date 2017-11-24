using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Blue.MVVM {

    /// <summary>
    /// eventargs that carry all information of the beginning change of a property
    /// </summary>
    /// <typeparam name="T">the type of the property</typeparam>
    public class PropertySettingEventArgs<T> : PropertySettingEventArgs {
        internal PropertySettingEventArgs(T oldValue, T newValue, string propertyName) 
            : base (oldValue, newValue, propertyName, EqualityComparer<T>.Default) {
        }

        /// <summary>
        /// the old value to be replaced
        /// </summary>
        public new T OldValue {
            get {
                return (T)base.OldValue;
            }
        }

        /// <summary>
        /// the new value to be set
        /// </summary>
        public new T NewValue {
            get {
                return (T)base.NewValue;
            }
        }

        /// <summary>
        /// gets an <see cref="IEqualityComparer"/> that is capable of comparing the old and new value
        /// </summary>
        public new IEqualityComparer<T> Comparer {
            get {
                return (EqualityComparer<T>)base.Comparer;
            }
        }

        internal PropertySetEventArgs ToSetEventArgs() {
            return new PropertySetEventArgs(OldValue, NewValue, PropertyName);
        }
    }

    /// <summary>
    /// EventArgs used before a property has been set
    /// </summary>
    public abstract class PropertySettingEventArgs : EventArgs {

        /// <summary>
        /// Initializes a new instance of <see cref="PropertySettingEventArgs"/>
        /// </summary>
        /// <param name="oldValue">the old value to be replaced</param>
        /// <param name="newValue">the new value to be set</param>
        /// <param name="propertyName">the affected propertyname</param>
        /// <param name="comparer">an equalitycomparer that is capable of comparing old and new value</param>
        protected PropertySettingEventArgs(object oldValue, object newValue, string propertyName, IEqualityComparer comparer) {
            OldValue        = oldValue;
            NewValue        = newValue;

            PropertyName    = propertyName;
            Comparer        = comparer;
        }

        /// <summary>
        /// the old value to be replaced
        /// </summary>
        public object   OldValue        { get; private set; }

        /// <summary>
        /// the new value to be set
        /// </summary>
        public object   NewValue        { get; private set; }

        /// <summary>
        /// name of the target property
        /// </summary>
        public string   PropertyName    {
            get ;
            private set;
        }

        public bool Cancel { get; set; }

        /// <summary>
        /// gets an <see cref="IEqualityComparer"/> that is capable of comparing the old and new value
        /// </summary>
        public IEqualityComparer Comparer {
            get;
            private set;
        }
    }

    /// <summary>
    /// EventArgs used when a proeprty has been set
    /// </summary>
    public class PropertySetEventArgs : EventArgs {

        internal PropertySetEventArgs(object oldValue, object newValue, string propertyName) {
            OldValue            = oldValue;
            NewValue            = newValue;

            PropertyName        = propertyName;
        }

        /// <summary>
        /// the old property value
        /// </summary>
        public object   OldValue        { get; private set; }

        /// <summary>
        /// the new proeprty value
        /// </summary>
        public object   NewValue        { get; private set; }


       /// <summary>
        /// name of the target property
        /// </summary>
        public string   PropertyName    {
            get ;
            private set;
        }
    }
}
