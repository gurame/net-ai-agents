using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleAgent;

public static class ChatAgent
{
    public static async Task RunAsync(IServiceProvider sp)
    {
        var client = sp.GetRequiredService<IChatClient>();
        var chatOptions = sp.GetRequiredService<ChatOptions>();

        var history = new List<ChatMessage>
        {
            new ChatMessage(ChatRole.System, "You are a helpful CLI assistant."),
        };

        Console.WriteLine("Ask me anything (empty = exit).");
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("> ");
            var userInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(userInput))
            {
                break;
            }
            Console.ResetColor();
            
            history.Add(new ChatMessage(ChatRole.User, userInput));
            
            var response = await client.GetResponseAsync(history, chatOptions);
            
            Console.WriteLine(response.Text);
            history.Add(new ChatMessage(ChatRole.Assistant, response.Text));
        }
    }
}