using Artworks.Uploading.CommonModel;
using Artworks.Uploading.Configuration.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Artworks.Uploading.Generators
{
    /// <summary>
    /// 复合图片生成。
    /// </summary>
    public class CompositePictureGenerator
    {
        private readonly List<IPictureGenerator> generators = new List<IPictureGenerator>();

        #region 单例实例

        private static object lockObj = new object();
        private static CompositePictureGenerator instanceObj = null;

        /// <summary>
        /// 实例
        /// </summary>
        public static CompositePictureGenerator Instance
        {
            get
            {
                if (instanceObj == null)
                {
                    lock (lockObj)
                    {
                        if (instanceObj == null)
                        {
                            instanceObj = new CompositePictureGenerator();
                        }
                    }
                }
                return instanceObj;
            }
        }

        #endregion

        #region 构造

        private CompositePictureGenerator()
        {
            try
            {
                var list = PictureGenerateRegistryConfiguration.Instance.GetValues<PictureGenerateRegistry>();
                foreach (PictureGenerateRegistry registry in list)
                {
                    string generateType = registry.GenerateType;
                    var generator = (IPictureGenerator)Activator.CreateInstance(Type.GetType(string.Format("Artworks.Uploading.Generators.{0}", generateType)), registry);
                    this.generators.Add(generator);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        /// <summary>
        /// 生成
        /// </summary>
        public IEnumerable<GenerateContext> Generate(GenerateArguments arguments)
        {
            return generators.Select(x => x.Generate(arguments)).ToList();
        }
    }
}
