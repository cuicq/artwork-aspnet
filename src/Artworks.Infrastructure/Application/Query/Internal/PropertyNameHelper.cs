using Artworks.Archetype;
using System;
using System.Linq.Expressions;

namespace Artworks.Infrastructure.Application.Query.Internal
{
    /// <summary>
    /// 反射属性帮助类。
    /// </summary>
    internal class PropertyNameHelper
    {
        public static string ResolvePropertyName<T>(Expression<Func<T, object>> expression)
        {
            var expr = expression.Body as MemberExpression;
            if (expr == null)
            {
                var u = expression.Body as UnaryExpression;
                expr = u.Operand as MemberExpression;
                object[] attributes = expr.Member.GetCustomAttributes(typeof(QueryFieldAttribute), false);
                foreach (QueryFieldAttribute item in attributes)
                {
                    return item.Name;
                }
            }
            return expr.ToString().Substring(expr.ToString().IndexOf(".") + 1);
        }

        public static string ResolveAttributeName<T>(Expression<Func<T, object>> expression)
        {
            string propertyName = string.Empty;
            var expr = expression.Body as MemberExpression;
            if (expr != null)
            {
                object[] attributes = expr.Member.GetCustomAttributes(typeof(QueryFieldAttribute), false);
                foreach (QueryFieldAttribute item in attributes)
                {
                    propertyName = item.Name;
                }
            }
            if (string.IsNullOrEmpty(propertyName))
            {
                propertyName = ResolvePropertyName(expression);
            }
            return propertyName;
        }

    }

}
