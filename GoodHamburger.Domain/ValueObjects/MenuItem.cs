using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Domain.ValueObjects
{
    public record MenuItem(
    string Nome,
    decimal Preco,
    TipoItem Tipo
);
}
