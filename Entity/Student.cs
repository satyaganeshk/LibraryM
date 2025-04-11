﻿// Renaming the class to avoid conflict with the existing 'Student' class in the same namespace.
using Newtonsoft.Json;

namespace LibraryM.Entity
{
    public class StudentEntity
    {
        [JsonProperty(PropertyName = "active", NullValueHandling = NullValueHandling.Ignore)]
        public bool Active { get; set; }

        [JsonProperty(PropertyName = "archieved", NullValueHandling = NullValueHandling.Ignore)]
        public bool Archieved { get; set; }

        [JsonProperty(PropertyName = "version", NullValueHandling = NullValueHandling.Ignore)]
        public int Version { get; set; }

        [JsonProperty(PropertyName = "prnNumber", NullValueHandling = NullValueHandling.Ignore)]
        public int PrnNumber { get; set; }

        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        public string UId { get; set; }

        [JsonProperty(PropertyName = "studentName", NullValueHandling = NullValueHandling.Ignore)]
        public string StudentName { get; set; }

        [JsonProperty(PropertyName = "branchName", NullValueHandling = NullValueHandling.Ignore)]
        public string BranchName { get; set; }

        [JsonProperty(PropertyName = "contactNumber", NullValueHandling = NullValueHandling.Ignore)]
        public double ContactNumber { get; set; }

        [JsonProperty(PropertyName = "studentAddress", NullValueHandling = NullValueHandling.Ignore)]
        public string StudentAddress { get; set; }

        [JsonProperty(PropertyName = "graduationYear", NullValueHandling = NullValueHandling.Ignore)]
        public string GraduationYear { get; set; }

        [JsonProperty(PropertyName = "studentEmail", NullValueHandling = NullValueHandling.Ignore)]
        public string StudentEmail { get; set; }

        [JsonProperty(PropertyName = "studentPassword", NullValueHandling = NullValueHandling.Ignore)]
        public string StudentPassword { get; set; }


        [JsonProperty(PropertyName = "updatedOn", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime UpdatedOn { get; set; }


        [JsonProperty(PropertyName = "dType", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentType { get; set; }


        [JsonProperty(PropertyName = "createdOn", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime CreatedOn { get; set; }
    }
}
