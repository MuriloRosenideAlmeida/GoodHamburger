using GoodHamburger.Domain.Exceptions;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Domain.Entities
{
    public class Pedido
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime CriadoEm { get; private set; } = DateTime.UtcNow;
        public DateTime? AtualizadoEm { get; private set; }

        public MenuItem? Lanche { get;  set; }
        public MenuItem? BatataFrita { get;  set; }
        public MenuItem? Refrigerante { get;  set; }

        public decimal Subtotal => CalcularSubtotal();
        public decimal PorcentagemDesconto => CalcularPorcentagemDesconto();
        public decimal Desconto => Subtotal * (PorcentagemDesconto / 100m);
        public decimal Total => Subtotal - Desconto;

        public void AddItem(MenuItem item)
        {
            if (item.Tipo == TipoItem.Lanche && Lanche != null)
                throw new DomainException("Pedido já contém um sanduíche.");

            if (item.Tipo == TipoItem.BatataFrita && BatataFrita != null)
                throw new DomainException("Pedido já contém batata frita.");

            if (item.Tipo == TipoItem.Refrigerante && Refrigerante != null)
                throw new DomainException("Pedido já contém um refrigerante.");

            switch (item.Tipo)
            {
                case TipoItem.Lanche: Lanche = item; break;
                case TipoItem.BatataFrita: BatataFrita = item; break;
                case TipoItem.Refrigerante: Refrigerante = item; break;
            }

            AtualizadoEm = DateTime.UtcNow;
        }

        public void RemoverItem(TipoItem type)
        {
            switch (type)
            {
                case TipoItem.Lanche: Lanche = null; break;
                case TipoItem.BatataFrita: BatataFrita = null; break;
                case TipoItem.Refrigerante: Refrigerante = null; break;
            }
            AtualizadoEm = DateTime.UtcNow;
        }

        private decimal CalcularSubtotal()
            => (Lanche?.Preco ?? 0m)
             + (BatataFrita?.Preco ?? 0m)
             + (Refrigerante?.Preco ?? 0m);

        private decimal CalcularPorcentagemDesconto()
        {
            bool hasLanche = Lanche != null;
            bool hasBatataFrita = BatataFrita != null;
            bool hasRefrigerante = Refrigerante != null;

            if (hasLanche && hasBatataFrita && hasRefrigerante) return 20m;
            if (hasLanche && hasRefrigerante) return 15m;
            if (hasLanche && hasBatataFrita) return 10m;

            return 0m;
        }
    }
}
