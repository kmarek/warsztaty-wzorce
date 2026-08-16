using Microsoft.Playwright;

namespace Testerzy.Trainings.Romanum.UI.Pages;

public class TeamsPage : BasePage
{
    public TeamsPage(IPage page) : base(page)
    {
    }

    // List view
    protected ILocator AddTeamButtonLocator => Page.Locator("[data-testid='add-team-btn']");
    protected ILocator SearchInputLocator => Page.Locator("[data-testid='teams-search-input']");
    protected ILocator SortTeamsButtonLocator => Page.Locator("[data-testid='sort-teams-btn']");
    protected ILocator TeamsTableLocator => Page.Locator("[data-testid='teams-table']");

    // One per row - scope further with .Nth()/.Filter() for a specific team
    protected ILocator EditTeamButtonLocator => Page.Locator("[data-testid='edit-team-btn']");
    protected ILocator DeleteTeamButtonLocator => Page.Locator("[data-testid='delete-team-btn']");

    // Add/edit side panel - no data-testid/id on these fields, only a `name` attribute (see missing-selectors list)
    protected ILocator TeamNameInputLocator => Page.Locator("[role='dialog'] input[name='name']");
    protected ILocator TeamDescriptionTextareaLocator => Page.Locator("[role='dialog'] textarea[name='description']");
    protected ILocator CreateTeamSubmitButtonLocator => Page.Locator("[data-testid='create-team-submit-btn']");
}
