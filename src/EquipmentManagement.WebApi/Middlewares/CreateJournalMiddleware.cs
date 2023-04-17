using EquipmentManagement.Application.Abstractions;

namespace EquipmentManagement.WebApi.Middlewares;

public class CreateJournalMiddleware
{
    private readonly RequestDelegate _next;

    public CreateJournalMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IJournalFactory journalFactory)
    {
        var username = context.User?.Identity?.Name ?? "неизвестно";
        journalFactory.Create(username);
        await _next.Invoke(context);
    }
}
