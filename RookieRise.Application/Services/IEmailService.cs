using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RookieRise.Data.Enums;

namespace RookieRise.Application.Services
{
    public interface IEmailService
    {
        Task SendAsync(string to, string subject, string htmlBody, string? from = null);
        Task SendRequestCancellationNotificationAsync(string toCompanyEmail, string employeeName, DateOnly startDate, DateOnly endDate);
        Task SendWeekendStatusUpdateEmailAsync(string toEmail, string employeeName, DateOnly startDate, DateOnly endDate, WeekendStatus status);
        Task SendNewVacationRequestNotificationAsync(string toEmail, string employeeId, string employeeName, DateOnly startDate, DateOnly endDate);
        Task SendOtpEmailAsync(string toEmail, string otpCode);
        Task SendNewWeekendRequestNotificationAsync(string toEmail, string employeeName, DateOnly startDate, DateOnly endDate);
        Task SendVacationStatusUpdateEmailAsync(string toEmail, string employeeName, DateOnly startDate, DateOnly endDate, VacationStatus status);
    }
}
