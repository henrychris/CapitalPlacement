using CapitalPlacementProj.Application.Interfaces;

namespace CapitalPlacementProj.Endpoints.Init
{
    public class InitializeEndpoint(IQuestionnaireService questionnaireService)
        : EndpointWithoutRequest
    {
        public override void Configure()
        {
            Post("/initialise");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            await questionnaireService.InitializeAsync();
            await SendOkAsync(ct);
        }
    }
}
