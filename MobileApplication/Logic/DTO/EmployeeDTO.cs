using System.Runtime.Serialization;

namespace Logic.DTO
{
    [DataContract]
    public class EmployeeDTO
    {
        [DataMember]
        public string name { get; set; }
    }
}
