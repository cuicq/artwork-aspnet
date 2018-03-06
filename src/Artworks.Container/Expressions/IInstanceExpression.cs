using System;

namespace Artworks.Container.Expressions {
    /// <summary>
    /// 实例扩展类接口。
    /// </summary>
    public interface IInstanceExpression {
        void Add(Type pluginType, Type concreteType, object target);
        void Add(Type pluginType, Type concreteType);
    }
}
