namespace Playbook.Data.ClickHouse
{
    /// <summary>
    /// The sort definition.
    /// </summary>
    public class SortDefinition
    {
        #region Fields

        /// <summary>
        /// The name.
        /// </summary>
        private string name;

        /// <summary>
        /// The sort order.
        /// </summary>
        private SortOrder sortOrder = SortOrder.Ascending;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SortDefinition"/> class.
        /// </summary>
        public SortDefinition()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortDefinition"/> class.
        /// </summary>
        /// <param name="columnName">
        /// The column name.
        /// </param>
        /// <param name="order">
        /// The order.
        /// </param>
        public SortDefinition(string columnName, SortOrder order)
        {
            this.name = columnName;
            this.sortOrder = order;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the column name.
        /// </summary>
        public string ColumnName
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
            }
        }

        /// <summary>
        /// Gets the column name without alias.
        /// </summary>
        public string ColumnNameWithoutAlias
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        public SortOrder Order
        {
            get
            {
                return this.sortOrder;
            }

            set
            {
                this.sortOrder = value;
            }
        }

        /// <summary>
        /// Gets the reverse order.
        /// </summary>
        public SortOrder ReverseOrder
        {
            get
            {
                return this.sortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            }
        }

        #endregion
    }
}