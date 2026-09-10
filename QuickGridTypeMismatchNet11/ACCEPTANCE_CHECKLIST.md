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
- [x] Uses the reusable `EmployeeLastNameColumn` mismatch component in both
      selected render configurations: Static SSR and Interactive Server.
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
- [x] Provides executable Development and Production browser-validation scripts.
- [x] Captures the Git commit and worktree state in every generated evidence set.

## Runtime acceptance - Development environment

- [x] `dotnet --version` prints `11.0.100-rc.1.26425.128`.
- [x] `dotnet restore` succeeds.
- [x] `dotnet list package` resolves QuickGrid `11.0.0-rc.1.26425.128`.
- [x] `dotnet build --configuration Debug --no-restore` succeeds without a
      compile-time mismatch diagnostic and is labeled as a Debug build.
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

## Runtime acceptance - Production environment

- [x] The application is published with the Release configuration.
- [x] The published application runs with `Environment=Production` from its
      published content root.
- [x] All three Static SSR mismatch routes show generic browser errors and log
      complete `InvalidOperationException` diagnostics on the server.
- [x] All three Interactive Server mismatch routes show generic browser details
      and a Run ID that correlates to the complete server diagnostic.
- [x] The reusable titled mismatch component executes in Static SSR and
      Interactive Server, including virtualization and `GridSort<Employee>`.
- [x] Employee baseline and both corrected controls render successfully.
- [x] `_framework/blazor.web.js` returns 200 and interactive circuits open.
- [x] No `NullReferenceException` is observed.
- [x] The validation runner exits nonzero when a required browser or log
      assertion is absent or incorrect.

## Review resolution checklist

- [x] Production execution covers every applicable Static SSR and Interactive
      Server mismatch and control scenario.
- [x] Build evidence is correctly labeled: Development uses Debug; Production
      build and publish use Release.
- [x] `EmployeeLastNameColumn` provides reusable mismatched-column coverage in
      both selected render configurations.
- [x] Runtime acceptance is executable and assertion-based rather than a manual
      checklist claim.
- [x] The report explicitly assesses whether the diagnostic is sufficient to
      locate and fix the offending column.
- [x] Generated evidence records the current Git SHA and worktree state in
      `E-DEV-00-source-revision.txt` and `E-PROD-00-source-revision.txt`.
- [ ] Commit the corrective changes, rerun both automated validations from a
      clean tree, and replace `main` evidence links with the resulting commit
      SHA. Current evidence correctly records base commit
      `a17f4bfc45b6c76c112c3b3f036608e505e2eb17` and `Worktree=Dirty`, so it
      must not be represented as evidence from that immutable commit.

## Cross-validation matrix

| Review concern | Implementation check | Independent runtime or evidence check | Result |
| --- | --- | --- | --- |
| Production execution | Production runner publishes Release output and launches it with the published directory as content root | Production browser run covers all nine scenarios; generic browser errors correlate with complete server diagnostics | PASS |
| Release-build accuracy | Development explicitly builds Debug; Production explicitly builds and publishes Release | `E-DEV-05-build.log` and `E-PROD-05-build.log` identify the correct configurations | PASS |
| Reusable column component | `EmployeeLastNameColumn` owns the titled `TemplateColumn<Employee>` and `GridSort<Employee>` | `SSR-TEMPLATE-TITLED` and `INTERACTIVE-TEMPLATE-TITLED-VIRTUALIZED` both produce the expected diagnostic | PASS |
| Commit/evidence identity | Automation writes source revision and worktree state before build and browser execution | Both latest evidence sets record `a17f4bfc...` and `Worktree=Dirty` | PARTIAL - clean committed rerun required |
| Executable runtime acceptance | Playwright assertions cover browser content, Run IDs, logs, assets, circuits, and controls | Development passes 41 assertions; Production passes 32 assertions; both capture 14 screenshots | PASS |
| Diagnostic sufficiency | Message contains the column label, expected item type, and parent-grid mismatch | Titled, unnamed, and property-column cases identify the exact declaration to correct | PASS |
