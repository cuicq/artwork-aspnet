using System;
using System.Linq.Expressions;
using Artworks.Infrastructure.Application.Query.CommonModel;
using Artworks.Infrastructure.Application.Query.Internal;

namespace Artworks.Infrastructure.Application.Query
{
    /// <summary>
    /// 筛选器定义。
    /// </summary>
    public class FilterDefinition
    {
        public static Filter Create<T>(Expression<Func<T, object>> expression, object argument, Operator @operator)
        {
            return FilterDefinition.Create<T>(expression, argument, @operator, Connector.Empty);
        }

        public static Filter Create<T>(Expression<Func<T, object>> expression, object argument, Operator @operator, Connector connector)
        {
            string property = PropertyNameHelper.ResolveAttributeName<T>(expression);
            if (!string.IsNullOrEmpty(property) && argument != null && !string.IsNullOrEmpty(argument.ToString()))
                return new Filter(property, @operator, argument, connector);
            return null;
        }
    }
}
