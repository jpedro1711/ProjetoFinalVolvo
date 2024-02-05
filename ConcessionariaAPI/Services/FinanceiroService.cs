using ConcessionariaAPI.Models;
using ConcessionariaAPI.Repositories;
using ConcessionariaAPI.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ConcessionariaAPI.Repositories.Dto;
using ConcessionariaAPI.Services.interfaces;
using ConcessionariaAPI.Repositories.interfaces;

namespace ConcessionariaAPI.Services
{
    public class FinanceiroService : IFinanceiroService<BalancoFinanceiro>
    {
        private IFinanceiroRepository<BalancoFinanceiro> _repository;        
        public FinanceiroService(ConcessionariaContext context) 
        {
            _repository = new FinanceiroRepository(context);            
        }

        public async Task<List<BalancoFinanceiro>> GetBalancoFinanceiro(){
            return await _repository.GetBalancoFinanceiro();
        }     
    }
}
