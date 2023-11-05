using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Talent : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public ColorType Hair { get; set; }
        public ColorType Eye { get; set; }
        public BodyType Body { get; set; }
        public EthnicityType Ethnicity { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public string Education { get; set; }
        public string PreviousProject { get; set; }
        public string Award { get; set; }
        public string Training { get; set; }
        public Guid personId { get; set; }
        public virtual Person Person { get; set; }

    }
}
