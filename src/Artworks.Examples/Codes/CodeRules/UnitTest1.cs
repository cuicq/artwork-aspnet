using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Artworks.Infrastructure.Application;
using Artworks.Examples.App_Code;
using System.IO;
using Artworks.Configuration;
using Artworks.CodeRule;
using Artworks.CodeRule.Generators;

namespace Artworks.Examples.Codes.CodeRules
{
    [TestClass]
    public class UnitTest1
    {
        static UnitTest1()
        {
            AppStartup.Configure();
        }


        [TestMethod]
        public void TestCodeRule1()
        {
            string seedKey = "销售订单";

            var dir = ArtworksConfiguration.Instance.GetValue<string>("seeds");
            var file = Path.Combine(Environment.CurrentDirectory, dir, seedKey + ".txt");
            if (File.Exists(file))
            {
                File.Delete(file);
            }

            var seedStore = new FileSeedStore();
            int index = seedStore.NextSeed(seedKey);
            index = seedStore.NextSeed(seedKey);
            index = seedStore.NextSeed(seedKey);
            index = seedStore.NextSeed(seedKey);


        }


        [TestMethod]
        public void TestCodeRule2()
        {
            var seedKey = "销售订单";

            var dir = ArtworksConfiguration.Instance.GetValue<string>("seeds");
            var file = Path.Combine(Environment.CurrentDirectory, dir, seedKey + ".txt");
            if (File.Exists(file))
            {
                File.Delete(file);
            }


            var interceptor = new CodeRuleInterceptor();

            interceptor
                .RegisterInterceptor(new DateTimeCodeGeneratorInterceptor())
                .RegisterInterceptor(new LiteralCodeGeneratorInterceptor())
                .RegisterInterceptor(new SeedCodeGeneratorInterceptor(new FileSeedStore()));

            var generator = interceptor.Intercept("前缀---<日期:yyyyMMdd>---中缀---<种子:销售订单>---后缀");

            Assert.IsNotNull(generator);

            Assert.AreEqual("前缀---" + DateTime.Now.ToString("yyyyMMdd") + "---中缀---00001---后缀", generator.Generate(new GenerateContext()));
            Assert.AreEqual("前缀---" + DateTime.Now.ToString("yyyyMMdd") + "---中缀---00002---后缀", generator.Generate(new GenerateContext()));
            Assert.AreEqual("前缀---" + DateTime.Now.ToString("yyyyMMdd") + "---中缀---00003---后缀", generator.Generate(new GenerateContext()));

            string key = generator.Generate(new GenerateContext());
            key = generator.Generate(new GenerateContext());
            key = generator.Generate(new GenerateContext());


        }
    }
}
