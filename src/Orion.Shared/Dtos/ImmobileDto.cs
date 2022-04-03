﻿using Orion.Core.Entities.Properties;

namespace Orion.Shared
{
    public class ImmobileDto
    {
        public Guid Id { get; set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Address { get; private set; }
        public string[] PhotosUrl { get; private set; }
        public Decimal Price { get; private set; }
        public AcquisitionType AcquisitionType { get; private set; }
        public ImmobileType ImmobileType { get; private set; }
    }
}