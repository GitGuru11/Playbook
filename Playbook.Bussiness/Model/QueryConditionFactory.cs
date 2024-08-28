using Playbook.Common.Tenant;
using Playbook.Data.ClickHouse;

namespace Playbook.Bussiness.Model
{
    /// <summary>
    /// Factory base class for PlaybookFactory.
    /// </summary>
    public partial class QueryConditionFactory : QueryConditionFactoryBase
    {
        public async Task<List<QueryCondition>> FindQueryConditionsByQueryId(string queryId)
        {
            await using var dataHandler = new DatabaseHandler();
            dataHandler.Command.CommandText = $"{SelectAllStatement} WHERE QueryId=@QUERYID";
            AddQueryIdParameter(dataHandler, queryId);
            return await FindAsync(dataHandler);
        }

        public async Task<List<QueryCondition>> FindQueryConditionsByQueryIds(IEnumerable<string> queryIds)
        {
            await using var dataHandler = new DatabaseHandler();
            dataHandler.Command.CommandText = $"{SelectAllStatement} WHERE QueryId IN ({queryIds})";
            return await FindAsync(dataHandler);        
        }
    }
}