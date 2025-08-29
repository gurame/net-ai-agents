using ConsoleAgent;
using dotenv.net;
using Microsoft.Extensions.Hosting;

DotEnv.Load();

var provider = "openai";
var model = "gpt-4.1-mini";
for (var i = 0; i < args.Length; i++)
{
    switch (args[i])
    {
        case "--provider" when i + 1 < args.Length:
            provider = args[i + 1];
            break;
        case "--model" when i + 1 < args.Length:
            model = args[i + 1];
            break;
    }
}

var builder = Host.CreateApplicationBuilder(args);
Startup.ConfigureServices(builder, provider, model);
var host = builder.Build();

await ChatAgent.RunAsync(host.Services);