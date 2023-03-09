using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebAdmin.Framework.Attributes;

namespace WebAdmin.Framework.Extentions
{
    public static partial class Extention
    {
        public static string Validate<T>(this T t)
        {
            Type type = t.GetType();
            PropertyInfo[] propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);//获取所有属性
            List<string> errorList = new List<string>();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.IsDefined(typeof(BaseAttribute)))//如果属性上有定义该属性,此步没有构造出实例
                {
                    foreach (BaseAttribute attribute in propertyInfo.GetCustomAttributes(typeof(BaseAttribute)))
                    {
                        if (!attribute.Validate(propertyInfo.GetValue(t, null)))
                            errorList.Add(attribute.error);
                    }
                }
            }
            return string.Join(",", errorList);
        }
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(this object value)
        {
            if (value!=null&& !string.IsNullOrWhiteSpace(value.ToString()))
                return false;
            else
                return true;
        }
    }
}
