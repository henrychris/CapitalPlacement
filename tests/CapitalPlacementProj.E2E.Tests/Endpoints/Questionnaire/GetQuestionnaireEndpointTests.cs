using CapitalPlacementProj.Application.Features.CreateQuestionnaire;
using CapitalPlacementProj.Application.Features.GetQuestionnaire;
using CapitalPlacementProj.Domain.Dtos;

namespace CapitalPlacementProj.E2E.Tests.Endpoints.Questionnaire
{
    public class GetQuestionnaireEndpointTests : EndToEndTestCase
    {
        protected override string Url => "/questionnaire";

        [Fact]
        public async Task Should_Get_Questionnaire_Successfully()
        {
            await InitialiseAsync();

            // Arrange
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

            var response = await Client.PostAsJsonAsync(Url, req);
            var body = await response.Content.ReadFromJsonAsync<CreateQuestionnaireResponse>();

            // Act
            var getResponse = await Client.GetAsync(Url + $"/{body!.QuestionnaireId}");
            var getBody = await getResponse.Content.ReadFromJsonAsync<GetQuestionnaireResponse>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            body.Should().NotBeNull();

            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            getBody.Should().NotBeNull();

            getBody!.Id.Should().Be(body.QuestionnaireId);
            getBody!.ProgramName.Should().Be(req.ProgramName);
            getBody!.ProgramDescription.Should().Be(req.ProgramDescription);
        }
    }
}
