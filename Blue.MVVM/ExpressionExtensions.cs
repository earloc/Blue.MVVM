using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Blue.MVVM {
    public static class ExpressionExtensions {

        public static string ExtractMemberName<T>(this Expression<Func<T>> expression) {
            MemberExpression memberExpression = null;
            var unary = expression.Body as UnaryExpression;
            if (unary != null) {
                memberExpression = unary.Operand as MemberExpression;
            }
            else {
                memberExpression = expression.Body as MemberExpression;
            }

            if (memberExpression == null)
                throw new ArgumentException(string.Format("'{0}' is not a valid MemberExpression", "expression"));

            return memberExpression.Member.Name;
        }

    }
}
