//namespace AutoResponse.Core.Errors
//{
//    using System;

//    public class EntityValidationError
//    {
//        public EntityValidationError(
//            string entityType, 
//            string propertyName, 
//            EntityValidationErrorCode code, 
//            string message = null)
//        {
//            if (string.IsNullOrWhiteSpace(entityType))
//            {
//                throw new ArgumentNullException(nameof(entityType));
//            }

//            if (string.IsNullOrWhiteSpace(propertyName))
//            {
//                throw new ArgumentNullException(nameof(propertyName));
//            }

//            if (code == EntityValidationErrorCode.None)
//            {
//                throw new ArgumentNullException(nameof(code));
//            }

//            this.EntityType = entityType;
//            this.EntityProperty = propertyName;
//            this.Code = code;
//            this.Message = message;
//        }

//        public string EntityType { get; }

//        public string EntityProperty { get; }

//        public EntityValidationErrorCode Code { get; }

//        public string Message { get; }
//    }
//}