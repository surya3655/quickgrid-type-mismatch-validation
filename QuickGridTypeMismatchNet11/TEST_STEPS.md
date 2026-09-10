# Exact validation steps and evidence

## 1. Evidence rules

Do not mark a scenario Passed from browser appearance alone.

Every scenario must have:

1. A screenshot showing the URL and visible result.
2. The matching `VALIDATION_SCENARIO_START` record.
3. The framework exception or successful completion record.
4. The SDK, package, runtime, environment, and QuickGrid version evidence.
5. A unique evidence filename tied to the scenario ID.

For broken Interactive Server scenarios, the observed exception appears in an
evidence panel. The panel is not sufficient without the matching server log.

## 2. Prepare Development evidence

1. Extract `QuickGridTypeMismatchNet11.zip`.
2. Open PowerShell.
3. Change directory to the extracted folder containing `global.json`.
4. Run:

   ```powershell
   .\Start-DevelopmentValidation.ps1
   ```

5. Wait until the console prints the listening URL.
6. Do not close this PowerShell window.
7. Open the listening URL in a browser.
8. On the home page, capture `E-DEV-07-home-runtime.png`.
9. The screenshot must show:
   - Browser URL.
   - Expected SDK.
   - Runtime version.
   - Framework and target framework.
   - Environment `Development`.
   - Expected QuickGrid package.
   - Actual QuickGrid assembly and informational version.
   - Operating system and process architecture.
10. Confirm the server log contains `VALIDATION_APPLICATION_START`.

## 3. Development scenario steps

Return to the home page before each scenario. Use a full page load, not a stale
tab. The home buttons disable enhanced navigation.

### BASELINE-EMPLOYEE

1. Navigate to `/`.
2. Click **Open employee baseline** (`open-baseline-employees`).
3. Confirm the page URL is `/baseline/employees`.
4. Confirm First name, Last name, and Department columns render.
5. Capture `E-DEV-08-baseline-employees.png`.
6. In the server log, find:
   - `ScenarioId=BASELINE-EMPLOYEE`.
   - `ParentItemType=QuickGridTypeMismatchNet11.Models.Employee`.
   - `ColumnItemType=QuickGridTypeMismatchNet11.Models.Employee`.
   - `VALIDATION_HTTP_COMPLETED` for `/baseline/employees`.
   - `StatusCode=200`.
7. Pass only if the grid renders and the request completes with 200.

### SSR-TEMPLATE-TITLED

1. Navigate to `/`.
2. Click **Open SSR titled mismatch** (`open-ssr-titled`).
3. Confirm the attempted URL is `/ssr/broken-titled`.
4. Development can show a developer exception page.
5. Capture `E-DEV-09-ssr-titled-browser.png`.
6. In the server log, find:
   - `ScenarioId=SSR-TEMPLATE-TITLED`.
   - `RenderMode=Static SSR`.
   - Parent type `WeatherForecast`.
   - Column type `Employee`.
   - `System.InvalidOperationException`.
   - The verbatim text `Column 'Employee last name' expects item type`.
   - The full type `QuickGridTypeMismatchNet11.Models.Employee`.
   - `which does not match the parent QuickGrid's item type.`
   - `VALIDATION_HTTP_FAILED` for `/ssr/broken-titled`.
7. Pass only if the complete diagnostic is present and the primary exception is
   not `NullReferenceException`.

### SSR-TEMPLATE-UNNAMED

1. Navigate to `/`.
2. Click **Open SSR unnamed mismatch** (`open-ssr-unnamed`).
3. Confirm the attempted URL is `/ssr/broken-unnamed`.
4. Capture `E-DEV-10-ssr-unnamed-browser.png`.
5. In the server log, find:
   - `ScenarioId=SSR-TEMPLATE-UNNAMED`.
   - `System.InvalidOperationException`.
   - `Column '(unnamed)' expects item type`.
   - The full `Employee` type.
   - The parent QuickGrid mismatch text.
   - `VALIDATION_HTTP_FAILED` for `/ssr/broken-unnamed`.
6. Fail if the column name is empty or `(unnamed)` is missing.

### SSR-PROPERTY-TITLED

1. Navigate to `/`.
2. Click **Open SSR PropertyColumn mismatch** (`open-ssr-property`).
3. Confirm the attempted URL is `/ssr/broken-property`.
4. Capture `E-DEV-11-ssr-property-browser.png`.
5. In the server log, find:
   - `ScenarioId=SSR-PROPERTY-TITLED`.
   - `System.InvalidOperationException`.
   - `Column 'First name' expects item type`.
   - The full `Employee` type.
   - `VALIDATION_HTTP_FAILED` for `/ssr/broken-property`.
6. Pass only if the same guard works for `PropertyColumn`.

### SSR-FIXED

1. Navigate to `/`.
2. Click **Open SSR corrected grid** (`open-ssr-fixed`).
3. Confirm the page URL is `/ssr/fixed`.
4. Confirm Date, Temperature (C), and Summary columns render.
5. Confirm rows contain Warm, Cloudy, and Rain.
6. Capture `E-DEV-12-ssr-fixed.png`.
7. In the server log, find:
   - `ScenarioId=SSR-FIXED`.
   - Parent and column item type `WeatherForecast`.
   - `VALIDATION_HTTP_COMPLETED` for `/ssr/fixed`.
   - `StatusCode=200`.
