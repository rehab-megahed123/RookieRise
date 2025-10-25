using System;
using System.Collections.Generic;
using System.Linq;

using MailKit.Net.Smtp;

using MailKit.Security;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RookieRise.Application.Services;
using RookieRise.Data.Models;
using Microsoft.Extensions.Logging;
using RookieRise.Application.Repositories;
using Microsoft.Extensions.Options;
using MimeKit;
using AuthenticationException = MailKit.Security.AuthenticationException;
using RookieRise.Data.Entities;
using RookieRise.Data.Enums;

namespace RookieRise.Infrastructure.Services
{
    

        public class EmailService : IEmailService
        {
            private readonly MailSettings _mailSettings;
            private readonly ILogger<EmailService> _logger;
            private readonly IEmailLogsRepository _emailLogsRepository;

            public EmailService(IOptions<MailSettings> mailSettings, ILogger<EmailService> logger, IEmailLogsRepository emailLogsRepository)
            {
                _mailSettings = mailSettings.Value;
                _logger = logger;
                _emailLogsRepository = emailLogsRepository;
            }
            public async Task SendAsync(string to, string subject, string htmlBody, string? from = null)
            {
                string? providerError = null;
                var senderEmail = from ?? _mailSettings.SenderEmail;

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("RookieRise", senderEmail));
                message.To.Add(new MailboxAddress("", to));
                message.Subject = subject;
                message.Body = new TextPart("html")
                {
                    Text = htmlBody
                };

                try
                {
                    using var client = new SmtpClient();
                    client.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
                    await client.ConnectAsync(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync("emailapikey", _mailSettings.ApiKey);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);

                    _logger.LogInformation("Email sent successfully to {ToEmail} with subject '{Subject}'.", to, subject);
                }
                catch (SmtpCommandException sce)
                {
                    providerError = $"SMTP Command Error: {sce.Message} (Code: {sce.ErrorCode})";
                    _logger.LogError(sce, "SMTP Command Error sending email to {ToEmail}. Error: {ProviderError}", to, providerError);
                    throw;
                }
                catch (SmtpProtocolException spe)
                {
                    providerError = $"SMTP Protocol Error: {spe.Message}";
                    _logger.LogError(spe, "SMTP Protocol Error sending email to {ToEmail}. Error: {ProviderError}", to, providerError);
                    throw;
                }
                catch (AuthenticationException ae)
                {
                    providerError = $"Authentication Error: {ae.Message}";
                    _logger.LogError(ae, "Authentication Error sending email to {ToEmail}. Error: {ProviderError}", to, providerError);
                    throw;
                }
                catch (Exception ex)
                {
                    providerError = $"General Error: {ex.Message}";
                    _logger.LogError(ex, "General Error sending email to {ToEmail}. Error: {ProviderError}", to, providerError);
                    throw;
                }
                finally
                {
                    if (providerError != null)
                    {
                        var failedLog = new EmailLogs
                        {
                            From = senderEmail,
                            To = to,
                            Title = subject,
                            Body = htmlBody,
                            EmailDate = DateTime.UtcNow,
                            ProviderError = providerError // Set the captured error
                        };
                        await _emailLogsRepository.AddLogAsync(failedLog);
                    }
                }
            }
            public async Task SendOtpEmailAsync(string toEmail, string otpCode)
            {
                string subject = "Your Email Change OTP Code";
                string htmlBody = $@"
            <h2>Verify Your New Email</h2>
            <p>Use the following OTP code to confirm your email change:</p>
            <h3 style='color: #2e6c80;'>{otpCode}</h3>
            <p>This code will expire in a short time, so please use it promptly.</p>";

                await SendAsync(toEmail, subject, htmlBody);
            }

