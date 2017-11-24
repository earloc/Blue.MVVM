using System;

namespace Blue.MVVM.Navigation.Notifications {
    public class NavigateToEventArgs : NavigationEventArgs {
        public NavigateToEventArgs(object source) {
            Source = source;
        }
        public object Source { get; }
    }
}
