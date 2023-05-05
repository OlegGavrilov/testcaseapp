using TestCaseApp.Startup;

// Web app host builder
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddCustomSwagger();

builder.Services.AddServices();
builder.Services.AddAuthInfrastructure();

var app = builder.Build();

// HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();

public partial class Program { }