using CapitalPlacementProj.Application.Features.CreateQuestionnaire;
using CapitalPlacementProj.Application.Interfaces;
using CapitalPlacementProj.Domain.Enums;

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

    public class CreateQuestionnaireValidator : Validator<CreateQuestionnaireRequest>
    {
        public CreateQuestionnaireValidator()
        {
            RuleFor(c => c.PersonalInfo).NotEmpty();
            RuleFor(c => c.PersonalInfo.FirstName).NotEmpty();
            RuleFor(c => c.PersonalInfo.LastName).NotEmpty();
            RuleFor(c => c.PersonalInfo.Email).NotEmpty();

            RuleFor(c => c.ProgramName).NotEmpty();
            RuleFor(c => c.ProgramDescription).NotEmpty();

            RuleFor(c => c.Questions.Count)
                .NotEmpty()
                //.GreaterThanOrEqualTo(1)
                .WithMessage("You must add at least one question.");

            var questionTypes = Enum.GetNames(typeof(QuestionType));

            // all questions must have a valid question type
            RuleForEach(c => c.Questions)
                .Must(q => questionTypes.Contains(q.QuestionType))
                .WithMessage(
                    "Invalid question type! Acceptable values are: Paragraph, YesNo, Dropdown, MultipleChoice, Date, Number. Question types are CASE SENSITIVE!"
                );

            // validation
        }
    }
}
