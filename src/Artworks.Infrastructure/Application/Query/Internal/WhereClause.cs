using Artworks.Database.CommonModel;
using Artworks.Database.Configuration;
using Artworks.Database.Core;
using Artworks.Infrastructure.Application.Query.CommonModel;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Artworks.Infrastructure.Application.Query.Internal
{
    /// <summary>
    ///  Where 子句，包含 WHERE。
    /// </summary>
    internal class WhereClause
    {
        private string clause = string.Empty;
        /// <summary>
        /// 子句。
        /// </summary>
        public string Clause
        {
            get { return clause; }
            set { clause = value; }
        }

        /// <summary>
        /// 命令参数。
        /// </summary>
        public object[] Arguments
        {
            get
            {
                if (this.Parameters == null) return null;
                return Parameters.ToArray();
            }
        }

        private QueryObject QueryObject { get; set; }


        private List<object> Parameters { get; set; }

        private int CurrentIndex { get; set; }


        private WhereClause(QueryObject queryObject, object[] arguments)
        {
            this.QueryObject = queryObject;
            if (arguments != null)
            {
                this.Parameters = new List<object>(arguments);
            }
        }
        public static WhereClause Create(QueryObject queryObject)
        {
            return Create(queryObject, null);
        }

        public static WhereClause Create(QueryObject queryObject, object[] arguments)
        {
            var that = new WhereClause(queryObject, arguments);
            that.Accept();
            return that;
        }


        private void Accept()
        {
            if (this.QueryObject.ChainCount == 0)
            {
                return;
            }

            StringBuilder sqlQuery = new StringBuilder(" where ");

            foreach (QueryChain queryChain in this.QueryObject.QueryChains)
            {
                switch (queryChain.ChainType)
                {
                    case ChainType.None:
                        Filter filter = (Filter)queryChain.Argument;
                        sqlQuery.AppendFormat(this.ClauseCode(filter));
                        if (filter.Connector != Connector.Empty)
                        {
                            sqlQuery.AppendFormat(filter.Connector.Code());
                        }
                        CurrentIndex++;
                        break;

                    case ChainType.Group:
                        StringBuilder groupQuery = new StringBuilder();
                        FilterGroup group = (FilterGroup)queryChain.Argument;
                        if (group.Filters.Length > 0)
                        {
                            groupQuery.Append(" ( ");
                            foreach (Filter child in group.Filters)
                            {
                                groupQuery.Append(this.ClauseCode(child));
                                if (child.Connector != Connector.Empty)
                                {
                                    groupQuery.AppendFormat(child.Connector.Code());
                                }

                                CurrentIndex++;
                            }
                            groupQuery.Append(" ) ");
                        }
                        sqlQuery.Append(groupQuery);
                        break;
                }
            }

            this.Clause = string.Format("{0}{1}", sqlQuery, ClauseOrderCode());
        }

        #region 辅助

        /// <summary>
        /// 条件代码
        /// </summary>
        private string ClauseCode(Filter filter)
        {
            object argument = filter.Argument;
            string format = string.Empty;
            string clause = string.Empty;

            if (this.Arguments != null)
            {
                format = @" {0} {1} @p{2}";
                bool flag = true;//标记是否生成SqlParameter
                switch (filter.Operator)
                {
                    case Operator.In:
                    case Operator.NotIn:
                        format = @" {0} {1} ({2})";
                        argument = ClauseInOrNotInCode(filter);
                        flag = false;
                        break;
                    case Operator.LeftLike:
                        argument = string.Format("%{0}", argument);
                        break;
                    case Operator.RightLike:
                        argument = string.Format("{0}%", argument);
                        break;
                    case Operator.Like:
                        argument = string.Format("%{0}%", argument);
                        break;
                }

                if (flag)
                {
                    DbParameter parameter = null;
                    if (DatabaseRegistryConfiguration.Instance.DataBaseType == DatabaseType.SqlServer)
                    {
                        parameter = new SqlParameter("@p" + CurrentIndex, argument);
                    }
                    else
                    {
                        parameter = new MySqlParameter("@p" + CurrentIndex, argument);
                    }
                    this.Parameters.Add(parameter);
                    return string.Format(format, filter.Property, filter.Operator.Code(), CurrentIndex);
                }
            }
            else
            {
                switch (filter.Operator)
                {
                    case Operator.In:
                    case Operator.NotIn:
                        format = "{0} {1} ({2}) ";
                        argument = ClauseInOrNotInCode(filter);
                        break;
                    case Operator.LeftLike:
                        format = "{0} {1} '%{2}' ";
                        break;
                    case Operator.RightLike:
                        format = "{0} {1} '{2}%' ";
                        break;
                    case Operator.Like:
                        format = "{0} {1} '%{2}%' ";
                        break;
                    default:
                        format = " {0} {1} '{2}' ";
                        break;
                }
            }

            return string.Format(format, filter.Property, filter.Operator.Code(), argument);
        }


        /// <summary>
        /// 构建In、NotIn条件
        /// </summary>
        /// <returns></returns>
        private string ClauseInOrNotInCode(Filter filter)
        {
            StringBuilder sqlQuery = new StringBuilder();
            if (filter.Argument is int[])
            {
                var list = filter.Argument as int[];

                for (int i = 0; i < list.Length; i++)
                {
                    var value = list[i];

                    if (this.Arguments != null)
                    {
                        sqlQuery.AppendFormat("@p{0}_{1}{2}", CurrentIndex, i, i == list.Length - 1 ? "" : ",");

                        DbParameter parameter = null;
                        if (DatabaseRegistryConfiguration.Instance.DataBaseType == DatabaseType.SqlServer)
                        {
                            parameter = new SqlParameter(string.Format("@p{0}_{1}", CurrentIndex, i), value);
                        }
                        else
                        {
                            parameter = new MySqlParameter(string.Format("@p{0}_{1}", CurrentIndex, i), value);
                        }

                        this.Parameters.Add(parameter);
                    }
                    else
                    {
                        sqlQuery.AppendFormat("{0}{1}", value, i == list.Length - 1 ? "" : ",");
                    }

                }
            }
            else if (filter.Argument is string[])
            {
                var list = filter.Argument as string[];
                for (int i = 0; i < list.Length; i++)
                {
                    var value = list[i];

                    if (this.Arguments != null)
                    {
                        sqlQuery.AppendFormat("@p{0}_{1}{2}", CurrentIndex, i, i == list.Length - 1 ? "" : ",");
                        DbParameter parameter = null;
                        if (DatabaseRegistryConfiguration.Instance.DataBaseType == DatabaseType.SqlServer)
                        {
                            parameter = new SqlParameter(string.Format("@p{0}_{1}", CurrentIndex, i), value);
                        }
                        else
                        {
                            parameter = new MySqlParameter(string.Format("@p{0}_{1}", CurrentIndex, i), value);
                        }

                        this.Parameters.Add(parameter);
                    }
                    else
                    {
                        sqlQuery.AppendFormat("'{0}'{1}", value, i == list.Length - 1 ? "" : ",");
                    }
                }
            }
            else
            {
                throw new QueryObjectException(Resource.EXCEPTION_QUERY_CLAUSE_INORNOTIN);
            }
            return sqlQuery.ToString();
        }

        /// <summary>
        /// 排序代码
        /// </summary>
        private string ClauseOrderCode()
        {
            var list = this.QueryObject.OrderClauses;
            if (list.Count == 0) return string.Empty;


            StringBuilder sqlQuery = new StringBuilder(" order by ");
            int count = list.Count;
            for (var i = 0; i < count; i++)
            {
                var item = list[i];

                sqlQuery.AppendFormat(@" {0} {1}{2}",
                    item.Property,
                    item.Direction == SorterDirection.DESC ? "desc" : "asc",
                    i != count - 1 ? "," : ""
                    );
            }
            return sqlQuery.ToString();
        }

        #endregion


    }
}
