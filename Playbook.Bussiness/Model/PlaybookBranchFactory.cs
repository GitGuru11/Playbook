using Playbook.Data.ClickHouse;

namespace Playbook.Bussiness.Model
{
    /// <summary>
    /// Factory base class for PlaybookBranchFactory.
    /// </summary>
    public partial class PlaybookBranchFactory : PlaybookBranchFactoryBase
    {
        public async Task<List<PlaybookBranch>> FindBranchesByParentIdAsync(string parentId)
        {
            await using var dataHandler = new DatabaseHandler();
            dataHandler.Command.CommandText = $"{SelectAllStatement} WHERE ParentId=@PARENTID";
            AddParentIParameter(dataHandler, parentId);
            return await FindAsync(dataHandler);
        }
    }
}