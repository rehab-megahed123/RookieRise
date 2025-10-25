using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RookieRise.Data.Enums;

namespace RookieRise.Data.Entities
{
    public class WeekendRequests : TrackableEntity
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int WorkYearId { get; set; }
        public WeekendStatus Status { get; set; }
    }
}
