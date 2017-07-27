using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation {
    public interface IViewTypeResolver {
        Task<Type> GetViewTypeAsync(Type viewModelType, object viewModel);
    }
}
