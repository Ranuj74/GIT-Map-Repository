using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MapAssignment.Models
{
    [Serializable]
    public class Address
    {
        public string Unit { get; set; }
        public string Street { get; set; }
        public string Town { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string headers { get; set; }
    }

    public class BasicAddress
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public List<string> types { get; set; }
    }
    public class AddressComponents
    {
         public List<BasicAddress> address_components { get; set; }
        public Geometry geometry {get; set;}
    }

    public class Result
    {
        public List<AddressComponents> results { get; set; }
    }

    public class Geometry
    {
        public LatLong location { get; set; }
    }

    public class LatLong
    {
        public string lat { get; set; }
        public string lng { get; set; }
    }
}