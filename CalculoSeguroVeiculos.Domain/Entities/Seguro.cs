using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculoSeguroVeiculos.Domain.Entities
{
    public class Seguro
    {
        // Identificador único do seguro
        public int SeguroID { get; set; }

        // Valor do seguro
        public decimal ValorSeguro { get; set; }

        // Taxa de risco (%)
        public decimal TaxaRisco { get; set; }

        // Prêmio de risco
        public decimal PremioRisco { get; set; }

        // Prêmio puro
        public decimal PremioPuro { get; set; }

        // Prêmio comercial
        public decimal PremioComercial { get; set; }

        // Chave estrangeira para a tabela Segurado
        public int SeguradoID { get; set; }

        // Chave estrangeira para a tabela Veiculo
        public int VeiculoID { get; set; }

        // Objeto de navegação para a tabela Veiculo
        public Veiculo Veiculo { get; set; } = new();

        // Objeto de navegação para a tabela Segurado
        public Segurado Segurado { get; set; } = new();
    }
}
