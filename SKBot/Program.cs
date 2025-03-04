// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
using Microsoft.Agents.Hosting.AspNetCore;
using Microsoft.Agents.State;
using Microsoft.Agents.Storage;
using Microsoft.SemanticKernel;
using ObservableAgents.ServiceDefaults;
using SKBot;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient();

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Add AspNet token validation
builder.Services.AddBotAspNetAuthentication(builder.Configuration);

// Add basic bot functionality
builder.AddBot<AIBot>();

builder.Services.AddKernel();
builder.Services.AddTransient<WeatherForecastAgent>();

builder.Services.AddSingleton<IStorage>(new MemoryStorage());
builder.Services.AddSingleton<ConversationState>();


builder.Services.AddAzureOpenAIChatCompletion(
        deploymentName: builder.Configuration.GetSection("AIServices:AzureOpenAI").GetValue<string>("DeploymentName")!,
        endpoint: builder.Configuration.GetSection("AIServices:AzureOpenAI").GetValue<string>("Endpoint")!,
        apiKey: builder.Configuration.GetSection("AIServices:AzureOpenAI").GetValue<string>("ApiKey")!);


WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapGet("/", () => "Microsoft Copilot SDK Sample");
    app.UseDeveloperExceptionPage();
    app.MapControllers().AllowAnonymous();
}
else
{
    app.MapControllers();
}
app.Run();