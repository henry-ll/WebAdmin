using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAdmin.Framework.Providers
{
    public class ManyStaticFileProvider : PhysicalFileProvider
    {
        public ManyStaticFileProvider(string root, string alias) : base(root)
        {
            this.Alias = alias;
        }
        public ManyStaticFileProvider(string root, Microsoft.Extensions.FileProviders.Physical.ExclusionFilters filters, string alias) : base(root, filters)
        {
            this.Alias = alias;
        }
        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; }
    }
}
