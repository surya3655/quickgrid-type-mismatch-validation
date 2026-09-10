using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Components.QuickGrid;

namespace QuickGridTypeMismatchNet11.Validation;

public sealed class ValidationEnvironmentInfo
{
    public const string ExpectedSdkVersion = "11.0.100-rc.1.26425.128";
    public const string ExpectedQuickGridPackageVersion = "11.0.0-rc.1.26425.128";

    public ValidationEnvironmentInfo(IWebHostEnvironment environment)
    {
        EnvironmentName = environment.EnvironmentName;

        var quickGridAssembly = typeof(QuickGrid<>).Assembly;
        QuickGridAssemblyVersion =
            quickGridAssembly.GetName().Version?.ToString() ?? "(unknown)";
        QuickGridInformationalVersion =
            quickGridAssembly
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                ?.InformationalVersion ?? "(unknown)";
    }

    public string EnvironmentName { get; }

    public string RuntimeVersion { get; } = Environment.Version.ToString();

    public string FrameworkDescription { get; } =
        RuntimeInformation.FrameworkDescription;

    public string TargetFramework { get; } =
        AppContext.TargetFrameworkName ?? "(unknown)";

    public string OperatingSystem { get; } =
        RuntimeInformation.OSDescription;

    public string ProcessArchitecture { get; } =
        RuntimeInformation.ProcessArchitecture.ToString();

    public string QuickGridAssemblyVersion { get; }

    public string QuickGridInformationalVersion { get; }
}
