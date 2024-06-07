using MOTChecker.Components;
using MOTChecker.Services.MOTApiService;
using MOTChecker.Services.MOTApiService.Configuration;
using MOTChecker.Services.MOTApiService.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped<IMOTApiService, MOTApiService>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//Add console logging support
builder.Logging.AddConsole();

//Load MOT API Settings
MOTApiServiceSettings.ApiKey = builder.Configuration.GetSection("MOTApiSettings").GetValue<string?>("ApiKey") ?? "";

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
