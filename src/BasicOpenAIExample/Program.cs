using dotenv.net;
using OpenAI.Chat;

DotEnv.Load();
var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
if (string.IsNullOrEmpty(apiKey))
    throw new InvalidOperationException("OPENAI_API_KEY environment variable not set");
    
ChatClient client = new(model: "gpt-5-nano", apiKey: apiKey);
List<ChatMessage> messages =
[
    new AssistantChatMessage("Hello, what do you want to do today?")
];

Console.WriteLine(messages[0].Content[0].Text);
while (true)
{
    Console.ForegroundColor = ConsoleColor.Blue;
    var userInput = Console.ReadLine();
    if (string.IsNullOrEmpty(userInput))
        break;
    Console.ResetColor();

    messages.Add(new UserChatMessage(userInput));

    ChatCompletion completion = client.CompleteChat(messages);
    
    var response = completion.Content[0].Text;
    messages.Add(new AssistantChatMessage(response));
    Console.WriteLine(response);
}