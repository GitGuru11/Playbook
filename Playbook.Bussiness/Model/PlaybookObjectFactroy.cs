using Playbook.Business.Model;
using Playbook.Data.ClickHouse;

namespace Playbook.Bussiness.Model
{
    /// <summary>
    /// Factory class for managing operations related to PlaybookObjects.
    /// </summary>
    public partial class PlaybookObjectFactory : PlaybookObjectFactoryBase
    {
        public async Task<List<PlaybookObject>> FindAllActiveAsync(SortOrder sort_order, string sort_column,
            string searchTerm, int skip, int limit)
        {
            await using var dataHandler = new DatabaseHandler();
            string searchQuery = string.IsNullOrEmpty(searchTerm) ? string.Empty : $" AND Name LIKE '%{searchTerm}%' OR Description LIKE '%{searchTerm}%'";
            string sortColumn = TransformNameColumn<PlaybookObject>(sort_column);
            string sortQuery = string.IsNullOrEmpty(sortColumn) ? string.Empty : $" ORDER BY {sortColumn} {(SortOrder.Ascending.Equals(sort_order) ? "ASC" : "DESC")}";
            string querySkipLimit = string.IsNullOrEmpty(sortQuery) ? $" ORDER BY CreatedAt OFFSET {skip} ROW FETCH FIRST {limit} ROWS ONLY" : $" OFFSET {skip} ROW FETCH FIRST {limit} ROWS ONLY";
            dataHandler.Command.CommandText = $"{SelectAllStatement} WHERE IsDeleted=@ISDELETED{searchQuery}{sortQuery}{querySkipLimit}";
            AddIsDeletedParameter(dataHandler, false);
            return await FindAsync(dataHandler);
        }

        public async Task<PlaybookObject?> FindByIdAsync(string id)
        {
            await using var dataHandler = new DatabaseHandler();
            dataHandler.Command.CommandText = $"{SelectAllStatement} WHERE Id=@ID";
            AddIdParameter(dataHandler, id);
            var result = await FindAsync(dataHandler);
            return result.Count > 0 ? result.First() : null;
        }

        private string TransformNameColumn<T>(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
            {
                return columnName;
            }
            else
            {
                T obj = Activator.CreateInstance<T>();
                return obj.GetType().GetProperties()
                    .First(c => string.Equals(c.Name, columnName, StringComparison.CurrentCultureIgnoreCase))?.Name;
            }
        }
    }
}
