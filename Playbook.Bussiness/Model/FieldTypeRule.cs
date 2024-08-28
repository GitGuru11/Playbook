using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playbook.Bussiness.Model
{
    // <summary>
    /// Represents the model for a rule associated with a field type.
    /// </summary>
    public class FieldTypeRule
    {
        /// <summary>
        /// Gets or sets the description of the rule.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the operator for the rule.
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// Gets or sets the label of the rule.
        /// </summary>
        public string Label { get; set; }
    }
}
