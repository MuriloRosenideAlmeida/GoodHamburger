using GoodHamburger.Application.DTOs;
using GoodHamburger.Application.Interfaces;
using GoodHamburger.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.API.Controllers;

[ApiController]
[Route("api/pedidos")]
public class PedidosController : ControllerBase
{
    private readonly IPedidoService _servico;

    public PedidosController(IPedidoService servico) => _servico = servico;

    [HttpGet]
    public async Task<IActionResult> BuscarTodos()
        => Ok(await _servico.BuscarTodosAsync());

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> BuscarPorId(Guid id)
        => Ok(await _servico.BuscarPorIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarPedidoDto dto)
    {
        var pedido = await _servico.CriarAsync(dto);
        return CreatedAtAction(nameof(BuscarPorId), new { id = pedido.Id }, pedido);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarPedidoDto dto)
        => Ok(await _servico.AtualizarAsync(id, dto));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Deletar(Guid id)
    {
        await _servico.DeletarAsync(id);
        return NoContent();
    }
}