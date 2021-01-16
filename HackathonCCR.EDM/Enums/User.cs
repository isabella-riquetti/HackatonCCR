using System.ComponentModel;

namespace HackathonCCR.EDM.Enums
{
    public class User
    {
        public enum Type
        {
            [Description("Mentorado")]
            Discover = 0,
            [Description("Mentor")]
            Mentor = 1
        }
    }
}
