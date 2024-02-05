namespace ConcessionariaAPI.Models.dtos;

public class BalancoFinanceiro
{
    public int Mes { get; set; }
    public int Ano { get; set; }
    public decimal Custos { get; set; }
    public decimal Vendido { get; set; }
    public decimal PercLucro { get; set; }
}