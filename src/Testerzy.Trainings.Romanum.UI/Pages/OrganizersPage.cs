using Microsoft.Playwright;

namespace Testerzy.Trainings.Romanum.UI.Pages;

public class OrganizersPage : BasePage
{
    public OrganizersPage(IPage page) : base(page)
    {
    }

    protected ILocator SearchInputLocator => Page.Locator("[data-testid='organizers-search-input']");

    // One per organizer - scope further with .Nth()/.Filter() for a specific organizer
    protected ILocator SelectOrganizerButtonLocator => Page.Locator("[data-testid='select-organizer-btn']");

    protected ILocator OrganizerTermsTableLocator => Page.Locator("[data-testid='organizer-terms-table']");
}
