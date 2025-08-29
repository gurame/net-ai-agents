using Anthropic.SDK;
using GeminiDotnet;
using GeminiDotnet.Extensions.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ConsoleAgent;
public static class Startup
{
    public static void ConfigureServices(HostApplicationBuilder builder, string provider, string model)
    {
        builder.Services.AddLogging(logging=>
        {
            logging.AddConsole().SetMinimumLevel(LogLevel.Information);
        });
        builder.Services.AddSingleton<ILoggerFactory>(sp =>
        {
            return LoggerFactory.Create(logging =>
            {
                logging.AddConsole().SetMinimumLevel(LogLevel.Information);
            });
        });

        builder.Services.AddSingleton<IChatClient>(sp =>
        {
            var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
            var client = provider switch
            {
                "openai" => new OpenAI.Chat.ChatClient(
                    model, 
                    Environment.GetEnvironmentVariable("OPENAI_API_KEY"))
                    .AsIChatClient(),
                "gemini" => new GeminiChatClient(new GeminiDotnet.GeminiClientOptions()
                        {
                            ApiKey = Environment.GetEnvironmentVariable("GEMINI_API_KEY")!,
                            ModelId = model,
                            ApiVersion = GeminiApiVersions.V1Beta,
                        }
                    ),
                "claude" => new AnthropicClient(new APIAuthentication(Environment.GetEnvironmentVariable("CLOUDE_API_KEY")!)).Messages,
                _ => throw new NotSupportedException($"The provider '{provider}' is not supported. Supported providers are 'openai' and 'gemini'.")
            };

            return new ChatClientBuilder(client)
                .UseLogging(loggerFactory)
                .UseFunctionInvocation(loggerFactory, c =>
                {
                    c.IncludeDetailedErrors = true;
                }).Build(sp);
        });

        builder.Services.AddTransient<ChatOptions>(sp => new ChatOptions()
        {
            ModelId = model,
            Temperature = 1,
            MaxOutputTokens = 5000
        });
    }
}