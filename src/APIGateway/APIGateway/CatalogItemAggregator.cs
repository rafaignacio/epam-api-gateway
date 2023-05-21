using Ocelot.Middleware;
using Ocelot.Multiplexer;

namespace APIGateway;

public class CatalogItemAggregator : IDefinedAggregator
{
    public Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
    {
        var catalogItemResponse = responses.Single(r => r.Request.Path.Value.Contains("/catalog/items")).Response;

        return Task.FromResult<DownstreamResponse>(null);
    }
}
