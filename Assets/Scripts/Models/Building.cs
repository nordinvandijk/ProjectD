using System;

namespace Models
{
    public class Building
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string BagId { get; set; }
    }
}