using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO
{
    public class TalentFilterDTO
    {
        public string Gender { get; set; }
        public string Body { get; set; }
        public string Hair { get; set; }
        public string Eye { get; set; }
        public string Ethnicity { get; set; }
        public int? Age { get; set; }
    }
}
