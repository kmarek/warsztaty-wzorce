using Microsoft.Playwright;

namespace Testerzy.Trainings.Romanum.UI.Pages;

public class AgeGroupsPage : BasePage
{
    public AgeGroupsPage(IPage page) : base(page)
    {
    }

    // List view
    protected ILocator AddAgeGroupButtonLocator => Page.Locator("[data-testid='add-age-group-btn']");
    protected ILocator SearchInputLocator => Page.Locator("[data-testid='age-groups-search-input']");
    protected ILocator AgeGroupsTableLocator => Page.Locator("[data-testid='age-groups-table']");

    // One per row - scope further with .Nth()/.Filter() for a specific age group
    protected ILocator EditAgeGroupButtonLocator => Page.Locator("[data-testid='edit-age-group-btn']");
    protected ILocator DeleteAgeGroupButtonLocator => Page.Locator("[data-testid='delete-age-group-btn']");

    // Add/edit side panel - no data-testid/id on these fields, only a `name` attribute (see missing-selectors list)
    protected ILocator AgeGroupNameInputLocator => Page.Locator("[role='dialog'] input[name='name']");
    protected ILocator AgeGroupDescriptionTextareaLocator => Page.Locator("[role='dialog'] textarea[name='description']");
    protected ILocator CreateAgeGroupSubmitButtonLocator => Page.Locator("[data-testid='create-age-group-submit-btn']");
}
