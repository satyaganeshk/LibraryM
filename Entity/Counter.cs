using Newtonsoft.Json;

public class Counter
{
    [JsonProperty("id")] 
    public string Id { get; set; }

    [JsonProperty("DocumentType")]
    public string DocumentType { get; set; } = "counter";

    [JsonProperty("value")]
    public int Value { get; set; }
}
