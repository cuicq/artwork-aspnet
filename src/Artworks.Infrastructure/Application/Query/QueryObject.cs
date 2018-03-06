using Artworks.Infrastructure.Application.Query.CommonModel;
using Artworks.Infrastructure.Application.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artworks.Infrastructure.Application.Query
{
    /// <summary>
    /// Query Object 模式。
    /// </summary>
    public class QueryObject
    {
        private List<Sorter> Sorts = new List<Sorter>();
        private Dictionary<string, QueryChain> queryChains = new Dictionary<string, QueryChain>();

        private QueryObject() { }

        public static QueryObject CreateInstance()
        {
            return new QueryObject();
        }

        /// <summary>
        /// 添加筛选器定义
        /// </summary>
        public void Add(Filter filter)
        {
            QueryChain chain = new QueryChain(filter);
            queryChains.Add(chain.ID, chain);
        }

        /// <summary>
        /// 创建组筛选器。
        /// </summary>
        public FilterGroup CreateGroup()
        {
            FilterGroup group = new FilterGroup();
            QueryChain chain = new QueryChain(group, ChainType.Group);
            queryChains.Add(chain.ID, chain);
            return group;
        }

        /// <summary>
        /// 添加查询排序条件。
        /// </summary>
        /// <param name="sort"></param>
        public void AddOrderClauses(Sorter sort)
        {
            this.Sorts.Add(sort);
        }


        /// <summary>
        /// 有效数量。
        /// </summary>
        public int ChainCount { get { return this.queryChains.Count; } }

        public IEnumerable<QueryChain> QueryChains
        {
            get
            {
                return this.queryChains.Values.AsEnumerable();
            }
        }

        /// <summary>
        /// 排序条件。
        /// </summary>
        public IList<Sorter> OrderClauses { get { return Sorts; } }
    }
}
