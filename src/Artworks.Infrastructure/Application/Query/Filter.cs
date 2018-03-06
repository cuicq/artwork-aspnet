using Artworks.Infrastructure.Application.Query.CommonModel;

namespace Artworks.Infrastructure.Application.Query
{
    /// <summary>
    /// 筛选器。
    /// </summary>
    public class Filter : IFilter
    {
        /// <summary>
        /// 属性。
        /// </summary>
        public string Property { get; private set; }
        /// <summary>
        /// 运算符。
        /// </summary>
        public Operator Operator { get; private set; }
        /// <summary>
        /// 参数。
        /// </summary>
        public object Argument { get; private set; }
        /// <summary>
        /// 连接器。
        /// </summary>

        public Connector Connector { get; set; }


        public Filter(string property, Operator @operator, object argument)
            : this(property, @operator, argument, Connector.Empty)
        {

        }

        public Filter(string property, Operator @operator, object argument, Connector connector)
        {
            this.Property = property;
            this.Operator = @operator;
            this.Argument = argument;
            this.Connector = connector;
        }

    }
}
