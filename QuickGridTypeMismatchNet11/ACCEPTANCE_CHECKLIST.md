# Sample acceptance checklist

## Structurally verified in this package

- [x] Targets `net11.0`.
- [x] Pins SDK `11.0.100-rc.1.26425.128` in `global.json`.
- [x] Sets `allowPrerelease` to `true`.
- [x] Disables SDK roll-forward to prevent accidental version substitution.
- [x] Pins QuickGrid `11.0.0-rc.1.26425.128`.
- [x] Contains unrelated `Employee` and `WeatherForecast` record types.
- [x] Contains an employee-grid baseline from before the refactor.
- [x] Contains correctly typed Static SSR and Interactive Server controls.
- [x] Contains titled `TemplateColumn<Employee>` mismatches.
- [x] Contains untitled `TemplateColumn<Employee>` mismatches.
- [x] Contains `PropertyColumn<Employee, string>` mismatches.
- [x] Contains a virtualized Interactive Server mismatch.
- [x] Keeps the parent items typed as `IQueryable<WeatherForecast>`.
- [x] Uses separate routes so one exception can't hide another scenario.
- [x] Uses Static SSR routes without an interactive render mode.
- [x] Uses Interactive Server routes with prerendering disabled.
- [x] Documents the Development validation configuration.
- [x] Documents exact commands, routes, expected messages, and evidence.
- [x] Displays runtime, framework, target framework, environment, QuickGrid
      assembly details, operating system, and architecture in the UI.
- [x] Logs application, HTTP request, circuit, scenario, exception, and
      successful Interactive Server render evidence.
- [x] Provides exact page-navigation and button-click instructions.
- [x] Provides a Development evidence-capture script.

## Runtime acceptance - Development environment

- [x] `dotnet --version` prints `11.0.100-rc.1.26425.128`.
- [x] `dotnet restore` succeeds.
- [x] `dotnet list package` resolves QuickGrid `11.0.0-rc.1.26425.128`.
- [x] `dotnet build` succeeds without a compile-time mismatch diagnostic.
- [x] `/baseline/employees` renders.
- [x] `/ssr/broken-titled` logs `InvalidOperationException` with
      `Employee last name` and the full `Employee` type name.
- [x] `/ssr/broken-unnamed` logs `InvalidOperationException` with `(unnamed)`.
- [x] `/ssr/broken-property` logs `InvalidOperationException` with
      `First name` and the full `Employee` type name.
- [x] `/ssr/fixed` renders the weather grid.
- [x] `/interactive/broken-titled` runs with virtualization and logs the titled
      `VALIDATION_EXCEPTION_OBSERVED` diagnostic after the circuit connects.
- [x] `/interactive/broken-unnamed` displays/logs `(unnamed)` after the circuit
      connects.
- [x] `/interactive/broken-property` displays/logs `First name` and `Employee`.
- [x] `/interactive/fixed` renders the weather grid.

The sample is accepted for Development validation. Runtime results are supported
by screenshots and server-side scenario, exception, or completion records.
