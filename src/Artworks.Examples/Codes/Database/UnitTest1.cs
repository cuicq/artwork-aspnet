using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using Artworks.Database;
using Artworks.Examples.App_Code;

namespace Artworks.Examples.Codes.Database
{
    [TestClass]
    public class UnitTest1
    {
        static UnitTest1()
        {
            AppStartup.Configure();
        }

        [TestMethod]
        public void TestSqlServer()
        {
            bool result = false;

            string sql = "select top 10 * from sysobjects ";
            var dt = Code.DatabaseOperator.Tester.ExecuteDataSet(sql, CommandType.Text).Tables[0];

            result = dt.Rows.Count > 0 ? true : false;

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestMySQL()
        {
            bool result = false;

            string sql = " select * from city  limit 10 ";
            var dt = DatabaseOperator.Master.ExecuteDataSet(sql, CommandType.Text).Tables[0];
            result = dt.Rows.Count > 0 ? true : false;

            Assert.IsTrue(result);
        }
    }
}
