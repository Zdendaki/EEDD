using Server;
using Server.Endpoints;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Logging.AddSimpleConsole(o =>
{
    o.SingleLine = true;
    o.TimestampFormat = "HH:mm:ss ";
    o.IncludeScopes = false;
});

if (OperatingSystem.IsWindows())
{
    builder.Services.AddWindowsService(o => o.ServiceName = $"EDD Server");
}

builder.Services.AddSingleton<RuntimeData>();
builder.Services.AddSingleton<EddServer>();
builder.Services.AddTransient<EddSession>();
builder.Services.AddSingleton<EvalServer>();
builder.Services.AddTransient<EvalSession>();
builder.Services.AddHostedService<Worker>();

IHost host = builder.Build();
host.Run();