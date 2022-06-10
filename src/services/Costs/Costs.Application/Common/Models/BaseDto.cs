namespace Costs.Application.Common.Models
{
    public abstract class BaseDto<KeyType> 
    {
        public KeyType Id { get; set; }
    }

    public abstract class BaseDto : BaseDto<int>
    {
    }
}
