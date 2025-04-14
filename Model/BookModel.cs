﻿using Newtonsoft.Json;

namespace LibraryM.DTO
{
    public class BookCreateModel
    {
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string BookType { get; set; }
    }
}

namespace LibraryM.Entity
{
    public class BookModel
    {
        //[JsonIgnore]
        //[JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        //public string Id { get; set; }

        //[JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        //public string UId { get; set; }

        [JsonProperty(PropertyName = "bookId", NullValueHandling = NullValueHandling.Ignore)]
        public string BookId { get; set; }

        [JsonProperty(PropertyName = "bookName", NullValueHandling = NullValueHandling.Ignore)]
        public string BookName { get; set; }

        [JsonProperty(PropertyName = "authorName", NullValueHandling = NullValueHandling.Ignore)]
        public string AuthorName { get; set; }
        [JsonProperty(PropertyName = "bookType", NullValueHandling = NullValueHandling.Ignore)]
        public string BookType { get; set; }

    }


}