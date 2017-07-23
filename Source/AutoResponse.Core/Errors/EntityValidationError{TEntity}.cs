//namespace AutoResponse.Core.Errors
//{
//    using System;
//    using System.Linq.Expressions;

//    using AutoResponse.Core.Helpers;

//    public class EntityValidationError<TEntity> : EntityValidationError
//    {
//        public EntityValidationError(Expression<Func<TEntity, object>> property, EntityValidationErrorCode code, string message = null)
//            : base(typeof(TEntity).Name, PropertyNameHelper.PropertyName(property), code, message)
//        {
//        }
//    }
//}