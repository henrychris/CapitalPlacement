using CapitalPlacementProj.Endpoints.Index;

namespace CapitalPlacementProj.E2E.Tests.Endpoints;

public class IndexEndpointTests : EndToEndTestCase
{
    protected override string Url => "/";

    [Fact]
    public async Task Should_Get_Information_Successfully()
    {
        // Arrange

        // Act
        var response = await Client.GetAsync(Url);
        var body = await response.Content.ReadFromJsonAsync<IndexResponse>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        body.Should().NotBeNull();
        body!.Message.Should().Be("Hello Fast Endpoints");
    }
}
