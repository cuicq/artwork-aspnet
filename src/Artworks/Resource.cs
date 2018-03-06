
namespace Artworks
{
    /// <summary>
    /// 系统资源。
    /// </summary>
    internal class Resource
    {
        public const string APPLICATION_CONFIG = @"app.xml";//应用程序配置
        public const string CONFIG_XPATH_EXCEPTION = "configuration/exception/registry";//异常注册
        public const string CONFIG_XPATH_SCHEDULE = @"configuration/schedule/add";//计划任务

        public const string EXCEPTION_NOTFOUND_APPFILE = "Not found the {0} application configuration files。";//未找到应用程序配置文件
    }
}
