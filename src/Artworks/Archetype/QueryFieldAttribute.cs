using System;

namespace Artworks.Archetype
{
    /// <summary>
    /// 实体查询映射字段。
    /// </summary>
    public class QueryFieldAttribute : Attribute
    {
        public string Name { get; private set; }
        public QueryFieldAttribute(string name)
        {
            this.Name = name;
        }
    }
}
