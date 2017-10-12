using System;
using System.Linq;
using System.Web.Http;

namespace Utilities.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public sealed class EnumAuthorizeAttribute : AuthorizeAttribute
    {
        public EnumAuthorizeAttribute(params string[] userProfilesRequired)
        {
            if (!userProfilesRequired.Any())
                throw new ArgumentException("role is required");
            var rolse = string.Join(",", userProfilesRequired);
            Roles = rolse;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum, Inherited = false)]
    public sealed class ModelNameAttribute : Attribute
    {
        public ModelNameAttribute(string name)
        {
            Name = name;
        }
        public string Name { get; private set; }
    }
}
