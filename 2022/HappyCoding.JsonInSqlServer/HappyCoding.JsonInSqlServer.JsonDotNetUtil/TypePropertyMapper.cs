using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace HappyCoding.JsonInSqlServer.JsonDotNetUtil
{
    public class TypePropertyMapper<TType>
    {
        private IDictionary<MemberInfo, string> _targetDict;

        internal TypePropertyMapper(IDictionary<MemberInfo, string> targetDict)
        {
            _targetDict = targetDict;
        }

        public TypePropertyMapper<TType> MapPropertyName<TProperty>(Expression<Func<TType, TProperty>> getPropertyExpression, string propertyName)
        {
            if (getPropertyExpression is not LambdaExpression lambdaExpression)
            {
                throw new ArgumentException("Unable to parse expression", nameof(getPropertyExpression));
            }
            if (lambdaExpression.Body is not MemberExpression memberExpression)
            {
                throw new ArgumentException("Unable to parse expression", nameof(getPropertyExpression));

            }

            _targetDict[memberExpression.Member] = propertyName;

            return this;
        }
    }
}
