using GoodHamburger.Application.DTOs;
using GoodHamburger.Application.Exceptions;
using GoodHamburger.Application.Interfaces;
using GoodHamburger.Application.Mappings;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Exceptions;
using GoodHamburger.Domain.ValueObjects;

namespace GoodHamburger.Application.Services;

public class PedidoServico : IPedidoService
{
    private readonly IPedidoRepository _repositorio;

    public PedidoServico(IPedidoRepository repositorio) => _repositorio = repositorio;

    public async Task<IEnumerable<PedidoDto>> BuscarTodosAsync()
    {
        var pedidos = await _repositorio.BuscarTodosAsync();
        return pedidos.Select(p => p.ParaDto());
    }

    public async Task<PedidoDto> BuscarPorIdAsync(Guid id)
    {
        var pedido = await _repositorio.BuscarPorIdAsync(id)
            ?? throw new NaoEncontradoException($"Pedido '{id}' não encontrado.");

        return pedido.ParaDto();
    }

    public async Task<PedidoDto> CriarAsync(CriarPedidoDto dto)
    {
        if (dto.Itens == null || dto.Itens.Count == 0)
            throw new DomainException("O pedido deve conter pelo menos um item.");

        var pedido = new Pedido();

        foreach (var nomeItem in dto.Itens)
        {
            var itemCardapio = Menu.FindByName(nomeItem)
                ?? throw new NaoEncontradoException($"Item '{nomeItem}' não existe no cardápio.");

            pedido.AddItem(itemCardapio);
        }

        await _repositorio.AdicionarAsync(pedido);
        return pedido.ParaDto();
    }

    public async Task<PedidoDto> AtualizarAsync(Guid id, AtualizarPedidoDto dto)
    {
        var pedido = await _repositorio.BuscarPorIdAsync(id)
            ?? throw new NaoEncontradoException($"Pedido '{id}' não encontrado.");

        if (dto.Itens == null || dto.Itens.Count == 0)
            throw new DomainException("O pedido deve conter pelo menos um item.");

        pedido.RemoverItem(TipoItem.Lanche);
        pedido.RemoverItem(TipoItem.BatataFrita);
        pedido.RemoverItem(TipoItem.Refrigerante);

        foreach (var nomeItem in dto.Itens)
        {
            var itemCardapio = Menu.FindByName(nomeItem)
                ?? throw new NaoEncontradoException($"Item '{nomeItem}' não encontrado.");

            pedido.AddItem(itemCardapio);
        }

        await _repositorio.AtualizarAsync(pedido);
        return pedido.ParaDto();
    }

    public async Task DeletarAsync(Guid id)
    {
        var pedido = await _repositorio.BuscarPorIdAsync(id)
            ?? throw new NaoEncontradoException($"Pedido '{id}' não encontrado.");

        await _repositorio.DeletarAsync(pedido);
    }
}