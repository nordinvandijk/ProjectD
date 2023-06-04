using System;

namespace Attributes.Player
{
    public class Building
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Lon { get; set; }
        public double Lat { get; set; }
        public string ImageUrl { get; set; }
    }
}