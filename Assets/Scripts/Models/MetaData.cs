namespace Models
{
    public class Address
    {
        public string HouseNumber { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
    }

    public class MetaData
    {
        public string gebouwnummer { get; set; }
        public Address Address { get; set; }
        public string hoogste_bouwlaag { get; set; }
        public string laagste_bouwlaag { get; set; }
        public string yearOfConstruction { get; set; }
        public string deviation { get; set; }
        public string creationDate { get; set; }
        public string begingeldigheid { get; set; }
        public string typeOmschr { get; set; }
        public double measuredHeight { get; set; }
        public string statusOmschr { get; set; }
        public string aantalBouwlagen { get; set; }
        public string avineonStatus { get; set; }
    }
}