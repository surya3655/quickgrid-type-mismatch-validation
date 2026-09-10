using QuickGridTypeMismatchNet11.Models;

namespace QuickGridTypeMismatchNet11.Components;

internal static class SampleData
{
    internal static IQueryable<Employee> Employees { get; } = new[]
    {
        new Employee("Avery", "Johnson", "Engineering"),
        new Employee("Mina", "Patel", "Operations"),
        new Employee("Diego", "Rivera", "Support")
    }.AsQueryable();

    internal static IQueryable<WeatherForecast> Forecasts { get; } = new[]
    {
        new WeatherForecast(new DateOnly(2026, 9, 9), 29, "Warm"),
        new WeatherForecast(new DateOnly(2026, 9, 10), 27, "Cloudy"),
        new WeatherForecast(new DateOnly(2026, 9, 11), 25, "Rain")
    }.AsQueryable();
}
