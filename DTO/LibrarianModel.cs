using Newtonsoft.Json;

namespace LibraryM.DTO
{
    public class LibrarianModel
    {
        [JsonProperty(PropertyName = "Name", NullValueHandling = NullValueHandling.Ignore)]
        public required string Name { get; set; }

        [JsonProperty(PropertyName = "mobileNo", NullValueHandling = NullValueHandling.Ignore)]
        public required double MobileNo { get; set; }

        [JsonProperty(PropertyName = "emailId", NullValueHandling = NullValueHandling.Ignore)]
        public required string EmailId { get; set; }

        [JsonProperty(PropertyName = "password", NullValueHandling = NullValueHandling.Ignore)]
        public required string Password { get; set; }

        [JsonProperty(PropertyName = "address", NullValueHandling = NullValueHandling.Ignore)]
        public required string Address { get; set; }

        


    }

    public class LibrarianUpdateModel
    {
        public string UId { get; set; }               // Required for finding the record
        public string Name { get; set; }
        public double MobileNo { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
    }



}