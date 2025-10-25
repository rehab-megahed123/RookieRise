using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RookieRise.Data.Entities;

namespace RookieRise.Application.Repositories
{
    public interface IEmailLogsRepository
    {
        Task SendAsync(string to, string subject, string body);
        Task SendOtpEmailAsync(string to, string otpCode);
        Task AddLogAsync(EmailLogs log);
        Task ClearLogsAsync();
        Task<(IList<EmailLogs> Logs, int TotalCount)> GetEmailLogsAsync(int pageNumber, int pageSize, string searchTerm = null);
        Task<EmailLogs> GetEmailLogByIdAsync(Guid id);
        Task DeleteLogAsync(Guid id);
    }
}
