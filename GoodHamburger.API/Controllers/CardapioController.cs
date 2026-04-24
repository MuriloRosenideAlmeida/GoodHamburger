using GoodHamburger.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.API.Controllers;

[ApiController]
[Route("api/cardapio")]
public class CardapioController : ControllerBase
{
    [HttpGet]
    public IActionResult BuscarCardapio()
    {
        var itens = Menu.Items.Select(i => new
        {
            i.Nome,
            Preco = i.Preco.ToString("C2"),
            Categoria = i.Tipo.ToString()
        });

        return Ok(itens);
    }
}
