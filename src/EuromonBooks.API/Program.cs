using EuromonBooks.API.Middleware;
using EuromonBooks.Database.EF;
using EuromonBooks.Domain;
using EuromonBooks.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Reflection;

ApiVersion ApiVersion = new(1, 0);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//Configure Services
AddVersioning(builder.Services);
AddMvc(builder.Services);
AddServices(builder.Services);
AddRepository(builder.Services);
AddDatabase(builder.Services);
AddSwagger(builder.Services);

var app = builder.Build();
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

//prettify this
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

UseSwagger(app, provider);
UseMiddleware(app);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

void AddVersioning(IServiceCollection services)
{
    services.AddApiVersioning(o =>
    {
        o.ReportApiVersions = true;
        o.AssumeDefaultVersionWhenUnspecified = true;
        o.DefaultApiVersion = ApiVersion;
    });

    services.AddVersionedApiExplorer(o =>
    {
        o.SubstituteApiVersionInUrl = true;
    });
}

void AddMvc(IServiceCollection services)
{
    var mvcBuilder = services.AddMvc();
    //.AddFluentValidation();
    AddJsonOptions(mvcBuilder);
    services.AddHttpContextAccessor();
    services.AddRouting(opt => opt.LowercaseUrls = true);
}

void AddJsonOptions(IMvcBuilder mvcBuilder)
{
    mvcBuilder.AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    });
}

void AddServices(IServiceCollection services)
{
    services.ConfigureDomainServices(builder.Configuration);
}

void AddRepository(IServiceCollection services)
{
    services.ConfigureRepository(builder.Configuration);
}

void AddDatabase(IServiceCollection services)
{
    services.ConfigureDatabase(builder.Configuration);
}

void UseMiddleware(IApplicationBuilder app)
{
    app.UseMiddleware<ExceptionMiddleware>();

    app.UseWhen(context => !context.Request.Path.Value.Contains("/login") & !context.Request.Path.Value.Contains("/register"), builder =>
    {
        builder.UseMiddleware<BaseHeadersMiddleware>();
    });
}

void AddSwagger(IServiceCollection services)
{
    // Register the Swagger generator, defining 1 or more Swagger documents
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc(ApiVersion.ToString(), new OpenApiInfo { Title = "PJ Example Api", Version = ApiVersion.ToString() });

        // Set the comments path for the Swagger JSON and UI.
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
        c.EnableAnnotations();
    });
}

void UseSwagger(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
{
    app.UseSwagger();
    app.UseSwaggerUI(
        options =>
        {
            // build a swagger endpoint for each discovered API version
            foreach (var groupName in provider.ApiVersionDescriptions.Select(x => x.GroupName))
            {
                options.SwaggerEndpoint($"/swagger/{groupName}/swagger.json", groupName.ToUpperInvariant());
            }
        });
}