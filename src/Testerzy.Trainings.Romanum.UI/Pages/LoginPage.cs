using Microsoft.Playwright;

namespace Testerzy.Trainings.Romanum.UI.Pages;

public class LoginPage : BasePage
{
    public LoginPage(IPage page) : base(page)
    {
    }

    protected ILocator LoginFormLocator => Page.Locator("[data-testid='login-form']");
    protected ILocator EmailLocator => Page.Locator("#email");
    protected ILocator PasswordLocator => Page.Locator("#password");
    protected ILocator TogglePasswordVisibilityLocator => Page.Locator("[data-testid='toggle-password-visibility-btn']");
    protected ILocator LoginButtonLocator => Page.Locator("[data-testid='login-btn']");
}
