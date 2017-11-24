using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;
using Blue.MVVM.Tests.Dummies;
using Blue.MVVM;
using System.Collections.Generic;

namespace System.ComponentModel {

    [TestClass]
    public  class NotifyPropertyChangedTests2 : NotifyPropertyChangedTestBase {
        protected override INotifyPropertyChangedDummy CreateUnitUnderTest() {
            return new NotifyPropertyChangedDummy();
        }

        //[TestCategory("Performance")]
        //[TestCategory("Nightly")]
        //[TestMethod]
        //public void PlainPropertyIsMaxXXXTimesFasterThanMinimalisticInterceptableProperty() {
        //    var plain                   = new NotifyPropertyChangedDummy();
        //    var interceptable           = new MinimalisticInterecptableHost();

        //    int count = 0;
        //    var actual = Measure.ExecutionTimeFactor( 
        //        () => plain.PlainProperty = count++,
        //        () => interceptable.MinimalisticInterceptableProperty = count++
        //    );
            

        //    var maximumFactor = 7d;

        //    actual.Factor.AssertNear(maximumFactor, 2);
        //}

        //[TestCategory("Performance")]
        //[TestCategory("Nightly")]
        //[TestMethod]
        //public void PlainPropertyIsMaxXXXTimesFasterThanNotificationOnlyPropertyWithoutHandler() {
        //    var plain                   = new NotifyPropertyChangedDummy();
        //    var dummy                   = new NotificationOnlyDummy();

        //    int count = 0;

        //    var actual = Measure.ExecutionTimeFactor( 
        //        () => plain.PlainProperty = count++,
        //        () => dummy.NotificationOnlyProperty = count++
        //    );
            
        //    var maximumFactor = 7.5d;

        //    actual.Factor.AssertNear(maximumFactor, 2);
        //}

        //[TestCategory("Performance")]
        //[TestCategory("Nightly")]
        //[TestMethod]
        //public void NotificationOnlyPropertyWithEmptyHandlerIsNearlyAsFastAsNotificationOnlyPropertyWithoutHandler() {
        //    var withoutHandler          = new NotificationOnlyDummy();
        //    var withHandler             = new NotificationOnlyDummy();


        //    withHandler.PropertyChanged += (sender, e) => {
        //        ;
        //    };
        //    int count = 0;

        //    var actual = Measure.ExecutionTimeFactor( 
        //        () => withoutHandler.NotificationOnlyProperty = count++,
        //        () => withHandler.NotificationOnlyProperty = count++
        //    );
            
        //    var maximumFactor = 1d;

        //    actual.Factor.AssertNear(maximumFactor, 0.2);
        //}



        [TestCategory("TDD")]
        [TestCategory("Nightly")]
        [TestMethod]
        public void RegisteredPreInterceptorGetsCalled() {
            var wasCalled = false;
            NotifyPropertyChangedCore.RegisterPreInterceptor((sender, e) => wasCalled = true);
            try { 

                var unitUnderTest = new NotificationOnlyDummy();

                unitUnderTest.NotificationOnlyProperty = 42;

                Assert.IsTrue(wasCalled);
            }
            finally {
                NotifyPropertyChangedCore.ClearInterceptors();
            }
        }

        [TestCategory("TDD")]
        [TestCategory("Nightly")]
        [TestMethod]
        public void StaticPreInterceptorsAreCalledBeforeInstancePreInterceptors() {
            var wasCalled = false;
            NotifyPropertyChangedCore.RegisterPreInterceptor((sender, e) => wasCalled = true);
            try { 

                var unitUnderTest = new NotificationOnlyDummy();

                var interceptable = (IPropertyInterceptor)unitUnderTest;

                var staticWasCalledBeforeInstance = false;
                interceptable.PreSet += (sender, e) => staticWasCalledBeforeInstance = wasCalled;

                unitUnderTest.NotificationOnlyProperty = 42;

                Assert.IsTrue(staticWasCalledBeforeInstance);
            }
            finally {
                NotifyPropertyChangedCore.ClearInterceptors();
            }
        }

        [TestCategory("TDD")]
        [TestCategory("Nightly")]
        [TestMethod]
        public void RegisteredPostInterceptorGetsCalled() {
            var wasCalled = false;
            NotifyPropertyChangedCore.RegisterPostInterceptor((sender, e) => wasCalled = true);
            try { 
                var unitUnderTest = new NotificationOnlyDummy();

                unitUnderTest.NotificationOnlyProperty = 42;

                Assert.IsTrue(wasCalled);
            }
            finally {
                NotifyPropertyChangedCore.ClearInterceptors();
            }
        }

