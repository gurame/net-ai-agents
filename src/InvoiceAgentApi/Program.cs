using dotenv.net;
using InvoiceAgentApi;
using Microsoft.Extensions.AI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhostV2", p => p
            .WithOrigins(
                "https://localhost:5002", 
                "https://127.0.0.1:5002"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
        // .AllowCredentials() // solo si usas cookies o credentials:'include'
    );
    
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.SetIsOriginAllowed(origin =>
        {
            if (string.IsNullOrWhiteSpace(origin)) return false;
            try
            {
                var uri = new Uri(origin);
                return uri.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase) ||
                       uri.Host.Equals("127.0.0.1");
            }
            catch
            {
                return false;
            }
        })
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

    
builder.WebHost.ConfigureKestrel(options=>
{
    //options.ListenAnyIP(5001); 
});

DotEnv.Load();

string provider = "openai";
string model = "gpt-4.1-mini";
for (int i = 0; i < args.Length; i++)
{
    if (args[i] == "--provider" && i + 1 < args.Length)
        provider = args[i + 1].ToLower();
    if (args[i] == "--model" && i + 1 < args.Length)
        model = args[i + 1];
}

Startup.ConfigureServices(builder, provider, model);

var systemPromptPath = Path.Combine(AppContext.BaseDirectory, "SystemPrompt.txt");
var systemPrompt = File.ReadAllText(systemPromptPath);

var app = builder.Build();

app.UseCors("AllowLocalhostV2");

app.MapPost("/chat", async (
    List<ChatMessage> messages,
    IChatClient client,
    ChatOptions chatOptions) =>
{
    var systemPromptWithDate = systemPrompt + "\n By the way today's date is " + DateTime.Now.ToLongDateString();
    var withSystemPrompt = (new[] { new ChatMessage(ChatRole.System, systemPromptWithDate) })
        .Concat(messages)
        .ToList();

    var response = await client.GetResponseAsync(withSystemPrompt, chatOptions);
    return Results.Ok(response.Messages);
});

app.Run();