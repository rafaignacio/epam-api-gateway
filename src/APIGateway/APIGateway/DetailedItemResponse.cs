using System.Text.Json.Serialization;

namespace APIGateway;

public class DetailedItemResponse
{
    public DetailedItemResponse() { }
    public string Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("image")]
    public string Image { get; set; }
    [JsonPropertyName("category")]
    public string Category { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("price")]
    public double Price { get; set; }
    [JsonPropertyName("amount")]
    public int Amount { get; set; }
    [JsonPropertyName("propeties")]
    public ItemProperties Properties { get; set; }
}

public class ItemProperties
{
    public ItemProperties() { }
    public string Brand { get; set; }
    public string Model { get; set; }
}
