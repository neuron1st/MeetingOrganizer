using MeetingOrganizer.Consts;
using MeetingOrganizer.Services.EmailSender;
using MeetingOrganizer.Services.RabbitMq;

namespace MeetingOrganizer.Services.Actions;

public class Action : IAction
{
    private readonly IRabbitMq rabbitMq;

    public Action(IRabbitMq rabbitMq)
    {
        this.rabbitMq = rabbitMq;
    }

    public async Task SendEmailAsync(EmailModel email)
    {
        await rabbitMq.PushAsync(RabbitMqTaskQueueNames.SendUserAccountEmail, email);
    }
}
