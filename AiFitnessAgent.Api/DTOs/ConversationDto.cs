namespace AiFitnessAgent.Api.DTOs;

public class CreateConversationDto
{
    public string Title { get; set; } = "Nova Conversa";
}

public class UpdateConversationTitleDto
{
    public string Title { get; set; } = string.Empty;
}

public class SaveMessagesDto
{
    public string UserMessage { get; set; } = string.Empty;
    public string AssistantMessage { get; set; } = string.Empty;
}
