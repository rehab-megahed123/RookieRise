using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RookieRise.Data.Enums;

namespace RookieRise.Data.Entities
{
    public class EmployeeVacation : TrackableEntity
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string CompanyId { get; set; }
        public string? EmployeeName { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int VacationTypeId { get; set; }
        public int WorkYearId { get; set; }
        public VacationStatus Status { get; set; }
        public string? Reason { get; set; }
        public string? Attachment { get; set; }
    }
}
