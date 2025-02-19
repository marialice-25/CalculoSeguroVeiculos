using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculoSeguroVeiculos.Domain.Entities
{
    public class Veiculo
    {
        // Identificador único do veículo
        public int VeiculoID { get; set; }

        // Valor do veículo
        public decimal Valor { get; set; }

        // Marca e modelo do veículo
        public string MarcaModelo { get; set; }
    }
}
