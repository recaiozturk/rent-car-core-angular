using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.RateLimiting;
using RentCarServer.Application;
using RentCarServer.Infrastructure;
using Scalar.AspNetCore;
using System.Threading.RateLimiting;
;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddRateLimiter(conf =>
{
    conf.AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.PermitLimit = 100;
        options.QueueLimit = 100;
        options.Window = TimeSpan.FromSeconds(1);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;

    });
});

builder.Services
    .AddControllers()
    .AddOData(opt =>
    {
        opt.Select().Filter().Count().Expand().OrderBy().SetMaxTop(null);
    });

builder.Services.AddCors();
builder.Services.AddOpenApi();


var app = builder.Build();
app.MapOpenApi();
app.MapScalarApiReference();
app.UseHttpsRedirection();
app.UseCors(x => x.
     AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetPreflightMaxAge(TimeSpan.FromMinutes(10)));
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers().RequireRateLimiting("fixed");



app.Run();
