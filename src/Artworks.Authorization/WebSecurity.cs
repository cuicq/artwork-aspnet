using Artworks.Container;
using Artworks.Infrastructure.Application.CommonModel;

namespace Artworks.Authorization
{

    /// <summary>
    /// web安全认证。
    /// </summary>
    public class WebSecurity
    {
        static WebSecurity()
        {
            //  AuthoricationStartup.Configure();
        }

        public static ResponseResult Login(string userName, string password, bool persistCookie = false)
        {
            var provider = ServiceLocator.Instance.GetService<IAuthorizationChain>();
            return provider.Login(userName, password, persistCookie);
        }

        public static ResponseResult CreateAccount(string userName, string password)
        {
            var provider = ServiceLocator.Instance.GetService<IAuthorizationChain>();
            return provider.CreateAccount(userName, password);
        }

        public static ResponseResult DeleteAccount(int userID)
        {
            var provider = ServiceLocator.Instance.GetService<IAuthorizationChain>();
            return provider.DeleteAccount(userID);
        }

        public static ResponseResult ChangePassword(string userName, string currentPassword, string newPassword)
        {
            var provider = ServiceLocator.Instance.GetService<IAuthorizationChain>();
            return provider.ChangePassword(userName, currentPassword, newPassword);
        }

        public static ResponseResult ResetPassword(string passwordResetToken, string newPassword)
        {
            return new ResponseResult { };
        }

        public static void Logout()
        {
            var provider = ServiceLocator.Instance.GetService<IAuthorizationChain>();
            provider.Logout();
        }
    }
}
