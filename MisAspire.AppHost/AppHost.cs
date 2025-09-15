using Aspire.Hosting;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.MisAspire_WebAPI>("misaspire-webapi");

builder.Build().Run();
