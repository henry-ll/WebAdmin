using System;

namespace WebAdmin.Framework.Util
{
    /// <summary>
    /// 单实例
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    public class SingleInstance<TClass> where TClass : class
    {
        /// <summary>
        /// 
        /// </summary>
        static TClass _instance { get; set; }

        static object _lock = new object();
        /// <summary>
        /// 
        /// </summary>
        public SingleInstance()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        public static TClass Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = Activator.CreateInstance(typeof(TClass)) as TClass;
                    }
                }
                return _instance;
            }
        }
    }
}
