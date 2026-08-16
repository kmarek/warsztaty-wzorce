using Microsoft.Playwright;

namespace Testerzy.Trainings.Romanum.UI.Pages;

public class PlaysPage : BasePage
{
    public PlaysPage(IPage page) : base(page)
    {
    }

    // List view
    protected ILocator AddPlayButtonLocator => Page.Locator("[data-testid='add-play-btn']");
    protected ILocator SearchInputLocator => Page.Locator("[data-testid='plays-search-input']");
    protected ILocator PlaysTableLocator => Page.Locator("[data-testid='plays-table']");
    protected ILocator SortNameHeaderLocator => Page.Locator("[data-testid='sort-name-header']");
    protected ILocator SortAgeGroupHeaderLocator => Page.Locator("[data-testid='sort-ageGroup-header']");
    protected ILocator SortTeamHeaderLocator => Page.Locator("[data-testid='sort-team-header']");
    protected ILocator SortPerformancesHeaderLocator => Page.Locator("[data-testid='sort-performances-header']");

    // One per row - scope further with .Nth()/.Filter() for a specific play
    protected ILocator EditPlayButtonLocator => Page.Locator("[data-testid='edit-play-btn']");
    protected ILocator DeletePlayButtonLocator => Page.Locator("[data-testid='delete-play-btn']");

    // Add/edit side panel - no data-testid/id on these fields, only a `name` attribute (see missing-selectors list)
    protected ILocator PlayNameInputLocator => Page.Locator("[role='dialog'] input[name='name']");
    protected ILocator PlayDescriptionTextareaLocator => Page.Locator("[role='dialog'] textarea[name='description']");
    protected ILocator PlayAgeGroupSelectLocator => Page.Locator("[role='dialog'] select[name='ageGroupId']");
    protected ILocator PlayTeamSelectLocator => Page.Locator("[role='dialog'] select[name='teamId']");
    protected ILocator CreatePlaySubmitButtonLocator => Page.Locator("[data-testid='create-play-submit-btn']");
}
