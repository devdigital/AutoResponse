//namespace AutoResponse.Core.Formatters
//{
//    using System;
//    using System.Collections.Generic;

// TODO: reinstate and turn to abstract that can map from types to strings
//    using AutoResponse.Core.ApiEvents;

//    public class AutoResponseApiEventCodeFormatter : IApiEventCodeFormatter
//    {
//        private readonly Dictionary<Type, string> maps;

//        public AutoResponseApiEventCodeFormatter()
//        {
//            this.maps = new Dictionary<Type, string>
//            {
//                { typeof(EntityCreatedApiEvent), "403" },
//                { typeof(EntityNotFoundApiEvent), "404" },
//                { typeof(EntityNotFoundQueryApiEvent), "404" },
//                { typeof(EntityPermissionApiEvent), "403" },
//                { typeof(EntityValidationApiEvent), "422" },
//                { typeof(ServiceErrorApiEvent), "500" },
//                { typeof(UnauthenticatedApiEvent), "401" }
//            };
//        }

//        public string GetCode(object apiEvent)
//        {
//            if (apiEvent == null)
//            {
//                throw new ArgumentNullException(nameof(apiEvent));
//            }

//            var apiEventType = apiEvent.GetType();
//            if (!this.maps.ContainsKey(apiEventType))
//            {
//                return "AR500";
//            }

//            var code = this.maps[apiEventType];
//            return $"AR{code}";
//        }
//    }
//}