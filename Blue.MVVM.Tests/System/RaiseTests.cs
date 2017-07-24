using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace Blue.MVVM.Portable40.Tests.System {
    public class DummyEventArgs : EventArgs {
      public int Value {
            get;
            set;
        }
    }

    [TestClass]
    public class RaiseTests {

        private class EventClass {
            public event EventHandler Event;
            public event EventHandler<DummyEventArgs> DummyEvent;
            public event PropertyChangedEventHandler PropertyChanged;

            public event Action IncompatibleEvent;

            public void RaiseEvent() {
                Raise.Event(this, Event);
            }

            public void RaiseEvent<T>(T e) where T : EventArgs {
                Raise.Event(this, Event, e);
            }

            public void RaiseDummyEvent(DummyEventArgs e) {
                Raise.Delegate(this, DummyEvent, e);
            }

            public void RaiseIncompatibleEvent(DummyEventArgs e) {
                Raise.Delegate(this, IncompatibleEvent, e);
            }

            public void RaisePropertyChangedEvent(string propertyName) {
                Raise.Event(this, PropertyChanged, propertyName);
            }
        }

        

        [TestCategory("TDD")]
        [TestCategory("Nightly")]
        [TestMethod]
        public void EventInvokesAllAttachedHandlers() {
            var dummy = new EventClass();

            var actual = 0;

            for (int i = 0; i < 10; i++ )
                dummy.Event += (sender, e) => actual++;

            dummy.RaiseEvent();

            var expected = 10;

            Assert.AreEqual(expected, actual);
        }

        [TestCategory("TDD")]
        [TestCategory("Nightly")]
        [TestMethod]
        public void EventInjectsEmptyEventArgs() {
            var dummy = new EventClass();

            var isEmptyEventArgs = false;

            dummy.Event += (sender, e) => isEmptyEventArgs = e == EventArgs.Empty;

            dummy.RaiseEvent();

            Assert.IsTrue(isEmptyEventArgs);
        }

        [TestCategory("TDD")]
        [TestCategory("Nightly")]
        [TestMethod]
        public void EventInjectsCustomEventArgs() {
            var dummy = new EventClass();

            var isCustomEventArgs = false;

            dummy.Event += (sender, e) => isCustomEventArgs = e is DummyEventArgs;

            dummy.RaiseEvent(new DummyEventArgs());

            Assert.IsTrue(isCustomEventArgs);
        }

        [TestCategory("TDD")]
        [TestCategory("Nightly")]
        [TestMethod]
        public void DelegateInjectsCustomEventArgsWithGenericEventHandler() {
            var dummy = new EventClass();

            var isCustomEventArgs = false;

            dummy.Event += (sender, e) => isCustomEventArgs = e is DummyEventArgs;

            dummy.RaiseEvent(new DummyEventArgs());

            Assert.IsTrue(isCustomEventArgs);
        }

        //[TestCategory("TDD")]
        //[TestCategory("Nightly")]
        //[TestMethod]
        //public void DelegateThrowsExceptionWhenInvokedDelegteIsNotWellFormedEventHandler() {
        //    var dummy = new EventClass();

        //    dummy.IncompatibleEvent += () => {
        //        ;
        //    };

        //    Assert.Catch<TargetParameterCountException> ( () => dummy.RaiseIncompatibleEvent(new DummyEventArgs()));

        //}
    }
}
