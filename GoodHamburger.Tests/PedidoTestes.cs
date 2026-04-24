using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Exceptions;
using GoodHamburger.Domain.ValueObjects;

namespace GoodHamburger.Tests;

public class PedidoTestes
{
    // itens reutilizáveis em todos os testes
    private static readonly MenuItem XBurger = new("X Burger", 5.00m, TipoItem.Lanche);
    private static readonly MenuItem XEgg = new("X Egg", 4.50m, TipoItem.Lanche);
    private static readonly MenuItem XBacon = new("X Bacon", 7.00m, TipoItem.Lanche);
    private static readonly MenuItem BatataFrita = new("Batata Frita", 2.00m, TipoItem.BatataFrita);
    private static readonly MenuItem Refrigerante = new("Refrigerante", 2.50m, TipoItem.Refrigerante);

    // Testes de desconto 

    [Fact]
    public void Lanche_Batata_Refrigerante_20_Porcento_Desconto()
    {
        var pedido = new Pedido();
        pedido.AddItem(XBurger);
        pedido.AddItem(BatataFrita);
        pedido.AddItem(Refrigerante);

        Assert.Equal(20m, pedido.PorcentagemDesconto);
        Assert.Equal(9.50m, pedido.Subtotal);
        Assert.Equal(1.90m, pedido.Desconto);
        Assert.Equal(7.60m, pedido.Total);
    }

    [Fact]
    public void Lanche_Refrigerante_15_Porcento_Desconto()
    {
        var pedido = new Pedido();
        pedido.AddItem(XBurger);
        pedido.AddItem(Refrigerante);

        Assert.Equal(15m, pedido.PorcentagemDesconto);
        Assert.Equal(7.50m, pedido.Subtotal);
        Assert.Equal(6.375m, pedido.Total);
    }

    [Fact]
    public void Lanche_Batata_10_Porcento_Desconto()
    {
        var pedido = new Pedido();
        pedido.AddItem(XBurger);
        pedido.AddItem(BatataFrita);

        Assert.Equal(10m, pedido.PorcentagemDesconto);
        Assert.Equal(7.00m, pedido.Subtotal);
        Assert.Equal(6.30m, pedido.Total);
    }

    [Fact]
    public void Lanche_Nao_Deve_Ter_Desconto()
    {
        var pedido = new Pedido();
        pedido.AddItem(XBacon);

        Assert.Equal(0m, pedido.PorcentagemDesconto);
        Assert.Equal(7.00m, pedido.Total);
    }

    [Fact]
    public void XBacon_Combo_Completo_Calcular()
    {
        var pedido = new Pedido();
        pedido.AddItem(XBacon);
        pedido.AddItem(BatataFrita);
        pedido.AddItem(Refrigerante);

        Assert.Equal(11.50m, pedido.Subtotal);
        Assert.Equal(20m, pedido.PorcentagemDesconto);
        Assert.Equal(9.20m, pedido.Total);
    }

    // Testes de validação 

    [Fact]
    public void Adicionar_Dois_Lanches_Lancar_Excecao()
    {
        var pedido = new Pedido();
        pedido.AddItem(XBurger);

        var ex = Assert.Throws<DomainException>(() => pedido.AddItem(XEgg));
        Assert.Contains("sanduíche", ex.Message.ToLower());
    }

    [Fact]
    public void Adicionar_Duas_Batatas_Lancar_Excecao()
    {
        var pedido = new Pedido();
        pedido.AddItem(BatataFrita);

        var ex = Assert.Throws<DomainException>(() => pedido.AddItem(BatataFrita));
        Assert.Contains("batata", ex.Message.ToLower());
    }

    [Fact]
    public void Adicionar_Dois_Refrigerantes_Lancar_Excecao()
    {
        var pedido = new Pedido();
        pedido.AddItem(Refrigerante);

        var ex = Assert.Throws<DomainException>(() => pedido.AddItem(Refrigerante));
        Assert.Contains("refrigerante", ex.Message.ToLower());
    }

    //Testes de remoção

    [Fact]
    public void Remover_Lanche_Zerar_Desconto()
    {
        var pedido = new Pedido();
        pedido.AddItem(XBurger);
        pedido.AddItem(BatataFrita);
        pedido.AddItem(Refrigerante);

        pedido.RemoverItem(TipoItem.Lanche);

        Assert.Null(pedido.Lanche);
        Assert.Equal(0m, pedido.PorcentagemDesconto);
    }
}