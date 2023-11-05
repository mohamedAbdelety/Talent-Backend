using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Person:IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime ModifiedOn { get; set; } = DateTime.Now;
        public string Name { get; set; }
        public bool IsApproved { get; set; } = false;
        public string ImgPath { get; set; }
        public PersonType PersonType { get; set; } = PersonType.Talent;
        public virtual ICollection<Media> Medias { get; set; }
        // socail media
        public virtual ICollection<SocailMedia> SocailMedias { get; set; }
    }
}
