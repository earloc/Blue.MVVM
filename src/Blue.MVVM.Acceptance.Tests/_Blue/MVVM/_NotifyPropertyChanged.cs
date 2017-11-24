using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Blue.MVVM;

#if Blue_Boot
using Blue.Boot;
#endif

namespace Blue.MVVM.Acceptance.Tests {
	/// <summary>
    /// base class which all Model / ViewModel classes should derive from. 
    /// Having an ultimate base-class between the package's classes and your custom classes is a best practice that gives you the freedom of  
    /// e.g. adding some mising convenience methods, etc.
    /// The partial modifier is used to support future (optional) extensions to Blue.MVVM
    /// In order to ensure that future releases of Blue.MVVM do not overwrite your custom logic, place custom members and logic in a seperate partial class file
    /// </summary>
    public partial class NotifyPropertyChanged : NotifyPropertyChangedBase {

		//dont place custom code here, as this "could" be possibly overwritten in future updates
        //place custom code in a seperate file, e.g.: NotifyPropertyChanged.cs and extend the partial class there

		static NotifyPropertyChanged() {
			
			#if Blue_Boot
				Boot.Strap();
			#endif

			InitializeType();
		}

		static partial void InitializeType();

		[Obsolete("notifyOnChangeOnly is no longer be used")]
        protected bool Set<T>(ref T target, T value, bool notifyOnChangeOnly, [CallerMemberName] string propertyName = "") {
            return Set(ref target, value, propertyName);
        }

		protected new bool Set<T>(ref T target, T value, [CallerMemberName] string propertyName = "") {
            return base.Set(ref target, value, propertyName);
        }

        protected new void OnPropertyChanged([CallerMemberName] string propertyName = "") {
            base.OnPropertyChanged(propertyName);
        }
    }

	public partial class NotifyPropertyChangedProxy : NotifyPropertyChangedProxyBase {

		//dont place custom code here, as this "could" be possibly overwritten in future updates
        //place custom code in a seperate file, e.g.: NotifyPropertyChanged.cs and extend the partial class there

		static NotifyPropertyChangedProxy() {
			
			#if Blue_Boot
				Boot.Strap();
			#endif

			InitializeType();
		}

		static partial void InitializeType();

        public NotifyPropertyChangedProxy(INotifyPropertyChanged sender, Func<PropertyChangedEventHandler> handlerProvider) 
            : base (sender, handlerProvider) {
        }

        [Obsolete("notifyOnChangeOnly is no longer be used")]
		protected bool Set<T>(ref T target, T value, bool notifyOnChangeOnly, [CallerMemberName] string propertyName = "") {
            return Set(ref target, value, propertyName);
        }

        protected new bool Set<T>(ref T target, T value, [CallerMemberName] string propertyName = "") {
            return base.Set(ref target, value, propertyName);
        }

        protected new void OnPropertyChanged([CallerMemberName] string propertyName = "") {
            base.OnPropertyChanged(propertyName);
        }
    }

	public interface INotifyPropertyChangedViaProxy : INotifyPropertyChanged {
        NotifyPropertyChangedProxy NotificationProxy { get; }
    }

	public static class INotifyPropertyChangedViaProxyExtensions {
		[Obsolete("notifyOnChangeOnly is no longer be used")]
        public static bool Set<T>(this INotifyPropertyChangedViaProxy instance, ref T target, T value, bool notifyOnChangeOnly, [CallerMemberName] string propertyName = "") {
			if (instance == null)
				throw new ArgumentNullException("instance");

            return instance.NotificationProxy.Set(ref target, value, propertyName);
        }

		public static bool Set<T>(this INotifyPropertyChangedViaProxy instance, ref T target, T value, [CallerMemberName] string propertyName = "") {
			if (instance == null)
				throw new ArgumentNullException("instance");

            return instance.NotificationProxy.Set(ref target, value, propertyName);
        }
    }
}
