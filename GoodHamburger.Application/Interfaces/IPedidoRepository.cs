using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Interfaces;

public interface IPedidoRepository
{
    Task<IEnumerable<Pedido>> BuscarTodosAsync();
    Task<Pedido?> BuscarPorIdAsync(Guid id);
    Task AdicionarAsync(Pedido pedido);
    Task AtualizarAsync(Pedido pedido);
    Task DeletarAsync(Pedido pedido);
}