using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Artworks.Schedule;
using Artworks.Examples.App_Code;
using Artworks.Examples.Codes.Cache.Code;

namespace Artworks.Examples.Codes.Cache
{
    [TestClass]
    public class UnitTest1
    {
        static UnitTest1()
        {
            AppStartup.Configure();
        }


        [TestMethod]
        public void TestCacheSchedule()
        {
            ScheduleManager.Instance.Startup();
        }

        [TestMethod]
        public void TestCache()
        {
            var dt = UserData.GetList2();
            dt = UserData.GetList2();
            dt = UserData.GetList2();
            Assert.IsTrue(dt.Rows.Count > 0 ? true : false);

        }
    }
}
