using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public static InterfaceInfoId CreateUnique()
        {
            // Assuming you have a mechanism to generate unique integers, like an auto-incremented ID from a database.
            // Here, just for example, let's simulate generating a unique integer ID.
            // This should be replaced with your actual unique ID generation logic.
            int uniqueId = GenerateUniqueId();
            return new InterfaceInfoId(uniqueId);
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        private static int GenerateUniqueId()
        {
            // This is a placeholder. Replace it with your unique integer ID generation logic.
            // For example, you might fetch this from a database sequence or an auto-increment column.
            // Here, we'll just use a static counter for demonstration purposes.

            // Simulate an ID generation logic
            Random random = new Random();
            return random.Next(1, int.MaxValue);
        }
    }

    //public sealed class InterfaceInfoId : ValueObject
    //{
    //    public Guid Value { get; }
    //    private InterfaceInfoId(Guid value)
    //    {
    //        Value = value;
    //    }
    //    public static InterfaceInfoId CreateUnique()
    //    {
    //        return new(Guid.NewGuid());
    //    }
    //    public override IEnumerable<object> GetEqualityComponents()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
