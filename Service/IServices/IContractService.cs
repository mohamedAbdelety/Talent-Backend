using Domain.Entities;
using Domain.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IContractService
    {
        public Task<Contract> AddContract(AddContractDto ContractDto);
        Task<IEnumerable<ReturnedContract>> GetAllContracts();
        Task Delete(Guid id);


    }
}
