using Microsoft.Playwright;

namespace Testerzy.Trainings.Romanum.UI.Pages;

public abstract class BasePage
{
    protected IPage Page { get; }

    protected BasePage(IPage page)
    {
        Page = page;
    }

    // Sidebar navigation - present on every authenticated page
    protected ILocator DashboardNavLinkLocator => Page.Locator("[data-testid='nav-link-dashboard']");
    protected ILocator PerformancesNavLinkLocator => Page.Locator("[data-testid='nav-link-performances']");
    protected ILocator ReportsNavLinkLocator => Page.Locator("[data-testid='nav-link-reports']");
    protected ILocator OrganizersNavLinkLocator => Page.Locator("[data-testid='nav-link-organizers']");
    protected ILocator ContactsNavLinkLocator => Page.Locator("[data-testid='nav-link-contacts']");
    protected ILocator PlaysNavLinkLocator => Page.Locator("[data-testid='nav-link-plays']");
    protected ILocator TeamsNavLinkLocator => Page.Locator("[data-testid='nav-link-teams']");
    protected ILocator VenuesNavLinkLocator => Page.Locator("[data-testid='nav-link-venues']");
    protected ILocator SeasonsNavLinkLocator => Page.Locator("[data-testid='nav-link-seasons']");
    protected ILocator UsersNavLinkLocator => Page.Locator("[data-testid='nav-link-users']");
    protected ILocator AgeGroupsNavLinkLocator => Page.Locator("[data-testid='nav-link-age-groups']");

    // User menu (bottom of sidebar)
    protected ILocator ToggleThemeButtonLocator => Page.Locator("[data-testid='toggle-theme-btn']");
    protected ILocator LogoutButtonLocator => Page.Locator("[data-testid='logout-btn']");

    // Top bar
    protected ILocator NotificationsButtonLocator => Page.Locator("[data-testid='notifications-btn']");

    // Every "add/edit" side panel in the app is a Radix Sheet reusing this same close control
    protected ILocator CloseSheetButtonLocator => Page.Locator("[data-slot='sheet-close']");
}
