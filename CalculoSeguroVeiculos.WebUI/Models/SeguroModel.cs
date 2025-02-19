namespace CalculoSeguroVeiculos.WebUI.Models
{
    public class SeguroModel
    {
        public decimal ValorVeiculo { get; set; }
        public string MarcaModelo { get; set; } = string.Empty;
        public string NomeSegurado { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public int Idade { get; set; }
        public decimal? ValorSeguro { get; set; }
        public decimal? TaxaRisco { get; set; }
        public decimal? PremioRisco { get; set; }
        public decimal? PremioPuro { get; set; }
        public decimal? PremioComercial { get; set; }
        public VeiculoModel Veiculo { get; set; }
        public SeguradoModel Segurado { get; set; }
        public List<SeguroModel> Seguros { get; set; }
    }
}
