using System.Runtime.Serialization;

namespace LogicClient.OTD
{ 
    [DataContract]
    public class PizzaOTD
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public float price { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public float discount { get; set; }
        [DataMember]
        public string image { get; set; }
    }
}
