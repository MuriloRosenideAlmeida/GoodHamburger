using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Application.DTOs
{
    public record PedidoDto(
    Guid Id,
    string? Lanche,
    string? BatataFrita,
    string? Refrigerante,
    decimal Subtotal,
    decimal PorcentagemDesconto,
    decimal Desconto,
    decimal Total,
    DateTime CriadoEm,
    DateTime? AtualizadoEm
);

    public record CriarPedidoDto(
        List<string> Itens
    );

    public record AtualizarPedidoDto(
        List<string> Itens
    );
}
