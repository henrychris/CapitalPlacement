using CapitalPlacementProj.Application.Features.CreateQuestionnaire;
using CapitalPlacementProj.Application.Features.GetQuestionnaire;
using CapitalPlacementProj.Application.Interfaces;
using CapitalPlacementProj.Application.Interfaces.Repositories;
using CapitalPlacementProj.Domain.Dtos;
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

            var questionnaire = new Questionnaire
            {
                ProgramName = request.ProgramName,
                ProgramDescription = request.ProgramDescription,
                PersonalInformation = request.PersonalInfo,
                Questions = questionObjects
            };

            return await questionnaireRepository.CreateQuestionnaireAsync(questionnaire);
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

        public async Task<GetQuestionnaireResponse?> GetQuestionnaireAsync(string questionnaireId)
        {
            var response = await questionnaireRepository.GetQuestionnaireAsync(questionnaireId);
            if (response == null)
            {
                return null;
            }

            return new GetQuestionnaireResponse
            {
                Id = response.Id,
                PersonalInformation = response.PersonalInformation,
                ProgramDescription = response.ProgramDescription,
                ProgramName = response.ProgramName,
                Questions = response
                    .Questions.Select(q => new QuestionObjectResponseDto
                    {
                        Id = q.Id,
                        Choices = q.Choices,
                        MaximumChoices = q.MaximumChoices,
                        Question = q.Question,
                        QuestionType = q.QuestionType.ToFriendlyString()
                    })
                    .ToList()
            };
        }
    }
}
