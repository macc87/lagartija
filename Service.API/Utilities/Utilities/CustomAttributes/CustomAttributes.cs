using System;
using System.Linq;
using System.Web.Http;

namespace Fantasy.API.Utilities.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public sealed class EnumAuthorizeAttribute : AuthorizeAttribute
    {
        public EnumAuthorizeAttribute(params string[] userProfilesRequired)
        {
            if (!userProfilesRequired.Any())
                throw new ArgumentException("role is required");
            var roles = string.Join(",", userProfilesRequired);
            Roles = roles;
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
