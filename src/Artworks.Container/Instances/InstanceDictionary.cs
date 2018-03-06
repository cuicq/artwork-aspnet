using System;
using System.Collections.Generic;
using System.Linq;

namespace Artworks.Container.Instances
{
    /// <summary>
    /// 实例字典。
    /// </summary>
    public class InstanceDictionary : Dictionary<Type, Instance>
    {

        public InstanceDictionary()
        {

        }

        public InstanceDictionary(Type type, Instance instance)
        {
            this.Add(type, instance);
        }

        public bool IsValid
        {
            get
            {
                return this.All(instance => instance.Value != null);
            }
        }

    }
}
