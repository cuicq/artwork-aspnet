using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Artworks.Examples.App_Code;
using Artworks.Infrastructure.Application.Query;
using Artworks.Infrastructure.Application.Query.CommonModel;
using Artworks.Examples.Codes.QueryObjects.Model;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using Artworks.Database;
using System.Data.Common;

namespace Artworks.Examples.Codes.QueryObjects
{
    [TestClass]
    public class UnitTest1
    {

        static UnitTest1()
        {
            AppStartup.Configure();
        }


        [TestMethod]
        public void TestQueryObject()
        {
            test2();
            Assert.IsTrue(true);
        }


        /// <summary>
        /// in / not in
        /// </summary>
        public void test1()
        {
            QueryObject query = QueryObject.CreateInstance();
            query.AddOrderClauses(Sorter.Create<TempData>(x => x.crdate, SorterDirection.DESC));
            query.Add(FilterDefinition.Create<TempData>(m => m.id, new int[] { 3, 5, 7, 8 }, Operator.NotIn, Connector.And));
            query.Add(FilterDefinition.Create<TempData>(m => m.name, new string[] { "sysscalartypes", "sysprivs", "sysobjvalues" }, Operator.In));
            IList<DbParameter> parameters = new List<DbParameter>();
            string clause = query.GetWhereClause(parameters);
            string sql = string.Format(" select * from sysobjects {0}", clause);
            var ds = DatabaseOperator.Master.ExecuteDataSet(sql, CommandType.Text, parameters.ToArray());
            var dt = ds.Tables[0];
            Assert.IsTrue(dt.Rows.Count > 0 ? true : false);
        }

        /// <summary>
        /// like
        /// </summary>
        public void test2()
        {
            string key = "ys";
            QueryObject query = QueryObject.CreateInstance();

            query.AddOrderClauses(Sorter.Create<TempData>(x => x.refdate, SorterDirection.DESC));
            query.Add(FilterDefinition.Create<TempData>(m => m.name, key, Operator.Like, Connector.And));

            var subQuery = query.CreateGroup();
            subQuery.Add(FilterDefinition.Create<TempData>(m => m.name, key, Operator.LeftLike, Connector.Or));
            subQuery.Add(FilterDefinition.Create<TempData>(m => m.xtype, "S", Operator.RightLike));

            IList<DbParameter> parameters = new List<DbParameter>();
            string clause = query.GetWhereClause();

            string sql = string.Format(" select * from sysobjects {0}", clause);
            var ds = DatabaseOperator.Master.ExecuteDataSet(sql, CommandType.Text, parameters.ToArray());

            var dt = ds.Tables[0];

            Assert.IsTrue(dt.Rows.Count > 0 ? true : false);
        }


        /// <summary>
        /// 存储过程test
        /// </summary>
        public void test3()
        {
            string key = "sysallocunits";
            QueryObject query = QueryObject.CreateInstance();

            query.Add(FilterDefinition.Create<TempData>(m => m.id, 7, Operator.Equal));
            query.Add(FilterDefinition.Create<TempData>(m => m.name, key, Operator.Like));

            IList<DbParameter> parameters = new List<DbParameter>();
            string clause = query.GetWhereClause(parameters);
            string cmdText = string.Format("{0}", clause);
            var ds = DatabaseOperator.Master.ExecuteDataSet(cmdText, CommandType.StoredProcedure, parameters.ToArray());
            var dt = ds.Tables[0];

            Assert.IsTrue(dt.Rows.Count > 0 ? true : false);
        }
    }
}
