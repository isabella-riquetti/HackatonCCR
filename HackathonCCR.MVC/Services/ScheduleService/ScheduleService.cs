using HackathonCCR.EDM.Models;
using HackathonCCR.EDM.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HackathonCCR.MVC.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationService _authenticationService;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;
        public ScheduleService(IUnitOfWork unitOfWork, IAuthenticationService authenticationService, IEmailService emailService, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _authenticationService = authenticationService;
            _emailService = emailService;
            _userService = userService;
        }

        public List<Schedule> GetUserSchedules(Guid? userId = null)
        {
            userId = userId != null ? userId : _authenticationService.GetAuthenticatedUserId();
            var schedules = _unitOfWork.RepositoryBase.GetIQueryable<Schedule>(s => s.Status != HackathonCCR.EDM.Enums.Schedule.Status.Removed && 
                (s.MentoredId == userId || s.MentorId == userId));
            return schedules.ToList();
        }

        public List<Schedule> GetMentorAvailableSchedules(Guid mentorId)
        {
            var schedules = _unitOfWork.RepositoryBase.GetIQueryable<Schedule>(s => s.MentorId == mentorId && s.Status == HackathonCCR.EDM.Enums.Schedule.Status.Available);
            return schedules.ToList();
        }

        public List<Schedule> GetCategoryAvailableSchedules(Guid categoryId)
        {
            var schedules = _unitOfWork.RepositoryBase.GetIQueryable<Schedule>(s => s.CategoryId == categoryId && s.Status == HackathonCCR.EDM.Enums.Schedule.Status.Available);
            return schedules.ToList();
        }

        public List<Schedule> GetDateAvailableSchedules(DateTime date)
        {
            var dateStart = date.Date;
            var dateEnd = dateStart.AddDays(1);
            var schedules = _unitOfWork.RepositoryBase
                .GetIQueryable<Schedule>(s => s.ScheduleAt >= dateStart
                && s.ScheduleAt < dateEnd
                && s.Status == HackathonCCR.EDM.Enums.Schedule.Status.Available);
            return schedules.ToList();
        }

        public List<Schedule> GetCurrentSchedules()
        {
            var biggerThan = DateTime.Now.Brasilia();
            var smallerThan = biggerThan.AddMinutes(30);
            var schedules = _unitOfWork.RepositoryBase
                .GetIQueryable<Schedule>(s => s.ScheduleAt >= biggerThan
                && s.ScheduleAt <= smallerThan
                && s.Status == HackathonCCR.EDM.Enums.Schedule.Status.Available);
            return schedules.ToList();
        }

        public int CreateAgenda(DateTime start, DateTime end, Guid categoryId)
        {
            var userId = _authenticationService.GetAuthenticatedUserId();
            var currentSchedules = GetUserSchedules();
            var newSchedules = new List<Schedule>();
            var newSchedule = new Schedule()
            {
                MentorId = userId,
                CategoryId = categoryId,
                Status = EDM.Enums.Schedule.Status.Available,
                ScheduleAt = start
            };
            newSchedules.Add(newSchedule);
            while (newSchedule.ScheduleAt.AddMinutes(30) < end)
            {
                newSchedule = new Schedule()
                {
                    MentorId = userId,
                    CategoryId = categoryId,
                    Status = HackathonCCR.EDM.Enums.Schedule.Status.Available,
                    ScheduleAt = newSchedule.ScheduleAt.AddMinutes(30)
                };
                newSchedules.Add(newSchedule);
            }

            var createSchedules = newSchedules.Except(newSchedules.Where(n => currentSchedules.Any(c => c.ScheduleAt == n.ScheduleAt))).ToList();
            _unitOfWork.RepositoryBase.AddAll(createSchedules);

            return createSchedules.Count;
        }

        public void Schedule(Guid scheduleId)
        {
            var schedule = _unitOfWork.RepositoryBase.FirstOrDefault<Schedule>(s => s.ScheduleId == scheduleId);

            var userId = _authenticationService.GetAuthenticatedUserId();
            var mentor = schedule.Mentor;
            var mentored = _userService.Get(userId);
            var appointmentId = _emailService.SendAppointment(mentor, mentored, schedule.ScheduleAt);

            schedule.MentoredId = userId;
            schedule.Status = EDM.Enums.Schedule.Status.Scheduled;
            schedule.AppointmentId = appointmentId;

            _unitOfWork.RepositoryBase.Edit(schedule);
            _unitOfWork.Commit();
        }

        public void CancelSchedule(Guid scheduleId)
        {
            var schedule = _unitOfWork.RepositoryBase.FirstOrDefault<Schedule>(s => s.ScheduleId == scheduleId);
            schedule.Status = EDM.Enums.Schedule.Status.Removed;
            _unitOfWork.RepositoryBase.Edit(schedule);

            var newEmptySchedule = new Schedule()
            {
                ScheduleId = Guid.NewGuid(),
                MentorId = schedule.MentoredId,
                Status = EDM.Enums.Schedule.Status.Available,
                ScheduleAt = schedule.ScheduleAt
            };
            _unitOfWork.RepositoryBase.Add(newEmptySchedule);
            _unitOfWork.Commit();

            _emailService.Cancel(schedule.Mentor, schedule.Mentored, schedule.AppointmentId);
        }
    }
}
