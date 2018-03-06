using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artworks.Container.CommonModel
{
    /// <summary>
    ///实例类型注册。
    /// </summary>
    public class TypeRegistry
    {

        private Type type;
        private Type mapTo;
        private Type intercept;
        private TypeRegistryMode mode = TypeRegistryMode.Dynamic;

        public TypeRegistry() { }

        public TypeRegistry(Type type, Type mapTo)
            : this(type, mapTo, null, TypeRegistryMode.Dynamic)
        {

        }

        public TypeRegistry(Type type, Type mapTo, Type intercept)
            : this(type, mapTo, intercept, TypeRegistryMode.Dynamic)
        {

        }

        public TypeRegistry(Type type, Type mapTo, Type intercept, TypeRegistryMode mode)
        {
            this.type = type;
            this.mapTo = mapTo;
            this.intercept = intercept;
            this.mode = mode;
        }

        public Type Type { get { return this.type; } }
        public Type MapTo { get { return this.mapTo; } }
        public Type Intercept { get { return this.intercept; } }
        public TypeRegistryMode Mode { get { return this.mode; } }

    }
}
