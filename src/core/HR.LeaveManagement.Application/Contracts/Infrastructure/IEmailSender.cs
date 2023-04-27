using HR.LeaveManagement.Application.Models;

namespace HR.LeaveManagement.Application.Contracts.Infrastructure;

public interface IEmailSender
{
    Task<bool> sendEmail(Email email);
}
