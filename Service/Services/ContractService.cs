using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Entities;
using Domain.Entities.DTO;
using Microsoft.EntityFrameworkCore;
using Repository.IRepositories;
using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Models;

namespace Service.Services
{
    public class ContractService : IContractService
    {
        private readonly IGenericRepository<Contract> _contractRepository;
        private readonly IGenericRepository<Preference> _preferenceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContractService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IGenericRepository<Contract> contractRepository,
            IGenericRepository<Preference> preferenceRepository
        )
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _contractRepository = contractRepository;
            _preferenceRepository = preferenceRepository;
        }
        public async Task<Contract> AddContract(AddContractDto ContractDto)
        {
            var contract = _mapper.Map<Contract>(ContractDto);
            // add Contract
            var addedContract = await _contractRepository.AddItemAsync(contract);
            await _unitOfWork.Commit();

            // update Preferences
            var preferences = await _preferenceRepository.GetDataAsQuery(p => p.contractId == null).ToListAsync();
            foreach (var pre in preferences)
                pre.contractId = contract.Id;
            _preferenceRepository.UpdateRange(preferences);
            await _unitOfWork.Commit();
            return addedContract;
        }
        public async Task Delete(Guid id)
        {
            var preferences = await _preferenceRepository.GetDataAsQuery(p => p.contractId == id).ToListAsync();
            _preferenceRepository.RemoveRange(preferences);
            var contract = await _contractRepository.GetItemAsync(id);
            _contractRepository.RemoveItem(contract);
            await _unitOfWork.Commit();
        }
        public async Task<IEnumerable<ReturnedContract>> GetAllContracts()
        {
            var contracts = await _contractRepository.GetDataAsQuery()
                                            .Include(c => c.Items)
                                            .ThenInclude(p =>  p.Person)
                                            .ToListAsync();
            return _mapper.Map<IEnumerable<ReturnedContract>>(contracts);
        }

    }
}
