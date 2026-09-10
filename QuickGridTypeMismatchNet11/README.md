# QuickGrid column item-type mismatch validation - .NET 11 RC1

This is a complete Blazor Web App for validating:

- Static server-side rendering (Static SSR)
- Interactive Server with prerendering disabled
- A titled mismatched column
- An unnamed mismatched column
- A mismatched `PropertyColumn`
- A virtualized Interactive Server mismatch
- The original employee-grid baseline
- Corrected control routes

See `ACCEPTANCE_CHECKLIST.md` for the final structural and runtime sign-off
matrix.

See `TEST_STEPS.md` for the exact navigation, button clicks, expected UI,
expected log markers, and evidence filenames.

Related issues:

- https://github.com/dotnet/aspnetcore/issues/69133
- https://github.com/dotnet/aspnetcore/issues/56437
- https://github.com/dotnet/aspnetcore/pull/67413

## Versions pinned by the sample

`global.json` requires:

```text
11.0.100-rc.1.26425.128
```

The project requires:

```text
Microsoft.AspNetCore.Components.QuickGrid 11.0.0-rc.1.26425.128
```

The SDK pin uses `rollForward: disable`, so the command fails clearly when the
exact RC1 SDK isn't installed instead of silently selecting another SDK.

## Verify the environment

Run these commands from the folder containing `global.json`:

```powershell
dotnet --version
dotnet --info
dotnet restore
dotnet list package
dotnet build
```

Expected SDK:

```text
11.0.100-rc.1.26425.128
```

Expected QuickGrid package:

```text
11.0.0-rc.1.26425.128
```

The build should succeed. This mismatch is diagnosed during component
initialization at runtime, not by the compiler.

## Run in Development

```powershell
.\Start-DevelopmentValidation.ps1
```

Open the URL printed by `dotnet run`.

## Routes and expected behavior

| Route | Rendering configuration | Expected result |
|---|---|---|
| `/baseline/employees` | Static SSR | Original employee grid renders with employee columns |
| `/ssr/broken-titled` | Static SSR | Request fails; server log contains the titled `InvalidOperationException` |
| `/ssr/broken-unnamed` | Static SSR | Request fails; server log identifies the column as `(unnamed)` |
| `/ssr/broken-property` | Static SSR | Request fails; server log identifies the `First name` PropertyColumn and `Employee` |
| `/ssr/fixed` | Static SSR | Weather grid renders |
| `/interactive/broken-titled` | Interactive Server, no prerender, virtualized | Evidence boundary displays the Development diagnostic and logs the titled exception with a Run ID |
| `/interactive/broken-unnamed` | Interactive Server, no prerender | Evidence boundary displays/logs the `(unnamed)` diagnostic |
| `/interactive/broken-property` | Interactive Server, no prerender | Evidence boundary displays/logs the `First name` PropertyColumn diagnostic |
| `/interactive/fixed` | Interactive Server, no prerender | Weather grid renders |

## Expected titled diagnostic

```text
System.InvalidOperationException:
Column 'Employee last name' expects item type
'QuickGridTypeMismatchNet11.Models.Employee',
which does not match the parent QuickGrid's item type.
```

Log prefixes and line wrapping can vary, but the exception type and complete
message must match.

## Expected unnamed diagnostic

```text
System.InvalidOperationException:
Column '(unnamed)' expects item type
'QuickGridTypeMismatchNet11.Models.Employee',
which does not match the parent QuickGrid's item type.
```

## Why the broken browser output can still look similar

The .NET 11 change improves the exception. It doesn't make an invalid grid
render:

- Static SSR can return a developer exception response in Development.
- Production can return a generic error response.
- Interactive Server uses an evidence boundary that logs the complete exception
  with the scenario Run ID. Development shows the details; Production displays
  a generic message with the Run ID.

The server log is the authoritative evidence.

## Run in Production

```powershell
.\Start-ProductionValidation.ps1
```

Production isn't required to expose exception details to visitors. Confirm the
full diagnostic in the server log.

## Evidence-oriented logging

Every scenario page includes a **Runtime validation details** panel showing the
runtime, framework, target framework, environment, expected package, actual
QuickGrid assembly details, operating system, and process architecture.

The sample emits timestamped, single-line markers:

- `VALIDATION_APPLICATION_START`
- `VALIDATION_HTTP_START`
- `VALIDATION_HTTP_COMPLETED`
- `VALIDATION_HTTP_FAILED`
- `VALIDATION_CIRCUIT_OPENED`
- `VALIDATION_CIRCUIT_CONNECTION_UP`
- `VALIDATION_CIRCUIT_CONNECTION_DOWN`
- `VALIDATION_CIRCUIT_CLOSED`
- `VALIDATION_SCENARIO_START`
- `VALIDATION_EXCEPTION_OBSERVED`
- `VALIDATION_RENDER_COMPLETED`

Each scenario-start record contains a unique `RunId`, scenario ID, render mode,
parent item type, column item type, runtime, target framework, environment, and
QuickGrid informational version.

## Evidence checklist

- [ ] `dotnet --version` is `11.0.100-rc.1.26425.128`.
- [ ] `dotnet list package` resolves QuickGrid `11.0.0-rc.1.26425.128`.
- [ ] Restore and build succeed.
- [ ] Build output doesn't report the mismatch.
- [ ] Original employee baseline renders.
- [ ] Static SSR titled route logs the complete titled message.
- [ ] Static SSR unnamed route logs `(unnamed)`.
- [ ] Static SSR PropertyColumn route logs `First name` and `Employee`.
- [ ] Interactive Server titled route logs the complete titled message.
- [ ] Interactive Server titled route runs with `Virtualize="true"`.
- [ ] Interactive Server unnamed route logs `(unnamed)`.
- [ ] Interactive Server PropertyColumn route logs `First name` and `Employee`.
- [ ] Static SSR fixed route renders the weather grid.
- [ ] Interactive Server fixed route renders the weather grid.
- [ ] Development browser behavior is captured.
- [ ] Production browser and server-log behavior are captured separately.

## Important validation conclusion

Passing validation means the invalid grid produces the clear
`InvalidOperationException`. It doesn't mean the broken grid renders. The two
fixed routes prove that changing only the column item type restores rendering.
