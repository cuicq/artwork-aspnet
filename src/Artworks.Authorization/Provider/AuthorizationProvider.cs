using Artworks.Authorization.CommonModel;
using Artworks.Authorization.CommonModel.Internal;
using Artworks.Infrastructure.Application.CommonModel;
using Artworks.Infrastructure.Application.Query;
using Artworks.Infrastructure.Application.Query.CommonModel;
using Artworks.Infrastructure.Application.Web.Security;
using Artworks.Utility;
using System;
using System.Collections.Generic;
using System.Web;

namespace Artworks.Authorization.Provider
{
    /// <summary>
    /// 认证处理实现。
    /// </summary>
    public class AuthorizationProvider : IAuthorizationChain
    {
        private IAuthorizationRepository repository;

        public AuthorizationProvider(IAuthorizationRepository repository)
        {
            if (repository == null) throw new AuthorizationException("IAuthorizationRepository未实现。");
            this.repository = repository;
        }


        public ResponseResult Login(string userName, string password, bool persistCookie = false)
        {
            ResponseResult result = new ResponseResult();

            try
            {
                if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                {
                    result.Message = "用户名或密码不能为空。";
                }
                else
                {
                    var nowDate = DateTime.Now;

                    var query = QueryObject.CreateInstance();
                    query.Add(FilterDefinition.Create<User>(m => m.Deleted, 0, Operator.Equal, Connector.And));
                    query.Add(FilterDefinition.Create<User>(m => m.Name, userName, Operator.Equal));
                    var user = this.repository.FindBy(query);
                    if (user == null)
                    {
                        //result.Message = "账户不存在。";
                        result.Message = "用户名或密码错误。";
                    }
                    else
                    {
                        //验证用户名和密码
                        if (user.Name.Equals(userName) && user.Password.Equals(Tool.MD5(password)))
                        {
                            //验证登陆是否频繁

                            var time = (nowDate - user.LoginDate);
                            if (time.TotalMinutes > 30 && user.FailedPwdCount <= 3)
                            {
                                string token = HttpContext.Current.Session.SessionID;
                                string ip = Tool.GetIP();

                                //更新用户信息
                                this.repository.UpdateUser(user.ID, token, ip, nowDate);

                                string displayName = user.Name;

                                IDictionary<string, string> extraData = new Dictionary<string, string>();
                                extraData.Add("id", user.ID.ToString());
                                extraData.Add("name", user.Name);
                                extraData.Add("token", token);
                                extraData.Add("ip", ip);

                                AccountAuthentication.RegisterClient(displayName, extraData, persistCookie);

                                result.Status = 1;//登陆成功
                                result.Data = user.ID;
                            }
                            else
                            {
                                result.Message = "登陆频繁，请30分钟之后再试。";
                            }
                        }
                        else
                        {
                            result.Message = "用户名或密码错误。";
                        }

                        if (result.Status == 0)
                        {
                            //更新登陆
                            this.repository.UpdateLoginFailed(user.ID, nowDate);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                ExceptionHelper.HandleException(ex);
            }
            return result;
        }

        public ResponseResult ChangePassword(string userName, string currentPassword, string newPassword)
        {
            ResponseResult result = new ResponseResult();

            var query = QueryObject.CreateInstance();
            query.Add(FilterDefinition.Create<User>(m => m.Deleted, 0, Operator.Equal, Connector.And));
            query.Add(FilterDefinition.Create<User>(m => m.Name, userName, Operator.Equal));
            var user = this.repository.FindBy(query);

            if (user.Password.Equals(Tool.MD5(currentPassword)))
            {
                if (this.repository.ChangePassword(user.ID, Tool.MD5(newPassword)))
                {
                    result.Status = 1;
                    result.Message = "密码修改成功。";
                }
            }
            else
            {
                result.Message = "原密码错误。";
            }

            return result;
        }

        public ResponseResult ResetPassword(string passwordResetToken, string newPassword)
        {
            throw new NotImplementedException();
        }

        public ResponseResult CreateAccount(string userName, string password)
        {
            ResponseResult result = new ResponseResult();
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                var query = QueryObject.CreateInstance();
                query.Add(FilterDefinition.Create<User>(m => m.Name, userName, Operator.Equal));
                var user = this.repository.FindBy(query);
                if (user == null)
                {
                    string token = HttpContext.Current.Session.SessionID;
                    string ip = Tool.GetIP();

                    if (this.repository.AddUser(userName, Tool.MD5(password), ip, token))
                    {
                        result.Status = 1;
                        result.Message = "创建账户成功。";
                    }
                    else
                    {
                        result.Message = "创建账户失败。";
                    }
                }
                else
                {
                    result.Message = "该账户已注册。";
                }
            }
            else
            {
                result.Message = "输入信息不正确，创建账户失败。";
            }
            return result;
        }

        public ResponseResult DeleteAccount(int userID)
        {
            ResponseResult result = new ResponseResult();
            if (userID > 0)
            {
                var query = QueryObject.CreateInstance();
                query.Add(FilterDefinition.Create<User>(m => m.ID, userID, Operator.Equal));
                var user = this.repository.FindBy(query);

                if (user != null)
                {
                    if (this.repository.DeleteUser(userID))
                    {
                        result.Status = 1;
                        result.Message = "删除账户成功。";
                        return result;
                    }
                }
            }
            result.Message = "删除账户失败。";
            return result;
        }

        public void Logout()
        {
            AccountAuthentication.Logout();
        }
    }
}
