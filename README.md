# warsztaty-wzorce

`warsztaty-wzorce` ("patterns workshop" in Polish) contains a single .NET solution, `Testerzy.Trainings.Romanum`, located at `src/Testerzy.Trainings.Romanum/`. It is a **test automation solution** for the "Romanum" web application — not the application itself — covering both UI tests (Playwright) and API tests (RestSharp). The repository is at a very early, skeleton stage: framework projects contain only a placeholder `Class1.cs`, and the `Api` test project has a single default (unmodified) test. There are no `ProjectReference`s wired up between projects yet.

## Solution structure

The solution file is `src/Testerzy.Trainings.Romanum/Testerzy.Trainings.Romanum.slnx` (new XML-based `.slnx` solution format, not the classic `.sln`). Projects are grouped on disk (and in the solution) into two top-level folders, all targeting `net10.0` with nullable reference types and implicit usings enabled:

- **Tests/** — the actual test projects.
  - `Testerzy.Trainings.Romanum.Api` — NUnit test project for the API (RestSharp-based).
  - `Testerzy.Trainings.Romanum.UI` — NUnit + Microsoft.Playwright.NUnit test project for browser/UI tests.
- **Framework/** — shared, non-test support libraries.
  - `Testerzy.Trainings.Romanum.Framework.Api` — shared API client/framework code (RestSharp clients, DTOs, endpoint wrappers) used by API (and potentially UI) tests. Currently an empty skeleton.
  - `Testerzy.Trainings.Romanum.Framework.Common` — shared/common helper library. Currently an empty skeleton.
  - `Testerzy.Trainings.Romanum.Framework.Configuration` — configuration library (e.g. environment/appsettings handling). Currently an empty skeleton.

Naming convention: test projects are named after the surface they exercise (`Api`, `UI`), with no `.Tests` suffix — the physical `Tests/` folder (and NUnit's own naming for the test classes inside) already conveys that, so the root namespace stays as `Testerzy.Trainings.Romanum.Api` / `Testerzy.Trainings.Romanum.UI` without stuttering. Supporting/non-test libraries live under `Framework/` and use the `Testerzy.Trainings.Romanum.Framework.*` namespace.

Test projects use NUnit 4.x with the `NUnit`, `NUnit.Analyzers`, and `NUnit3TestAdapter` packages, plus `coverlet.collector` for coverage. `Testerzy.Trainings.Romanum.UI` additionally references `Microsoft.Playwright.NUnit` and globally uses `Microsoft.Playwright.NUnit`, `System.Text.RegularExpressions`, and `System.Threading.Tasks`.

## Common commands

Run all commands from `src/Testerzy.Trainings.Romanum/` (where the `.slnx` file lives), or pass the solution/project path explicitly.

```
# Build the whole solution
dotnet build Testerzy.Trainings.Romanum.slnx

# Run all tests
dotnet test Testerzy.Trainings.Romanum.slnx

# Run a single test project
dotnet test Tests/Testerzy.Trainings.Romanum.Api/Testerzy.Trainings.Romanum.Api.csproj
dotnet test Tests/Testerzy.Trainings.Romanum.UI/Testerzy.Trainings.Romanum.UI.csproj

# Run a single test by fully-qualified name (NUnit)
dotnet test --filter "FullyQualifiedName~Testerzy.Trainings.Romanum.Api.Tests.Test1"
```

For `Testerzy.Trainings.Romanum.UI`, Playwright browsers must be installed once before UI tests can run (from that project's output directory after a build):

```
pwsh bin/Debug/net10.0/playwright.ps1 install
```
