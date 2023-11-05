using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DTO
{
    public class PreferenceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        // base 64
        public string Img { get; set; }
    }
}
