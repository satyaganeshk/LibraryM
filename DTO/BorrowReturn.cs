using Newtonsoft.Json;

namespace LibraryM.DTO
{
    public class BorrowReturnModel
    {
        //[JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        //public string Id { get; set; }
        //[JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        //public string UId { get; set; }
        //[JsonProperty(PropertyName = "userId", NullValueHandling = NullValueHandling.Ignore)]
        //public string UserId { get; set; }
    
        [JsonProperty(PropertyName = "bookIssue", NullValueHandling = NullValueHandling.Ignore)]
        public bool bookIssue { get; set; }


        [JsonProperty(PropertyName = "issueDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime IssueDate { get; set; }

        [JsonProperty(PropertyName = "returnBook", NullValueHandling = NullValueHandling.Ignore)]
        public bool returnBook { get; set; }

        [JsonProperty(PropertyName = "returnDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime ReturnDate { get; set; }


        [JsonProperty(PropertyName = "bookUid", NullValueHandling = NullValueHandling.Ignore)]
        public string? BookUid { get; set; }


    }
}
