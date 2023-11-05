using Domain.Entities;
using Domain.Entities.DTO;
using Service.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Models;

namespace Service.IServices
{
    public interface IStarService
    {
        Task<PagedResponse<ReturnedStar>> Get(StarFilterDto talentfilter, PaginationFilter pagination);
        Task<IEnumerable<ReturnedStar>> GetAllStars();
        Task<Star> ReverseApproveStatus(Guid id);
        Task ImportStarFile(StarImportDTO StarImportDTO);
        Task Delete(Guid id);
        Task DeleteAll();
    }
}
