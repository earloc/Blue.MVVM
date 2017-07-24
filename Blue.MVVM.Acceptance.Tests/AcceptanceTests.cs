using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blue.MVVM.Acceptance.Tests {
    [TestClass]
    public class AcceptanceTests {
        [TestCategory("Acceptance")]
        [TestCategory("Nightly")]
        [TestMethod]
        public void UsingPropertyExpressionIsFasterThanMVVMLight_NoHandler() {
            var blueVM          = new BlueVM();
            var competitorVM    = new LightVM();

            var count           = 0;
            var actual          = Measure.ExecutionTimeFactor( () => blueVM.IntProperty40 = count++, () => competitorVM.IntProperty = count++);

            Assert.IsTrue(actual.IsFaster(), string.Format("[{0}] is not faster than [{1}]", actual.MinExecutionTimeFaster, actual.MinExecutionTimeSlower));
        }

        [TestCategory("Acceptance")]
        [TestCategory("Nightly")]
        [TestMethod]
        public void UsingCallerMemberNameIsFasterThanMVVMLight_NoHandler() {
            var blueVM          = new BlueVM();
            var competitorVM    = new LightVM();

            var count           = 0;
            var actual          = Measure.ExecutionTimeFactor( () => blueVM.IntProperty45 = count++, () => competitorVM.IntProperty = count++);

            var minimumFactor   = 10;

            Assert.IsTrue(actual.IsFaster(), (string.Format("[{0}] is not faster than [{1}] by factor [{2}]", actual.MinExecutionTimeFaster, actual.MinExecutionTimeSlower, minimumFactor)));
        }

        [TestCategory("Acceptance")]
        [TestCategory("Nightly")]
        [TestMethod]
        public void UsingPropertyExpressionIsFasterThanCaliburnMicro_NoHandler() {
            var blueVM          = new BlueVM();
            var competitorVM    = new CaliburnVM();

            var count           = 0;
            var actual          = Measure.ExecutionTimeFactor( () => blueVM.IntProperty40 = count++, () => competitorVM.IntProperty40 = count++);

            Assert.IsTrue(actual.IsFaster(), string.Format("[{0}] is not faster than [{1}]", actual.MinExecutionTimeFaster, actual.MinExecutionTimeSlower));
        }

        [TestCategory("Acceptance")]
        [TestCategory("Nightly")]
        [TestMethod]
        public void UsingCallerMemberNameIsFasterThanCaliburnMicro40_NoHandler() {
            var blueVM          = new BlueVM();
            var competitorVM    = new CaliburnVM();

            var count           = 0;
            var actual          = Measure.ExecutionTimeFactor( () => blueVM.IntProperty45 = count++, () => competitorVM.IntProperty40 = count++);

            var minimumFactor   = 8;

            Assert.IsTrue(actual.IsFasterBy(minimumFactor), (string.Format("[{0}] is not faster than [{1}] by factor [{2}]", actual.MinExecutionTimeFaster, actual.MinExecutionTimeSlower, minimumFactor)));
        }

        [TestCategory("Acceptance")]
        [TestCategory("Nightly")]
        [TestCategory("IndicatesPerformanceBoost")]
        [TestMethod]
        public void UsingCallerMemberNameIsSlowerThanCaliburnMicro45_NoHandler() {
            var blueVM          = new BlueVM();
            var competitorVM    = new CaliburnVM();

            var count           = 0;
            var actual          = Measure.ExecutionTimeFactor( () => competitorVM.IntProperty45 = count++, () => blueVM.IntProperty45 = count++);

            Assert.IsTrue(actual.IsFaster());
            
        }
    }
}
