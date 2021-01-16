using HackathonCCR.EDM.Programmability.Functions;
using HackathonCCR.EDM.Programmability.Stored_Procedures;

namespace HackathonCCR.EDM.Context
{
    public partial class BaseContext
    {
        public StoredProcedures StoredProcedures { get; set; }
        public ScalarValuedFunctions ScalarValuedFunctions { get; set; }
        public TableValuedFunctions TableValuedFunctions { get; set; }
    }
}
