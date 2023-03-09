using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace WebAdmin.Framework.Helper
{
    /// <summary>
    /// 数据源转换帮助类
    /// </summary>
    public class DataHelper
    {
        /// <summary>
        /// 根据条件过滤表的内容
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static DataTable DataFilter(DataTable dt, string condition)
        {
            if (IsExistRows(dt))
            {
                if (string.IsNullOrWhiteSpace(condition.Trim()))
                    return dt;
                else
                {
                    DataTable newdt = new DataTable();
                    newdt = dt.Clone();
                    DataRow[] dr = dt.Select(condition);
                    for (int i = 0; i < dr.Length; i++)
                        newdt.ImportRow(dr[i]);
                    return newdt;
                }
            }
            else
                return null;
        }
        /// <summary>
        /// 检查DataTable 是否有数据行
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static bool IsExistRows(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
                return true;
            return false;
        }
    }
}
