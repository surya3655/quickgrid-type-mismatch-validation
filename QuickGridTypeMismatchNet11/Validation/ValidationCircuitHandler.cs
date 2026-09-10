using Microsoft.AspNetCore.Components.Server.Circuits;

namespace QuickGridTypeMismatchNet11.Validation;

public sealed class ValidationCircuitHandler : CircuitHandler
{
    private readonly ILogger<ValidationCircuitHandler> _logger;
    private readonly ValidationEnvironmentInfo _environment;

    public ValidationCircuitHandler(
        ILogger<ValidationCircuitHandler> logger,
        ValidationEnvironmentInfo environment)
    {
        _logger = logger;
        _environment = environment;
    }

    public override Task OnCircuitOpenedAsync(
        Circuit circuit,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "VALIDATION_CIRCUIT_OPENED CircuitId={CircuitId} Runtime={Runtime} " +
            "Environment={Environment} QuickGridInformational={QuickGridInformational}",
            circuit.Id,
            _environment.RuntimeVersion,
            _environment.EnvironmentName,
            _environment.QuickGridInformationalVersion);

        return Task.CompletedTask;
    }

    public override Task OnConnectionUpAsync(
        Circuit circuit,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "VALIDATION_CIRCUIT_CONNECTION_UP CircuitId={CircuitId}",
            circuit.Id);

        return Task.CompletedTask;
    }

    public override Task OnConnectionDownAsync(
        Circuit circuit,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "VALIDATION_CIRCUIT_CONNECTION_DOWN CircuitId={CircuitId}",
            circuit.Id);

        return Task.CompletedTask;
    }

    public override Task OnCircuitClosedAsync(
        Circuit circuit,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "VALIDATION_CIRCUIT_CLOSED CircuitId={CircuitId}",
            circuit.Id);

        return Task.CompletedTask;
    }
}
