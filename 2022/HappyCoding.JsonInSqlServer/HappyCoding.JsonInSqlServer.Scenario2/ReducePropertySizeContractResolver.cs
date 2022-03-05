using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HappyCoding.JsonInSqlServer.JsonModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HappyCoding.JsonInSqlServer.Scenario2
{
    public class ReducePropertySizeContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var result = base.CreateProperty(member, memberSerialization);

            var attrib = member.GetCustomAttribute<PropertyShortNameAttribute>();
            if (attrib != null)
            {
                result.PropertyName = attrib.ShortName;
            }

            return result;
        }
    }
}
