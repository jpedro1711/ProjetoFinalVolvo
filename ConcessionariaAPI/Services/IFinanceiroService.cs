using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcessionariaAPI.Repositories.Dto;

namespace ConcessionariaAPI.Services
{
    public interface IFinanceiroService<T> where T : class
    {
        Task<List<BalancoFinanceiro>> GetBalancoFinanceiro();         
    }
}