using yuapi.Domain.Common.Models;

namespace yuapi.Domain.InterfaceInfoAggregate.ValueObjects
{
    public sealed class InterfaceInfoId : ValueObject
    {
        public int Value { get; }
        private InterfaceInfoId(int value)
        {
            Value = value;
        }
        public static InterfaceInfoId Create(int value)
        {
            return new InterfaceInfoId(value);
        }
        public static InterfaceInfoId CreateUnique()
        {
            int uniqueId = GenerateUniqueId();
            return new InterfaceInfoId(uniqueId);
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
