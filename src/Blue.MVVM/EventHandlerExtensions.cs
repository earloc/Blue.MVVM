using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System {
    public static class EventHandlerExtensions {
        public static EventHandler<T> Combine<T>(this IEnumerable<EventHandler<T>> source) where T : EventArgs {

            if (source == null || source.Count() == 0)
                return null;

            var combination = source.First();

            foreach (var handler in source.Skip(1))
                combination += handler;

            return combination;
        }
    }
}
