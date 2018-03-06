using Artworks.Authorization.CommonModel;
using Artworks.Infrastructure.Application.Query;
using System;

namespace Artworks.Authorization
{
    /// <summary>
    /// 认证仓储接口。
    /// </summary>
    public interface IAuthorizationRepository
    {
        /// <summary>
        /// 查询用户信息。
        /// </summary>
        User FindBy(QueryObject query);

        /// <summary>
        /// 更新用户信息
        /// </summary>
        bool UpdateUser(int userID, string token, string ip, DateTime loginDate);

        /// <summary>
        /// 更新错误登陆信息
        /// </summary>
        bool UpdateLoginFailed(int userID, DateTime loginDate);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        bool ChangePassword(int userID, string password);

        /// <summary>
        /// 创建用户
        /// </summary>
        bool AddUser(string userName, string password, string ip, string token);

        /// <summary>
        /// 删除用户
        /// </summary>
        bool DeleteUser(int userID);
    }
}
