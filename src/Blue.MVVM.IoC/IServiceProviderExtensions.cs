using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System {

    /// <summary>
    /// Extensions for IServiceLocator
    /// </summary>
    public static class IServiceProviderExtensions {

        /// <summary>
        /// resolves T and executes constructionConfig on the resolved instance prior it is returned. This can be used as an async, DI - aware constructor equivalent
        /// </summary>
        /// <typeparam name="T">the type to be resolved</typeparam>
        /// <param name="source">the discrete IServiceLocator implementation that actually resolves type T</param>
        /// <param name="asyncInitializer">the async 'initialization' logic that should be executed directly after resolving the instance of type T</param>
        /// <returns></returns>
        public static Task<T> GetAsync<T>(this IServiceProvider source, Func<T, Task> asyncInitializer = null) where T : class {
            if (source == null)
                throw new ArgumentNullException(nameof(source), "must not be null");

            return source.GetAsAsync<T>(typeof(T));
        }

        public static async Task<T> GetAsAsync<T>(this IServiceProvider source, Type type, Func<T, Task> asyncInitializer = null) where T : class {
            if (source == null)
                throw new ArgumentNullException(nameof(source), "must not be null");

            var instance = source.GetAs<T>(type);

            return await InitializeAsync(instance, asyncInitializer);
        }

        public static T GetAs<T>(this IServiceProvider source, Type type, bool allownull = false) where T : class {
            if (source == null)
                throw new ArgumentNullException(nameof(source), "must not be null");

            var instance = source.GetService(type);

            if (instance == null) {
                if (allownull)
                    return default(T);
                throw new Exception($"ServiceProvider was unable to create an instance of type {type.FullName}");
            }

            var typedInstance = instance as T;

            if (typedInstance == null)
                throw new Exception($"Invalid instance, expected '{typeof(T).FullName}', but was '{instance.GetType().FullName} ({instance.ToString()})'");

            return typedInstance;
        }

        private static async Task<T> InitializeAsync<T>(T instance, Func<T, Task> asyncInitializer) {
            if (asyncInitializer != null)
                await asyncInitializer(instance);
            return instance;
        }

    }
}
