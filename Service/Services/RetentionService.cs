using AutoMapper;
using Domain.Entities;
using Repository.IRepositories;
using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class RetentionService : IRetentionService
    {
        private readonly IGenericRepository<Person> _personRepository;
        private readonly IGenericRepository<Contract> _contractRepository;
        private readonly IGenericRepository<Preference> _preferenceRepository;
        private readonly IGenericRepository<Media> _mediaRepository;

        private readonly IUnitOfWork _unitOfWork;

        public RetentionService(
            IUnitOfWork unitOfWork,
            IGenericRepository<Person> personRepository,
            IGenericRepository<Contract> contractRepository,
            IGenericRepository<Preference> preferenceRepository,
            IGenericRepository<Media> mediaRepository
        )
        {
            _unitOfWork = unitOfWork;
            _personRepository = personRepository;
            _contractRepository = contractRepository;
            _preferenceRepository = preferenceRepository;
            _mediaRepository = mediaRepository;
        }
        public async Task DeleteAll()
        {
            var preferences = await _preferenceRepository.GetAllAsync();
            _preferenceRepository.RemoveRange(preferences);

            var mdias = await _mediaRepository.GetAllAsync();
            _mediaRepository.RemoveRange(mdias);

            var contracts = await _contractRepository.GetAllAsync();
            _contractRepository.RemoveRange(contracts);

            var persons = await _personRepository.GetAllAsync();
            _personRepository.RemoveRange(persons);
            await _unitOfWork.Commit();
        }
    }
}
