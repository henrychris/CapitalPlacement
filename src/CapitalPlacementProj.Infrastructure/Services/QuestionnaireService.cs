using CapitalPlacementProj.Application.Features.CreateQuestionnaire;
using CapitalPlacementProj.Application.Interfaces;
using CapitalPlacementProj.Application.Interfaces.Repositories;
using CapitalPlacementProj.Domain.Entities;
using CapitalPlacementProj.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace CapitalPlacementProj.Infrastructure.Services
{
    public class QuestionnaireService(
        IQuestionnaireRepository questionnaireRepository,
        ILogger<QuestionnaireService> logger
    ) : IQuestionnaireService
    {
        public async Task InitializeAsync()
        {
            await questionnaireRepository.InitializeAsync();
        }

        public async Task<Questionnaire?> CreateQuestionnaireAsync(
            CreateQuestionnaireRequest request
        )
        {
            var questionObjects = request
                .Questions.Select(q => new QuestionObject
                {
                    Question = q.Question,
                    QuestionType = ConvertToEnum(q.QuestionType),
                    Choices = q.Choices,
                    MaximumChoices = q.MaximumChoices,
                })
                .ToList();

            var survey = new Questionnaire
            {
                ProgramName = request.ProgramName,
                ProgramDescription = request.ProgramDescription,
                PersonalInformation = request.PersonalInfo,
                Questions = questionObjects
            };

            return await questionnaireRepository.CreateQuestionnaireAsync(survey);
        }

        private static QuestionType ConvertToEnum(string questionType)
        {
            return questionType switch
            {
                "Paragraph" => QuestionType.Paragraph,
                "YesNo" => QuestionType.YesNo,
                "Dropdown" => QuestionType.Dropdown,
                "MultipleChoice" => QuestionType.MultipleChoice,
                "Date" => QuestionType.Date,
                "Number" => QuestionType.Number,
                _ => throw new ArgumentException("Invalid question type")
            };
        }
    }
}
