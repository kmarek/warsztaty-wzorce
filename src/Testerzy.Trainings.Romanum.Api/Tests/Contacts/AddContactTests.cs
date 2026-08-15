using FluentAssertions;
using RestSharp;
using System.Net;
using Testerzy.Trainings.Romanum.Framework.Api.Requests;
using Testerzy.Trainings.Romanum.Framework.Api.Responses;
using Testerzy.Trainings.Romanum.Framework.Configuration.Models;

namespace Testerzy.Trainings.Romanum.Api.Tests.Contacts;

public class AddContactTests : BaseApiTest
{
    [Test]
    public void Verify_ContactCanBeAdded()
    {
        var account = Settings.TestData.Accounts.First(u => u.Type == AccountType.Administrator);
        var tokens = OAuthTokenClient.GetTokenByPassword(account.Username, account.Password);

        RestRequest request = new("/api/v1/contacts", Method.Post);
        PostContactRequest requestBody = new()
        {
            Kind = "SCHOOL",
            FirstName = "John",
            LastName = "Doe",
            InstitutionName = "Acme Institution",
            Street = "Main Street",
            Email = "",
            City = "Sanok",
            AgeGroupIds = []
        };
        request.AddJsonBody<PostContactRequest>(requestBody);
        AddAuthHeaders(request, tokens.AccessToken);

        RestResponse<ContactResponse> response = RestClient.Execute<ContactResponse>(request);
        Console.WriteLine(response.Content);
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        ContactResponse? contact = response.Data;
        contact.Should().NotBeNull();
        contact!.Id.Should().NotBeNullOrEmpty();
        contact.Kind.Should().Be(requestBody.Kind);
        contact.FirstName.Should().Be(requestBody.FirstName);
        contact.LastName.Should().Be(requestBody.LastName);
        contact.InstitutionName.Should().Be(requestBody.InstitutionName);
        contact.Street.Should().Be(requestBody.Street);
        contact.Email.Should().BeNull("an empty email is normalized to null by the API");
    }
}
