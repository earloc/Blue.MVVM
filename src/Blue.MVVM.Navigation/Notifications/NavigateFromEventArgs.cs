using System;

namespace Blue.MVVM.Navigation.Notifications {
    public class NavigateFromEventArgs : NavigationEventArgs {
        public NavigateFromEventArgs(object target) {
            Target = target;
        }
        public object Target { get; }
    }
}
