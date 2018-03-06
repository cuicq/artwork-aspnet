
using Artworks.Container.CommonModel;
namespace Artworks.Container.Expressions
{
    /// <summary>
    /// 初始化接口类型。
    /// </summary>
    public interface IInitializationExpression
    {
        /// <summary>
        /// 添加注册实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void AddRegistry<T>() where T : Registry, new();
        /// <summary>
        /// 添加注册实例
        /// </summary>
        /// <param name="registry"></param>
        void AddRegistry(Registry registry);
        /// <summary>
        /// 是否使用默认配置文件
        /// </summary>
        bool UseDefaultConfigurationFile { set; }
        /// <summary>
        /// 自定义配置文件
        /// </summary>
        /// <param name="fileName"></param>
        void ConfigurationFile(string fileName);
    }
}
