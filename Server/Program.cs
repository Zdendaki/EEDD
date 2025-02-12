using Server;
using Server.Endpoints;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

if (OperatingSystem.IsWindows())
{
    builder.Services.AddWindowsService(o => o.ServiceName = $"EDD Server");
}

builder.Services.AddSingleton<RuntimeData>();
builder.Services.AddSingleton<EddServer>();
builder.Services.AddTransient<EddSession>();
builder.Services.AddHostedService<Worker>();

IHost host = builder.Build();
host.Run();