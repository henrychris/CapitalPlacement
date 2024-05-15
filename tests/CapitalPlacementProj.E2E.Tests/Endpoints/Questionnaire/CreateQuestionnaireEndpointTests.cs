using CapitalPlacementProj.Application.Features.CreateQuestionnaire;
using CapitalPlacementProj.Domain.Dtos;

namespace CapitalPlacementProj.E2E.Tests.Endpoints.Questionnaire
{
    public class CreateQuestionnaireEndpointTests : EndToEndTestCase
    {
        protected override string Url => "/questionnaire";

        [Fact]
        public async Task Should_Create_Questionnaire_Successfully()
        {
            await InitialiseAsync();

            // arrange
            var req = new CreateQuestionnaireRequest
            {
                ProgramName = "Test Questionnaire!",
                ProgramDescription = "Program Description",
                PersonalInfo = new Domain.Entities.PersonalInformation
                {
                    FirstName = new Domain.Entities.PersonalInformationObject(),
                    LastName = new Domain.Entities.PersonalInformationObject(),
                    Email = new Domain.Entities.PersonalInformationObject(),

                    CurrentResidence = new Domain.Entities.ProfileInformation { IsHidden = true },
                    Gender = new Domain.Entities.ProfileInformation(),
                    IdNumber = new Domain.Entities.ProfileInformation(),
                    Nationality = new Domain.Entities.ProfileInformation(),
                    Phone = new Domain.Entities.ProfileInformation(),
                    DateOfBirth = new Domain.Entities.ProfileInformation(),
                },

                Questions =
                [
                    new QuestionObjectDto
                    {
                        Question = "How are you doing?",
                        QuestionType = "Paragraph"
                    },
                    new QuestionObjectDto { Question = "How old are you?", QuestionType = "Number" }
                ]
            };

            // Act
            var response = await Client.PostAsJsonAsync(Url, req);
            var body = await response.Content.ReadFromJsonAsync<CreateQuestionnaireResponse>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            body.Should().NotBeNull();
        }

        [Fact]
        public async Task Create_Questionnaire_Should_Fail_No_Questions_Added()
        {
            await InitialiseAsync();

            // arrange
            var req = new CreateQuestionnaireRequest
            {
                ProgramName = "Test Questionnaire!",
                ProgramDescription = "Program Description",
                PersonalInfo = new Domain.Entities.PersonalInformation
                {
                    FirstName = new Domain.Entities.PersonalInformationObject(),
                    LastName = new Domain.Entities.PersonalInformationObject(),
                    Email = new Domain.Entities.PersonalInformationObject(),

                    CurrentResidence = new Domain.Entities.ProfileInformation { IsHidden = true },
                    Gender = new Domain.Entities.ProfileInformation(),
                    IdNumber = new Domain.Entities.ProfileInformation(),
                    Nationality = new Domain.Entities.ProfileInformation(),
                    Phone = new Domain.Entities.ProfileInformation(),
                    DateOfBirth = new Domain.Entities.ProfileInformation(),
                }
            };

            // Act
            var response = await Client.PostAsJsonAsync(Url, req);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Fact]
        public async Task Create_Questionnaire_Should_Fail_Invalid_Question_Type()
        {
            await InitialiseAsync();

            // arrange
            var req = new CreateQuestionnaireRequest
            {
                ProgramName = "Test Questionnaire!",
                ProgramDescription = "Program Description",
                PersonalInfo = new Domain.Entities.PersonalInformation
                {
                    FirstName = new Domain.Entities.PersonalInformationObject(),
                    LastName = new Domain.Entities.PersonalInformationObject(),
                    Email = new Domain.Entities.PersonalInformationObject(),

                    CurrentResidence = new Domain.Entities.ProfileInformation { IsHidden = true },
                    Gender = new Domain.Entities.ProfileInformation(),
                    IdNumber = new Domain.Entities.ProfileInformation(),
                    Nationality = new Domain.Entities.ProfileInformation(),
                    Phone = new Domain.Entities.ProfileInformation(),
                    DateOfBirth = new Domain.Entities.ProfileInformation(),
                },

                Questions =
                [
                    new QuestionObjectDto { Question = "How are you doing?", QuestionType = "Text" }
                ]
            };

            // Act
            var response = await Client.PostAsJsonAsync(Url, req);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }
    }
}
