using GoodHamburger.Application.Interfaces;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Infrastructure.Repositories;

public class PedidoRepositorio : IPedidoRepository
{
    private readonly AppDbContext _ctx;

    public PedidoRepositorio(AppDbContext ctx) => _ctx = ctx;

    public async Task<IEnumerable<Pedido>> BuscarTodosAsync()
        => await _ctx.Pedidos.ToListAsync();

    public async Task<Pedido?> BuscarPorIdAsync(Guid id)
        => await _ctx.Pedidos.FindAsync(id);

    public async Task AdicionarAsync(Pedido pedido)
    {
        await _ctx.Pedidos.AddAsync(pedido);
        await _ctx.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Pedido pedido)
    {
        _ctx.Pedidos.Update(pedido);
        await _ctx.SaveChangesAsync();
    }

    public async Task DeletarAsync(Pedido pedido)
    {
        _ctx.Pedidos.Remove(pedido);
        await _ctx.SaveChangesAsync();
    }
}