using System.Text;
using System.Threading;

namespace Blue.MVVM {

    public class ExecutedEventArgs<TIn, TOut> : ExecutingEventArgs<TIn> {
        public ExecutedEventArgs(TIn parameter, TOut result) 
            : base (parameter) {
            Result = result;
        }

        public TOut Result { get; }
    }
}
