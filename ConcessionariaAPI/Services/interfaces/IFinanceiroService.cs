using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcessionariaAPI.Models.dtos;

namespace ConcessionariaAPI.Services.interfaces
{
    public interface IFinanceiroService
    {
        Task<List<BalancoFinanceiro>> GetBalancoFinanceiro();
    }
}