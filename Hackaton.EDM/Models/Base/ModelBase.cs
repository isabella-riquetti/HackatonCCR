using HackatonCCR.EDM.Helper;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace HackatonCCR.EDM.Models
{
    public class ModelBase : INotifyPropertyChanged, IModelBase
    {
        public ModelBase(string tableName, string primaryKeyField)
        {
            TableName = tableName;
            PrimaryKey = primaryKeyField;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(PropertyChangedEventArgs eventArgs)
        {
            var value = this.GetType().GetProperty(eventArgs.PropertyName).GetValue(this);
            if (value != null && !value.GetType().FullName.Contains("System.Collections"))
            {
                var primaryKey = PrimaryKey == eventArgs.PropertyName;

                RaisePropertyChanged(value != null ? value.ToString() : null, primaryKey, eventArgs.PropertyName);
            }
        }

        [NotMapped]
        [IgnoreToDatatable(IgnorePropertyToDatatable = true)]
        public string TableName { get; set; }

        [NotMapped]
        [IgnoreToDatatable(IgnorePropertyToDatatable = true)]
        public string PrimaryKey { get; set; }

        public void RaisePropertyChanged(string newValue, bool primaryKey = false, [CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
