[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace CapitalPlacementProj.E2E.Tests;

public abstract class EndToEndTestCase : IAsyncDisposable
{
    protected readonly WebApplicationFactory<Program> Application;
    protected readonly HttpClient Client;
    protected readonly string DatabaseName;
    protected abstract string Url { get; }

    protected EndToEndTestCase()
    {
        var random = new Random();

        DatabaseName = $"{DatabaseName}_{random.Next()}";

        Application = new WebApplicationFactory<Program>();

        Application = Application.WithWebHostBuilder(hostBuilder =>
        {
            hostBuilder.ConfigureServices(services => { });
        });

        Client = Application.CreateClient();
    }

    protected async Task InitialiseAsync()
    {
        await Client.PostAsync("/initialise", null);
    }

    public async ValueTask DisposeAsync()
    {
        await Application.DisposeAsync();
    }
}
