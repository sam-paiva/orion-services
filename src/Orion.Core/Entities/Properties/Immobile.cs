using Orion.Core.Base;
using Orion.Core.Entities.Users;

namespace Orion.Core.Entities.Properties
{
    public class Immobile : Entity
    {
        public Immobile(string title, string description, string address, string[] photosUrl, decimal price, AcquisitionType acquisitionType, ImmobileType immobileType)
        {
            Title = title;
            Description = description;
            Address = address;
            PhotosUrl = photosUrl;
            Price = price;
            AcquisitionType = acquisitionType;
            ImmobileType = immobileType;
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Address { get; private set; }
        public string[] PhotosUrl { get; private set; }
        public Decimal Price { get; private set; }
        public AcquisitionType AcquisitionType { get; private set; }
        public ImmobileType ImmobileType { get; private set; }
        public User UserOwner { get; }
        public Guid UserId { get; }
    }
}
