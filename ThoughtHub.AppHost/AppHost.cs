var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.ThoughtHub_Api>("main-api")
	.WithHttpHealthCheck("/health");

var website = builder.AddProject<Projects.ThoughtHub_UI_BlazorWasm>("website")
	.WithExternalHttpEndpoints()
	.WithReference(api)
	.WaitFor(api);

builder.Build().Run();