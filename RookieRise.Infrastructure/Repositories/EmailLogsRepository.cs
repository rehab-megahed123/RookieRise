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
    public class EmailLogsRepository : IEmailLogsRepository
    {
        private readonly AppDbContext _context;

        public EmailLogsRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddLogAsync(EmailLogs log)
        {
            _context.EmailLogs.Add(log);
            await _context.SaveChangesAsync();
        }
        public async Task ClearLogsAsync()
        {
            var allLogs = await _context.EmailLogs.ToListAsync();
            _context.EmailLogs.RemoveRange(allLogs);
            await _context.SaveChangesAsync();
        }
        public async Task<EmailLogs> GetEmailLogByIdAsync(Guid id)
        {
            return await _context.EmailLogs.FirstOrDefaultAsync(log => log.Id == id);
        }
        public async Task DeleteLogAsync(Guid id)
        {
            var logToDelete = await _context.EmailLogs.FirstOrDefaultAsync(log => log.Id == id);
            if (logToDelete != null)
            {
                _context.EmailLogs.Remove(logToDelete);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<(IList<EmailLogs> Logs, int TotalCount)> GetEmailLogsAsync(int pageNumber, int pageSize, string searchTerm = null)
        {
            var query = _context.EmailLogs.AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchTerm))
                query = query.Where(log => log.From.Contains(searchTerm) || log.To.Contains(searchTerm));
            var totalCount = await query.CountAsync();
            var logs = await query.Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync();
            return (logs, totalCount);
        }

        public async Task SendAsync(string to, string subject, string body)
        {
            var log = new EmailLogs
            {
                To = to,
                Title = subject,
                Body = body,
                EmailDate = DateTime.Now,
            };

            _context.EmailLogs.Add(log);
            await _context.SaveChangesAsync();
        }

        public async Task SendOtpEmailAsync(string to, string otpCode)
        {
            var subject = "OTP Confirmation";
            var body = $"Your OTP code is: {otpCode}";
            await SendAsync(to, subject, body);
        }
    }
}
