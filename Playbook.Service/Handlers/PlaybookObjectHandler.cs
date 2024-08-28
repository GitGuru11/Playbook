using Playbook.Bussiness.Model;
using Playbook.Common.Instrumentation;
using Playbook.Data.ClickHouse;
using Playbook.Service.Contracts;

namespace Playbook.Service.Handlers
{
    public class PlaybookObjectHandler
    {
        public async Task<PlaybookObjectListResponse> ProcessGetPlaybookObjectRequestAsync(string? sort_order,
            string? sort_column, string? searchTerm, int skip = 0, int limit = 10)
        {
            var response = await new PlaybookObjectFactory().FindAllActiveAsync(
                sort_order is null || sort_order.Contains("desc", StringComparison.CurrentCultureIgnoreCase)
                    ? SortOrder.Descending
                    : SortOrder.Ascending,
                sort_column,
                searchTerm,
                skip,
                limit
            );

            return new PlaybookObjectListResponse(playbookObjects: response, count: response.Count, limit: limit, skip: skip, searchTerm: searchTerm ?? "");
        }

        public async Task<PlaybookObject> ProcessGetPlaybookObjectRequestAsync(string id)
        {
            var playbookObjectFactory = new PlaybookObjectFactory();
            var response = await playbookObjectFactory.FindByIdAsync(id);
            if (response is null)
            {
                InstrumentationContext.Current.Error("ProcessGetPlaybookObjectRequestAsync",
                    "Not finding Playbook Object.");
            }
            return new PlaybookObject
            {
                Id = response.Id,
                Name = response.Name,
                Description = response.Description,
                Category = response.Category,
                ObjectTypeId = response.ObjectTypeId
            };
        }

        public async Task<PlaybookObjectResponse> ProcessCreatePlaybookObjectRequestAsync(PlaybookObjectRequest request)
        {
            
            var playbookObjectFactory = new PlaybookObjectFactory();

            var playbookObject = ConvertEntityToDto(request, true);

            if (!await playbookObjectFactory.SaveAsync(playbookObject))
            {
                InstrumentationContext.Current.Error("ProcessAddPlaybookObjectRequestAsync",
                    "Error occured while saving Playbook record.");
                var ErrorResponse = new PlaybookObjectResponse { Id = string.Empty , Status = false };
                return ErrorResponse;
            }
            var response = new PlaybookObjectResponse { Id = playbookObject.Id, Status = true };
            return response;
        }

        public async Task<string> ProcessUpdatePlaybookObjectRequestAsync(PlaybookObjectRequest request, string id)
        {
            
            var playbookObjectFactory = new PlaybookObjectFactory();
            var playbookObject = await playbookObjectFactory.FindByIdAsync(id);
            if (playbookObject is not null)
            {
                playbookObject.Name = request.Name;
                playbookObject.Description = request.Description;
                playbookObject.Category = request.Category;

                if (!await playbookObjectFactory.SaveAsync(playbookObject))
                {
                    InstrumentationContext.Current.Error("ProcessUpdatePlaybookObjectRequestAsync",
                        "Problem updating PlaybookObject.");
                }
            }
            else
            {
                InstrumentationContext.Current.Error("ProcessUpdatePlaybookObjectRequestAsync",
                    "Not finding PlaybookObject.");
            }
            return id;
        }

        public async Task<string> ProcessDeletePlaybookObjectRequestAsync(string id)
        {
            var playbookObjectFactory = new PlaybookObjectFactory();

            var playbookObject = await playbookObjectFactory.FindByIdAsync(id);

            if (playbookObject is not null)
            {
                playbookObject.IsDeleted = true;

                if (!await playbookObjectFactory.SaveAsync(playbookObject))
                {
                    InstrumentationContext.Current.Error("ProcessDeletePlaybookObjectRequestAsync",
                        "Problem updating PlaybookObject.");
                }
            }
            else
            {
                InstrumentationContext.Current.Error("ProcessDeletePlaybookObjectRequestAsync",
                    "Not finding PlaybookObject.");
            }
            return id;
        }

        private PlaybookObject ConvertEntityToDto(PlaybookObjectRequest item, bool isNew = false) =>
            new()
            {
                Id = UuidFactory.GetUuid(),
                ObjectTypeId = item.ObjectTypeId,
                Name = item.Name,
                Description = item.Description,
                Category = item.Category,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsNew = isNew
            };

        

    }
}
