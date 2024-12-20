using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// Databases

var postgres = builder.AddPostgres("postgres").WithPgAdmin();
var catalogDb = postgres.AddDatabase("CatalogDB");

// Cache
var redis = builder.AddRedis("cache");

// DB Manager Apps

builder.AddProject<Catalog_Data_Manager>("catalog-db-mgr")
    .WithReference(catalogDb)
    .WithReference(redis);


// API Apps

var catalogApi = builder.AddProject<Catalog_API>("catalog-api")
    .WithReference(redis)
    .WithReference(catalogDb);

// Apps

builder.AddProject<WebApp>("webapp")
    .WithReference(catalogApi)
    .WithReference(redis);

// Inject assigned URLs for Catalog API
catalogApi.WithEnvironment("CatalogOptions__PicBaseAddress", () => catalogApi.GetEndpoint("http").Url);

builder.Build().Run();
