namespace Playbook.Service.Contracts
{
    public record ObjectFieldResponse
    {
        public List<CustomObjectFieldResponse> objectFields { get; set; }
        public int count  { get; set; }
        public int skip { get; set; }
        public int limit { get; set; }
        public string search_term  { get; set; }

        public ObjectFieldResponse(List<Bussiness.Model.ObjectField> objectFields, int count, int skip, int limit, string search_term)
        {
            this.objectFields = objectFields.Select(MapToCustomResponse).ToList();
            this.count = count;
            this.skip = skip;
            this.limit = limit;
            this.search_term = search_term;
        }
        private CustomObjectFieldResponse MapToCustomResponse(Bussiness.Model.ObjectField item) =>
            new(
                item.Id,
                item.ObjectTypeId,
                item.Name,
                item.Label,
                item.FieldType,
                item.OptionId
            );
    }

    public record CustomObjectFieldResponse(
        string Id,
        string ObjectTypeId,
        string Name,
        string Label,
        string FieldType,
        string OptionId
    );
}