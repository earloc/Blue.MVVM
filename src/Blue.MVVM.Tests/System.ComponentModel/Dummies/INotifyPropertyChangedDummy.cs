using System;
using System.ComponentModel;
using System.Linq.Expressions;
namespace Blue.MVVM.Tests.Dummies {
    public interface INotifyPropertyChangedDummy : INotifyPropertyChanged {
        
        void OnPropertyChangedSurrogate<T>(Expression<Func<T>> propertyExpression);
        
        bool SetSurrogate<T>(ref T target, T value, Expression<Func<T>> propertyExpression, bool notifyOnChangeOnly);

        int MyProperty                          { get; set; }
        ReferenceType ReferenceProperty         { get; set; }

        int PlainProperty                       { get; set; }
        int ChangeAgnosticNotificationProperty  { get; set; }
        int ChangeAwareNotificationProperty     { get; set; }

        int InlineProperty                      { get; set; }
    }
}
