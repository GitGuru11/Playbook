using System;
using System.Collections.Generic;
using System.Linq;

namespace Playbook.Service.Contracts
{
    public record PlaybookObjectListResponse
    {
        public List<CustomPlaybookObjectResponse> PlaybookObjects { get; set; } // List of Playbook Objects.
        public int Count { get; set; } // Total count of Playbook Objects.
        public int Skip { get; set; } // Number of objects skipped for pagination.
        public int Limit { get; set; } // Limit of objects per page.
        public string searchTerm { get; set; } // Search term used for filtering results.

        public PlaybookObjectListResponse(List<Bussiness.Model.PlaybookObject> playbookObjects, int count, int skip, int limit, string searchTerm)
        {
            this.PlaybookObjects = playbookObjects.Select(MapToCustomResponse).ToList();
            this.Count = count;
            this.Skip = skip;
            this.Limit = limit;
            this.searchTerm = searchTerm;
        }

        private CustomPlaybookObjectResponse MapToCustomResponse(Bussiness.Model.PlaybookObject item) =>
            new(
                item.Id,
                item.Name,
                item.Description,
                item.Category
            );
    }

    public record CustomPlaybookObjectResponse(
        string Id, // Unique identifier of the Playbook Object.
        string Name, // Name of the Playbook Object.
        string Description, // Description of the Playbook Object.
        string Category // Category of the Playbook Object.
    );
}
