using System;
using System.Reflection.Emit;

namespace Artworks.Container.Interceptors.InstanceInterceptors.InterfaceInterception {

    public partial class InterfaceInterceptorClassGenerator {

        private static ModuleBuilder GetModuleBuilder() {
            string moduleName = Guid.NewGuid().ToString("N");
#if DEBUG
            return assemblyBuilder.DefineDynamicModule(moduleName, moduleName + ".dll", false );
#else
            return assemblyBuilder.DefineDynamicModule(moduleName);
            //   return assemblyBuilder.DefineDynamicModule("Artworks.Dynamic", "Artworks.Dynamic" + ".dll", true);

#endif

        }
    }
}
