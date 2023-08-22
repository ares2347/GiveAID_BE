using GAID.Domain.Models.Email;

namespace GAID.Application.Email;

public interface IEmailService
{
    Task SendEmailNotification(EmailTemplateType templateType,string receiverEmail, Dictionary<string, string>? subjectReplacements, Dictionary<string, string>? bodyReplacements,
        CancellationToken _ = default);
}