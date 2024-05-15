using CapitalPlacementProj.Application.Features.UpdateQuestionInQuestionnaire;
using CapitalPlacementProj.Application.Interfaces;
using CapitalPlacementProj.Domain.Enums;

namespace CapitalPlacementProj.Endpoints.Questionnaire
{
    public class UpdateQuestionnaireEndpoint(IQuestionnaireService questionnaireService)
        : Endpoint<UpdateQuestionInQuestionnaireRequest>
    {
        public override void Configure()
        {
            Put("/questionnaire/{questionnaireId}/question/{questionId}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(
            UpdateQuestionInQuestionnaireRequest request,
            CancellationToken ct
        )
        {
            var questionnaireId = Route<string>("questionnaireId");
            var questionId = Route<string>("questionId");

            var questionnaire = await questionnaireService.GetQuestionnaireAsync(questionnaireId!);
            if (questionnaire == null)
            {
                await SendErrorsAsync(cancellation: ct);
                return;
            }

            var question = questionnaire.Questions.FirstOrDefault(q => q.Id == questionId);
            if (question == null)
            {
                await SendErrorsAsync(cancellation: ct);
                return;
            }

            question.Question = request.QuestionObjectDto.Question;
            question.QuestionType = request.QuestionObjectDto.QuestionType;
            question.Choices = request.QuestionObjectDto.Choices;
            question.MaximumChoices = request.QuestionObjectDto.MaximumChoices;

            await questionnaireService.UpdateQuestionnaireAsync(questionnaire);
            await SendOkAsync(cancellation: ct);
        }
    }

    public class UpdateQuestionInQuestionnaireValidator
        : Validator<UpdateQuestionInQuestionnaireRequest>
    {
        public UpdateQuestionInQuestionnaireValidator()
        {
            RuleFor(c => c.QuestionObjectDto.Question)
                .NotEmpty()
                .WithMessage("The question text is required!");
            RuleFor(c => c.QuestionObjectDto.QuestionType)
                .NotEmpty()
                .WithMessage("The question type is required!");

            var questionTypes = Enum.GetNames(typeof(QuestionType));

            // all questions must have a valid question type
            RuleFor(c => c.QuestionObjectDto)
                .Must(q => questionTypes.Contains(q.QuestionType))
                .WithMessage(
                    "Invalid question type! Acceptable values are: Paragraph, YesNo, Dropdown, MultipleChoice, Date, Number"
                );

            // validation
        }
    }
}
