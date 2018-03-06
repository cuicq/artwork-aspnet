using System.Collections.Generic;

namespace Artworks.Infrastructure.Application.Query
{
    /// <summary>
    /// 筛选器组。
    /// </summary>
    public class FilterGroup
    {
        private List<Filter> filters;

        /// <summary>
        ///  子筛选器。
        /// </summary>
        public IFilter[] Filters { get { return this.filters.ToArray(); } }

        public FilterGroup()
        {
            this.filters = new List<Filter>();
        }

        /// <summary>
        /// 组新增筛选器
        /// </summary>
        public void Add(Filter filter)
        {
            this.filters.Add(filter);
        }

    }
}
