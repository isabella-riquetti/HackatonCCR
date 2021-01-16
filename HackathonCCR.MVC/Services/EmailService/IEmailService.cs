using HackathonCCR.EDM.Models;
using System;
using static HackathonCCR.MVC.Services.EmailService;

namespace HackathonCCR.MVC.Services
{
    public interface IEmailService
    {
        Guid SendAppointment(User mentor, User mentored, DateTime schedule);
        void Cancel(User mentor, User mentored, Guid inviteId);
    }
}
