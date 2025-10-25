using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RookieRise.Data.Entities
{
    public class OfficialVacancy : TrackableEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Arabic Name is required.")]
        [MaxLength(100, ErrorMessage = "Arabic Name cannot exceed 100 characters.")]
        public string NameAr { get; set; }
        [Required(ErrorMessage = "English Name is required.")]
        [MaxLength(100, ErrorMessage = "English Name cannot exceed 100 characters.")]
        public string NameEn { get; set; }
        [Required(ErrorMessage = "Start Date is required.")]
        public DateOnly StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required.")]
        public DateOnly EndDate { get; set; }
        public int VacationDays { get; set; }
        public bool IsActive { get; set; } = true;
        public int WorkYear { get; set; }
        public string? CompanyId { get; set; }
        public string? CompanyName { get; set; }

    }
}
