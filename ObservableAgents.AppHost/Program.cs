var builder = DistributedApplication.CreateBuilder(args);
builder.AddProject<Projects.SKBot>("skbot");
builder.AddProject<Projects.RootBoot>("rootboot");
builder.Build().Run();
