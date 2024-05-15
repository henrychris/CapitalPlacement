using CapitalPlacementProj.Application.Features.CreateQuestionnaire;
using CapitalPlacementProj.Application.Interfaces;

namespace CapitalPlacementProj.Endpoints.Questionnaire
{
    public class CreateQuestionnaireEndpoint(IQuestionnaireService questionnaireService)
        : Endpoint<CreateQuestionnaireRequest, CreateQuestionnaireResponse>
    {
        public override void Configure()
        {
            Post("/questionnaire");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateQuestionnaireRequest req, CancellationToken ct)
        {
            var response = await questionnaireService.CreateQuestionnaireAsync(req);
            if (response is null)
            {
                await SendErrorsAsync(cancellation: ct);
                return;
            }

            await SendOkAsync(
                new CreateQuestionnaireResponse { QuestionnaireId = response.Id },
                ct
            );
        }
    }
}
