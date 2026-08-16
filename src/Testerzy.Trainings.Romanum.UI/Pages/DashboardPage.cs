using Microsoft.Playwright;

namespace Testerzy.Trainings.Romanum.UI.Pages;

public class DashboardPage : BasePage
{
    public DashboardPage(IPage page) : base(page)
    {
    }

    protected ILocator CalendarPrevMonthButtonLocator => Page.Locator("[data-testid='calendar-prev-month-btn']");
    protected ILocator CalendarNextMonthButtonLocator => Page.Locator("[data-testid='calendar-next-month-btn']");
    protected ILocator CalendarTodayButtonLocator => Page.Locator("[data-testid='calendar-today-btn']");

    // Matches one cell per day in the grid - scope further with .Nth()/.Filter() for a specific day
    protected ILocator CalendarDayCellLocator => Page.Locator("[data-testid='calendar-day-cell']");

    protected ILocator ViewAllPerformancesLinkLocator => Page.Locator("[data-testid='view-all-performances-link']");

    // One per upcoming performance card - scope further with .Nth()/.Filter() for a specific one
    protected ILocator UpcomingPerformanceLinkLocator => Page.Locator("[data-testid='upcoming-performance-link']");
}
