using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace QuickGridTypeMismatchNet11.Validation;

public sealed class EvidenceErrorBoundary : ErrorBoundary
{
    [Inject]
    public ValidationRecorder Evidence { get; set; } = default!;

    [Parameter, EditorRequired]
    public string ScenarioId { get; set; } = string.Empty;

    [Parameter, EditorRequired]
    public string RunId { get; set; } = string.Empty;

    protected override Task OnErrorAsync(Exception exception)
    {
        Evidence.LogObservedException(
            RunId,
            ScenarioId,
            "Interactive Server ErrorBoundary",
            exception);

        return base.OnErrorAsync(exception);
    }
}
