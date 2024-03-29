using System.Text;
using Asp.Versioning.ApiExplorer;
using GameStore.API;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    // .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    // .MinimumLevel.Override("GameStore.API.Controllers.CatalogController", LogEventLevel.Debug)
    .WriteTo.Console(LogEventLevel.Debug)
    .WriteTo.File("logs/gamestoreapi.txt", LogEventLevel.Information, rollingInterval: RollingInterval.Day)
    .CreateLogger();

    // var logger = new LoggerConfiguration()
    //     .ReadFrom.Configuration(configuration)
    //     .CreateLogger();    

var builder = WebApplication.CreateBuilder(args);

// builder.Logging.ClearProviders();
// builder.Logging.AddConsole();

builder.Host.UseSerilog();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
builder.Services.RegisterDataLayer(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddApiVersioningConfiguration();

builder.Services.AddSwaggerConfiguration();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Authentication:Issuer"],
                        ValidAudience = builder.Configuration["Authentication:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
                    };
                });

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuthorizationHandler, MustOwnGameHandler>();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowedDevOrigin", policy => {
        policy
            .WithOrigins("https://gamestore-ui.dev.com")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .Build();
    });

    options.AddPolicy("AllowOnlyPOSTGET", policy => {
        policy
            .WithOrigins("https://localhost:7079", "http://localhost:5146")
            .WithMethods("POST", "GET")
            .Build();
    });
});

builder.Services.AddAuthorization(options => {
    options.AddPolicy("CanGetLibraryGame", UserRolePolicies.CanGetLibraryGame());
    options.AddPolicy("CanCreateLibraryGame", UserRolePolicies.CanCreateLibraryGame());

    options.AddPolicy("OwnGame", policyBuilder => {
        policyBuilder.RequireAuthenticatedUser();
        policyBuilder.AddRequirements(new MustOwnGameRequirement());
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    app.UseSwagger();
    app.UseSwaggerUI(options => {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }

        options.RoutePrefix = "";
    });
// }

app.UseHttpsRedirection();

// app.UseStaticFiles();

app.UseRouting();

app.UseCors();

app.UseMiddleware<AddSecurityHeadersMiddleware>();

app.Use((ctx, next) => {

    // Some headers can't be removed
    ctx.Response.Headers.Remove("Server");

    // 
    ctx.Response.Headers.Remove("x-header-to-remove");
    
    return next();
});

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

app.Run();
