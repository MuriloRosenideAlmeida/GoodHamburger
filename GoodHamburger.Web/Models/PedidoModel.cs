namespace GoodHamburger.Web.Models;

public class PedidoModel
{
    public Guid Id { get; set; }
    public string? Lanche { get; set; }
    public string? BatataFrita { get; set; }
    public string? Refrigerante { get; set; }
    public decimal Subtotal { get; set; }
    public decimal PorcentagemDesconto { get; set; }
    public decimal Desconto { get; set; }
    public decimal Total { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime? AtualizadoEm { get; set; }
}

public class ItemCardapioModel
{
    public string Nome { get; set; } = string.Empty;
    public string Preco { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
}