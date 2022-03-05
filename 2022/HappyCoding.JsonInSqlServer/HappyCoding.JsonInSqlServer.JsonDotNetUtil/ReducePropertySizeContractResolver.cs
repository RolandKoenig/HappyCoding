using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HappyCoding.JsonInSqlServer.JsonDotNetUtil
{
    public class ReducePropertySizeContractResolver : DefaultContractResolver
    {
        private readonly Dictionary<MemberInfo, string> _propertyNameMapping;

        public ReducePropertySizeContractResolver()
        {
            _propertyNameMapping = new Dictionary<MemberInfo, string>();
        }

        public TypePropertyMapper<T> MapPropertyForType<T>()
        {
            return new TypePropertyMapper<T>(_propertyNameMapping);
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var result = base.CreateProperty(member, memberSerialization);

            if (_propertyNameMapping.TryGetValue(member, out var mappedName))
            {
                result.PropertyName = mappedName;
            }
            else
            {
                throw new InvalidOperationException(
                    $"No mapping found for member '{member.Name}' from type '{member.DeclaringType?.FullName}'!");
            }

            return result;
        }
    }
}
