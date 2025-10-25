using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RookieRise.Application.MenueItems.DTOS
{
    public class MenuItemDto
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string? IconUrl { get; set; }
        public int? ParentMenuId { get; set; }
        public int? PageId { get; set; }
        public bool IsActive { get; set; }
    }
}
