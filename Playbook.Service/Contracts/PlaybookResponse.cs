namespace Playbook.Service.Contracts
{
    public record PlaybookResponse
    {
        public List<CustomPlaybookResponse> playbooks { get; set; }
        public int count  { get; set; }
        public int skip { get; set; }
        public int limit { get; set; }
        public string search_term  { get; set; }

        public PlaybookResponse(List<Bussiness.Model.Playbook> playbooks, int count, int skip, int limit, string search_term)
        {
            this.playbooks = playbooks.Select(MapToCustomResponse).ToList();
            this.count = count;
            this.skip = skip;
            this.limit = limit;
            this.search_term = search_term;
        }
        private CustomPlaybookResponse MapToCustomResponse(Bussiness.Model.Playbook item) =>
            new(
                item.Id,
                item.Name,
                item.Description,
                item.ObjectTypeId,
                item.Enable,
                item.LastRun
            );
    }

    public record CustomPlaybookResponse(
        string Id,
        string Name,
        string Description,
        int ObjectTypeId,
        bool Enable,
        DateTime LastRun
    );


}