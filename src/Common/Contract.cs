using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Common
{
    public static class Contract
    {
        static Func<object> func;
        static Func<string> stringFunc;
        public static void AssertNotNull(Expression<Func<object>> expr)
        {
            if (func == null)
            {
                func = expr.Compile();
            }
            if (func() == null)
            {
                string paramName = ((MemberExpression)expr.Body).Member.Name;
                throw new ArgumentNullException(paramName);
            }
        }

        public static void AssertNotNullOrEmpty(Expression<Func<string>> expr)
        {
            if (stringFunc == null)
            {
                stringFunc = expr.Compile();
            }
            if (string.IsNullOrEmpty(stringFunc().Trim()))
            {
                string paramName = ((MemberExpression)expr.Body).Member.Name;
                throw new ArgumentException(string.Format("String is null or empty: '{0}'", paramName));
            }
        }
    }
}