8. Pass only if changing the column type restores rendering.

### INTERACTIVE-TEMPLATE-TITLED-VIRTUALIZED

1. Navigate to `/`.
2. Click **Open Interactive titled mismatch** (`open-interactive-titled`).
3. Confirm the page URL is `/interactive/broken-titled`.
4. Confirm the runtime panel is visible.
5. Confirm the server log contains `VALIDATION_CIRCUIT_OPENED` and
   `VALIDATION_CIRCUIT_CONNECTION_UP`.
6. Capture `E-DEV-13-interactive-titled-before-click.png`.
7. Click **Run titled virtualized mismatch** (`run-interactive-titled`) once.
8. Development must display **Expected validation failure observed**.
9. Confirm the error panel Run ID matches the log Run ID.
10. Confirm the panel shows `System.InvalidOperationException`.
11. Confirm the panel shows the complete titled message.
12. Capture `E-DEV-14-interactive-titled-after-click.png`.
13. In the server log, find:
    - `ScenarioId=INTERACTIVE-TEMPLATE-TITLED-VIRTUALIZED`.
    - `RenderMode=Interactive Server, prerender disabled, Virtualize=true`.
    - Parent type `WeatherForecast`.
    - Column type `Employee`.
    - `System.InvalidOperationException`.
    - `Column 'Employee last name' expects item type`.
    - The full `Employee` type and parent mismatch text.
    - `VALIDATION_EXCEPTION_OBSERVED` with the same Run ID.
14. Pass only if the scenario-start record, error panel, and observed-exception
    log all carry the same Run ID.

### INTERACTIVE-TEMPLATE-UNNAMED

1. Start a fresh browser navigation to `/`.
2. Click **Open Interactive unnamed mismatch** (`open-interactive-unnamed`).
3. Confirm the page URL is `/interactive/broken-unnamed`.
4. Capture `E-DEV-15-interactive-unnamed-before-click.png`.
5. Click **Run unnamed mismatch** (`run-interactive-unnamed`) once.
6. Capture `E-DEV-16-interactive-unnamed-after-click.png`.
7. In the server log, find:
   - `ScenarioId=INTERACTIVE-TEMPLATE-UNNAMED`.
   - `System.InvalidOperationException`.
   - `Column '(unnamed)' expects item type`.
   - The full `Employee` type and parent mismatch text.
   - `VALIDATION_EXCEPTION_OBSERVED` with the error panel Run ID.
8. Fail if `(unnamed)` is absent or the Run IDs differ.

### INTERACTIVE-PROPERTY-TITLED

1. Start a fresh browser navigation to `/`.
2. Click **Open Interactive PropertyColumn mismatch**
   (`open-interactive-property`).
3. Confirm the page URL is `/interactive/broken-property`.
4. Capture `E-DEV-17-interactive-property-before-click.png`.
5. Click **Run PropertyColumn mismatch** (`run-interactive-property`) once.
6. Capture `E-DEV-18-interactive-property-after-click.png`.
7. In the server log, find:
   - `ScenarioId=INTERACTIVE-PROPERTY-TITLED`.
   - `System.InvalidOperationException`.
   - `Column 'First name' expects item type`.
   - The full `Employee` type and parent mismatch text.
   - `VALIDATION_EXCEPTION_OBSERVED` with the error panel Run ID.
8. Pass only if the `PropertyColumn` diagnostic and Run ID correlation are
   proven.

### INTERACTIVE-FIXED

1. Start a fresh browser navigation to `/`.
2. Click **Open Interactive corrected grid** (`open-interactive-fixed`).
3. Confirm the page URL is `/interactive/fixed`.
4. Capture `E-DEV-19-interactive-fixed-before-click.png`.
5. Click **Render corrected grid** (`run-interactive-fixed`).
6. Confirm Date, Temperature (C), and Summary columns render.
7. Confirm Warm, Cloudy, and Rain rows appear.
8. Capture `E-DEV-20-interactive-fixed-after-click.png`.
9. In the server log, find:
   - `ScenarioId=INTERACTIVE-FIXED`.
   - Parent and column type `WeatherForecast`.
   - `VALIDATION_RENDER_COMPLETED`.
10. Pass only if both the rendered grid and completion log are present.

## 4. Final evidence review

Mark validation Passed only if:

- SDK and QuickGrid versions match exactly.
- Build succeeds without reporting the mismatch.
- All six broken cases produce `InvalidOperationException`.
- Titled cases name the column.
- Untitled cases use `(unnamed)`.
- PropertyColumn cases identify `First name`.
- Every diagnostic contains the full `Employee` type.
- Static SSR evidence includes failed request logs.
- Interactive evidence includes scenario-start and observed-exception logs.
- Baseline and both fixed controls render.
- No result relies only on browser output.
