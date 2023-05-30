using System;
using UnityEngine;



namespace Attributes.Player
{
    public class Building
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Lon { get; set; }
        public double Lat { get; set; }
        public string? Image_url { get; set; }
        public UnityEngine.Texture2D? Image { get; set; }
    }
}