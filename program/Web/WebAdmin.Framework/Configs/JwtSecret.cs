using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAdmin.Framework.Configs
{
    /// <summary>
    /// Jwt配置
    /// </summary>
    public class JwtSecret
    {
        /// <summary>
        /// SecretKey
        /// </summary>
        public string SecretKey { get; set; }
        /// <summary>
        /// 发行人
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        ///Audience
        /// </summary>
        public string Audience { get; set; }
    }
}
