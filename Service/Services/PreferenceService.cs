using AutoMapper;
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
    public class PreferenceService : IPreferenceService
    {
        private readonly IGenericRepository<Preference> _prefrenceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PreferenceService(
                IGenericRepository<Preference> prefrenceRepository,
                IUnitOfWork unitOfWork,
                IMapper mapper
            ) {
            _prefrenceRepository = prefrenceRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        
        public async Task Delete(Guid id)
        {
            var preference = await _prefrenceRepository.GetItemAsync(id);
            if (preference != null)
            {
                _prefrenceRepository.RemoveItem(preference);
                await _unitOfWork.Commit();
            }
        }
        public async Task DeleteAll()
        {
            var preferences = await _prefrenceRepository.GetDataAsQuery().ToListAsync();
            _prefrenceRepository.RemoveRange(preferences);
            await _unitOfWork.Commit();
        }

        public async Task<HashSet<Guid>> GetPreferedSet()
        {
            var lst = await _prefrenceRepository.GetDataAsQuery(p => p.contractId == null)
                                                .GroupBy(p => p.personId)
                                                .Select(g => g.Key)
                                                .ToListAsync();
            return lst.ToHashSet();
        }

        public async Task<int> GetPreferences()
        {
            return await _prefrenceRepository.GetDataAsQuery(p => p.contractId == null).CountAsync();
        }

        public async Task<IEnumerable<PreferenceDto>> GetPreferencesList()
        {
            var prefrences = await _prefrenceRepository.GetDataAsQuery(p => p.contractId == null)
                                                 .Include(p => p.Person)
                                                 .ToListAsync();
            return _mapper.Map<IEnumerable<PreferenceDto>>(prefrences);
        }

        private async Task<PreferenceDto> GetPreferenceById(Guid id)
        {
            var pre = await _prefrenceRepository.GetDataAsQuery(p => p.Id == id)
                .Include(p => p.Person).FirstOrDefaultAsync();
            return _mapper.Map<PreferenceDto>(pre);
        }

        public async Task<PreferenceDto> HirePerson(Guid personId)
        {
            var preference = await _prefrenceRepository.GetDataAsQuery(p => p.contractId == null && p.personId == personId).FirstOrDefaultAsync();
            if (preference == null)
            {
                var pre = await _prefrenceRepository.AddItemAsync(new Preference() {
                    personId = personId,
                });
                await _unitOfWork.Commit();
                // get added one
                return await GetPreferenceById(pre.Id);
            }
            return null;
        }

        public async Task<bool> IsPrefered(Guid personId)
        {
            var preference = await _prefrenceRepository.GetDataAsQuery(p => p.personId == personId).FirstOrDefaultAsync();
            return preference != null;
        }
    }
}
