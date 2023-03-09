using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAdmin.Framework.Configs;
using WebAdmin.Framework.Extentions;
using WebAdmin.Framework.Operators;

namespace WebAdmin.Framework.Helper
{
    /// <summary>
    /// Jwt帮助类
    /// </summary>
    public class JwtHelper
    {
        /// <summary>
        /// 自定义Claim 参数
        /// </summary>
        private static readonly string custom = "info";
        /// <summary>
        /// 生成Jwt
        /// </summary>
        /// <param name="operators">存放的实体 数据</param>
        /// <returns></returns>
        public static string IssueJwt(JwtOperator operators)
        {
            try
            {
                var date = DateTime.Now;
                string exp = $"{new DateTimeOffset(date.AddMinutes(AppSetting.JwtExpMinutes)).ToUnixTimeSeconds()}";
                var claims = new List<Claim>
                {
                new Claim(ClaimTypes.Name,operators.UserName),
                new Claim(custom,operators.ToJson()),
                //“jti”（JWT ID）声明为JWT提供了唯一标识符。
                //标识符值的分配方式必须确保相同值的概率可以忽略不计，意外分配给不同的数据对象；
                //如果应用程序使用多个发行者，必须防止值之间的冲突由不同的发行人制作。
                //可以使用“jti”声明以防止JWT被重放。“jti”值是一个case-敏感字符串。此声明的使用是可选的。
                new Claim(JwtRegisteredClaimNames.Jti,operators.UserId),
                //Jwt颁发时间
                new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(date).ToUnixTimeSeconds()}"),
                //Jwt容错时间
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(date).ToUnixTimeSeconds()}") ,
                //JWT过期时间
                //验证是否过期 从User读取过期 时间，再将时间戳转换成日期，如果时间在半个小时内即将过期，通知前台刷新JWT
                //int val= HttpContext.User.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Exp).FirstOrDefault().Value;
                //new DateTime(621355968000000000 + (long)val* (long)10000000, DateTimeKind.Utc).ToLocalTime()
                //默认设置jwt过期时间120分钟
                new Claim (JwtRegisteredClaimNames.Exp,exp),
                //发行人
                new Claim(JwtRegisteredClaimNames.Iss,AppSetting.Secret.Issuer),
                //Audience
                new Claim(JwtRegisteredClaimNames.Aud,AppSetting.Secret.Audience),
               };
                //秘钥16位
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSetting.Secret.SecretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                JwtSecurityToken securityToken = new JwtSecurityToken(issuer: AppSetting.Secret.Issuer, claims: claims, signingCredentials: creds);
                string jwt = new JwtSecurityTokenHandler().WriteToken(securityToken);
                return jwt;
            }
            catch
            {
            }
            return string.Empty;
        }
        /// <summary>
        /// 生成Jwt
        /// </summary>
        /// <param name="operators">存放的实体 数据</param>
        /// <param name="date">Jwt的开始时间</param>
        /// <returns></returns>
        public static string IssueJwt(JwtOperator operators, DateTime date)
        {
            try
            {
                string exp = $"{new DateTimeOffset(DateTime.Now.AddMinutes(AppSetting.JwtExpMinutes)).ToUnixTimeSeconds()}";
                var claims = new List<Claim>
                {
                new Claim(ClaimTypes.Name,operators.UserName),
                new Claim(custom,operators.ToJson()),
                //“jti”（JWT ID）声明为JWT提供了唯一标识符。
                //标识符值的分配方式必须确保相同值的概率可以忽略不计，意外分配给不同的数据对象；
                //如果应用程序使用多个发行者，必须防止值之间的冲突由不同的发行人制作。
                //可以使用“jti”声明以防止JWT被重放。“jti”值是一个case-敏感字符串。此声明的使用是可选的。
                new Claim(JwtRegisteredClaimNames.Jti,operators.UserId),
                //Jwt颁发时间
                new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                //Jwt容错时间
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                //JWT过期时间
                //验证是否过期 从User读取过期 时间，再将时间戳转换成日期，如果时间在半个小时内即将过期，通知前台刷新JWT
                //int val= HttpContext.User.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Exp).FirstOrDefault().Value;
                //new DateTime(621355968000000000 + (long)val* (long)10000000, DateTimeKind.Utc).ToLocalTime()
                //默认设置jwt过期时间120分钟
                new Claim (JwtRegisteredClaimNames.Exp,exp),
                //发行人
                new Claim(JwtRegisteredClaimNames.Iss,AppSetting.Secret.Issuer),
                //Audience
                new Claim(JwtRegisteredClaimNames.Aud,AppSetting.Secret.Audience),
               };
                //秘钥16位
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSetting.Secret.SecretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                JwtSecurityToken securityToken = new JwtSecurityToken(issuer: AppSetting.Secret.Issuer, claims: claims, signingCredentials: creds);
                string jwt = new JwtSecurityTokenHandler().WriteToken(securityToken);
                return jwt;
            }
            catch
            {
            }
            return string.Empty;
        }
        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static string SerializeJwt(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            string info = string.Empty;
            try
            {
                if (jwtStr.IndexOf("Bearer ") != -1)
                    jwtStr = jwtStr.Replace("Bearer ", "");
                JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
                info = jwtToken.Payload[custom]?.ToString();
            }
            catch
            {
            }
            return info;
        }
        /// <summary>
        /// 获取过期时间
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static DateTime? GetExp(string jwtStr)
        {
            try
            {
                if (jwtStr.IndexOf("Bearer ") != -1)
                    jwtStr = jwtStr.Replace("Bearer ", "");
                var jwtHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
                DateTime lTime = (jwtToken.Payload[JwtRegisteredClaimNames.Exp] ?? 0).GetTimeSpmpToDate();
                return lTime;
            }
            catch
            {
            }
            return null;
        }
        /// <summary>
        /// 判断是否过期
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static bool IsExp(string jwtStr)
        {
            if (GetExp(jwtStr)==null)
                return true;
            return GetExp(jwtStr).Value < DateTime.Now;
        }
        /// <summary>
        /// 获取用户Id
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static string GetUserId(string jwtStr)
        {
            try
            {
                if (jwtStr.IndexOf("Bearer ") != -1)
                    jwtStr = jwtStr.Replace("Bearer ", "");
                var jwtHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
                return jwtToken.Id;
            }
            catch
            {
            }
            return string.Empty;
        }
    }
}
