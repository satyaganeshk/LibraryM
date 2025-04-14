using Newtonsoft.Json;
namespace LibraryM.DTO
{
    public class StudentModel

    {
        //[JsonIgnore]
        //[JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        //public required string UId { get; set; }

        [JsonProperty(PropertyName = "prnNumber", NullValueHandling = NullValueHandling.Ignore)]
        public required int PrnNumber { get; set; }

        [JsonProperty(PropertyName = "studentName", NullValueHandling = NullValueHandling.Ignore)]
        public required string StudentName { get; set; }

        [JsonProperty(PropertyName = "branchName", NullValueHandling = NullValueHandling.Ignore)]
        public required string BranchName { get; set; }

        [JsonProperty(PropertyName = "contactNumber", NullValueHandling = NullValueHandling.Ignore)]
        public required double ContactNumber { get; set; }

        [JsonProperty(PropertyName = "studentAddress", NullValueHandling = NullValueHandling.Ignore)]
        public required string StudentAddress { get; set; }

        [JsonProperty(PropertyName = "graduationYear", NullValueHandling = NullValueHandling.Ignore)]
        public required string GraduationYear { get; set; }

        [JsonProperty(PropertyName = "studentEmail", NullValueHandling = NullValueHandling.Ignore)]
        public required string StudentEmail { get; set; }

        [JsonProperty(PropertyName = "studentPassword", NullValueHandling = NullValueHandling.Ignore)]
        public required string StudentPassword { get; set; }

    }

    public class StudentModelUpdate

    {
        [JsonIgnore]
        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        public required string UId { get; set; }

        [JsonProperty(PropertyName = "prnNumber", NullValueHandling = NullValueHandling.Ignore)]
        public required int PrnNumber { get; set; }

        [JsonProperty(PropertyName = "studentName", NullValueHandling = NullValueHandling.Ignore)]
        public required string StudentName { get; set; }

        [JsonProperty(PropertyName = "branchName", NullValueHandling = NullValueHandling.Ignore)]
        public required string BranchName { get; set; }

        [JsonProperty(PropertyName = "contactNumber", NullValueHandling = NullValueHandling.Ignore)]
        public required double ContactNumber { get; set; }

        [JsonProperty(PropertyName = "studentAddress", NullValueHandling = NullValueHandling.Ignore)]
        public required string StudentAddress { get; set; }

        [JsonProperty(PropertyName = "graduationYear", NullValueHandling = NullValueHandling.Ignore)]
        public required string GraduationYear { get; set; }

        [JsonProperty(PropertyName = "studentEmail", NullValueHandling = NullValueHandling.Ignore)]
        public required string StudentEmail { get; set; }

        [JsonProperty(PropertyName = "studentPassword", NullValueHandling = NullValueHandling.Ignore)]
        public required string StudentPassword { get; set; }

    }
}
