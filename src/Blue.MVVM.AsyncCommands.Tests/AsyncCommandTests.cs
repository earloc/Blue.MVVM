using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blue.MVVM.Commands;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Blue.MVVM.AsyncCommands.Tests {
    [TestClass]
    public class AsyncCommandTests {

        private enum TestEnum {
            A = 512,
            B = 1024
        };

        [TestMethod]
        public void ExecutingAsyncCarriesEnumerationCommandParameterInEventArgs() {

            var command = new AsyncCommand<TestEnum>(async p => {
                await Task.Yield();
            });

            var expected = TestEnum.B;
            var actual = TestEnum.A;

            command.ExecutingAsync += async (sender, e) => {
                using (e.RequestDeferral()) {
                    await Task.Yield();
                    actual = e.CommandParameter;
                }
            };

            command.Execute(expected);


            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RequestedDeferralsAreAwaitedBeforeCoreExecutionIsInvoked() {
            var first = "first";
            var last = "last";

            var collection = new BlockingCollection<string>();

            var command = new AsyncCommand<object>(async p => {
                await Task.Delay(100);
                collection.Add(last);
            });

            command.ExecutingAsync += async (sender, e) => {
                using (e.RequestDeferral()) {
                    await Task.Delay(200);
                    collection.Add(first);
                }
            };

            command.Execute(null);

            var expected = first;
            var actual = collection.Take();

            Assert.AreEqual(expected, actual);
        }
    }
}
