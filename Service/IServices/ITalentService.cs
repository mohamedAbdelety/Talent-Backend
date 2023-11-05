using Domain.Entities;
using Domain.Entities.DTO;
using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.DTO;
using Util.Models;

namespace Service.IServices
{
    public interface ITalentService
    {
        Task<Talent> GetTalent(Guid id);
        Task<PagedResponse<ReturnedTalent>> GetTalents(TalentFilterDTO talentfilter, PaginationFilter pagination);
        Task<IEnumerable<ReturnedTalent>> GetAllTalents();
        Task<Talent> AddTalent(talentDto talent);
        Task<Talent> ReverseApproveStatus(Guid id);
        Task Delete(Guid id);
        Task DeleteAll();
    }
}
