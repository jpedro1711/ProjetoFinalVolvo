using ConcessionariaAPI.Exceptions;
using ConcessionariaAPI.Models;
using ConcessionariaAPI.Repositories.Dto;
using ConcessionariaAPI.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConcessionariaAPI.Repositories
{
    public class FinanceiroRepository : IFinanceiroRepository<BalancoFinanceiro>, IDisposable
    {
        private ConcessionariaContext _context;
        private VendedorRepository _vendedorRepository;
        private bool disposed = false;
        public FinanceiroRepository(ConcessionariaContext context)
        {
            _context = context;
            _vendedorRepository = new VendedorRepository(context);
        }

        public async Task<List<BalancoFinanceiro>> GetBalancoFinanceiro(){

            List<BalancoFinanceiro> balancoFinanceiro = new List<BalancoFinanceiro>();                       

            List<int?> IdsVendedores = _context.Vendedor.Select(v => v.VendedorId).ToList();

            DateTime DataInicio = _context.Vendedor.Min(vendedor => vendedor.DataAdmissao);

            int anos = DateTime.Now.Year - DataInicio.Year;
            int anoInicio = DataInicio.Year;


            for(int i = 0; i < anos; i++){
                anoInicio += 1;
                for(int j = 1; j <=12; j++){
                    
                    if(DateTime.Now.Year == anoInicio && j == DateTime.Now.Month){
                        return balancoFinanceiro;
                    }

                    BalancoFinanceiro balancoMes = new BalancoFinanceiro();

                    balancoMes.Mes = j;
                    balancoMes.Ano = anoInicio;

                    foreach(int vendedorID in IdsVendedores){
                        var salario = await _vendedorRepository.GetSalarioMesAnoNE(vendedorID, j, anoInicio);
                        if(salario != null){
                            foreach(Salario s in salario){
                                balancoMes.Custos = balancoMes.Custos + (decimal) s.SalarioCalculado;
                            }
                        }                        
                    }

                    List<int> IdsVeiculos = _context.Venda
                        .Where(v=> v.DataVenda.Month == j && v.DataVenda.Year == anoInicio).Select(v => v.VeiculoId).ToList();

                    foreach(int idVeiculo in IdsVeiculos){
                        decimal valorVeiculo = _context.Veiculo.Where(v => v.VeiculoId == idVeiculo).Select(v=> v.Valor).FirstOrDefault();

                        if(valorVeiculo != 0){
                            balancoMes.Vendido = balancoMes.Vendido + valorVeiculo;
                        }
                    }

                    decimal percLucro = ((balancoMes.Vendido - balancoMes.Custos) / balancoMes.Custos)*100;
                    balancoMes.PercLucro = percLucro;

                    balancoFinanceiro.Add(balancoMes);                   
                }
            }             

            return balancoFinanceiro;
        }     
        
        protected async virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    await _context.DisposeAsync();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
