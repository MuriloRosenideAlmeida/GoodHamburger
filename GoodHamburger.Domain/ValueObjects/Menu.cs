using GoodHamburger.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Domain.ValueObjects
{
    public static class Menu
    {
        public static readonly IReadOnlyList<MenuItem> Items = new List<MenuItem>
    {
        new("X Burger",     5.00m, TipoItem.Lanche),
        new("X Egg",        4.50m, TipoItem.Lanche),
        new("X Bacon",      7.00m, TipoItem.Lanche),
        new("Batata Frita", 2.00m, TipoItem.BatataFrita),
        new("Refrigerante", 2.50m, TipoItem.Refrigerante),
    };

        public static MenuItem? FindByName(string nome)
            => Items.FirstOrDefault(i =>
                string.Equals(i.Nome, nome, StringComparison.OrdinalIgnoreCase));
    }
}
