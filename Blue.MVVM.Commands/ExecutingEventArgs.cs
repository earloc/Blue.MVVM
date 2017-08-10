namespace Blue.MVVM {
    public class ExecutingEventArgs<TIn> : DeferrableEventArgs<TIn> {
        public ExecutingEventArgs(TIn parameter) {
            Parameter = parameter;
        }

        public TIn Parameter { get; }
    }
}
