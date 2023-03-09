using System;
namespace WebAdmin.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public abstract class BaseAttribute : System.Attribute
    {
        public virtual string error { get; set; }
        public abstract bool Validate(object value);
    }
}