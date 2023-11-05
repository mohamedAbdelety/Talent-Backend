using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Domain.Entities.DTO
{

    public class talentDto {
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime ModifiedOn { get; set; } = DateTime.Now;
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
        public bool IsApproved { get; set; } = false;
        public PersonDto Person { get; set; }
    }
}
