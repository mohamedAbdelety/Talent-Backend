using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DTO
{
    public class PersonDto
    {
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime ModifiedOn { get; set; } = DateTime.Now;
        public string Name { get; set; }
        public bool IsApproved { get; set; } = false;
        public IFormFile Img { get; set; }
        public ICollection<IFormFile> Imgs { get; set; }
        public ICollection<SocailMediaDto> SocailMedias { get; set; }
    }
}
