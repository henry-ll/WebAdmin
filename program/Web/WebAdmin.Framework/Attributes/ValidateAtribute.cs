using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebAdmin.Framework.Enums;
using WebAdmin.Framework.Extentions;

namespace WebAdmin.Framework.Attributes
{

    /// <summary>
    /// 正则验证
    /// </summary>
    public class RegexAttribute : BaseAttribute
    {
        public string regexText;
        public override bool Validate(object value)
        {
            var regex = new Regex(regexText);
            return regex.Match(value.ToString()).Success;
        }
        /// <summary>
        /// 身份证格式
        /// </summary>
        public class Required : BaseAttribute
        {
            public ValidateEnum Type = ValidateEnum.Null;
            public override string error
            {
                get
                {
                    if (base.error != null)
                        return base.error;
                    return "身份证格式不正确";
                }
                set => base.error = value;
            }
            public override bool Validate(object value)
            {
                switch (Type)
                {
                    case ValidateEnum.Null:
                        return !(value == null);
                    case ValidateEnum.Idcard:
                        return IsIdCard(value?.ToString());
                    case ValidateEnum.NullOrWhiteSpace:
                        if (value == null)
                            return false;
                        return !string.IsNullOrWhiteSpace(value.ToString());
                    case ValidateEnum.Phone:
                        return IsPhoneNo(value.ToString());
                    case ValidateEnum.Int:
                        return IsInt(value);
                    case ValidateEnum.url:
                        return IsUrl(value.ToString());
                    case ValidateEnum.Number:
                        return IsNumber(value.ToString());
                    case ValidateEnum.Date:
                        return IsDate(value);
                    default:
                        return true;
                }
            }
            /// <summary>
            /// 判断身份证
            /// </summary>
            /// <param name="id_card"></param>
            /// <returns></returns>
            public bool IsIdCard(string id_card)
            {
                if (string.IsNullOrWhiteSpace(id_card))
                    return false;
                double t = 0;
                List<string> qz_1 = new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };//权重
                List<string> qz_2 = new List<string> { "1", "0", "X", "9", "8", "7", "6", "5", "4", "3", "2" };//权重对应值
                List<string> xs = new List<string> { "7", "9", "10", "5", "8", "4", "2", "1", "6", "3", "7", "9", "10", "5", "8", "4", "2" };//系数
                DateTime time;
                if (id_card.Trim().Length == 15)
                {
                    DateTime d;
                    string str = "19" + id_card.Trim().Substring(6, 2) + "-" + id_card.Trim().Substring(8, 2) + "-" + id_card.Trim().Substring(10, 2);
                    if (DateTime.TryParse(str, out d))
                    {
                        time = str.ToDate();
                        return true;
                    }
                    else
                        return false;
                }
                for (int i = 0; i < id_card.Trim().Length - 1; i++)
                    t += id_card.Trim().Length == 18 ? int.Parse(id_card.Trim().Substring(i, 1)) * int.Parse(xs[i]) : 0;
                bool bl = id_card.Trim().Length == 18 ? id_card.Trim().Substring(17, 1) == qz_2[qz_1.IndexOf((t % 11).ToString())] ? true : false : false;
                return bl;
            }
            /// <summary>
            /// 判断url
            /// </summary>
            /// <param name="str"></param>
            /// <returns></returns>
            public bool IsUrl(string str)
            {
                if (string.IsNullOrWhiteSpace(str))
                    return false;
                string Url = @"(http://)?([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
                return Regex.IsMatch(str, Url);

            }
            /// <summary>
            /// 判断是不是正确的手机号码
            /// </summary>
            /// <param name="input"></param>
            /// <returns></returns>
            public bool IsPhoneNo(string input)
            {
                if (string.IsNullOrWhiteSpace(input))
                    return false;
                if (input.Length != 11)
                    return false;

                if (new Regex(@"^1[3578][01379]\d{8}$").IsMatch(input) || new Regex(@"^1[34578][01256]\d{8}").IsMatch(input) || new Regex(@"^(1[012345678]\d{8}|1[345678][0123456789]\d{8})$").IsMatch(input))
                    return true;
                return false;
            }
            public bool IsDate(object str)
            {
                DateTime dateTime = DateTime.Now;
                if (str == null || str.ToString() == "")
                    return false;
                return DateTime.TryParse(str.ToString(), out dateTime);
            }
            /// <summary>
            /// 根据传入格式判断是否为小数
            /// </summary>
            /// <param name="str"></param>
            /// <param name="formatString">18,5</param>
            /// <returns></returns>
            public bool IsNumber(string str)
            {
                if (string.IsNullOrWhiteSpace(str)) return false;
                return Regex.IsMatch(str, @"^[+-]?\d*[.]?\d*$");
            }
            public bool IsInt(object obj)
            {
                if (obj == null)
                    return false;
                bool reslut = int.TryParse(obj.ToString(), out int _number);
                return reslut;
            }
        }
    }
}