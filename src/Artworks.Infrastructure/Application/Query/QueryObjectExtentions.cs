using Artworks.Infrastructure.Application.Query.Internal;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace Artworks.Infrastructure.Application.Query
{
    /// <summary>
    /// Query Object 模式扩展。
    /// </summary>
    public static class QueryObjectExtentions
    {
        public static string GetWhereClause(this QueryObject queryObject)
        {
            var whereClause = WhereClause.Create(queryObject);
            return whereClause.Clause;
        }

        public static string GetWhereClause(this QueryObject queryObject, IList<DbParameter> parameters)
        {
            var whereClause = WhereClause.Create(queryObject, parameters.ToArray());

            string clause = whereClause.Clause;

            if (!string.IsNullOrEmpty(clause))
            {
                parameters.Clear();
                foreach (object argument in whereClause.Arguments)
                {
                    parameters.Add((DbParameter)argument);
                }
            }

            return clause;
        }

    }
}
