using CapitalPlacementProj.Application.Features.AnswerQuestionnaire;
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
        IQuestionnaireResponseRepository questionnaireResponseRepository,
        ILogger<QuestionnaireService> logger
    ) : IQuestionnaireService
    {
        public async Task InitializeAsync()
        {
            await questionnaireRepository.InitializeAsync();
            await questionnaireResponseRepository.InitializeAsync();
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

        public async Task UpdateQuestionnaireAsync(GetQuestionnaireResponse questionnaire)
        {
            var updatedQuestionnaire = new Questionnaire
            {
                Id = questionnaire.Id,
                PersonalInformation = questionnaire.PersonalInformation,
                ProgramDescription = questionnaire.ProgramDescription,
                ProgramName = questionnaire.ProgramName,
                Questions = questionnaire
                    .Questions.Select(q => new QuestionObject
                    {
                        Id = q.Id,
                        Choices = q.Choices,
                        MaximumChoices = q.MaximumChoices,
                        Question = q.Question,
                        QuestionType = ConvertToEnum(q.QuestionType)
                    })
                    .ToList()
            };

            await questionnaireRepository.UpdateQuestionnaireAsync(updatedQuestionnaire);
        }

        public async Task SaveQuestionnaireResponseAsync(
            AnswerQuestionnaireRequest request,
            GetQuestionnaireResponse questionnaire,
            string questionnaireId
        )
        {
            // fetch the question from the questionnaire using its id.
            // get the question type
            // convert the question to its relevant value

            foreach (var item in request.Responses)
            {
                var question = questionnaire.Questions.FirstOrDefault(x => x.Id == item.QuestionId);
                if (question is null)
                {
                    continue;
                }

                if (
                    question.QuestionType == QuestionType.MultipleChoice.ToString()
                    || question.QuestionType == QuestionType.Dropdown.ToString()
                )
                {
                    if (item.Answer is string[] || item.Answer is List<string>)
                    {
                        continue;
                    }
                    else if (item.Answer is string)
                    {
                        // Convert the string answer to a string array with a single element
                        item.Answer = new string[] { (string)item.Answer };
                    }
                    else if (item.Answer == null)
                    {
                        // Handle the case where the answer is null
                        item.Answer = Array.Empty<string>(); // Empty string array or list
                    }
                    else
                    {
                        // Handle invalid answer type
                        // You may throw an exception or handle it based on your requirements
                        // For now, setting it to "N/A" as a string
                        item.Answer = item.Answer.ToString() ?? "N/A";
                    }
                }
                else if (question.QuestionType == QuestionType.Paragraph.ToString())
                {
                    item.Answer = item.Answer?.ToString() ?? "N/A";
                }
                else if (question.QuestionType == QuestionType.Number.ToString())
                {
                    if (int.TryParse(item.Answer?.ToString(), out int ans))
                    {
                        item.Answer = ans;
                        continue;
                    }
                    item.Answer = "N/A";
                }
                else if (question.QuestionType == QuestionType.Date.ToString())
                {
                    if (DateTime.TryParse(item?.Answer?.ToString(), out DateTime ans))
                    {
                        item.Answer = ans;
                        continue;
                    }

                    item.Answer = "N/A";
                }
            }
            // convert objects to their actual values

            var questionnaireResponse = new QuestionnaireResponse
            {
                QuestionnaireId = questionnaireId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                CurrentResidence = request.CurrentResidence,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                IdNumber = request.IdNumber,
                Nationality = request.Nationality,
                Phone = request.Phone,
                UserId = request.UserId,
                Responses = request.Responses
            };

            await questionnaireResponseRepository.SaveQuestionnaireResponseAsync(
                questionnaireResponse
            );
        }
    }
}
