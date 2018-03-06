using Artworks.Container.CommonModel;
using Artworks.Container.Interceptors.InstanceInterceptors.InterfaceInterception;
using Artworks.Examples.Codes.Application.Code.CommonModel.Contracts;
using Artworks.Examples.Codes.Application.Code.Repositories;
using Artworks.Examples.Codes.Application.Code.Services;
using Artworks.Examples.Codes.Application.Code.Services.Internal;
using Artworks.Infrastructure.Application.Persistence;
using Artworks.Infrastructure.Application.Validation;

namespace Artworks.Examples.Codes.Application.Code
{
    public class PluginRegistry : Registry
    {
        public PluginRegistry()
        {
            For<IRepositoryContext>().Use<DatabaseContext>();
            For<IValidationService>().Use<ValidationService>();


            //代码方式添加对象依赖关系
            For<IUserService>().Use<UserService>().Interceptor<InterfaceInterceptor>();
            For<IUserRepository>().Use<UserRepository>();

            #region 文件注入
            /*
            //文件注入
            var types = TypeRegistryConfiguration.Instance.GetValues<TypeRegistry>();
            foreach (var plugin in types)
            {
                if (plugin.Intercept != null)
                {
                    For(plugin.Type).Use(plugin.MapTo).Interceptor(plugin.Intercept);
                }
                else
                {
                    For(plugin.Type).Use(plugin.MapTo);
                }
            }
                   * */
            #endregion


        }
    }
}
