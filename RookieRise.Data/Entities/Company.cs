using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RookieRise.Data.Entities
{
   public class Company :TrackableEntity
    #region NotesForMe  
        //1-inherit TrackableEntity عشان تساعدنا بعد كده ن auditكل action مين عمله وفى أي وقت 
    #endregion
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string WebsiteUrl { get; set; }
        public string? LogoUrl { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? UserId { get; set; }
        public TimeSpan? CoreStartTime { get; set; }
        public TimeSpan? CoreEndTime { get; set; }
        public double? DailyWorkingHours { get; set; }
    }
}
