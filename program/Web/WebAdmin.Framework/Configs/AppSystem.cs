using System.Collections.Generic;

namespace WebAdmin.Framework.Configs
{
    public class AppSystem
    {
        public IList<AppSettings> AppSettings { get; set; }
    }
    public class AppSettings
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
