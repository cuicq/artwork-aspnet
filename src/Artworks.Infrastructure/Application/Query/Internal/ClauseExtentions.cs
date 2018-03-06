using Artworks.Infrastructure.Application.Query.CommonModel;

namespace Artworks.Infrastructure.Application.Query.Internal
{
    /// <summary>
    /// 条件扩展
    /// </summary>
    internal static class ClauseExtentions
    {
        /// <summary>
        /// 筛选器连接器。
        /// </summary>
        /// <returns></returns>
        public static string Code(this Connector connector)
        {
            string result = string.Empty;
            switch (connector)
            {
                case Connector.And:
                    result = " and ";
                    break;
                case Connector.Or:
                    result = " or ";
                    break;
                default: break;
                //throw new QueryObjectException(Resource.EXCEPTION_QUERY_CONNECTOR);
            }
            return result;
        }


        /// <summary>
        /// 筛选器运算符。
        /// </summary>
        /// <returns></returns>
        public static string Code(this Operator @operator)
        {
            switch (@operator)
            {
                case Operator.Equal:
                    return "=";
                case Operator.NotEqual:
                    return "<>";
                case Operator.LessThanOrEquals:
                    return "<=";
                case Operator.LessThan:
                    return "<";
                case Operator.GreaterThan:
                    return ">";
                case Operator.GreaterThanOrEquals:
                    return ">=";
                case Operator.LeftLike:
                case Operator.RightLike:
                case Operator.Like:
                    return "like";
                case Operator.In:
                    return "in";
                case Operator.NotIn:
                    return "not in";
                default: break;
                // throw new ApplicationException("No operator defined.");
            }

            return string.Empty;
        }

    }
}