        [TestCategory("TDD")]
        [TestCategory("Nightly")]
        [TestMethod]
        public void StaticPostInterceptorsAreCalledBeforeInstancePostInterceptors() {
            var wasCalled = false;
            try { 
                NotifyPropertyChangedCore.RegisterPostInterceptor((sender, e) => wasCalled = true);

                var unitUnderTest = new NotificationOnlyDummy();

                var interceptable = (IPropertyInterceptor)unitUnderTest;

                var staticWasCalledBeforeInstance = false;
                interceptable.PostSet += (sender, e) => staticWasCalledBeforeInstance = wasCalled;

                unitUnderTest.NotificationOnlyProperty = 42;

                Assert.IsTrue(staticWasCalledBeforeInstance);
            }
            finally { 
                NotifyPropertyChangedCore.ClearInterceptors();
            }
        }

        //[TestCategory("Performance")]
        //[TestCategory("Nightly")]
        //[TestMethod]
        //public void NotificationPropertiesAreMaximumXXXTimesSlowerThanPlainOldProperties () {
        //    base.NotificationPropertiesAreMaximumXXXTimesSlowerThanPlainOldProperties(2);
        //}

        //[TestCategory("Performance")]
        //[TestCategory("Nightly")]
        //[TestMethod]
        //public void ChangeAgnosticNotificationPropertiesAreMaxXXXTimesSlowerThanPlainOldProperties () {
        //    base.ChangeAgnosticNotificationPropertiesAreMaxXXXTimesSlowerThanPlainOldProperties(1.5);
        //}

        //[TestCategory("Performance")]
        //[TestCategory("Nightly")]
        //[TestMethod]
        //public void ChangeAwareNotificationPropertiesAreMaxXXXTimesSlowerThanPlainOldProperties () {
        //    base.ChangeAwareNotificationPropertiesAreMaxXXXTimesSlowerThanPlainOldProperties(1.5);
        //}
    }

    [TestClass]
    public class NotifyPropertyChangedProxyTests : NotifyPropertyChangedTestBase {
        protected override INotifyPropertyChangedDummy CreateUnitUnderTest() {
            return new NotifyPropertyChangedDummyUsingProxy();
        }

        //[TestCategory("Performance")]
        //[TestCategory("Nightly")]
        //[TestMethod]
        //public void NotificationPropertiesAreMaximumXXXTimesSlowerThanPlainOldProperties () {
        //    base.NotificationPropertiesAreMaximumXXXTimesSlowerThanPlainOldProperties(1);
        //}

        //[TestCategory("Performance")]
        //[TestCategory("Nightly")]
        //[TestMethod]
        //public void ChangeAgnosticNotificationPropertiesAreMaxXXXTimesSlowerThanPlainOldProperties () {
        //    base.ChangeAgnosticNotificationPropertiesAreMaxXXXTimesSlowerThanPlainOldProperties(1.2);
        //}

        //[TestCategory("Performance")]
        //[TestCategory("Nightly")]
        //[TestMethod]
        //public void ChangeAwareNotificationPropertiesAreMaxXXXTimesSlowerThanPlainOldProperties () {
        //    base.ChangeAwareNotificationPropertiesAreMaxXXXTimesSlowerThanPlainOldProperties(2.1);
        //}
    }

    public abstract class NotifyPropertyChangedTestBase {

        protected abstract INotifyPropertyChangedDummy CreateUnitUnderTest();

        [TestCategory("TDD")]
        [TestCategory("Nightly")]
        [TestMethod]
        public void SetReturnsTrueWhenCalledWithDifferentValue() {

            var dummy           = CreateUnitUnderTest();
            int target          = 0;
            var valueWasChanged = dummy.SetSurrogate(ref target, 42, () => dummy.MyProperty, false);
            dummy.MyProperty    = 42;

            Assert.IsTrue(valueWasChanged);
        }

        //[TestCategory("TDD")]
        //[TestCategory("Nightly")]
        //[TestMethod]
        //public void PropertyExpressionThrowsExceptionrWhenExpressionIsNotAMemberExpression() {

        //    var dummy   = CreateUnitUnderTest();
        //    int target  = -1;

        //    Assert.Catch<ArgumentException>(() => dummy.SetSurrogate(ref target, 42, () => 3, false));
        //    Assert.Catch<ArgumentException>(() => dummy.SetSurrogate(ref target, 42, () => Convert.ToInt32(dummy.ToString()), false));
        //}

        [TestCategory("TDD")]
        [TestCategory("Nightly")]
        [TestMethod]
        public void OnPropertyChangedOfInvokesEventWithCorerctMemberName() {

            var dummy       = CreateUnitUnderTest();

            string actual   = null;

            dummy.PropertyChanged += (sender, e) => actual = e.PropertyName;

            dummy.OnPropertyChangedSurrogate( () => dummy.MyProperty);

            var expected    = "MyProperty";

            Assert.AreEqual(expected, actual);
        }
        
