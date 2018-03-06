
namespace Artworks.Container.Expressions {
    /// <summary>
    /// 表示该类为初始化类
    /// </summary>
    public class InitializationExpression : ConfigurationExpression, IInitializationExpression {

        public void ConfigurationFile(string fileName) {


        }

        public bool UseDefaultConfigurationFile { get; set; }
    }
}