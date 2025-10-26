using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RookieRise.Data.Entities
{
    public class EmployeeDocument : TrackableEntity
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentUrl { get; set; }
    }
}
