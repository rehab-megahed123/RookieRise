using RookieRise.Presistance.Extensions;
using RookieRise.Infrastructure.Extensions;
using RookieRise.Application.Extensions;
using RookieRise.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// ================================
// ✅ Add Services (Extensions)
// ================================

builder.Services.AddPersistence(builder.Configuration);    // DbContext + Identity + Hangfire
builder.Services.AddInfrastructure(builder.Configuration); // Repositories + Services + JWT
RookieRise.Application.Extensions.ServicesCollectionExtensions.AddApplication(builder.Services);
RookieRise.API.Extensions.ServiceCollectionExtensions.AddPresentation(builder.Services, builder.Configuration);                         // MediatR + AutoMapper

// ================================
// ✅ Build App
// ================================
var app = builder.Build();

// ================================
// ✅ Middleware Pipeline
// ================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();