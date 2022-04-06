using Orion.Core.Base;

namespace Orion.Core.Entities.Properties
{
    public class Address : Entity
    {
        public Address(string city, string district, string state)
        {
            City = city;
            District = district;
            State = state;
        }

        public Address() {}

        public string City { get; private set; }
        public string District { get; private set; }
        public string State { get; private set; }
        public virtual Immobile? Immobile { get; }
        public Guid ImmobileId { get;}

        public override bool IsValid()
        {
            return ValidationResult.IsValid;
        }

        internal void UpdateAddress(string city, string district, string state)
        {
            City = city;
            District = district;
            State = state;
        }
    }
}
