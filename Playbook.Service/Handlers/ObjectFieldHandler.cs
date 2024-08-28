using Playbook.Bussiness.Model;
using Playbook.Common.Instrumentation;
using Playbook.Data.ClickHouse;
using Playbook.Service.Contracts;

namespace Playbook.Service.Handlers
{
    public class ObjectFieldHandler
    { 
        public async Task<ObjectFieldResponse> ProcessGetObjectFieldRequestAsync(string? sort_order, 
            string? sort_column, string? search_term, int skip = 0, int limit = 10)
        {
            var response = await new ObjectFieldFactory().FindAllActiveAsync(
                sort_order is null || sort_order.Contains("desc", StringComparison.CurrentCultureIgnoreCase)
                    ? SortOrder.Descending
                    :  SortOrder.Ascending,
                sort_column,
                search_term,
                skip,
                limit
            );

            return new ObjectFieldResponse(objectFields: response, count: response.Count, limit: limit, skip: skip, search_term: search_term ?? "");
        }
        
        public async Task<ObjectField> ProcessGetObjectFieldRequestAsync(string id)
        {
            var objectFieldFactory = new ObjectFieldFactory();
            var response = await objectFieldFactory.FindAsync(id);
            if (response is null)
            {
                InstrumentationContext.Current.Error("ProcessGetObjectFieldRequestAsync",
                    "Not finding Object Field.");
            }
            return response ?? new ObjectField();        
        }

        public async Task<bool> ProcessCreateObjectFieldRequestAsync(ObjectFieldRequest request)
        {
            var objectFieldFactory = new ObjectFieldFactory();

            var objectField = ConvertEntityToDto(request, true);

            return await objectFieldFactory.SaveAsync(objectField);
        }
        
        public async Task<string> ProcessUpdateObjectFieldRequestAsync(ObjectFieldRequest request, string id)
        {
            var objectFieldFactory = new ObjectFieldFactory();
            var objectField = await objectFieldFactory.FindAsync(id);
            if (objectField is not null)
            {
                objectField.Label = request.Label;
                objectField.Name = request.Name;
                
                if (!await objectFieldFactory.SaveAsync(objectField))
                {
                    InstrumentationContext.Current.Error("ProcessUpdateObjectFieldRequestAsync",
                        "Problem updating enable ObjectField.");
                }
            }
            else
            {
                InstrumentationContext.Current.Error("ProcessUpdateObjectFieldRequestAsync",
                    "Not finding ObjectField.");
            }
            return id;        
        }
        
        public async Task<string> ProcessDeleteObjectFieldRequestAsync(string id)
        {
            var objectFieldFactory = new ObjectFieldFactory();
            
            var objectField = await objectFieldFactory.FindAsync(id);

            if (objectField is not null)
            {
                objectField.IsDeleted = true;

                if (!await objectFieldFactory.SaveAsync(objectField))
                {
                    InstrumentationContext.Current.Error("ProcessDeleteObjectFieldRequestAsync",
                        "Problem updating enable ObjectField.");
                }
            }
            else
            {
                InstrumentationContext.Current.Error("ProcessDeleteObjectFieldRequestAsync",
                    "Not finding ObjectField.");
            }
            return id;
        }
        
        private ObjectField ConvertEntityToDto(ObjectFieldRequest item, bool isNew = false) =>
            new()
            {
                Id = UuidFactory.GetUuid(),
                ObjectTypeId = item.ObjectTypeId,
                Name = item.Name,
                Label = item.Label,
                FieldType = item.FieldType,
                OptionId = item.OptionId,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsNew = isNew
            };
        
    }
}
