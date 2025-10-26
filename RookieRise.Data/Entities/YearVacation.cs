using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RookieRise.Data.Entities
{
    public class YearVacation : TrackableEntity
    {
        public int Id { get; set; }
        public int WorkYearId { get; set; }
        public int VacationTypeId { get; set; }
        public bool IsActive { get; set; }
    }
}
