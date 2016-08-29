using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Blue.MVVM;

#if Blue_Boot
using Blue.Boot;
#endif

namespace $rootnamespace$ {
    /// <summary>
    /// base class which all Model / ViewModel classes should derive from. 
    /// Having an ultimate base-class between the package's classes and your custom classes is a best practice that gives you the freedom of  
    /// e.g. adding some mising convenience methods, etc.
    /// The partial modifier is used to support future (optional) extensions to Blue.MVVM
    /// In order to ensure that future releases of Blue.MVVM do not overwrite your custom logic, place custom members and logic in a seperate partial class file
    /// </summary>
    public partial class NotifyPropertyChanged : NotifyPropertyChangedBase {
        static NotifyPropertyChanged() {

#if Blue_Boot
				Boot.Strap();
#endif

            InitializeType();
        }

        static partial void InitializeType();

        //dont place custom code here, as this "could" be possibly overwritten in future updates
        //place custom code in a seperate file, e.g.: NotifyPropertyChanged.cs and extend the partial class there
    }

    public partial class NotifyPropertyChangedProxy : NotifyPropertyChangedProxyBase {
        static NotifyPropertyChangedProxy() {

#if Blue_Boot
				Boot.Strap();
#endif

            InitializeType();
        }

        static partial void InitializeType();

        //dont place custom code here, as this "could" be possibly overwritten in future updates
        //place custom code in a seperate file, e.g.: NotifyPropertyChanged.cs and extend the partial class there

        public NotifyPropertyChangedProxy(INotifyPropertyChanged sender, Func<PropertyChangedEventHandler> handlerProvider)
            : base(sender, handlerProvider) {
        }
    }

    public interface INotifyPropertyChangedViaProxy : INotifyPropertyChanged {
        NotifyPropertyChangedProxy NotificationProxy { get; }
    }

    public static class INotifyPropertyChangedViaProxyExtensions {
        [Obsolete("notifyOnChangeOnly is no longer be used")]
        public static bool Set<T>(this INotifyPropertyChangedViaProxy instance, ref T target, T value, bool notifyOnChangeOnly, Expression<Func<T>> propertyExpression) {
            return Set<T>(instance, ref target, value, propertyExpression);
        }

        public static bool Set<T>(this INotifyPropertyChangedViaProxy instance, ref T target, T value, Expression<Func<T>> propertyExpression) {
            if (instance == null)
                throw new ArgumentNullException("instance");
            return instance.NotificationProxy.Set(ref target, value, propertyExpression);
        }
    }
}
