using System;

namespace HackathonCCR.EDM.Helper
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class IgnoreToDatatableAttribute : Attribute
    {
        public bool IgnorePropertyToDatatable { get; set; }

        public IgnoreToDatatableAttribute()
        {
        }

    }
}
