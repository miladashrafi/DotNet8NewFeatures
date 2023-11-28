using DotNet8NewFeatures.Context;
using DotNet8NewFeatures.Models;
using DotNet8NewFeatures.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//redis output cache
builder.AddRedisOutputCache("Redis");
//builder.Services.AddStackExchangeRedisOutputCache(options =>
//{
//    options.Configuration = builder.Configuration.GetConnectionString("Redis");
//    options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions()
//    {
//        AbortOnConnectFail = false,
//    };
//});

builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(builder =>
        builder.Expire(TimeSpan.FromSeconds(30)));
});

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddKeyedSingleton<IKeyedService, KeyedService1>("Service1");
builder.Services.AddKeyedSingleton<IKeyedService, KeyedService2>("Service2");


#region NEW IDENTITY
builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseInMemoryDatabase("AppDb"));

builder.Services.AddIdentityCore<TestUser>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();
#endregion

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        //options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

//New HierarchyId
//builder.Services.AddDbContext<DataContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"), sql =>
//    {
//        sql.UseHierarchyId();
//        sql.EnableRetryOnFailure();
//    });
//});
builder.AddSqlServerDbContext<DataContext>("SqlServer", configureDbContextOptions: optionBuilder =>
{
    optionBuilder.UseSqlServer(sql =>
    {
        sql.UseHierarchyId();
        sql.EnableRetryOnFailure();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "Bearer",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();
app.UseOutputCache();
app.MapDefaultEndpoints();

#region NEW IDENTITY
app.MapIdentityApi<TestUser>();
#endregion

InitializeDatabase(app);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();

app.MapControllers();

app.Run();

void InitializeDatabase(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    var pendingMigrations = dbContext.Database.GetPendingMigrations();

    if (dbContext.Database.GetPendingMigrations().Any())
    {
        dbContext.Database.Migrate();
    }
}