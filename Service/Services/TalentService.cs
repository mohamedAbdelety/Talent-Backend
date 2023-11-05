using AutoMapper;
using Domain.Entities;
using Domain.Entities.DTO;
using Microsoft.EntityFrameworkCore;
using Repository.IRepositories;
using Repository.UnitOfWork;
using Service.DTO;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Util.DTO;
using Util.Models;

namespace Service.Services
{
    public class TalentService : ITalentService
    {
        private readonly IGenericRepository<Talent> _talentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPreferenceService _preferenceService;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public TalentService(
            IMapper mapper, 
            IUnitOfWork unitOfWork,
            IGenericRepository<Talent> talentRepository,
            IPreferenceService preferenceService,
            IFileService fileService
        )
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _talentRepository = talentRepository;
            _fileService = fileService;
            _preferenceService = preferenceService;
        }
        
        public async Task<Talent> AddTalent(talentDto talentDto)
        {
            string ImgName = await _fileService.GetUploadedFilePathAsync(talentDto.Person.Img);
            var talent = _mapper.Map<Talent>(talentDto);
            talent.Person.ImgPath = ImgName;
            if (talentDto.Person.Imgs.Count > 0)
                talent.Person.Medias = new List<Media>();
            foreach (var img in talentDto.Person.Imgs)
            {
                var path = await _fileService.GetUploadedFilePathAsync(img);
                talent.Person.Medias.Add(
                    new Media()
                    {
                        Path = path,
                        Type = MediaTybe.Image
                    }
                );
            }
            var addedTalent = await _talentRepository.AddItemAsync(talent);
            await _unitOfWork.Commit();
            return addedTalent;
        }
        public async Task<Talent> GetTalent(Guid id)
        {
            return await _talentRepository.GetItemAsync(id);
        }
        public async Task<PagedResponse<ReturnedTalent>> GetTalents(TalentFilterDTO talentfilter, PaginationFilter pagination)
        {
            IQueryable<Talent> talentQuery = _talentRepository.GetDataAsQuery(t => t.Person.IsApproved)
                                                              .Include(t => t.Person)                                              
                                                              .ThenInclude(p => p.Medias)
                                                              .Include(t => t.Person)
                                                              .ThenInclude(p => p.SocailMedias);
            talentQuery = FilterTalents(talentQuery,talentfilter);
            List<Talent> result = null;
            var TotalCount = await talentQuery.CountAsync();
            if (pagination.IsPaginationRequest)
            {
                result = await talentQuery.Skip(pagination.PageNumber * pagination.PageSize)
                                          .Take(pagination.PageSize).ToListAsync();
            }
        
            var set = await _preferenceService.GetPreferedSet();
            var talentDtos = result.Select(t =>
            {
                var talent = _mapper.Map<ReturnedTalent>(t);
                talent.IsPrefered = set.Contains(t.personId);
                return talent;
            }).ToList();

            return new PagedResponse<ReturnedTalent>(talentDtos, pagination, TotalCount);
        }
        public async Task<IEnumerable<ReturnedTalent>> GetAllTalents()
        {
            var talents = await _talentRepository.GetDataAsQuery()
                                          .Include(t => t.Person)
                                          .ThenInclude(p => p.Medias)
                                          .ToListAsync();
            return _mapper.Map<List<ReturnedTalent>>(talents);    
        }
        public async Task<Talent> ReverseApproveStatus(Guid id)
        {
            var talent = await _talentRepository.GetDataAsQuery(t => t.Id == id)
                                                .Include(t => t.Person)
                                                .FirstOrDefaultAsync();
            talent.Person.IsApproved = !talent.Person.IsApproved;
            _talentRepository.UpdateItem(talent);
            await _unitOfWork.Commit();
            return talent;
        }
        public async Task Delete(Guid id)
        {
            var talent = await _talentRepository.GetItemAsync(id);
            _talentRepository.RemoveItem(talent);
            await _unitOfWork.Commit();
        }
        public async Task DeleteAll()
        {
            var talents = await _talentRepository.GetAllAsync();
            _talentRepository.RemoveRange(talents);
            await _unitOfWork.Commit();
        }
        public bool ValidBirthDate(DateTime start,int min,int max )
        {
            DateTime end = DateTime.Now;
            int years = (end.Year - start.Year - 1) +
                (((end.Month > start.Month) ||
                ((end.Month == start.Month) && (end.Day >= start.Day))) ? 1 : 0);
            return years < max && years >= min;
        }
        private IQueryable<Talent> FilterTalents(IQueryable<Talent> talentQuery,TalentFilterDTO talentfilter)
        {
            if (!string.IsNullOrEmpty(talentfilter.Gender))
                talentQuery = talentQuery.Where(t => t.Gender == talentfilter.Gender);
            if (!string.IsNullOrEmpty(talentfilter.Eye))
                talentQuery = talentQuery.Where(t => t.Eye == (ColorType)Enum.Parse(typeof(ColorType), talentfilter.Eye));
            if (!string.IsNullOrEmpty(talentfilter.Hair))
                talentQuery = talentQuery.Where(t => t.Hair == (ColorType)Enum.Parse(typeof(ColorType), talentfilter.Hair));
            if (!string.IsNullOrEmpty(talentfilter.Body))
                talentQuery = talentQuery.Where(t => t.Body == (BodyType)Enum.Parse(typeof(BodyType), talentfilter.Body));
            if (!string.IsNullOrEmpty(talentfilter.Ethnicity))
                talentQuery = talentQuery.Where(t => t.Ethnicity == (EthnicityType)Enum.Parse(typeof(EthnicityType), talentfilter.Ethnicity));
            if (talentfilter.Age != null)
            {
                int min = 0, max = 100;
                switch (talentfilter.Age)
                {
                    case 0:
                        max = 18;
                        break;
                    case 18:
                        min = 18; max = 30;
                        break;
                    case 30:
                        min = 30; max = 45;
                        break;
                    case 45:
                        min = 45; max = 60;
                        break;
                    default:
                        min = 60;
                        break;
                }
                talentQuery = talentQuery.Where(t => DateTime.Now.Year - t.BirthDate.Year >= min && DateTime.Now.Year - t.BirthDate.Year < max);
            }
            return talentQuery;
        }
    }
}
