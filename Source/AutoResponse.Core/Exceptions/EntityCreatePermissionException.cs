﻿namespace AutoResponse.Core.Exceptions
{
    using System;

    public class EntityCreatePermissionException : Exception, IEntityCreatePermissionException
    {
        public EntityCreatePermissionException(string userId, string entityType)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (string.IsNullOrWhiteSpace(entityType))
            {
                throw new ArgumentNullException(nameof(entityType));
            }

            this.UserId = userId;
            this.EntityType = entityType;
        }

        public EntityCreatePermissionException(string userId, string entityType, string entityId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (string.IsNullOrWhiteSpace(entityType))
            {
                throw new ArgumentNullException(nameof(entityType));
            }

            if (string.IsNullOrWhiteSpace(entityId))
            {
                throw new ArgumentNullException(nameof(entityId));
            }

            this.UserId = userId;
            this.EntityType = entityType;
            this.EntityId = entityId;
        }

        public string UserId { get; }

        public string EntityType { get; }

        public string EntityId { get; }
    }
}