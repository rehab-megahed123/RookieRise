using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RookieRise.Application.Repositories;
using RookieRise.Data.Data;
using RookieRise.Data.Entities;

namespace RookieRise.Infrastructure.Repositories
{
    public class OtpRepository : IOtpRepository
    {
        private readonly AppDbContext _context;
        public OtpRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<OTP>> GetAllOtpsForEmailAndUserTypeAsync(string email, string userType)
        {
            return await _context.OTPs
                                 .Where(o => o.Email == email && o.UserType == userType)
                                 .OrderByDescending(o => o.CreatedAt)
                                 .ToListAsync();
        }
        public async Task<Dictionary<string, DateTime>> GetLatestOtpCreationDatesForEmailsAsync(List<string> emails)
        {
            var latestOtps = await _context.OTPs
                .Where(o => emails.Contains(o.Email))
                .GroupBy(o => o.Email)
                .Select(g => new { Email = g.Key, LatestDate = g.Max(o => o.CreatedAt) })
                .ToListAsync();

            return latestOtps.ToDictionary(x => x.Email, x => x.LatestDate);
        }
        public async Task AddOtpAsync(OTP otp)
        {
            await _context.OTPs.AddAsync(otp);
        }
        public async Task MarkOtpAsUsedAsync(OTP oTP)
        {
            oTP.IsUsed = true;
            _context.Update(oTP);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OTP>> GetExpiredOrUsedOtpsAsync()
        {
            return await _context.OTPs
               .Where(o => o.IsUsed || o.ExpirationTime <= DateTime.UtcNow)
               .ToListAsync();
        }

        public async Task<OTP?> GetLatestOtpAsync(string email, string userType)
        {
            return await _context.OTPs
                .Where(x => x.Email == email && x.UserType == userType)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();
        }
        public async Task<OTP?> GetValidOtpAsync(string email, string otpCode)
        {
            // Normalize both the database email and the input email for a case-insensitive match
            var storedOtp = await _context.OTPs
                .FirstOrDefaultAsync(o => o.Email.ToLower() == email.ToLower() && o.OtpCode == otpCode && !o.IsUsed);

            if (storedOtp == null || storedOtp.ExpirationTime < DateTime.UtcNow)
            {
                return null;
            }
            return storedOtp;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
