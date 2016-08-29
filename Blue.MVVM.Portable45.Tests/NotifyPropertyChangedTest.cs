using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blue.MVVM.Net45.Tests {
    [TestClass]
    public class NotifyPropertyChangedTests {

        [TestCategory("TDD")]
        [TestCategory("Nightly")]
        [TestMethod]
        public void SetWithAutoFillParameterYieldsCorrectPropertyName() {
            var instance = new NotifyPropertyChanged();

            string actualName = null;

            instance.PropertyChanged += (sender, e) => actualName = e.PropertyName;

            instance.MyProperty = 42;

            string expectedName = "MyProperty";

            Assert.AreEqual(expectedName, actualName);
        }
    }
}
