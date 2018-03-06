using Artworks.Infrastructure.Application.Web.Security.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artworks.Infrastructure.Application.Web.Security
{
    /// <summary>
    /// 认证客户端数据。
    /// </summary>
    public class AuthenticationClientData
    {
        /// <summary>
        /// 显示名
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// 扩展数据
        /// </summary>
        public IDictionary<string, string> ExtraData { get; private set; }

        public string Token { get; private set; }

        public AuthenticationClientData(string displayName, IDictionary<string, string> extraData)
        {
            this.DisplayName = displayName;
            this.ExtraData = extraData;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> item in this.ExtraData)
            {
                sb.AppendFormat("{0}:{1}{2}", item.Key, item.Value, Consts.separator);
            }
            return sb.ToString().TrimEnd(Consts.separator);
        }
    }
}
