using System;

namespace Artworks.Container.Instances
{
    /// <summary>
    /// 实例映射类。
    /// </summary>
    public class InstanceMap : IInstanceMap
    {

        private readonly InstanceDictionary instances = new InstanceDictionary();

        public void Add(Type pluginType, Instance instance)
        {
            if (!this.instances.ContainsKey(pluginType))
            {
                this.instances.Add(pluginType, instance);
            }
        }

        public InstanceDictionary InstanceDictionary
        {
            get { return this.instances; }
        }
    }
}