        //[TestCategory("TDD")]
        //[TestCategory("Nightly")]
        //[TestMethod]
        //public void SetWithReferenceTypesNotImplementingIComparerDoesNotThrowException() {
            
        //    var dummy               = CreateUnitUnderTest();

        //    var first               = new ReferenceType();
        //    var second              = new ReferenceType();

        //    Assert.DoesNotThrow( () => dummy.SetSurrogate(ref  first, second, () => dummy.ReferenceProperty, false));
        //}

        [TestCategory("TDD")]
        [TestCategory("Nightly")]
        [TestMethod]
        public void SetWithDifferentReferenceReturnsTrue() {
            
            var dummy               = CreateUnitUnderTest();

            var first               = new ReferenceType();
            var second              = new ReferenceType();

            var actual              = dummy.SetSurrogate(ref  first, second, () => dummy.ReferenceProperty, false);

            Assert.IsTrue(actual);
        }

        //protected void ChangeAwareNotificationPropertiesAreMaxXXXTimesSlowerThanPlainOldProperties (double tolerance) {
        //    var unit = CreateUnitUnderTest();

        //    int count = 0;
        //    var actual          = Measure.ExecutionTimeFactor(
        //        () => unit.PlainProperty = count++, 
        //        () => unit.ChangeAwareNotificationProperty = count++
        //    );

        //    var maximumFactor = 6d;

        //    actual.Factor.AssertNear(maximumFactor, tolerance);
        //}

        //[TestCategory("Performance")]
        //[TestCategory("Nightly")]
        //[TestMethod]
        //public void ChangeAgnosticNotificationPropertiesAreMaxXXXTimesSlowerThanChangeAwareProperties () {
        //    var unit = CreateUnitUnderTest();

        //    int count = 0;
        //    var actual          = Measure.ExecutionTimeFactor(
        //        () => unit.ChangeAgnosticNotificationProperty   = count++, 
        //        () => unit.ChangeAwareNotificationProperty      = count++
        //    );

        //    var maximumFactor = 1d;
        //    actual.Factor.AssertNear(maximumFactor, 0.1);
        //}

        [TestCategory("TDD")]
        [TestCategory("Nightly")]
        [TestMethod]
        public void PreSetInterceptorUsesUnitUnderTestAsSender() {
            var unitUnderTest = CreateUnitUnderTest();

            bool senderIsUnitUnderTest = false;
            NotifyPropertyChangedCore.RegisterPreInterceptor((sender, e) => senderIsUnitUnderTest = object.ReferenceEquals(sender, unitUnderTest));
            try { 

                unitUnderTest.ChangeAwareNotificationProperty = 42;

                Assert.IsTrue(senderIsUnitUnderTest);
            }
            finally {
                NotifyPropertyChangedCore.ClearInterceptors();
            }
        }

        [TestCategory("TDD")]
        [TestCategory("Nightly")]
        [TestMethod]
        public void PostSetInterceptorUsesUnitUnderTestAsSender() {
            var unitUnderTest = CreateUnitUnderTest();

            bool senderIsUnitUnderTest = false;
            NotifyPropertyChangedCore.RegisterPostInterceptor((sender, e) => senderIsUnitUnderTest = object.ReferenceEquals(sender, unitUnderTest));

            try { 
                unitUnderTest.ChangeAwareNotificationProperty = 42;

                Assert.IsTrue(senderIsUnitUnderTest);
            }
            finally {
                NotifyPropertyChangedCore.ClearInterceptors();
            }
        }

        //protected void NotificationPropertiesAreMaximumXXXTimesSlowerThanPlainOldProperties (int tolerance) {
        //    var unit = CreateUnitUnderTest();

        //    int count = 0;
        //    var actual                = Measure.ExecutionTimeFactor( 
        //        () => unit.PlainProperty    = count++, 
        //        () => unit.InlineProperty   = count++
        //    );
            
        //    var maximumFactor = 3d;

        //    actual.Factor.AssertNear(maximumFactor, tolerance);
        //}

        //protected void ChangeAgnosticNotificationPropertiesAreMaxXXXTimesSlowerThanPlainOldProperties (double tolerance) {
        //    var unit = CreateUnitUnderTest();
            
        //    int count = 0;
        //    var actual          = Measure.ExecutionTimeFactor(
        //        () => unit.PlainProperty  = count++, 
        //        () => unit.ChangeAgnosticNotificationProperty = count++
        //    );

        //    var maximumFactor = 7d;
        //    actual.Factor.AssertNear(maximumFactor, tolerance);
        //}
    }
}
