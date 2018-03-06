using System;
using Artworks.Container.Instances;

namespace Artworks.Container.Expressions
{
    /// <summary>
    /// 实例扩展。
    /// </summary>
    public class InstanceExpression : IInstanceExpression
    {

        private readonly InstanceDictionary instances = new InstanceDictionary();
        private readonly IInstanceMap map;

        public InstanceExpression()
        {
            map = new InstanceMap();
        }

        internal IInstanceMap BuildMap()
        {
            return map;
        }


        public void Add(Type pluginType, Type concreteType, object target)
        {
            map.Add(pluginType,
                         new Instance
                         {
                             PluginType = pluginType,
                             ConcreteType = concreteType,
                             Target = target,
                         });
        }

        public void Add(Type pluginType, Type concreteType)
        {
            this.Add(pluginType, concreteType, null);
        }
    }
}
