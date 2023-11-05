using Domain.Interfaces;
using System;

namespace Domain.Entities
{
    public class Media : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public virtual Person Person { get; set; }
        public string Path { get; set; }
        public MediaTybe Type { get; set; }
    }
}
