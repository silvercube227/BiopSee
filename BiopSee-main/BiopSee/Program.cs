global using BiopSee.Models;
global using BiopSee.Data;
global using BiopSee.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<ITranslationService, TranslationService>();
// builder.Services.AddSingleton<ITranslationService, MockTranslationService>();
builder.Services.AddSingleton<IPictureService, PictureService>();
builder.Services.AddSingleton<ICalibrationService, CalibrationService>();
builder.Services.AddSingleton<IScanner, Scanner>();
builder.Services.AddSingleton<IScansService, ScansService>();
builder.Services.AddSingleton<IAnalysisService, MockAnalysisService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapControllers();   
app.MapFallbackToPage("/_Host");
app.Run();
