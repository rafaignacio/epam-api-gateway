using Ocelot.Middleware;
using Ocelot.Multiplexer;
using System.Text.Json;

namespace APIGateway;

public class CatalogItemAggregator : IDefinedAggregator
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CatalogItemAggregator(IHttpClientFactory httpClientFactory) => 
        _httpClientFactory = httpClientFactory;

    public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
    {
        var itemsJson = await responses[0].Items.DownstreamResponse().Content.ReadAsStringAsync();

        var items = JsonSerializer.Deserialize<List<DetailedItemResponse>>(itemsJson);

        if (items is null)
            return new DownstreamResponse(new HttpResponseMessage(System.Net.HttpStatusCode.NotFound));

        foreach (var item in items)
            item.Properties = await GetItemProperties();

        var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        response.Content = new StringContent(JsonSerializer.Serialize(items), 
            new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));

        return new DownstreamResponse(response);
    }

    private async Task<ItemProperties> GetItemProperties()
    {
        var client = _httpClientFactory.CreateClient();
        return await client.GetFromJsonAsync<ItemProperties>("http://localhost:5002/items/1/properties");
    }
}
