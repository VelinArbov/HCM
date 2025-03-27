var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.HCM_ApiService>("apiservice");

builder.AddProject<Projects.HCM_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
