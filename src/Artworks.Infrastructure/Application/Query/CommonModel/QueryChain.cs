using System;

namespace Artworks.Infrastructure.Application.Query.CommonModel
{

    public enum ChainType
    {
        None,
        Group
    }

    /// <summary>
    /// 查询参数。
    /// </summary>
    public class QueryChain
    {
        public string ID { get; private set; }

        public object Argument { get; set; }

        public ChainType ChainType { get; set; }

        public QueryChain(object argument)
            : this(argument, ChainType.None)
        {

        }

        public QueryChain(object argument, ChainType chainType)
        {
            this.ID = Guid.NewGuid().ToString("N");
            this.Argument = argument;
            this.ChainType = chainType;
        }
    }
}
