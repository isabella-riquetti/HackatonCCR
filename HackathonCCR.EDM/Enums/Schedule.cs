using System.ComponentModel;

namespace HackathonCCR.EDM.Enums
{
    public class Schedule
    {
        public enum Status
        {
            [Description("Disponível")]
            Available = 0,
            [Description("Agendado")]
            Scheduled = 1,
            [Description("Removido")]
            Removed = 2
        }
    }
}
