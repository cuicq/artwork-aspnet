using System;
using Artworks.Container.Expressions;

namespace Artworks.Container
{
    /// <summary>
    /// 对象注册接口。
    /// </summary>
    public interface IRegistry
    {
        PluginTypeExpression For<T>() where T : class;
        PluginTypeExpression For(Type pluginType);
    }
}
