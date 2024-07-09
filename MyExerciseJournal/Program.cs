using Blazored.LocalStorage;
using MudBlazor.Services;
using MyExerciseJournal.Components;
using MyExerciseJournal.Persistence;
using MyExerciseJournal.Services;
using MyExerciseJournal.ViewModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddMudServices();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<ExerciseRepository>();
builder.Services.AddScoped<ExerciseAuthenticationService>();
builder.Services.AddTransient<RegisterViewModel>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
