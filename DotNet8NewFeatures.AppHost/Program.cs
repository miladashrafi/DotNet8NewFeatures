using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("Redis");
var sql = builder.AddSqlServerConnection("SqlServer");

//var sql = builder.AddSqlServerContainer("SqlServer", "P@ssw0rd")
//    .AddDatabase("dbtest").WithVolumeMount("c:\\sql\\data", "/var/opt/mssql/data");

builder.AddProject<Projects.DotNet8NewFeatures>("dotnet8newfeatures")
    .WithReference(redis)
    .WithReference(sql);

builder.Build().Run();
