using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DTO
{
    public class MediaDto
    {
        public Byte[] content { get; set; }
        public MediaTybe type { get; set; }
    }
}
