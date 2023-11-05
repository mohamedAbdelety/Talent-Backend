using Domain.Entities.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IPreferenceService
    {
        Task<PreferenceDto> HirePerson(Guid personId);
        Task<int> GetPreferences();
        Task<IEnumerable<PreferenceDto>> GetPreferencesList();
        Task<bool> IsPrefered(Guid personId);
        Task<HashSet<Guid>> GetPreferedSet();
        Task Delete(Guid id);
        Task DeleteAll();


    }
}
