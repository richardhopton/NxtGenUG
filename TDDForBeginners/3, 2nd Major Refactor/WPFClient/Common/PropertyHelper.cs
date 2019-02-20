using System;
using System.Linq.Expressions;

namespace WPFClient.Common
{
    public static class PropertyHelper
    {
        public static String GetPropertyName<T>(Expression<Func<T, Object>> propertyExpression)
        {
            var lambda = propertyExpression as LambdaExpression;
            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = lambda.Body as UnaryExpression;
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else
            {
                memberExpression = lambda.Body as MemberExpression;
            }
            if (memberExpression != null)
            {
                return memberExpression.Member.Name;
            }
            return null;
        }
    }
}