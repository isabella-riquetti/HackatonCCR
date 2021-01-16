using HackathonCCR.EDM.Models;
using System;

namespace HackathonCCR.MVC.Services
{
    public interface IEmailService
    {
        Guid SendAppointment(User mentor, User discover, DateTime schedule);
        void Cancel(User mentor, User discover, Guid inviteId);
    }
}
