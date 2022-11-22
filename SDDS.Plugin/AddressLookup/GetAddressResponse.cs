using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SDDS.Plugin.AddressLookup
{
    [DataContract(Name = "header")]
    public class header
    {
        [DataMember]
        //public string query { get; set; }
        //[DataMember]
        public string offset { get; set; }
        [DataMember]
        public string totalresults { get; set; }
        [DataMember]
        public string format { get; set; }
        [DataMember]
        public string dataset { get; set; }
        [DataMember]
        public string lr { get; set; }
        [DataMember]
        public string maxresults { get; set; }
        [DataMember]
        public string matching_totalresults { get; set; }
    }


    public class Rootobject
    {
        public Header header { get; set; }
        public Result[] results { get; set; }
    }

    public class Header
    {
        public object uri { get; set; }
        public string query { get; set; }
        public int offset { get; set; }
        public int totalresults { get; set; }
        public string format { get; set; }
        public string dataset { get; set; }
        public string lr { get; set; }
        public int maxresults { get; set; }
    }

    public class Result
    {
        public Address address { get; set; }
    }

    public class Address
    {
        public string addressLine { get; set; }
        public string subBuildingName { get; set; }
        public object buildingName { get; set; }
        public string buildingNumber { get; set; }
        public string street { get; set; }
        public object locality { get; set; }
        public object dependentLocality { get; set; }
        public string town { get; set; }
        public string county { get; set; }
        public string postcode { get; set; }
        public string country { get; set; }
        public float xCoordinate { get; set; }
        public float yCoordinate { get; set; }
        public string uprn { get; set; }
        public float match { get; set; }
        public string matchDescription { get; set; }
        public string language { get; set; }
    }

}