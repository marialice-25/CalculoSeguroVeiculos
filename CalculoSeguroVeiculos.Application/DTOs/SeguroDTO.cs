using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculoSeguroVeiculos.Application.DTOs
{
    public class SeguroDto
    {
        public decimal ValorVeiculo { get; set; }
        public string MarcaModelo { get; set; } = string.Empty;
        public string NomeSegurado { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public int Idade { get; set; }
    }
}