            public async Task SendWeekendStatusUpdateEmailAsync(string toEmail, string employeeName, DateOnly startDate, DateOnly endDate, WeekendStatus status)
            {
                string subject = status switch
                {
                    WeekendStatus.Approved => "Your weekend work request has been approved",
                    WeekendStatus.Rejected => "Your weekend work request has been rejected",
                    WeekendStatus.Canceled => "Your weekend work request has been canceled",
                    _ => "Weekend work request status updated"
                };

                string body = $@"
        <p>Dear {employeeName ?? "Employee"},</p>
        <p>Your weekend work request from <b>{startDate:yyyy-MM-dd}</b> to <b>{endDate:yyyy-MM-dd}</b> has been <b>{status}</b> by the company.</p>
        <p>Thank you.</p>";

                await SendAsync(toEmail, subject, body);
            }

            public async Task SendVacationStatusUpdateEmailAsync(string toEmail, string employeeName, DateOnly startDate, DateOnly endDate, VacationStatus status)
            {
                string subject = status switch
                {
                    VacationStatus.Approved => "Your vacation request has been approved",
                    VacationStatus.Rejected => "Your vacation request has been rejected",
                    VacationStatus.Canceled => "Your vacation request has been canceled",
                    _ => "Vacation request status updated"
                };
                string body = $@"
        <p>Dear {employeeName ?? "Employee"},</p>
        <p>Your vacation request from <b>{startDate:yyyy-MM-dd}</b> to <b>{endDate:yyyy-MM-dd}</b> has been <b>{status}</b> by the company.</p>
        <p>Thank you.</p>";

                await SendAsync(toEmail, subject, body);
            }

            public async Task SendNewVacationRequestNotificationAsync(string toEmail, string employeeId, string employeeName, DateOnly startDate, DateOnly endDate)
            {
                if (string.IsNullOrEmpty(toEmail))
                {
                    _logger.LogWarning("Attempted to send new vacation request notification to a null or empty email for employee {EmployeeId} ({EmployeeName}).", employeeId, employeeName);
                    return;
                }

                string subject = "New Vacation Request Submitted";
                string body = $@"
        <p>An employee has submitted a vacation request:</p>
        <ul>
            <li><strong>Employee ID:</strong> {employeeId}</li>
            <li><strong>Employee Name:</strong> {employeeName}</li>
            <li><strong>Vacation Dates:</strong> {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}</li>
        </ul>
        <p>Please review and approve/reject it in the system.</p>";

                await SendAsync(toEmail, subject, body);
            }

            public async Task SendNewWeekendRequestNotificationAsync(string toEmail, string employeeName, DateOnly startDate, DateOnly endDate)
            {
                if (string.IsNullOrEmpty(toEmail))
                {
                    _logger.LogWarning("Attempted to send new weekend request notification to a null or empty email for employee {EmployeeName}.", employeeName);
                    return;
                }

                string subject = "New Weekend Request Submitted";
                string body = $@"
        <p>An employee has submitted a weekend work request:</p>
        <ul>
            <li><strong>Employee Name:</strong> {employeeName}</li>
            <li><strong>Requested Dates:</strong> {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}</li>
        </ul>
        <p>Please review and process this request in the system.</p>";

                await SendAsync(toEmail, subject, body);
            }

            public async Task SendRequestCancellationNotificationAsync(string toCompanyEmail, string employeeName, DateOnly startDate, DateOnly endDate)
            {
                if (string.IsNullOrEmpty(toCompanyEmail))
                {
                    _logger.LogWarning("Attempted to send request cancellation notification to a null or empty company email for employee {EmployeeName}.", employeeName);
                    return;
                }

                string subject = $"Request Cancellation: {employeeName}";
                string body = $@"
            <p>Dear Company Management,</p>
            <p>An employee has canceled his request:</p>
            <ul>
                <li><strong>Employee Name:</strong> {employeeName}</li>
                <li><strong>Canceled Dates:</strong> {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}</li>
            </ul>
            <p>The request status has been updated to 'Canceled' in the system.</p>";

                await SendAsync(toCompanyEmail, subject, body);
            }
        }
    }
