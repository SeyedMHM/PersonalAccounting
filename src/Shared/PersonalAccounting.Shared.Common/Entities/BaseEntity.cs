namespace PersonalAccounting.Shared.Common.Entites
{
    public interface IEntity
    {
    }
    
    public abstract class BaseEntity<KeyType> : IEntity
    {
        public KeyType Id { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<int>
    {
    }
}