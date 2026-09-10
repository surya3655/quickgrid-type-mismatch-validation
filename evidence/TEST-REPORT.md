# QuickGrid type-mismatch validation report

- **Repository:** [surya3655/quickgrid-type-mismatch-validation](https://github.com/surya3655/quickgrid-type-mismatch-validation)
- **Test date:** 2026-09-10
- **Validation environment:** Development on Windows x64
- **Target framework:** `net11.0` (`.NETCoreApp,Version=v11.0`)
- **.NET SDK:** `11.0.100-rc.1.26425.128`
- **ASP.NET Core runtime:** `11.0.0-rc.1.26425.128`
- **QuickGrid:** `11.0.0-rc.1.26425.128`

## Verdict

**PASS - accepted for Development validation.**

The evidence confirms that a QuickGrid column whose generic item type does not
match its parent `QuickGrid<TGridItem>` fails with a descriptive
`InvalidOperationException`. The behavior is verified for titled and untitled
`TemplateColumn<Employee>` instances, `PropertyColumn<Employee, string>`,
Static SSR, Interactive Server, and an interactive virtualized grid.

All six mismatch scenarios identify the column and the complete incompatible
type. The employee baseline and both corrected `WeatherForecast` grids render
successfully, demonstrating that the failures are caused by the deliberate
column/parent type mismatch rather than by general QuickGrid rendering.

This report covers the Development environment only.

## Verified configuration

| Check | Observed result | Status | Evidence |
| --- | --- | --- | --- |
| .NET SDK | `11.0.100-rc.1.26425.128` | PASS | [SDK version](https://github.com/surya3655/quickgrid-type-mismatch-validation/blob/main/evidence/development-20260910-090851/E-DEV-01-dotnet-version.txt) |
| Runtime and host | `.NET 11.0.0-rc.1.26425.128`, Windows x64 | PASS | [SDK and runtime information](https://github.com/surya3655/quickgrid-type-mismatch-validation/blob/main/evidence/development-20260910-090851/E-DEV-02-dotnet-info.txt) |
| Restore | Dependencies restored successfully | PASS | [Restore log](https://github.com/surya3655/quickgrid-type-mismatch-validation/blob/main/evidence/development-20260910-090851/E-DEV-03-restore.log) |
| QuickGrid package | Requested and resolved `11.0.0-rc.1.26425.128` | PASS | [Package list](https://github.com/surya3655/quickgrid-type-mismatch-validation/blob/main/evidence/development-20260910-090851/E-DEV-04-packages.log) |
| Release build | Build succeeded with 0 warnings and 0 errors | PASS | [Build log](https://github.com/surya3655/quickgrid-type-mismatch-validation/blob/main/evidence/development-20260910-090851/E-DEV-05-build.log) |
| Application environment | `Development` | PASS | [Server validation log](https://github.com/surya3655/quickgrid-type-mismatch-validation/blob/main/evidence/development-20260910-090851/E-DEV-06-server.log) |

The complete Development evidence set is available in
[evidence/development-20260910-090851/](https://github.com/surya3655/quickgrid-type-mismatch-validation/tree/main/evidence/development-20260910-090851).

## Scenario results

| Test case | Scenario and expected behavior | Result | Browser evidence | Server evidence |
| --- | --- | --- | --- | --- |
| TC-01 | Employee baseline: matching `QuickGrid<Employee>` and employee columns render successfully | PASS | [Screenshot](https://github.com/surya3655/quickgrid-type-mismatch-validation/blob/main/evidence/screenshot/tc-01.png) | [`BASELINE-EMPLOYEE`, HTTP completed](https://github.com/surya3655/quickgrid-type-mismatch-validation/blob/main/evidence/development-20260910-090851/E-DEV-06-server.log) |
| TC-02 | Static SSR titled `TemplateColumn<Employee>` mismatch names `Employee last name` | PASS | [Screenshot](https://github.com/surya3655/quickgrid-type-mismatch-validation/blob/main/evidence/screenshot/tc-02.png) | [`SSR-TEMPLATE-TITLED`, HTTP failed with expected exception](https://github.com/surya3655/quickgrid-type-mismatch-validation/blob/main/evidence/development-20260910-090851/E-DEV-06-server.log) |
| TC-03 | Static SSR untitled `TemplateColumn<Employee>` mismatch uses `(unnamed)` | PASS | [Screenshot](https://github.com/surya3655/quickgrid-type-mismatch-validation/blob/main/evidence/screenshot/tc-03.png) | [`SSR-TEMPLATE-UNNAMED`, HTTP failed with expected exception](https://github.com/surya3655/quickgrid-type-mismatch-validation/blob/main/evidence/development-20260910-090851/E-DEV-06-server.log) |
| TC-04 | Static SSR `PropertyColumn<Employee, string>` mismatch names `First name` | PASS | [Screenshot](https://github.com/surya3655/quickgrid-type-mismatch-validation/blob/main/evidence/screenshot/tc-04.png) | [`SSR-PROPERTY-TITLED`, HTTP failed with expected exception](https://github.com/surya3655/quickgrid-type-mismatch-validation/blob/main/evidence/development-20260910-090851/E-DEV-06-server.log) |
| TC-05 | Corrected Static SSR `QuickGrid<WeatherForecast>` renders successfully | PASS | [Screenshot](https://github.com/surya3655/quickgrid-type-mismatch-validation/blob/main/evidence/screenshot/tc-05.png) | [`SSR-FIXED`, HTTP completed](https://github.com/surya3655/quickgrid-type-mismatch-validation/blob/main/evidence/development-20260910-090851/E-DEV-06-server.log) |
| TC-06 | Interactive Server virtualized titled mismatch names `Employee last name` | PASS | [Before](https://github.com/surya3655/quickgrid-type-mismatch-validation/blob/main/evidence/screenshot/tc-06-01.png) / [after](https://github.com/surya3655/quickgrid-type-mismatch-validation/blob/main/evidence/screenshot/tc-06-02.png) | [`INTERACTIVE-TEMPLATE-TITLED-VIRTUALIZED`, exception observed](https://github.com/surya3655/quickgrid-type-mismatch-validation/blob/main/evidence/development-20260910-090851/E-DEV-06-server.log) |
| TC-07 | Interactive Server untitled mismatch uses `(unnamed)` | PASS | [Before](https://github.com/surya3655/quickgrid-type-mismatch-validation/blob/main/evidence/screenshot/tc-07-01.png) / [after](https://github.com/surya3655/quickgrid-type-mismatch-validation/blob/main/evidence/screenshot/tc-07-02.png) | [`INTERACTIVE-TEMPLATE-UNNAMED`, exception observed](https://github.com/surya3655/quickgrid-type-mismatch-validation/blob/main/evidence/development-20260910-090851/E-DEV-06-server.log) |
| TC-08 | Interactive Server `PropertyColumn<Employee, string>` mismatch names `First name` | PASS | [Before](https://github.com/surya3655/quickgrid-type-mismatch-validation/blob/main/evidence/screenshot/tc-08-01.png) / [after](https://github.com/surya3655/quickgrid-type-mismatch-validation/blob/main/evidence/screenshot/tc-08-02.png) | [`INTERACTIVE-PROPERTY-TITLED`, exception observed](https://github.com/surya3655/quickgrid-type-mismatch-validation/blob/main/evidence/development-20260910-090851/E-DEV-06-server.log) |
| TC-09 | Corrected Interactive Server `QuickGrid<WeatherForecast>` renders successfully | PASS | [Before](https://github.com/surya3655/quickgrid-type-mismatch-validation/blob/main/evidence/screenshot/tc-09-01.png) / [after](https://github.com/surya3655/quickgrid-type-mismatch-validation/blob/main/evidence/screenshot/tc-09-02.png) | [`INTERACTIVE-FIXED`, render completed](https://github.com/surya3655/quickgrid-type-mismatch-validation/blob/main/evidence/development-20260910-090851/E-DEV-06-server.log) |

All scenario screenshots are collected in
[evidence/screenshot/](https://github.com/surya3655/quickgrid-type-mismatch-validation/tree/main/evidence/screenshot).

## Diagnostic verification

The mismatch cases consistently produce `System.InvalidOperationException`
with the following message forms:

| Column case | Verified diagnostic |
| --- | --- |
| Titled template column | `Column 'Employee last name' expects item type 'QuickGridTypeMismatchNet11.Models.Employee', which does not match the parent QuickGrid's item type.` |
| Untitled template column | `Column '(unnamed)' expects item type 'QuickGridTypeMismatchNet11.Models.Employee', which does not match the parent QuickGrid's item type.` |
| Titled property column | `Column 'First name' expects item type 'QuickGridTypeMismatchNet11.Models.Employee', which does not match the parent QuickGrid's item type.` |

For Static SSR, the server log records the scenario start, expected
`InvalidOperationException`, and failed HTTP request. For Interactive Server,
it records the scenario start and `VALIDATION_EXCEPTION_OBSERVED` after the
circuit connects. The corrected Interactive Server scenario records
`VALIDATION_RENDER_COMPLETED`.

## Acceptance summary

- SDK, runtime, target framework, and QuickGrid package versions match the
  intended .NET 11 release-candidate configuration.
- Restore and Release build complete successfully without a compile-time type
  mismatch diagnostic.
- All six deliberately broken grids produce the expected descriptive
  `InvalidOperationException` at render time.
- Titled, untitled, `PropertyColumn`, Static SSR, Interactive Server, and
  virtualization behavior are covered.
- The employee baseline and both corrected weather grids render successfully.
- Runtime results are supported by browser screenshots and server-side
  scenario, exception, request, or render-completion records.

The detailed execution procedure and acceptance criteria are documented in
[TEST_STEPS.md](https://github.com/surya3655/quickgrid-type-mismatch-validation/blob/main/TEST_STEPS.md)
and [ACCEPTANCE_CHECKLIST.md](https://github.com/surya3655/quickgrid-type-mismatch-validation/blob/main/ACCEPTANCE_CHECKLIST.md).