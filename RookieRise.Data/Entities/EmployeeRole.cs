using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RookieRise.Data.Entities
{
    public class EmployeeRole : TrackableEntity
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public int RoleId { get; set; }
    }
}
