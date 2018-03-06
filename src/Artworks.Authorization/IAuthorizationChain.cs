using Artworks.Infrastructure.Application.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artworks.Authorization
{

    /// <summary>
    /// 认证接口。
    /// </summary>
    public interface IAuthorizationChain
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="persistCookie">是否保留Cookie</param>
        /// <returns></returns>
        ResponseResult Login(string userName, string password, bool persistCookie = false);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="currentPassword">当前密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        ResponseResult ChangePassword(string userName, string currentPassword, string newPassword);

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="passwordResetToken">重置密码TOKEN</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        ResponseResult ResetPassword(string passwordResetToken, string newPassword);

        /// <summary>
        /// 创建账户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        ResponseResult CreateAccount(string userName, string password);

        /// <summary>
        /// 删除账户
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        ResponseResult DeleteAccount(int userID);

        /// <summary>
        /// 退出
        /// </summary>
        void Logout();
    }
}
