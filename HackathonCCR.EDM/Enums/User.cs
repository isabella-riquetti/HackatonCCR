using System;
using System.ComponentModel;

namespace HackathonCCR.EDM.Enums
{
    public class User
    {
        [Flags]
        public enum Type
        {
            [Description("Mentorado")]
            Discover = 1 << 0,
            [Description("Mentor")]
            Mentor = 1 << 1
        }
    }
}
