using System;
using System.Collections.Generic;

namespace Attributes.TerrainLoading
{
    public class Geometrie
    {
        public string type { get; set; }
        public List<List<List<double>>> coordinates { get; set; }
    }

    public class Links
    {
        public Self self { get; set; }
    }

    public class Pand
    {
        public string identificatie { get; set; }
        public string domein { get; set; }
        public Geometrie geometrie { get; set; }
        public string oorspronkelijkBouwjaar { get; set; }
        public string status { get; set; }
        public string geconstateerd { get; set; }
        public string documentdatum { get; set; }
        public string documentnummer { get; set; }
        public Voorkomen voorkomen { get; set; }
    }

    public class BagData
    {
        public Pand pand { get; set; }
        public Links _links { get; set; }
    }

    public class Self
    {
        public string href { get; set; }
    }

    public class Voorkomen
    {
        public DateTime tijdstipRegistratie { get; set; }
        public int versie { get; set; }
        public string beginGeldigheid { get; set; }
        public DateTime tijdstipRegistratieLV { get; set; }
    }
}