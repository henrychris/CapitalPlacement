using CapitalPlacementProj.Application.Features.GetQuestionnaire;
using CapitalPlacementProj.Application.Interfaces;

namespace CapitalPlacementProj.Endpoints.Questionnaire
{
    public class GetQuestionnaireEndpoint(IQuestionnaireService questionnaireService)
        : EndpointWithoutRequest<GetQuestionnaireResponse>
    {
        public override void Configure()
        {
            Get("/questionnaire/{questionnaireId}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var questionnaireId = Route<string>("questionnaireId");
            GetQuestionnaireResponse? questionnaire =
                await questionnaireService.GetQuestionnaireAsync(questionnaireId!);

            if (questionnaire is null)
            {
                await SendErrorsAsync(cancellation: ct);
                return;
            }

            await SendOkAsync(questionnaire, ct);
        }
    }
}
