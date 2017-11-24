using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Blue.MVVM.Tests {

    [TestClass]
    public class ObjectPipeLineTests {

        private ObjectPipeLine<Guid, Guid> CreateUnitUnderTest() {
            return new ObjectPipeLine<Guid, Guid>();
        }

        [TestCategory("TDD")]
        [TestCategory("Nightly")]
        [TestMethod]
        public void AddingTwoEnterPipesWithDifferentPriorityAreReturnedWithAscendingPriority() {

            var unitUnderTest   = CreateUnitUnderTest();

            var enteringPipeA = Guid.NewGuid();
            var enteringPipeB = Guid.NewGuid();

            unitUnderTest.AddEnterPipe(1, enteringPipeA);
            unitUnderTest.AddEnterPipe(2, enteringPipeB);

            var expectedFirst   = enteringPipeA;

            var actualFirst     = unitUnderTest.EnteringPipeLine.First();

            Assert.AreEqual(expectedFirst, actualFirst);
        }

        [TestCategory("TDD")]
        [TestCategory("Nightly")]
        [TestMethod]
        public void AddingTwoLeavePipesWithDifferentPriorityAreReturnedWithDescendingPriority() {

            var unitUnderTest   = CreateUnitUnderTest();

            var enteringPipeA = Guid.NewGuid();
            var enteringPipeB = Guid.NewGuid();

            unitUnderTest.AddLeavePipe(1, enteringPipeA);
            unitUnderTest.AddLeavePipe(2, enteringPipeB);

            var expectedFirst   = enteringPipeB;

            var actualFirst     = unitUnderTest.LeavingPipeLine.First();

            Assert.AreEqual(expectedFirst, actualFirst);
        }
    }
}
