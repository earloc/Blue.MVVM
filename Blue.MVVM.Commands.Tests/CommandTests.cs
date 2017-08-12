using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Threading;

namespace Blue.MVVM.Commands.Tests {
    [TestClass]
    public class CommandTests {

        [TestMethod]
        [TestCategory("TDD")]
        public async Task ExecuteAsyncReturnsDefaultOfTWhenCanExecuteReturnsFalse_SyncExecute() {
            var command = new Function<object, bool>(p => Task.FromResult(true), p => false);

            var actual = await command.ExecuteAsync(null);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        [TestCategory("TDD")]
        public async Task ExecuteAsyncReturnsResultWHenCanExecuteIsTrue_SyncExecute() {
            var command = new Function<object, bool>(p => Task.FromResult(true), p => true);

            var actual = await command.ExecuteAsync(null);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        [TestCategory("TDD")]
        public async Task ExecuteAsyncReturnsResultWHenCanExecuteIsTrue_AsyncExecute() {
            var command = new Function<object, bool>(p => Task.FromResult(true), p => true);

            var actual = await command.ExecuteAsync(null);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        [TestCategory("TDD")]
        public async Task ExecuteAsyncReturnsDefaultOfTWhenCanExecuteReturnsFalse_AsyncExecute() {
            var command = new Function<object, bool>(async p => await Task.FromResult(true), p => false);

            var actual = await command.ExecuteAsync(null);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        [TestCategory("TDD")]
        public async Task CanExecuteReturnsFalseWhenCommandIsExecuting() {

            var canExecuteDuringExecution = true;

            Command command = null;

            command = new Command(() => { canExecuteDuringExecution = command.CanExecute(null); }, () => true);

            await command.ExecuteAsync(null);

            Assert.IsFalse(canExecuteDuringExecution);
        }

        [TestMethod]
        [TestCategory("TDD")]
        public async Task RecursiveExecutionIsBlockedDuringExecutionPerDefault() {

            var callCounter = 0;

            Command command = null;

            command = new Command(async () => {
                var calls = Interlocked.Increment(ref callCounter);
                if (calls > 10)
                    return;
                await command.ExecuteAsync(null);
            },
            () => true);

            await command.ExecuteAsync(null);

            var expected = 1;
            var actual = callCounter;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory("TDD")]
        public async Task RecursiveExecutionIsNotBlockedDuringExecutionWhenNotBlockingRecursiveCalls() {

            var callCounter = 0;

            Command command = null;

            command = new Command(async () => {
                var calls = Interlocked.Increment(ref callCounter);
                if (calls > 10)
                    return;
                await command.ExecuteAsync(null);
            },
            () => true) {
                BlocksRecursions = false
            };
            await command.ExecuteAsync(null);

            var expected = 11;
            var actual = callCounter;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory("TDD")]
        public async Task CallingExecuteEvaluatesCanExecuteAndSilentlySkipsExecutionWhenNotExecutable() {

            var hasExecuted = false;

            var command = new Command(() => hasExecuted = true, () => false);

            await command.ExecuteAsync(null);

            Assert.IsFalse(hasExecuted);
        }

        [TestMethod]
        [TestCategory("TDD")]
        public async Task FiresExecutingEventPriorExecution() {

            var executeTime = DateTime.MinValue;

            var command = new Command(async () => {
                await Task.Delay(50);
                executeTime = DateTime.Now;
            });

            var executingTime = DateTime.MaxValue;
            command.Executing += (sender, e) => {
                executingTime = DateTime.Now;
            };

            await command.ExecuteAsync(null);

            var expected = true;
            var actual = executingTime < executeTime;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory("TDD")]
        public async Task ExecutesPriorFiringExecutedEvent() {

            var executeTime = DateTime.MaxValue;

            var command = new Command(async () => {
                executeTime = DateTime.Now;
                await Task.Delay(50);
            });

            var executedTime = DateTime.MinValue;
            command.Executed += (sender, e) => {
                executedTime = DateTime.Now;
            };

            await command.ExecuteAsync(null);

            var expected = true;
            var actual = executedTime > executeTime;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory("TDD")]
        public async Task ExecutingEventArgsHoldsExecutionResult() {

            var returnValue = DateTime.Now;

            var command = new Function<object, DateTime>((p) => Task.FromResult(returnValue));

            var actual = DateTime.MinValue;

            command.Executed += (sender, e) => {
                actual = e.Result;
            };

            await command.ExecuteAsync(null);

            var expected = returnValue;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory("TDD")]
        public async Task WaitsForPendingDeferralsPriorExecution() {

            var hasWaited = false;
            var eventFinished = false;

            var command = new Command(() => {
                if (eventFinished)
                    hasWaited = true;
            });

            command.Executing += async (sender, e) => {
                using (e.RequestDeferral()) {
                    await Task.Delay(25);
                    eventFinished = true;
                }
            };

            await command.ExecuteAsync(null);

            Assert.IsTrue(hasWaited);
        }

        [TestMethod]
        [TestCategory("TDD")]
        public async Task ExecuteAsyncWaitsForPendingDeferrals() {

            var executedFinishedAt = DateTime.MaxValue;

            var command = new Command(() => { });

            command.Executed += async (sender, e) => {
                using (e.RequestDeferral()) {
                    executedFinishedAt = DateTime.Now;
                    await Task.Delay(25);
                }
            };

            await command.ExecuteAsync(null);
            var callReturnedAt = DateTime.Now;

            var expected = true;
            var actual = executedFinishedAt.Ticks < callReturnedAt.Ticks;

            Assert.AreEqual(expected, actual);
        }

    }
}
