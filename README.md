# Capital Placement Assessment 
 An assessment for a role at Capital Placement.

 # Description

 ## Why Fast Endpoints?

 This application is built using [FastEndpoints](https://fast-endpoints.com/). It is a project I use whenever I need to quickly bootstrap & build an application. This exact application was built using a template I built for situations like this. I could have built this using full-fat controllers or minimal apis, but chose this cause it fit the situation & saved me a bunch of setup.

 ## Application Structure

 The directory is structured like so:
 
 ```text
    src/
        CapitalPlacementProj/
        CapitalPlacementProj.Application/
        CapitalPlacementProj.Common/
        CapitalPlacementProj.Domain/
        CapitalPlacementProj.Infrastructure
    tests/
        CapitalPlacementProj.E2E.Tests
 ```

 The `Program.cs` file is located in `CapitalPlacementProj/`.

 ## Usage

 This application requires ONE configuration setting to be added. In `CapitalPlacementProj/` add a json file named `appsettings.Development.json`. Add the following section:

 ```json
 "ConnectionStrings": {
    "CosmosDb": "<Your Connection String>"
  }
 ```

 The connection string should be in this format: "AccountEndpoint=<URL>;AccountKey=<KEY>==". If this connection string is incorrect, the application will FAIL. If connecting to an emulator, its certificate must be trusted, as this application isn't setup to ignore TLS/SSL.

 With that done, you can now **run** the application. The server will start on `https://localhost:7030`.

 Next, send an empty POST request to https://localhost:7030/initialise. This will create the necessary containers on Azure Cosmos DB. If your connection string was incorrect, this will fail, and so will the other endpoints.
 
 That is all the necessary setup. There is a postman collection included with this project showing example requests.

 ## Testing

 To run the tests, navigate to the root of the project. If you are in the right place, the folder structure should be:

 ```text
    src/
    tests/
 ```

 Run the following command:

 ```bash
dotnet test
 ```

Note that I only included tests for creating & fetching questionnaires due to time constraints.

## Gotchas

A gotcha is something I have to make you aware of because I was unable to resolve it due to time. Of course, some of them are expected like:

- insufficent validation for all actions. For this, I assume that validation is done on the frontend, even though a usual tip for backend engineering is to never trust the frontend. For the purposes of this demo, I am trusting the 'frontend'.
- choices provided for `MultipleChoice` or `DropDown` questions must be strings. If they aren't provided as strings, the application might throw an exception, or simply ignore the choice. It's a bit random, unfortunately. Of course, with more time, I'd have resolved this gotcha.

## Other Stuff

I do hope this satisfies the requirements, or is at least "enlightening" in one way or the other. I hope to hear from the team at Capital Placement again. You all seem to be highly talented, and I'd be honoured to join and learn from, and work with you all.