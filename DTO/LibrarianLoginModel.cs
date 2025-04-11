using Newtonsoft.Json;
namespace LibraryM.DTO
{
    public class LibrarianLoginModel
    {
        [JsonProperty(PropertyName = "uid", NullValueHandling = NullValueHandling.Ignore)]
        public required string UId { get; set; }

        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public required string Email { get; set; }

        [JsonProperty(PropertyName = "password", NullValueHandling = NullValueHandling.Ignore)]
        public required string Password { get; set; }

    }
}
