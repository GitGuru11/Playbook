using Playbook.Data.ClickHouse;

namespace Playbook.Bussiness.Model
{
    /// <summary>
    /// Factory base class for PlaybookRevisionFactory.
    /// </summary>
    public partial class PlaybookRevisionFactory : PlaybookRevisionFactoryBase
    {
        public async Task<List<PlaybookRevision>> FindRevisionsPlaybooksByIdAsync(string? playbookId)
        {
            await using var dataHandler = new DatabaseHandler();
            dataHandler.Command.CommandText = $"{SelectAllStatement} WHERE IsDeleted=@ISDELETED{(string.IsNullOrEmpty(playbookId) ? "" : $" AND PlaybookId LIKE + {playbookId}")}";
            AddIsDeletedParameter(dataHandler, false);
            return await FindAsync(dataHandler);
        }

    }
}