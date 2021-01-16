using HackatonCCR.EDM.Programmability.Functions;
using HackatonCCR.EDM.Programmability.Stored_Procedures;

namespace HackatonCCR.EDM.Context
{
    public partial class BaseContext
    {
        public StoredProcedures StoredProcedures { get; set; }
        public ScalarValuedFunctions ScalarValuedFunctions { get; set; }
        public TableValuedFunctions TableValuedFunctions { get; set; }
    }
}
