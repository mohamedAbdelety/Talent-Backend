using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Star : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime ModifiedOn { get; set; } = DateTime.Now;
        public string UserName { get; set; }
        public int Followers { get; set; }
        public int Following { get; set; }
        public int Uploads { get; set; }
        public Guid personId { get; set; }
        public virtual Person Person { get; set; }
    }
}
