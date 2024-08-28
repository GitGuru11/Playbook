using System;
using static System.Net.Mime.MediaTypeNames;

namespace Playbook.Bussiness.Model
{
    public class FieldTypeFactory
    {

        public FieldTypeFactory()
        {
        }

        public async Task<List<FieldType>> FindAllFieldTypesAsync()
        {
            return await Task.FromResult(FieldTypes.Values.ToList());
        }

        public static readonly Dictionary<string, FieldType> FieldTypes = new Dictionary<string, FieldType>
        {
            { FieldType.TextField, new FieldType{ Key = "Text", Label = "Text"} },
            { FieldType.ReferenceField, new FieldType{ Key = "Reference", Label = "Reference"} },
            { FieldType.PicklistField, new FieldType{ Key = "Picklist", Label = "Picklist"} },
            { FieldType.DateField, new FieldType{ Key = "Date", Label = "Date"} },
            { FieldType.TrueFalseField, new FieldType{ Key = "True-False", Label = "True-False"} },
            { FieldType.DecimalField, new FieldType{ Key = "Decimal", Label = "Decimal"} },
            { FieldType.IntField, new FieldType{ Key = "Int", Label = "Int"} },
            { FieldType.PercentageField, new FieldType{ Key = "Percentage", Label = "Percentage"} }
        };
    }
}

