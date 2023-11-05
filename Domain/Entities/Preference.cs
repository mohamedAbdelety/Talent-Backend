using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Preference : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime ModifiedOn { get; set; } = DateTime.Now;
        public Guid personId { get; set; }
        public virtual Person Person { get; set; }
        public Guid? contractId { get; set; }
        public virtual Contract Contract { get; set; }

    }
}
