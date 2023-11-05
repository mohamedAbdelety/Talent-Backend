using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DTO
{
    public class ReturnedStar
    {
        public Guid Id { get; set; }
        public Guid personId { get; set; }
        public string UserName { get; set; }
        public int Followers { get; set; }
        public int Following { get; set; }
        public int Uploads { get; set; }
        public string Name { get; set; }
        public bool IsApproved { get; set; } = true;
        public bool IsPrefered { get; set; } = false;
        public string Img { get; set; }
        public List<string> Medias { get; set; }
    }
}
