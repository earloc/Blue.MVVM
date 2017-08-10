namespace Blue.MVVM.Commands {
    public class ExecutingEventArgs<TIn> : DeferrableEventArgs<TIn> {
        public ExecutingEventArgs(TIn parameter) {
            Parameter = parameter;
        }

        public TIn Parameter { get; }
    }
}
