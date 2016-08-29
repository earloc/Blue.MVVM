using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blue.MVVM {
    /// <summary>
    /// Interface used for proeprty-interception
    /// </summary>
    public interface IPropertyInterceptor {

        /// <summary>
        /// occurs before a property is set
        /// </summary>
        event EventHandler<PropertySettingEventArgs>    PreSet;

        /// <summary>
        /// occurs after a property has been set
        /// </summary>
        event EventHandler<PropertySetEventArgs>        PostSet;

    }
}
