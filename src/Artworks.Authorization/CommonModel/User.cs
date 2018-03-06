using Artworks.Archetype;
using System;

namespace Artworks.Authorization.CommonModel
{
    /// <summary>
    /// 用户信息。
    /// </summary>
    public class User
    {
        [QueryField("user_id")]
        public int ID { get; set; }
        [QueryField("user_name")]
        public string Name { get; set; }
        public string Password { get; set; }
        [QueryField("login_ip")]
        public string LoginIP { get; set; }
        [QueryField("login_date")]

        public DateTime LoginDate { get; set; }
        /// <summary>
        /// 密码错误次数
        /// </summary>
        public int FailedPwdCount { get; set; }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        public int Deleted { get; set; }
    }
}
