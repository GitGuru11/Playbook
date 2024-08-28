using Playbook.Business.Model;
using Playbook.Data.ClickHouse;

namespace Playbook.Bussiness.Model
{
    /// <summary>
    /// Factory base class for ObjectFieldFactory.
    /// </summary>
    public partial class ObjectFieldFactory : ObjectFieldFactoryBase
    {
        public async Task<List<ObjectField>> FindAllActiveAsync(SortOrder sort_order, string sort_column, 
            string search_term, int skip, int limit)
        {
            await using var dataHandler = new DatabaseHandler();
            string searchQuery = string.IsNullOrEmpty(search_term) ? string.Empty : $" AND Name LIKE '%{search_term}%' OR Description LIKE '%{search_term}%'";
            string sortColumn = TransformNameColumn<ObjectField>(sort_column);
            string sortQuery = string.IsNullOrEmpty(sortColumn) ? string.Empty : $" ORDER BY {sortColumn} {(SortOrder.Ascending.Equals(sort_order) ? "ASC" : "DESC")}";
            string querySkipLimit = string.IsNullOrEmpty(sortQuery) ? $" ORDER BY CreatedAt OFFSET {skip} ROW FETCH FIRST {limit} ROWS ONLY" :$" OFFSET {skip} ROW FETCH FIRST {limit} ROWS ONLY";
            dataHandler.Command.CommandText = $"{SelectAllStatement} WHERE IsDeleted=@ISDELETED{searchQuery}{sortQuery}{querySkipLimit}";
            AddIsDeletedParameter(dataHandler, false);
            return await FindAsync(dataHandler);
        }
        
        public async Task<ObjectField?> FindAsync(String id)
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