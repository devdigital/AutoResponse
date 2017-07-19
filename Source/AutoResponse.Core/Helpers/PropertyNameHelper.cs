namespace AutoResponse.Core.Helpers
{
    using System;
    using System.Linq.Expressions;

    public static class PropertyNameHelper
    {
        public static string PropertyName<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> property)
        {
            var lambda = (LambdaExpression)property;

            MemberExpression memberExpression;
            var body = lambda.Body as UnaryExpression;

            if (body != null)
            {
                var unaryExpression = body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambda.Body;
            }

            return memberExpression.Member.Name;
        }
    }
}