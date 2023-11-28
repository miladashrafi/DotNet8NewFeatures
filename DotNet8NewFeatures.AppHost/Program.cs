var builder = DistributedApplication.CreateBuilder(args);


// comment to use external resources as containers, uncomment below section
#region Only Use Resources with my own connection strings
var redis = builder.AddRedis("Redis");
var sql = builder.AddSqlServerConnection("SqlServer");
#endregion

//comment to use your own external resources, uncomment upper section
#region Automatically create a container for every resource, use it and then clear on application shutdown.
//var sql = builder.AddSqlServerContainer("SqlServer", "P@ssw0rd")
//    .AddDatabase("dbtest").WithVolumeMount("c:\\sql\\data", "/var/opt/mssql/data");

//var redis = builder.AddRedisContainer("Redis"); 
#endregion

builder.AddProject<Projects.DotNet8NewFeatures>("dotnet8newfeatures")
    .WithReference(redis)
    .WithReference(sql);

builder.Build().Run();
