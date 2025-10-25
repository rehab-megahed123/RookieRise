using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RookieRise.Data.Entities
{
    public class EmailLogs
    {
        #region NotesForMe
        //1-Guid:عشان ال id يكون unique وصعب ال id يتكرر حتي لو اكتر من جهاز بيcreate GUIDS في نفس الوقت
        #endregion
        public Guid Id { get; set; } = Guid.NewGuid();
        public string From { get; set; }
        public string To { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime EmailDate { get; set; }
        public string? ProviderError { get; set; }
    }
}
