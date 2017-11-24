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
        public async Task ExecutingAsyncCarriesEnumerationCommandParameterInEventArgs() {

            var command = new Command<TestEnum>((x) => Task.FromResult(true));

            var expected = TestEnum.B;
            var actual = TestEnum.A;

            command.Executing += async (sender, e) => {
                using (e.RequestDeferral()) {
                    await Task.Yield();
                    actual = e.Parameter;
                }
            };

            await command.ExecuteAsync(expected);


            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task RequestedDeferralsAreAwaitedBeforeCoreExecutionIsInvoked() {
            var first = "first";
            var last = "last";

            var collection = new BlockingCollection<string>();

            var command = new Command(async () => {
                await Task.Delay(100);
                collection.Add(last);
            });

            command.Executing += async (sender, e) => {
                using (e.RequestDeferral()) {
                    await Task.Delay(200);
                    collection.Add(first);
                }
            };

            await command.ExecuteAsync(null);

            var expected = first;
            var actual = collection.Take();

            Assert.AreEqual(expected, actual);
        }
    }
}
