using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RookieRise.Data.Entities
{
    public class WorkYear : TrackableEntity
    {
        public int Id { get; set; }
        public string NameAr { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; } = new DateOnly(2025, 1, 1);
        public DateOnly EndDate { get; set; } = new DateOnly(2025, 12, 31);
        public bool IsCurrent { get; set; }
        public string? CompanyId { get; set; }
        public string? CompanyName { get; set; }
    }
}
