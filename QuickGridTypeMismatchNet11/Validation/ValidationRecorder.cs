using Microsoft.AspNetCore.Http;

namespace QuickGridTypeMismatchNet11.Validation;

public sealed class ValidationRecorder
{
    private readonly ILogger<ValidationRecorder> _logger;
    private readonly ValidationEnvironmentInfo _environment;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ValidationRecorder(
        ILogger<ValidationRecorder> logger,
        ValidationEnvironmentInfo environment,
        IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _environment = environment;
        _httpContextAccessor = httpContextAccessor;
    }

    public void LogApplicationStart()
    {
        _logger.LogInformation(
            "VALIDATION_APPLICATION_START ExpectedSdk={ExpectedSdk} Runtime={Runtime} " +
            "Framework={Framework} TargetFramework={TargetFramework} Environment={Environment} " +
            "ExpectedQuickGrid={ExpectedQuickGrid} QuickGridAssembly={QuickGridAssembly} " +
            "QuickGridInformational={QuickGridInformational} OS={OS} Architecture={Architecture}",
            ValidationEnvironmentInfo.ExpectedSdkVersion,
            _environment.RuntimeVersion,
            _environment.FrameworkDescription,
            _environment.TargetFramework,
            _environment.EnvironmentName,
            ValidationEnvironmentInfo.ExpectedQuickGridPackageVersion,
            _environment.QuickGridAssemblyVersion,
            _environment.QuickGridInformationalVersion,
            _environment.OperatingSystem,
            _environment.ProcessArchitecture);
    }

    public string LogScenarioStart(
        string scenarioId,
        string renderMode,
        Type parentItemType,
        Type columnItemType,
        string expectedBehavior)
    {
        var runId = Guid.NewGuid().ToString("N");
        var httpContext = _httpContextAccessor.HttpContext;

        _logger.LogInformation(
            "VALIDATION_SCENARIO_START RunId={RunId} ScenarioId={ScenarioId} " +
            "RenderMode={RenderMode} ParentItemType={ParentItemType} " +
            "ColumnItemType={ColumnItemType} ExpectedBehavior={ExpectedBehavior} " +
            "Runtime={Runtime} TargetFramework={TargetFramework} Environment={Environment} " +
            "QuickGridInformational={QuickGridInformational} TraceId={TraceId} Path={Path}",
            runId,
            scenarioId,
            renderMode,
            parentItemType.FullName,
            columnItemType.FullName,
            expectedBehavior,
            _environment.RuntimeVersion,
            _environment.TargetFramework,
            _environment.EnvironmentName,
            _environment.QuickGridInformationalVersion,
            httpContext?.TraceIdentifier ?? "(interactive-circuit)",
            httpContext?.Request.Path.Value ?? "(interactive-circuit)");

        return runId;
    }

    public void LogObservedException(
        string runId,
        string scenarioId,
        string observationPoint,
        Exception exception)
    {
        _logger.LogError(
            exception,
            "VALIDATION_EXCEPTION_OBSERVED RunId={RunId} ScenarioId={ScenarioId} " +
            "ObservationPoint={ObservationPoint} ExceptionType={ExceptionType} " +
            "ExceptionMessage={ExceptionMessage}",
            runId,
            scenarioId,
            observationPoint,
            exception.GetType().FullName,
            exception.Message);
    }

    public void LogRenderCompleted(
        string runId,
        string scenarioId,
        string result)
    {
        _logger.LogInformation(
            "VALIDATION_RENDER_COMPLETED RunId={RunId} ScenarioId={ScenarioId} Result={Result}",
            runId,
            scenarioId,
            result);
    }

    public string LogHttpRequestStart(HttpContext context)
    {
        var runId = Guid.NewGuid().ToString("N");

        _logger.LogInformation(
            "VALIDATION_HTTP_START RunId={RunId} TraceId={TraceId} Method={Method} " +
            "Path={Path} QueryString={QueryString} Environment={Environment} Runtime={Runtime}",
            runId,
            context.TraceIdentifier,
            context.Request.Method,
            context.Request.Path,
            context.Request.QueryString,
            _environment.EnvironmentName,
            _environment.RuntimeVersion);

        return runId;
    }

    public void LogHttpRequestCompleted(string runId, HttpContext context)
    {
        _logger.LogInformation(
            "VALIDATION_HTTP_COMPLETED RunId={RunId} TraceId={TraceId} Method={Method} " +
            "Path={Path} StatusCode={StatusCode}",
            runId,
            context.TraceIdentifier,
            context.Request.Method,
            context.Request.Path,
            context.Response.StatusCode);
    }

    public void LogHttpRequestFailed(
        string runId,
        HttpContext context,
        Exception exception)
    {
        _logger.LogError(
            exception,
            "VALIDATION_HTTP_FAILED RunId={RunId} TraceId={TraceId} Method={Method} " +
            "Path={Path} ExceptionType={ExceptionType} ExceptionMessage={ExceptionMessage}",
            runId,
            context.TraceIdentifier,
            context.Request.Method,
            context.Request.Path,
            exception.GetType().FullName,
            exception.Message);
    }
}
