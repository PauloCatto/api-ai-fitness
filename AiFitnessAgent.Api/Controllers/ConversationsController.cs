using System.Security.Claims;
using AiFitnessAgent.Api.Data;
using AiFitnessAgent.Api.DTOs;
using AiFitnessAgent.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AiFitnessAgent.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ConversationsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ConversationsController(AppDbContext context)
    {
        _context = context;
    }

    private Guid GetUserId()
    {
        var raw = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");
        return Guid.Parse(raw!);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = GetUserId();
        var conversations = await _context.Conversations
            .Where(c => c.UserId == userId)
            .OrderByDescending(c => c.UpdatedAt)
            .Select(c => new
            {
                c.Id,
                c.Title,
                c.CreatedAt,
                c.UpdatedAt,
                MessageCount = c.Messages.Count
            })
            .ToListAsync();

        return Ok(conversations);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateConversationDto dto)
    {
        var userId = GetUserId();
        var conversation = new Conversation
        {
            UserId = userId,
            Title = dto.Title
        };

        _context.Conversations.Add(conversation);
        await _context.SaveChangesAsync();

        return Ok(new { conversation.Id, conversation.Title, conversation.CreatedAt, conversation.UpdatedAt });
    }

    [HttpPut("{id}/title")]
    public async Task<IActionResult> UpdateTitle(Guid id, [FromBody] UpdateConversationTitleDto dto)
    {
        var userId = GetUserId();
        var conversation = await _context.Conversations
            .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

        if (conversation is null) return NotFound();

        conversation.Title = dto.Title;
        conversation.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return Ok(new { conversation.Id, conversation.Title, conversation.UpdatedAt });
    }

    [HttpGet("{id}/messages")]
    public async Task<IActionResult> GetMessages(Guid id)
    {
        var userId = GetUserId();
        var exists = await _context.Conversations.AnyAsync(c => c.Id == id && c.UserId == userId);
        if (!exists) return NotFound();

        var messages = await _context.ConversationMessages
            .Where(m => m.ConversationId == id)
            .OrderBy(m => m.CreatedAt)
            .Select(m => new { m.Id, m.Role, m.Content, m.CreatedAt })
            .ToListAsync();

        return Ok(messages);
    }

    [HttpPost("{id}/messages")]
    public async Task<IActionResult> SaveMessages(Guid id, [FromBody] SaveMessagesDto dto)
    {
        var userId = GetUserId();
        var conversation = await _context.Conversations
            .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

        if (conversation is null) return NotFound();

        var userMsg = new ConversationMessage
        {
            ConversationId = id,
            Role = "user",
            Content = dto.UserMessage
        };

        var assistantMsg = new ConversationMessage
        {
            ConversationId = id,
            Role = "assistant",
            Content = dto.AssistantMessage,
            CreatedAt = DateTime.UtcNow.AddMilliseconds(1)
        };

        _context.ConversationMessages.AddRange(userMsg, assistantMsg);
        conversation.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return Ok(new
        {
            userMessage = new { userMsg.Id, userMsg.Role, userMsg.Content, userMsg.CreatedAt },
            assistantMessage = new { assistantMsg.Id, assistantMsg.Role, assistantMsg.Content, assistantMsg.CreatedAt }
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = GetUserId();
        var conversation = await _context.Conversations
            .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

        if (conversation is null) return NotFound();

        _context.Conversations.Remove(conversation);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
