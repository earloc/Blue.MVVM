using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blue.MVVM {
    internal class ObjectPipeLine<TEnter, TLeave> {

        private class Pipe<T> {
            public T    Instance;
            public int  Priority;
        }

        public ObjectPipeLine() {
            EnteringPipeLine    = Enumerable.Empty<TEnter>();
            LeavingPipeLine     = Enumerable.Empty<TLeave>();
        }

        private readonly List<Pipe<TEnter>> _EnteringPipes = new List<Pipe<TEnter>>();

        internal void AddEnterPipe(int priority, TEnter instance) {
            _EnteringPipes.Add(new Pipe<TEnter>() {
                Instance = instance,
                Priority = priority
            });

            EnteringPipeLine = (from pipe in _EnteringPipes
                               orderby pipe.Priority ascending
                               select pipe.Instance).ToArray();
        }

        public IEnumerable<TEnter> EnteringPipeLine {
            get;
            private set;
        }

        private readonly List<Pipe<TLeave>> _LeavingPipes = new List<Pipe<TLeave>>();

        internal void AddLeavePipe(int priority, TLeave instance) {
            _LeavingPipes.Add(new Pipe<TLeave>() {
                Instance = instance,
                Priority = priority
            });

            LeavingPipeLine = (from pipe in _LeavingPipes
                               orderby pipe.Priority descending
                               select pipe.Instance).ToArray();
        }

        public IEnumerable<TLeave> LeavingPipeLine {
            get;
            private set;
        }

        public void Add(int priority, TEnter enterPipe, TLeave leavePipe) {
            AddEnterPipe(priority, enterPipe);
            AddLeavePipe(priority, leavePipe);
        }

        public void Clear() {
            _EnteringPipes.Clear();
            _LeavingPipes.Clear();
        }

    }
}
