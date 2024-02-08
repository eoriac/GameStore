using GameStore.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMongoDb(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddApiVersioningConfiguration();

builder.Services.AddSwaggerConfiguration();

builder.Services.AddScoped<IGameRepository, GameRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        // foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        // {
            options.SwaggerEndpoint($"/swagger/Default_API/swagger.json", "Default API");
        //}

        //options.RoutePrefix = "";        
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
