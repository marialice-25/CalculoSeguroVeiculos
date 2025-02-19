using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculoSeguroVeiculos.Domain.Entities
{
    public class Segurado
    {
        // Identificador único do segurado
        public int SeguradoID { get; set; }

        // Nome do segurado
        public string Nome { get; set; }

        // CPF do segurado (11 caracteres)
        public string CPF { get; set; }

        // Idade do segurado
        public int Idade { get; set; }
    }
}
