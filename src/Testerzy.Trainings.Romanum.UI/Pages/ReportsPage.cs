using Microsoft.Playwright;

namespace Testerzy.Trainings.Romanum.UI.Pages;

public class ReportsPage : BasePage
{
    public ReportsPage(IPage page) : base(page)
    {
    }

    protected ILocator ExportCsvButtonLocator => Page.Locator("[data-testid='export-csv-btn']");
    protected ILocator SeasonFilterSelectLocator => Page.Locator("[data-testid='report-season-filter-select']");
    protected ILocator OrganizerFilterSelectLocator => Page.Locator("[data-testid='report-organizer-filter-select']");
    protected ILocator TeamFilterSelectLocator => Page.Locator("[data-testid='report-team-filter-select']");
    protected ILocator PlayFilterSelectLocator => Page.Locator("[data-testid='report-play-filter-select']");
    protected ILocator GroupBySelectLocator => Page.Locator("[data-testid='report-groupby-select']");
    protected ILocator ReportGroupedTableLocator => Page.Locator("[data-testid='report-grouped-table']");
}
