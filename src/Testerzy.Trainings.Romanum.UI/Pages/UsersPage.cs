using Microsoft.Playwright;

namespace Testerzy.Trainings.Romanum.UI.Pages;

public class UsersPage : BasePage
{
    public UsersPage(IPage page) : base(page)
    {
    }

    // List view
    protected ILocator AddUserButtonLocator => Page.Locator("[data-testid='add-user-btn']");
    protected ILocator UsersTableLocator => Page.Locator("[data-testid='users-table']");

    // One per row - scope further with .Nth()/.Filter() for a specific user
    protected ILocator DeleteUserButtonLocator => Page.Locator("[data-testid='delete-user-btn']");

    // Add side panel - no data-testid/id on these fields, only a `name` attribute (see missing-selectors list)
    protected ILocator UserNameInputLocator => Page.Locator("[role='dialog'] input[name='name']");
    protected ILocator UserEmailInputLocator => Page.Locator("[role='dialog'] input[name='email']");
    protected ILocator UserRoleSelectLocator => Page.Locator("[role='dialog'] select[name='role']");
    protected ILocator CreateUserSubmitButtonLocator => Page.Locator("[data-testid='create-user-submit-btn']");
}
