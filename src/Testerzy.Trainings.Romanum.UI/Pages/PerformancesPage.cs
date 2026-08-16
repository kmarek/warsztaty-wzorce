using Microsoft.Playwright;

namespace Testerzy.Trainings.Romanum.UI.Pages;

public class PerformancesPage : BasePage
{
    public PerformancesPage(IPage page) : base(page)
    {
    }

    // List view
    protected ILocator AddPerformanceButtonLocator => Page.Locator("[data-testid='add-performance-btn']");
    protected ILocator FilterUpcomingButtonLocator => Page.Locator("[data-testid='filter-upcoming-btn']");
    protected ILocator FilterAllButtonLocator => Page.Locator("[data-testid='filter-all-btn']");
    protected ILocator FilterPastButtonLocator => Page.Locator("[data-testid='filter-past-btn']");
    protected ILocator SeasonFilterSelectLocator => Page.Locator("[data-testid='season-filter-select']");
    protected ILocator SearchInputLocator => Page.Locator("[data-testid='performances-search-input']");
    protected ILocator ViewListButtonLocator => Page.Locator("[data-testid='view-list-btn']");
    protected ILocator ViewCalendarButtonLocator => Page.Locator("[data-testid='view-calendar-btn']");
    protected ILocator PerformancesTableLocator => Page.Locator("[data-testid='performances-table']");

    // One per row - scope further with .Nth()/.Filter() for a specific performance
    protected ILocator PerformanceAllocationsLinkLocator => Page.Locator("[data-testid='performance-allocations-link']");
    protected ILocator EditPerformanceButtonLocator => Page.Locator("[data-testid='edit-performance-btn']");
    protected ILocator DeletePerformanceButtonLocator => Page.Locator("[data-testid='delete-performance-btn']");

    // Add/edit side panel
    protected ILocator DateTimeLocator => Page.Locator("#dateTime");
    protected ILocator PricePerSeatLocator => Page.Locator("#pricePerSeat");
    protected ILocator PlaySelectLocator => Page.Locator("#playId");
    protected ILocator VenueSelectLocator => Page.Locator("#venueId");
    protected ILocator OrganizerSelectLocator => Page.Locator("#organizerId");
    protected ILocator StatusOpenRadioLocator => Page.Locator("[role='dialog'] input[name='status'][value='OPEN']");
    protected ILocator StatusLockedRadioLocator => Page.Locator("[role='dialog'] input[name='status'][value='LOCKED']");
    protected ILocator CreatePerformanceSubmitButtonLocator => Page.Locator("[data-testid='create-performance-submit-btn']");
}
