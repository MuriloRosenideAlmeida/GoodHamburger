using GoodHamburger.Application.DTOs;

namespace GoodHamburger.Application.Interfaces;

public interface IPedidoService
{
    Task<IEnumerable<PedidoDto>> BuscarTodosAsync();
    Task<PedidoDto> BuscarPorIdAsync(Guid id);
    Task<PedidoDto> CriarAsync(CriarPedidoDto dto);
    Task<PedidoDto> AtualizarAsync(Guid id, AtualizarPedidoDto dto);
    Task DeletarAsync(Guid id);
}