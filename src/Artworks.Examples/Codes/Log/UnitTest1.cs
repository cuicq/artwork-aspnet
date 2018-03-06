using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Artworks.Examples.App_Code;
using Artworks.Log;

namespace Artworks.Examples.Codes.Log
{
    [TestClass]
    public class UnitTest1
    {
        static UnitTest1()
        {
            AppStartup.Configure();
        }

        [TestMethod]
        public void TestLog()
        {
            string message = "测试日志";
            var exception = new System.Exception("自定义异常");

            LogHelper.Debug(message);
            LogHelper.Debug(message, exception);
            Assert.IsTrue(true);
        }
    }
}
