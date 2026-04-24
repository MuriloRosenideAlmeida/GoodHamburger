using GoodHamburger.Application.Exceptions;
using GoodHamburger.Domain.Exceptions;

namespace GoodHamburger.API.Middleware;

public class ExcecaoMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExcecaoMiddleware> _logger;

    public ExcecaoMiddleware(RequestDelegate next, ILogger<ExcecaoMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext ctx)
    {
        try
        {
            await _next(ctx);
        }
        catch (NaoEncontradoException ex)
        {
            ctx.Response.StatusCode = StatusCodes.Status404NotFound;
            ctx.Response.ContentType = "application/json";
            await ctx.Response.WriteAsJsonAsync(new { erro = ex.Message });
        }
        catch (DomainException ex)
        {
            ctx.Response.StatusCode = StatusCodes.Status400BadRequest;
            ctx.Response.ContentType = "application/json";
            await ctx.Response.WriteAsJsonAsync(new { erro = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado: {Message}", ex.Message);
            ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
            ctx.Response.ContentType = "application/json";
            await ctx.Response.WriteAsJsonAsync(new { erro = "Ocorreu um erro interno. Tente novamente." });
        }
    }
}