using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RookieRise.Data.Entities
{
    public class Page : TrackableEntity
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string RouteUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
