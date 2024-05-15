using CapitalPlacementProj.Application.Features.AnswerQuestionnaire;
using CapitalPlacementProj.Application.Interfaces;

namespace CapitalPlacementProj.Endpoints.Questionnaire.AnswerQuestionnaire
{
    public class AnswerQuestionnaireEndpoint(IQuestionnaireService questionnaireService)
        : Endpoint<AnswerQuestionnaireRequest>
    {
        public override void Configure()
        {
            Post("/questionnaire/{questionnaireId}/answer");
            AllowAnonymous();
        }

        public override async Task HandleAsync(
            AnswerQuestionnaireRequest request,
            CancellationToken ct
        )
        {
            var questionnaireId = Route<string>("questionnaireId");
            var questionnaire = await questionnaireService.GetQuestionnaireAsync(questionnaireId!);
            if (questionnaire == null)
            {
                await SendErrorsAsync(cancellation: ct);
                return;
            }

            await questionnaireService.SaveQuestionnaireResponseAsync(
                request,
                questionnaire,
                questionnaireId!
            );
            await SendOkAsync(cancellation: ct);
        }
    }
}
