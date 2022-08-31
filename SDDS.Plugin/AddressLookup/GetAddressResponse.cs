using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SDDS.Plugin.AddressLookup
{
    //// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    //[DataContract(Namespace = "AddressLookup")]
    //public class Address
    //{
    //    [DataMember]
    //    public string AddressLine { get; set; }
    //    [DataMember]
    //    public string BuildingNumber { get; set; }
    //    [DataMember]
    //    public string Street { get; set; }
    //    [DataMember]
    //    public string Town { get; set; }
    //    [DataMember]
    //    public string County { get; set; }
    //    [DataMember]
    //    public string Postcode { get; set; }
    //    [DataMember]
    //    public string Country { get; set; }
    //    [DataMember]
    //    public int XCoordinate { get; set; }
    //    [DataMember]
    //    public int YCoordinate { get; set; }
    //    [DataMember]
    //    public string UPRN { get; set; }
    //    [DataMember]
    //    public string Match { get; set; }
    //    [DataMember]
    //    public string MatchDescription { get; set; }
    //    [DataMember]
    //    public string Language { get; set; }
    //    [DataMember]
    //    public string SubBuildingName { get; set; }
    //    [DataMember]
    //    public string BuildingName { get; set; }
    //}
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
   // [DataContract(Namespace = "AddressLookup")]
    //public class Info
    //{
    //    [DataMember]
    //    public string id { get; set; }
    //    [DataMember]
    //    public DateTime dateTime { get; set; }
    //    [DataMember]
    //    public string method { get; set; }
    //    [DataMember]
    //    public string service { get; set; }
    //    [DataMember]
    //    public string url { get; set; }
    //    [DataMember]
    //    public string nodeID { get; set; }
    //    [DataMember]
    //    public string atomID { get; set; }
    //}
    //[DataContract(Name ="results")]
    //public class Results
    //{
    //    [DataMember(Name ="Address")]
    //    public Address Address { get; set; }
    //}
    //[DataContract(Name = "Root")]
    //public class Root
    //{
    //    [DataMember]
    //    public Header header { get; set; }
    //    [DataMember]
    //    public List<Results> results { get; set; }
    //    [DataMember]
    //    public Info _info { get; set; }
    //}


}
