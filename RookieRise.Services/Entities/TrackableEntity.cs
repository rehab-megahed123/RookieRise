using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RookieRise.Data.Entities
{
    #region NotesForMe:
    //   1-Abstract class : لان ده class الهدف منه اجمع الحاجات المشتركه بين ال classes الاساسيه 
    //      وعايزه أمنع اى حد انه يعمل منه object لكن ال classes اللى هتورث منه يتعمل منها object عادي
    #endregion
    public abstract class TrackableEntity
    {
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
