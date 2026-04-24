using GoodHamburger.Application.DTOs;
using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Mappings;

public static class PedidoMappings
{
    public static PedidoDto ParaDto(this Pedido p) => new(
        p.Id,
        p.Lanche?.Nome,
        p.BatataFrita?.Nome,
        p.Refrigerante?.Nome,
        p.Subtotal,
        p.PorcentagemDesconto,
        p.Desconto,
        p.Total,
        p.CriadoEm,
        p.AtualizadoEm
    );
}