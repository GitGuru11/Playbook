using System.Collections.Generic;
using System.Threading.Tasks;
using Playbook.Bussiness.Model;
using Playbook.Service.Contracts;

namespace Playbook.Service.Handlers
{
    public class FieldTypeHandler
    {
        public async Task<List<FieldType>> ProcessFieldTypesRequestAsync()
        {
            var factory = new FieldTypeFactory();
            var res = await factory.FindAllFieldTypesAsync();
            return res;
        }

        public async Task<Dictionary<string, List<FieldTypeRule>>> ProcessFieldTypesRulesRequestAsync()
        {
            var factory = new FieldTypeRuleFactory();
            var res = await factory.FindAllRulesAsync();
            return res;
        }

        public async Task<Dictionary<string, List<FieldTypeRule>>> ProcessRulesByFieldTypeequestAsync(string fieldType)
        {
            var factory = new FieldTypeRuleFactory();
            var res = await factory.FindByFieldTypeAsync(fieldType);
            return res;
        }
    }
}
