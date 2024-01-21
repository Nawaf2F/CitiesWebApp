using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApp;
using WebApp.DbContexts;
using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Types of services: 1. Singleton 2. Scoped 3. Transient

//1. Singleton - One instance of a resource, reused anytime it's requested.
builder.Services.AddSingleton<CitiesDataStore>();

builder.Services.AddControllers().AddNewtonsoftJson();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c => c.DocumentFilter<JsonPatchDocumentFilter>());
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("CityInfoApiBearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Input a valid token to access this API"
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference {
                Type = ReferenceType.SecurityScheme,
                Id = "CityInfoApiBearerAuth"     }
            },   new List<string>() }
    });
});

//Databse Connection
builder.Services.AddDbContext<CityInfoContext>(dbContextOptions => dbContextOptions.UseSqlServer(builder.Configuration["ConnectionStrings:CityInfoDBConnectionString"]));

//2. Scoped - One instance of a resource, but only for the current request.
builder.Services.AddScoped<ICityInfoRepository, CityInfoRepository>();

//3. Transient - A different instance of a resource, everytime it's requested.

//Mapping between Dto's & Entities
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Authentication
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}


app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
