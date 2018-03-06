using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Artworks.Examples.App_Code;
using Artworks.Anomaly;
using Artworks.Infrastructure.Application.Persistence.CommonModel;
using Artworks.Infrastructure.Application.Service.CommonModel;

namespace Artworks.Examples.Codes.Exception
{
    [TestClass]
    public class UnitTest1
    {
        static UnitTest1()
        {
            AppStartup.Configure();
        }


        [TestMethod]
        public void TestExceptionHanding()
        {
            bool result = false;

            try
            {
                int.Parse("a");
            }
            catch (System.Exception ex)
            {
                result = ExceptionManager.HandleException(new RepositoryException(ex.Message, ex));
            }

            try
            {
                throw new System.Exception(" custom exception");
            }
            catch (System.Exception ex)
            {
                result = ExceptionManager.HandleException(new ServiceException(ex.Message, ex));
            }

            Assert.IsTrue(result);
        }
    }
}
