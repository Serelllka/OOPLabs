using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BackupsExtra.Tools
{
    public class PrivateContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);
            if (!property.Writable)
            {
                var info = member as PropertyInfo;
                bool hasPrivateSetter = info?.GetSetMethod(true) != null;
                property.Writable = hasPrivateSetter;
            }

            return property;
        }
    }
}