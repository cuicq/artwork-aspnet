using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Artworks.Infrastructure.Application;
using Artworks.Examples.Codes.Application.Code;
using Artworks.Container;
using Artworks.Examples.Codes.Application.Code.Services;
using Artworks.Examples.Codes.Application.Code.CommonModel;
using Artworks.Infrastructure.Application.CommonModel;

namespace Artworks.Examples.Codes.Application
{
    [TestClass]
    public class UnitTest1
    {
        static UnitTest1()
        {
            try
            {
                ApplicationRuntime.Instance.Regist(new WebApp());
                var currentApp = ApplicationRuntime.Instance.CurrentApp;
                currentApp.Startup();
            }
            catch (System.Exception ex)
            {

            }

        }

        [TestMethod]
        public void TestWebApp()
        {
            var service = ServiceLocator.Instance.GetService<IUserService>();

            Model item1 = new Model
            {
                ID = 1,
                Name = "admin",
                CreateDate = DateTime.Now
            };

            ResponseResult result = service.AggregateRootService.Create(item1);

            var item2 = service.Get(1);
            item2.Name = "cuicq";

            service.AggregateRootService.Update(item2);

            var item3 = service.Get(1);

            result = service.AggregateRootService.Delete(new RequestContext { Data = 1 });

            bool flag = result.Status == 1 ? true : false;

            Assert.IsTrue(flag);

        }
    }
}
