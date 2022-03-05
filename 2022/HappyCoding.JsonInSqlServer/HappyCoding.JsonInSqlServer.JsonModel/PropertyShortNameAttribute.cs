using System;

namespace HappyCoding.JsonInSqlServer.JsonModel
{
    public class PropertyShortNameAttribute : Attribute
    {
        public string ShortName { get; }

        public PropertyShortNameAttribute(string shortName)
        {
            this.ShortName = shortName;
        }
    }
}
