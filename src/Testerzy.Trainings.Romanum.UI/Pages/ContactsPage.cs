using Microsoft.Playwright;

namespace Testerzy.Trainings.Romanum.UI.Pages;

public class ContactsPage : BasePage
{
    public ContactsPage(IPage page) : base(page)
    {
    }

    // List view
    protected ILocator AddContactButtonLocator => Page.Locator("[data-testid='add-contact-btn']");
    protected ILocator FilterSchoolsButtonLocator => Page.Locator("[data-testid='filter-schools-btn']");
    protected ILocator FilterVenuesButtonLocator => Page.Locator("[data-testid='filter-venues-btn']");
    protected ILocator CityFilterSelectLocator => Page.Locator("[data-testid='city-filter-select']");
    protected ILocator SearchInputLocator => Page.Locator("[data-testid='contacts-search-input']");
    protected ILocator ContactsTableLocator => Page.Locator("[data-testid='contacts-table']");

    // One per row - scope further with .Nth()/.Filter() for a specific contact
    protected ILocator EditContactButtonLocator => Page.Locator("[data-testid='edit-contact-btn']");
    protected ILocator DeleteContactButtonLocator => Page.Locator("[data-testid='delete-contact-btn']");

    // Add/edit side panel (same form/testids for both flows)
    protected ILocator ContactKindSchoolButtonLocator => Page.Locator("[data-testid='contact-kind-school-btn']");
    protected ILocator ContactKindVenueButtonLocator => Page.Locator("[data-testid='contact-kind-venue-btn']");
    protected ILocator FirstNameLocator => Page.Locator("#firstName");
    protected ILocator LastNameLocator => Page.Locator("#lastName");
    protected ILocator InstitutionNameLocator => Page.Locator("#institutionName");
    protected ILocator PositionLocator => Page.Locator("#position");
    protected ILocator StreetLocator => Page.Locator("#street");
    protected ILocator StreetNumberLocator => Page.Locator("#streetNumber");
    protected ILocator PostalCodeLocator => Page.Locator("#postalCode");
    protected ILocator CityLocator => Page.Locator("#city");
    protected ILocator PhoneLocator => Page.Locator("#phone");
    protected ILocator EmailLocator => Page.Locator("#email");
    protected ILocator Email2Locator => Page.Locator("#email2");
    protected ILocator ContactFormSubmitButtonLocator => Page.Locator("[data-testid='contact-form-submit-btn']");
}
