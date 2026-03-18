using MongoDB.Bson.Serialization.Attributes;

namespace Hoya.Inventory.Domain.Entities
{
    public abstract class AggregateRoot
    {
        [BsonId]
        public string Id { get; protected set; }

        public bool IsActive { get; private set; } = true;
        public bool IsDeleted { get; private set; } = false;

        public DateTime CreatedAt { get; private set; }
        public string? CreatedBy { get; private set; }

        public DateTime? UpdatedAt { get; private set; }
        public string? UpdatedBy { get; private set; }

        public DateTime? DeletedAt { get; private set; }
        public string? DeletedBy { get; private set; }

        protected AggregateRoot()
        {
            // Required for MongoDB deserialization
        }

        protected AggregateRoot(string userId)
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.UtcNow;
            CreatedBy = userId;
        }

        public void SetCreated(string userId)
        {
            CreatedBy = userId;
        }

        public void MarkUpdated(string userId)
        {
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = userId;
        }

        public void Delete(string userId)
        {
            IsDeleted = true;
            IsActive = false;
            DeletedAt = DateTime.UtcNow;
            DeletedBy = userId;
        }

        public void Activate(string userId)
        {
            IsActive = true;
            MarkUpdated(userId);
        }

        public void Deactivate(string userId)
        {
            IsActive = false;
            MarkUpdated(userId);
        }
    }

}
