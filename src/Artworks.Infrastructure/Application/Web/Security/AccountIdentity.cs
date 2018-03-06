using Artworks.Utility.Common;
using System;

namespace Artworks.Infrastructure.Application.Web.Security
{
    /// <summary>
    /// 账户身份验证进行了身份验证的用户标识。无法继承此类。
    /// </summary>
    [Serializable]
    public sealed class AccountIdentity : IAccountIdentity
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public bool IsAuthenticated { get; private set; }
        public string Token { get; private set; }

        public string AuthenticationType { get { return "sso"; } }

        public AccountIdentity() { }

        public AccountIdentity(AuthenticationClientData clientData)
        {
            if (clientData != null)
            {
                if (!string.IsNullOrEmpty(clientData.DisplayName) && clientData.ExtraData.Count > 0)
                {
                    string id = string.Empty;
                    clientData.ExtraData.TryGetValue("id", out id);

                    string token = string.Empty;
                    clientData.ExtraData.TryGetValue("token", out token);

                    this.ID = TypeUtil.GetInt(id, 0);
                    this.Name = clientData.DisplayName;
                    this.Token = token;

                    if (this.ID > 0 && !string.IsNullOrEmpty(this.Token))
                    {
                        this.IsAuthenticated = true;
                    }


                }
            }
        }
    }
}
