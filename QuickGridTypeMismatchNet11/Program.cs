using QuickGridTypeMismatchNet11.Components;
using QuickGridTypeMismatchNet11.Validation;
using Microsoft.AspNetCore.Components.Server.Circuits;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddSimpleConsole(options =>
{
    options.SingleLine = true;
    options.TimestampFormat = "yyyy-MM-dd HH:mm:ss.fff zzz ";
});

builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<ValidationEnvironmentInfo>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<ValidationRecorder>();
builder.Services.AddScoped<CircuitHandler, ValidationCircuitHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    var recorder = context.RequestServices.GetRequiredService<ValidationRecorder>();
    var runId = recorder.LogHttpRequestStart(context);

    try
    {
        await next();
        recorder.LogHttpRequestCompleted(runId, context);
    }
    catch (Exception exception)
    {
        recorder.LogHttpRequestFailed(runId, context, exception);
        throw;
    }
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Services.GetRequiredService<ValidationRecorder>().LogApplicationStart();

app.Run();
