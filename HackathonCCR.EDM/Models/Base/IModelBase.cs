using HackathonCCR.EDM.Helper;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace HackathonCCR.EDM.Models
{
    public interface IModelBase
    {

        event PropertyChangedEventHandler PropertyChanged;

        [NotMapped]
        [IgnoreToDatatable(IgnorePropertyToDatatable = true)]
        string TableName { get; set; }

        [NotMapped]
        [IgnoreToDatatable(IgnorePropertyToDatatable = true)]
        string PrimaryKey { get; set; }

        void RaisePropertyChanged(string newValue, bool primaryKey = false, [CallerMemberName] string prop = "");
    }
}
