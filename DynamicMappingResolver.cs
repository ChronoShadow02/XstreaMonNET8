using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace XstreaMonNET8
{
    internal class DynamicMappingResolver : DefaultContractResolver
    {
        private readonly Dictionary<Type, Dictionary<string, string>> _memberNameToJsonNameMap;

        public DynamicMappingResolver(Dictionary<Type, Dictionary<string, string>> memberNameToJsonNameMap)
        {
            _memberNameToJsonNameMap = memberNameToJsonNameMap;
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (_memberNameToJsonNameMap.TryGetValue(member.DeclaringType!, out var mapping) &&
                mapping.TryGetValue(member.Name, out var jsonName))
            {
                property.PropertyName = jsonName;
            }

            return property;
        }
    }
}
