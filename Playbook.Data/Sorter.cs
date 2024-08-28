namespace Playbook.Data.ClickHouse
{
    using System.Collections.Generic;
    using Playbook.Data.ClickHouse;

    /// <summary>
    /// The sorter.
    /// </summary>
    public static class Sorter
    {
        #region Public Methods and Operators

        /// <summary>
        /// The columns.
        /// </summary>
        /// <param name="colList">
        /// The col list.
        /// </param>
        /// <returns>
        /// The System.String.
        /// </returns>
        public static string Columns(List<SortDefinition> colList)
        {
            var sortedList = string.Empty;

            foreach (SortDefinition column in colList)
            {
                if (column.ColumnName.Trim().Length > 0)
                {
                    sortedList += column.ColumnName + (column.Order == SortOrder.Ascending ? " ASC" : " DESC") + ", ";
                }
            }

            return sortedList == string.Empty ? string.Empty : "ORDER BY " + sortedList.Trim().TrimEnd(',');
        }

        #endregion
    }
}