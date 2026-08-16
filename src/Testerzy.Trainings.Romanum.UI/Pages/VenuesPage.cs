using Microsoft.Playwright;

namespace Testerzy.Trainings.Romanum.UI.Pages;

public class VenuesPage : BasePage
{
    public VenuesPage(IPage page) : base(page)
    {
    }

    // List view
    protected ILocator AddVenueButtonLocator => Page.Locator("[data-testid='add-venue-btn']");
    protected ILocator SearchInputLocator => Page.Locator("[data-testid='venues-search-input']");
    protected ILocator VenuesTableLocator => Page.Locator("[data-testid='venues-table']");

    // One per row - scope further with .Nth()/.Filter() for a specific venue
    protected ILocator EditVenueLayoutButtonLocator => Page.Locator("[data-testid='edit-venue-layout-btn']");

    // Add side panel - no data-testid/id on these fields, only a `name` attribute (see missing-selectors list)
    protected ILocator VenueNameInputLocator => Page.Locator("[role='dialog'] input[name='name']");
    protected ILocator VenueAddressInputLocator => Page.Locator("[role='dialog'] input[name='address']");
    protected ILocator VenueContactSelectLocator => Page.Locator("[role='dialog'] select[name='contactSelect']");
    protected ILocator CreateVenueSubmitButtonLocator => Page.Locator("[data-testid='create-venue-submit-btn']");

    // Detail view (same URL, client-side master-detail - no separate route)
    protected ILocator BackToVenuesButtonLocator => Page.Locator("[data-testid='back-to-venues-btn']");
    protected ILocator DeleteVenueButtonLocator => Page.Locator("[data-testid='delete-venue-btn']");
    protected ILocator SaveVenueLayoutButtonLocator => Page.Locator("[data-testid='save-venue-layout-btn']");
    protected ILocator AddSectionButtonLocator => Page.Locator("[data-testid='add-section-btn']");
    protected ILocator EditVenueContactButtonLocator => Page.Locator("[data-testid='edit-venue-contact-btn']");

    // One per section row - scope further with .Nth()/.Filter() for a specific section
    protected ILocator SectionLabelInputLocator => Page.Locator("[data-testid='section-label-input']");
    protected ILocator SectionRowCountInputLocator => Page.Locator("[data-testid='section-rowcount-input']");
    protected ILocator SectionSeatsPerRowInputLocator => Page.Locator("[data-testid='section-seatsperrow-input']");
    protected ILocator RemoveSectionButtonLocator => Page.Locator("[data-testid='remove-section-btn']");
}
