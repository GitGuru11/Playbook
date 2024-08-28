using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playbook.Bussiness.Model
{
    /// <summary>
    /// Represents the model for a field type.
    /// </summary>
    public class FieldType
    {
        public const string TextField = "Text";
        public const string ReferenceField = "Reference";
        public const string PicklistField = "Picklist";
        public const string DateField = "Date";
        public const string TrueFalseField = "True-False";
        public const string DecimalField = "Decimal";
        public const string IntField = "Int";
        public const string PercentageField = "Percentage";

        /// <summary>
        /// Gets or sets the description of the Key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the label of the field type.
        /// </summary>
        public string Label { get; set; }
    }

}
