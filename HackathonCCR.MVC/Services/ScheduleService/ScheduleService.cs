using HackathonCCR.EDM.Models;
using HackathonCCR.EDM.UnitOfWork;
using HackathonCCR.MVC.Models.Schedule;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public List<UserSchedule> GetUserScheduledMentorship()
        {
            var userId = _authenticationService.GetAuthenticatedUserId();
            var schedulesDb = _unitOfWork.RepositoryBase
                .Get<Schedule>(s => s.Status != EDM.Enums.Schedule.Status.Available
                && (s.DiscoverId == userId || s.MentorId == userId));
            var schedules = ConvertSchedules(schedulesDb, userId);
            return schedules;
        }

        public List<Schedule> GetUserSchedules()
        {
            var userId = _authenticationService.GetAuthenticatedUserId();
            var schedules = _unitOfWork.RepositoryBase
                .GetIQueryable<Schedule>(s => s.Status != HackathonCCR.EDM.Enums.Schedule.Status.Removed
                && (s.DiscoverId == userId || s.MentorId == userId));
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

        public SchedulesByDate GetDateAvailableSchedules(DateTime date)
        {
            var userId = _authenticationService.GetAuthenticatedUserId();
            var dateStart = date.Date;
            if(dateStart == DateTime.Now.Brasilia().Date)
                dateStart = DateTime.Now.Brasilia();
            var dateEnd = dateStart.Date.AddDays(1);
            var schedulesDb = _unitOfWork.RepositoryBase
                .GetIQueryable<Schedule>(s => s.ScheduleAt >= dateStart
                && s.ScheduleAt < dateEnd
                && s.Status == HackathonCCR.EDM.Enums.Schedule.Status.Available)
                .OrderBy(s => s.ScheduleAt)
                .Take(10)
                .ToList();
            var schedules = ConvertSchedules(schedulesDb, userId);
            return new SchedulesByDate()
            {
                Date = date.ToString("dd/MM"),
                Schedules = schedules
            };
        }

        public List<UserSchedule> GetCurrentAvailableSchedules()
        {
            var userId = _authenticationService.GetAuthenticatedUserId();
            var biggerThan = DateTime.Now.Brasilia().AddMinutes(-30);
            var smallerThan = biggerThan.AddMinutes(60);
            var schedulesDb = _unitOfWork.RepositoryBase
                .GetIQueryable<Schedule>(s => s.ScheduleAt >= biggerThan
                && s.ScheduleAt <= smallerThan
                && s.Status == HackathonCCR.EDM.Enums.Schedule.Status.Available)
                .GroupBy(s => s.MentorId)
                .Select(s => s.FirstOrDefault())
                .Take(10)
                .ToList();
            var schedules = ConvertSchedules(schedulesDb, userId);
            return schedules;
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
            var discover = _userService.Get(userId);
            var appointmentId = _emailService.SendAppointment(mentor, discover, schedule.ScheduleAt);

            schedule.DiscoverId = userId;
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
                MentorId = schedule.MentorId,
                Status = EDM.Enums.Schedule.Status.Available,
                ScheduleAt = schedule.ScheduleAt
            };
            _unitOfWork.RepositoryBase.Add(newEmptySchedule);
            _unitOfWork.Commit();

            _emailService.Cancel(schedule.Mentor, schedule.Discover, (Guid)schedule.AppointmentId);
        }

        private List<UserSchedule> ConvertSchedules(List<Schedule> schedulesDb, Guid userId)
        {
            var schedules = new List<UserSchedule>(); 
            
            foreach (var scheduleDb in schedulesDb)
            {
                var courseName = scheduleDb.Category.Description;
                var courseColor = scheduleDb.Category.Color;
                var time = String.Concat(scheduleDb.ScheduleAt.ToShortTimeString(), " - " + scheduleDb.ScheduleAt.AddMinutes(30).ToShortTimeString());
                var date = scheduleDb.ScheduleAt.ToShortDateString();
                var dateMonth = scheduleDb.ScheduleAt.ToString("MMMM", new CultureInfo("pt-BR")).Substring(0, 3).ToUpper();
                var dateDay = scheduleDb.ScheduleAt.ToString("dd");
                var partner = scheduleDb.MentorId == userId ? scheduleDb.Discover : scheduleDb.Mentor;
                var partnerName = partner.Name;
                var partnerFirstName = partner.Name.Split(' ')[0];
                var partnerPicture = partner.Picture != null && partner.Picture.Length > 0 ? Convert.ToBase64String(partner.Picture) : DefaultImage;

                var schedule = new UserSchedule()
                {
                    Course = courseName,
                    CourseColor = courseColor ?? "#E95FF5",
                    PartnerName = partnerName,
                    PartnerFirstName = partnerFirstName,
                    PartnerPicture = partnerPicture,
                    ScheduleDate = date,
                    ScheduleDateMonth = dateMonth,
                    ScheduleDateDay = dateDay,
                    ScheduleTime = time
                };
                schedules.Add(schedule);
            }
            return schedules;
        }

        private const string DefaultImage = "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"
            ;

    }
}
