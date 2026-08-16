using Microsoft.Playwright;

namespace Testerzy.Trainings.Romanum.UI.Pages;

public class SeasonsPage : BasePage
{
    public SeasonsPage(IPage page) : base(page)
    {
    }

    // List view
    protected ILocator AddSeasonButtonLocator => Page.Locator("[data-testid='add-season-btn']");
    protected ILocator SeasonsTableLocator => Page.Locator("[data-testid='seasons-table']");

    // One per row - scope further with .Nth()/.Filter() for a specific season
    protected ILocator ToggleSeasonRepertoireButtonLocator => Page.Locator("[data-testid='toggle-season-repertoire-btn']");
    protected ILocator SetCurrentSeasonButtonLocator => Page.Locator("[data-testid='set-current-season-btn']");
    protected ILocator EditSeasonButtonLocator => Page.Locator("[data-testid='edit-season-btn']");
    protected ILocator DeleteSeasonButtonLocator => Page.Locator("[data-testid='delete-season-btn']");

    // Add side panel
    protected ILocator SeasonNameInputLocator => Page.Locator("#create-name");
    protected ILocator SeasonStartDateInputLocator => Page.Locator("#create-start");
    protected ILocator SeasonEndDateInputLocator => Page.Locator("#create-end");
    protected ILocator CreateSeasonSubmitButtonLocator => Page.Locator("[data-testid='create-season-submit-btn']");
}
