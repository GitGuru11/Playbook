using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playbook.Bussiness.Model
{
    // <summary>
    /// Represents the model for a rule associated with a field type.
    /// </summary>
    public class FieldTypeRuleFactory
    {
        public async Task<Dictionary<string, List<FieldTypeRule>>> FindAllRulesAsync()
        {
            var mergedDict = new[] { TextFieldRules, ReferenceFieldRules, PicklistFieldRules,
                                    DateFieldRules, TrueFalseFieldRules, DecimalFieldRules, IntFieldRules, PercentageFieldRules }
            .SelectMany(dict => dict)
            .GroupBy(kv => kv.Key)
            .ToDictionary(g => g.Key, g => g.First().Value);

            return await Task.FromResult(mergedDict);
        }

        public async Task<Dictionary<string, List<FieldTypeRule>>> FindByFieldTypeAsync(string fieldType)
        {
            if (string.IsNullOrEmpty(fieldType))
                throw new ArgumentNullException(nameof(fieldType));

            var rule = new Dictionary<string, List<FieldTypeRule>>();
            switch (fieldType.ToLower())
            {
                case "text":
                    rule = TextFieldRules;
                    break;
                case "reference":
                    rule = ReferenceFieldRules;
                    break;
                case "picklist":
                    rule = PicklistFieldRules;
                    break;
                case "date":
                    rule = DateFieldRules;
                    break;
                case "true-false":
                    rule = TrueFalseFieldRules;
                    break;
                case "int":
                    rule = IntFieldRules;
                    break;
                case "decimal":
                    rule = DecimalFieldRules;
                    break;
                case "percentage":
                    rule = PercentageFieldRules;
                    break;
                default:
                    break;
            }
            return await Task.FromResult(rule);
        }


        public static readonly Dictionary<string, List<FieldTypeRule>> TextFieldRules = new Dictionary<string, List<FieldTypeRule>>
        {

            { FieldType.TextField, new List<FieldTypeRule> {

                new FieldTypeRule { Key = "equals", Label = "equals", Operator = "==" },
                new FieldTypeRule { Key = "does not equal", Label = "does not equal", Operator = "!=" },
                new FieldTypeRule { Key = "contains", Label = "contains", Operator = "CONTAINS" },
                new FieldTypeRule { Key = "does not contain", Label = "does not contain", Operator = "DOES_NOT_CONTAIN" },
                new FieldTypeRule { Key = "is blank", Label = "is blank", Operator = "IS_BLANK" },
                new FieldTypeRule { Key = "is present", Label = "is present", Operator = "IS_PRESENT" }
                }}
        };

        public static readonly Dictionary<string, List<FieldTypeRule>> ReferenceFieldRules = new Dictionary<string, List<FieldTypeRule>>
        {

            { FieldType.ReferenceField, new List<FieldTypeRule> {

                new FieldTypeRule { Key = "is", Label = "is", Operator = "=" },
                new FieldTypeRule { Key = "is not", Label = "is not", Operator = "!=" },
                new FieldTypeRule { Key = "is present", Label = "is present", Operator = "IS_PRESENT" },
                new FieldTypeRule { Key = "is blank", Label = "is blank", Operator = "IS_BLANK" },
                new FieldTypeRule { Key = "is any of", Label = "is any of", Operator = "ANY_OF" },
                new FieldTypeRule { Key = "is none of", Label = "is none of", Operator = "NONE_OF" }
            }}
        };

        public static readonly Dictionary<string, List<FieldTypeRule>> PicklistFieldRules = new Dictionary<string, List<FieldTypeRule>>
        {

            { FieldType.PicklistField, new List<FieldTypeRule> {

                new FieldTypeRule { Key = "is", Label = "is", Operator = "=" },
                new FieldTypeRule { Key = "is not", Label = "is not", Operator = "!=" },
                new FieldTypeRule { Key = "is any of", Label = "is any of", Operator = "ANY_OF" },
                new FieldTypeRule { Key = "is none of", Label = "is none of", Operator = "NONE_OF" }
            }}
        };

        public static readonly Dictionary<string, List<FieldTypeRule>> DateFieldRules = new Dictionary<string, List<FieldTypeRule>>
        {

            { FieldType.DateField, new List<FieldTypeRule> {

                new FieldTypeRule { Key = "equals", Label = "equals", Operator = "==" },
                new FieldTypeRule { Key = "does not equal", Label = "does not equal", Operator = "!=" },
                new FieldTypeRule { Key = "is before", Label = "is before", Operator = "BEFORE" },
                new FieldTypeRule { Key = "is after", Label = "is after", Operator = "AFTER" },
                new FieldTypeRule { Key = "is on or before", Label = "is on or before", Operator = "ON_OR_BEFORE" },
                new FieldTypeRule { Key = "is on or after", Label = "is on or after", Operator = "ON_OR_AFTER" },
                new FieldTypeRule { Key = "is blank", Label = "is blank", Operator = "IS_BLANK" },
                new FieldTypeRule { Key = "is present", Label = "is present", Operator = "IS_PRESENT" },
                new FieldTypeRule { Key = "is within the next", Label = "is within the next", Operator = "WITHIN_NEXT" },
                new FieldTypeRule { Key = "is not within the next", Label = "is not within the next", Operator = "NOT_WITHIN_NEXT" },
                new FieldTypeRule { Key = "is within the past", Label = "is within the past", Operator = "WITHIN_PAST" },
                new FieldTypeRule { Key = "is not within the past", Label = "is not within the past", Operator = "NOT_WITHIN_PAST" },
                new FieldTypeRule { Key = "is today", Label = "is today", Operator = "TODAY" },
                new FieldTypeRule { Key = "is before today", Label = "is before today", Operator = "BEFORE_TODAY" },
                new FieldTypeRule { Key = "is after today", Label = "is after today", Operator = "AFTER_TODAY" },
                new FieldTypeRule { Key = "is within the current month", Label = "is within the current month", Operator = "CURRENT_MONTH" },
                new FieldTypeRule { Key = "is within the previous month", Label = "is within the previous month", Operator = "PREVIOUS_MONTH" },
                new FieldTypeRule { Key = "is within the next month", Label = "is within the next month", Operator = "NEXT_MONTH" },
                new FieldTypeRule { Key = "is within the current fiscal quarter", Label = "is within the current fiscal quarter", Operator = "CURRENT_FISCAL_QUARTER" },
                new FieldTypeRule { Key = "is within the previous fiscal quarter", Label = "is within the previous fiscal quarter", Operator = "PREVIOUS_FISCAL_QUARTER" },
                new FieldTypeRule { Key = "is within the next fiscal quarter", Label = "is within the next fiscal quarter", Operator = "NEXT_FISCAL_QUARTER" },
                new FieldTypeRule { Key = "is within the current fiscal year", Label = "is within the current fiscal year", Operator = "CURRENT_FISCAL_YEAR" },
                new FieldTypeRule { Key = "is within the previous fiscal year", Label = "is within the previous fiscal year", Operator = "PREVIOUS_FISCAL_YEAR" },
                new FieldTypeRule { Key = "is within the next fiscal year", Label = "is within the next fiscal year", Operator = "NEXT_FISCAL_YEAR" }
            }}
        };

        public static readonly Dictionary<string, List<FieldTypeRule>> TrueFalseFieldRules = new Dictionary<string, List<FieldTypeRule>>
        {

            { FieldType.TrueFalseField, new List<FieldTypeRule> {

                new FieldTypeRule { Key = "is True", Label = "is True", Operator = "TRUE" },
                new FieldTypeRule { Key = "is False", Label = "is False", Operator = "FALSE" }
            }}
        };

        public static readonly Dictionary<string, List<FieldTypeRule>> DecimalFieldRules = new Dictionary<string, List<FieldTypeRule>>
        {

            { FieldType.DecimalField, new List<FieldTypeRule> {

                new FieldTypeRule { Key = "equals", Label = "equals", Operator = "==" },
                new FieldTypeRule { Key = "does not equal", Label = "does not equal", Operator = "!=" },
                new FieldTypeRule { Key = "is less than", Label = "is less than", Operator = "<" },
                new FieldTypeRule { Key = "is greater than", Label = "is greater than", Operator = ">" },
                new FieldTypeRule { Key = "is less than or equal to", Label = "is less than or equal to", Operator = "<=" },
                new FieldTypeRule { Key = "is greater than or equal to", Label = "is greater than or equal to", Operator = ">=" },
                new FieldTypeRule { Key = "is blank", Label = "is blank", Operator = "IS_BLANK" },
                new FieldTypeRule { Key = "is present", Label = "is present", Operator = "IS_PRESENT" },
                new FieldTypeRule { Key = "has changed %", Label = "has changed %", Operator = "CHANGED_PERCENT" }
            }}
        };

        public static readonly Dictionary<string, List<FieldTypeRule>> IntFieldRules = new Dictionary<string, List<FieldTypeRule>>
        {

            { FieldType.IntField, new List<FieldTypeRule> {

                new FieldTypeRule { Key = "equals", Label = "equals", Operator = "==" },
                new FieldTypeRule { Key = "does not equal", Label = "does not equal", Operator = "!=" },
                new FieldTypeRule { Key = "is less than", Label = "is less than", Operator = "<" },
                new FieldTypeRule { Key = "is greater than", Label = "is greater than", Operator = ">" },
                new FieldTypeRule { Key = "is less than or equal to", Label = "is less than or equal to", Operator = "<=" },
                new FieldTypeRule { Key = "is greater than or equal to", Label = "is greater than or equal to", Operator = ">=" },
                new FieldTypeRule { Key = "is blank", Label = "is blank", Operator = "IS_BLANK" },
                new FieldTypeRule { Key = "is present", Label = "is present", Operator = "IS_PRESENT" },
                new FieldTypeRule { Key = "has changed %", Label = "has changed %", Operator = "CHANGED_PERCENT" }
            }}
        };

        public static readonly Dictionary<string, List<FieldTypeRule>> PercentageFieldRules = new Dictionary<string, List<FieldTypeRule>>
        {

            { FieldType.PercentageField, new List<FieldTypeRule> {

                new FieldTypeRule { Key = "equals", Label = "equals", Operator = "==" },
                new FieldTypeRule { Key = "does not equal", Label = "does not equal", Operator = "!=" },
                new FieldTypeRule { Key = "is less than", Label = "is less than", Operator = "<" },
                new FieldTypeRule { Key = "is greater than", Label = "is greater than", Operator = ">" },
                new FieldTypeRule { Key = "is less than or equal to", Label = "is less than or equal to", Operator = "<=" },
                new FieldTypeRule { Key = "is greater than or equal to", Label = "is greater than or equal to", Operator = ">=" },
                new FieldTypeRule { Key = "is blank", Label = "is blank", Operator = "IS_BLANK" },
                new FieldTypeRule { Key = "is present", Label = "is present", Operator = "IS_PRESENT" },
                new FieldTypeRule { Key = "has changed %", Label = "has changed %", Operator = "CHANGED_PERCENT" }
            }}
        };

    }
}
