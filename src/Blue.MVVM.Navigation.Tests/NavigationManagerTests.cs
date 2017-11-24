using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blue.MVVM.Navigation.Notifications;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation.Tests {
    [TestClass]
    public class NavigationManagerTests {

        private class ViewModel : INotifyNavigatedTo, INotifyPreviewNavigateTo, INotifyPreviewNavigatedFrom, INotifyNavigatedFrom {
            void INotifyNavigatedFrom.OnNavigatedFrom(NavigateFromEventArgs e) => OnNavigatedFromCalled = true;
            public bool OnNavigatedFromCalled = false;

            void INotifyNavigatedTo.OnNavigatedTo(NavigateToEventArgs e) => OnNavigatedToCalled = true;
            public bool OnNavigatedToCalled = false;

            void INotifyPreviewNavigatedFrom.OnPreviewNavigateFrom(NavigateFromEventArgs e) => OnPreviewNavigateFromCalled = true;
            public bool OnPreviewNavigateFromCalled = false;

            void INotifyPreviewNavigateTo.OnPreviewNavigateTo(NavigateToEventArgs e) => OnPreviewNavigateToCalled = true;
            public bool OnPreviewNavigateToCalled = false;
        }

        [TestCategory("TDD")]
        [TestMethod]
        public async Task NavigateToViewModelFiresLifeCycleEvent_OnSource_PreviewNavigateFrom() {
            var sut = new NavigationManager();

            var source = new ViewModel();
            var target = new object();

            await sut.NavigateAsync(source, target, async () => await Task.Yield());

            Assert.IsTrue(source.OnPreviewNavigateFromCalled);
        }

        [TestCategory("TDD")]
        [TestMethod]
        public async Task NavigateToViewModelFiresLifeCycleEvent_OnTarget_PreviewNavigateTo() {
            var sut = new NavigationManager();

            var source = new object();
            var target = new ViewModel();

            await sut.NavigateAsync(source, target, async () => await Task.Yield());

            Assert.IsTrue(target.OnPreviewNavigateToCalled);
        }

        [TestCategory("TDD")]
        [TestMethod]
        public async Task NavigateToViewModelFiresLifeCycleEvent_OnTarget_NavigateTo() {
            var sut = new NavigationManager();

            var source = new object();
            var target = new ViewModel();

            await sut.NavigateAsync(source, target, async () => await Task.Yield());

            Assert.IsTrue(target.OnNavigatedToCalled);
        }

        [TestCategory("TDD")]
        [TestMethod]
        public async Task NavigateToViewModelFiresLifeCycleEvent_OnSource_NavigateFrom() {
            var sut = new NavigationManager();

            var source = new ViewModel();
            var target = new object();

            await sut.NavigateAsync(source, target, async () => await Task.Yield());

            Assert.IsTrue(source.OnNavigatedFromCalled);
        }

        
    }
}
