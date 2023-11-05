using Domain.Entities.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Service.DTO;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Models;
using AutoMapper;
using Repository.IRepositories;
using Repository.UnitOfWork;
using System.ComponentModel.Design;
using Google.Apis.YouTube.v3.Data;
using System.ComponentModel;
using OfficeOpenXml;

namespace Service.Services
{
    public class StarService : IStarService
    {
        private readonly IGenericRepository<Star> _starRepository;
        private readonly IPreferenceService _preferenceService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IExcelServiceUtil _ExcelServiceUtil;

        public StarService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IPreferenceService preferenceService,
            IGenericRepository<Star> starRepository,
            IExcelServiceUtil ExcelServiceUtil
        )
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _starRepository = starRepository;
            _preferenceService = preferenceService;
            _ExcelServiceUtil = ExcelServiceUtil;
        }

        public async Task Delete(Guid id)
        {
            var star = await _starRepository.GetItemAsync(id);
            _starRepository.RemoveItem(star);
            await _unitOfWork.Commit();
        }

        public async Task DeleteAll()
        {
            var talents = await _starRepository.GetAllAsync();
            _starRepository.RemoveRange(talents);
            await _unitOfWork.Commit();
        }

        public async Task<PagedResponse<ReturnedStar>> Get(StarFilterDto starfilter, PaginationFilter pagination)
        {
            IQueryable<Star> starQuery = _starRepository.GetDataAsQuery(s => s.Person.IsApproved)
                                                              .Include(s => s.Person)
                                                              .ThenInclude(p => p.Medias)
                                                              .Include(s => s.Person)
                                                              .ThenInclude(p => p.SocailMedias);
            if (starfilter.Followers != null)
                starQuery = FilterFollowers(starQuery, (int)starfilter.Followers);
            if (starfilter.Uploads != null)
                starQuery = FilterUploads(starQuery, (int)starfilter.Uploads);
            List<Star> result = null;
            var TotalCount = await starQuery.CountAsync();
            if (pagination.IsPaginationRequest)
            {
                result = await starQuery.Skip(pagination.PageNumber * pagination.PageSize)
                                          .Take(pagination.PageSize).ToListAsync();
            }else 
                result = await starQuery.ToListAsync();
            var set = await _preferenceService.GetPreferedSet();
            var starDtos =  result.Select(s =>
            {
                var star = _mapper.Map<ReturnedStar>(s);
                star.IsPrefered = set.Contains(s.personId);
                return star;
            }).ToList();
            return new PagedResponse<ReturnedStar>(starDtos, pagination, TotalCount);
        }

        public async Task<IEnumerable<ReturnedStar>> GetAllStars()
        {
            var stars = await _starRepository.GetDataAsQuery()
                                            .Include(s => s.Person)
                                            .ThenInclude(p => p.Medias)
                                            .Include(s => s.Person)
                                            .ThenInclude(p => p.SocailMedias)
                                            .ToListAsync();
            return _mapper.Map<List<ReturnedStar>>(stars);
        }

        public async Task ImportStarFile(StarImportDTO StarImportDTO)
        {
            string filePath = await _ExcelServiceUtil.GetUploadedFilePathAsync(StarImportDTO.File);
            List<Star> stars = new List<Star>();
            var lines = await File.ReadAllLinesAsync(filePath);
            for (int i = 1;i < lines.Length;i++)
            {
                // username,full_name,image_url,followers,following,Uploadedposts,photos,active_user
                string line = lines[i];
                var record = line.Split(','); // 8 records
                // try parse
                string followersStr = record[3];
                int followers = 0, following = 0, uploads = 0;
                if (followersStr[followersStr.Length - 1] == 'K')
                    followersStr = followersStr.Substring(0, followersStr.Length - 1) + "000";

                int.TryParse(followersStr, out followers);
                int.TryParse(record[4], out following);
                int.TryParse(record[5], out uploads);

                if (record[record.Length - 1] == "-1" || followers == -1 || following == -1 || uploads == -1)
                    continue;
                var star = new Star()
                {
                    Followers = followers,
                    Following = following,
                    Uploads = uploads,
                    UserName = record[0],
                    Person = new Person()
                    {
                        PersonType = PersonType.Star,
                        IsApproved = true,
                        Name = record[1],
                        ImgPath = record[2],
                        Medias = new List<Media>()              
                    }
                };
                stars.Add(star);
            }
            await _starRepository.AddRangeAsync(stars);
            await _unitOfWork.Commit();
        }

        public async Task<Star> ReverseApproveStatus(Guid id)
        {
            var star = await _starRepository.GetDataAsQuery(s => s.Id == id)
                                                .Include(s => s.Person)
                                                .FirstOrDefaultAsync();
            star.Person.IsApproved = !star.Person.IsApproved;
            _starRepository.UpdateItem(star);
            await _unitOfWork.Commit();
            return star;
        }
        private IQueryable<Star> FilterFollowers(IQueryable<Star> starQuery, int followers)
        {
            int min = 0, max = Int32.MaxValue;
            switch (followers)
            {
                case 0:
                    max = 100;
                    break;
                case 100:
                    min = 100; max = 1000;
                    break;
                case 1000:
                    min = 1000; max = 10000;
                    break;
                case 10000:
                    min = 10000; max = 100000;
                    break;
                default:
                    min = 100000;
                    break;
            }
            return starQuery.Where(s => s.Followers >= min && s.Followers < max);
        }
        private IQueryable<Star> FilterUploads(IQueryable<Star> starQuery, int uploads)
        {
            int min = 0, max = Int32.MaxValue;
            switch (uploads)
            {
                case 0:
                    max = 10;
                    break;
                case 10:
                    min = 10; max = 50;
                    break;
                case 50:
                    min = 50; max = 100;
                    break;
                case 100:
                    min = 100; max = 500;
                    break;
                case 500:
                    min = 500; max = 1000;
                    break;
                default:
                    min = 1000;
                    break;
            }
            return starQuery.Where(s => s.Uploads >= min && s.Uploads < max);
        }

    }
}
