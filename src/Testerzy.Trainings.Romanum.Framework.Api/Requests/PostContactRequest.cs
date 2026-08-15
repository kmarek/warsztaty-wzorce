using System;
using System.Collections.Generic;
using System.Text;

namespace Testerzy.Trainings.Romanum.Framework.Api.Requests;

public class PostContactRequest
{
    public string Kind { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string InstitutionName { get; set; }
    public string Street { get; set; }
    public string StreetNumber { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Email2 { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public string Position { get; set; }
    public string Acronym { get; set; }
    public string[]? AgeGroupIds { get; set; }
}
