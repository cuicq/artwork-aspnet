using Artworks.Infrastructure.Application.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Artworks.Infrastructure.Application.Query.CommonModel
{
    /// <summary>
    /// 查询排序条件。
    /// </summary>
    public sealed class Sorter
    {
        /// <summary>
        /// 属性。
        /// </summary>
        public string Property { get; private set; }

        /// <summary>
        /// 排序方向。
        /// </summary>
        public SorterDirection Direction { get; private set; }


        public Sorter(string property, SorterDirection direction)
        {
            this.Property = property;
            this.Direction = direction;
        }

        public static Sorter Create<T>(Expression<Func<T, object>> expression, SorterDirection direction)
        {
            string property = PropertyNameHelper.ResolvePropertyName<T>(expression);
            return new Sorter(property, direction);
        }
    }

}
