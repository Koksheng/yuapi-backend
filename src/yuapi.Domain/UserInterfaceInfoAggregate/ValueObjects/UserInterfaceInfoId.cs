using yuapi.Domain.Common.Models;

namespace yuapi.Domain.UserInterfaceInfoAggregate.ValueObjects
{
    public sealed class UserInterfaceInfoId : AggregateRootId<int>
    {
        public override int Value { get; protected set; }
        private UserInterfaceInfoId(int value)
        {
            Value = value;
        }
        public static UserInterfaceInfoId Create(int value)
        {
            return new UserInterfaceInfoId(value);
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
