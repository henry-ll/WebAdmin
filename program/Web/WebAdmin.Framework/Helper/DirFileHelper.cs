using System;
using System.Data;
using System.IO;
using System.Text;

namespace WebAdmin.Framework.Helper
{
    /// <summary>
    /// 文件操作夹
    /// </summary>
    public static class DirFileHelper
    {
        /// <summary>
        /// 创建文件并写入文件内容
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="content">内容</param>
        public static void CreateFileContent(string path, string content)
        {
            FileInfo file = new FileInfo(path);
            var dic = file.Directory;
            if (!dic.Exists)
                dic.Create();
            StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8);
            sw.Write(content);
            sw.Close();
        }
    }
}
