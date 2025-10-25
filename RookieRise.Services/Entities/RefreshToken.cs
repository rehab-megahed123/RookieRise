using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RookieRise.Data.Entities
{
    #region NotesForME
    // - Used to renew the user's JWT access token without logging in again.
    // - Contains the token string, creation time, and expiration time.
    // - IsExpired checks if the token is no longer valid.
    // - IsPersistent indicates if the token should survive after logout or session end.
    // - Marked with [Owned] → means it's stored inside the parent entity's table (e.g. User)
    //   instead of having a separate database table.
    // - Useful for one-to-one value objects that belong only to a single user. (مثلا عندي كل user عنده refreshToken واحد )
    #endregion
    [Owned]
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
        #region NoteForMe
        // currenttime >= expired??
        #endregion
        public DateTime CreatedOn { get; set; }
        public bool IsPersistent { get; set; }
        #region NoteForMe
        // IsPersistent====> true ======> "Remember me enabled"
        //IsPresistent=====>false=======>"Token expired when session ends"
        #endregion
    }
}
