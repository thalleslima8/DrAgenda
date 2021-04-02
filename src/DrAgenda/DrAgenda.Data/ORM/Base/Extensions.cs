using FluentNHibernate.Mapping;

namespace Codtran.Data.ORM.Base
{
    public static class Extensions
    {
        public static IdentityPart Guid(this IdentityPart identityPart)
        {
            return identityPart.Access.CamelCaseField(Prefix.Underscore).GeneratedBy.Guid();
        }
    }
}
