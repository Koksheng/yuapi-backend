using yuapi.Domain.Common.Models;

namespace yuapi.Domain.MenuAggregate.ValueObjects
{
    public sealed class MenuId : AggregateRootId<int>
    {
        public override int Value { get; protected set; }
        private MenuId(int value)
        {
            Value = value;
        }
        public static MenuId Create(int value)
        {
            return new MenuId(value);
        }
        public static MenuId CreateUnique()
        {
            int uniqueId = GenerateUniqueId();
            return new MenuId(uniqueId);
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        private static int GenerateUniqueId()
        {
            // Simulate an ID generation logic
            Random random = new Random();
            return random.Next(1, int.MaxValue);
        }
    }
}
