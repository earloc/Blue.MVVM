using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation {
    public class MappedViewTypeResolver : Dictionary<Type, Type>, IViewTypeResolver {
        public Task<Type> GetViewTypeAsync(Type viewModelType, object viewModel) => Task.FromResult(this[viewModelType]);
    }
}
