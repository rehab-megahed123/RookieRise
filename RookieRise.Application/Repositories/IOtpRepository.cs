using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RookieRise.Data.Entities;

namespace RookieRise.Application.Repositories
{
    public interface IOtpRepository
    {
        Task<OTP?> GetLatestOtpAsync(string email, string userType);
        Task MarkOtpAsUsedAsync(OTP oTP);
        Task<List<OTP>> GetExpiredOrUsedOtpsAsync();
        Task<List<OTP>> GetAllOtpsForEmailAndUserTypeAsync(string email, string userType);
        Task<OTP?> GetValidOtpAsync(string email, string otpCode);
        Task AddOtpAsync(OTP otp);
        Task<Dictionary<string, DateTime>> GetLatestOtpCreationDatesForEmailsAsync(List<string> emails);
        Task SaveChangesAsync();
    }
}
