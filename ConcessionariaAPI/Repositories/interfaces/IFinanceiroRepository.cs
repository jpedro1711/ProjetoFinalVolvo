using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcessionariaAPI.Repositories.Dto;

namespace ConcessionariaAPI.Repositories.interfaces
{
    public interface IFinanceiroRepository<T> where T : class
    {
        Task<List<BalancoFinanceiro>> GetBalancoFinanceiro();
    }
}