using System.Data;
using System.Data.Common;
using Playbook.Data.ClickHouse;
using ClickHouse.Client.ADO.Parameters;
using Playbook.Common.Tenant;

namespace Playbook.Bussiness.Model
{
    /// <summary>
    /// Factory base class for PlaybookNodeFactoryBase.
    /// </summary>
    public partial class QueryFactoryBase : DataObjectFactory<Query>
    {
        // Fields
        public const string FIELD_NAME_QUERYID = "QueryId";
        public const string FIELD_NAME_NODEID = "NodeId";
        public const string FIELD_NAME_PARENTID = "ParentId";
        public const string FIELD_NAME_ISAND = "IsAnd";

        // Parameters
        public const string PARAMETER_NAME_QUERYID = "@QUERYID";
        public const string PARAMETER_NAME_NODEID = "@NODEID";
        public const string PARAMETER_NAME_PARENTID = "@PARENTID";
        public const string PARAMETER_NAME_ISAND = "@ISAND";

        /// <summary>
        /// Gets the table name.
        /// </summary>
        public override string TableName => "Query";

        /// <summary>
        /// Gets the primary key name.
        /// </summary>
        public override string PrimaryKeyName => "QueryId";

        protected override string SelectAllStatement =>
            $"SELECT QueryId, IsAnd, ParentId, NodeId FROM {TenantContext.Current.TenantName}.Query";

        /// <summary>
        /// Gets the insert statement.
        /// </summary>
        protected override string InsertStatement =>
            $"INSERT INTO {TenantContext.Current.TenantName}.Query (QueryId, IsAnd, ParentId, NodeId) VALUES (@QUERYID, @ISAND, @PARENTID, @NODEID)";

        /// <summary>
        /// Gets the update statement.
        /// </summary>
        protected override string UpdateStatement =>
            $"ALTER TABLE {TenantContext.Current.TenantName}.Query UPDATE IsAnd = @ISAND, ParentId = @PARENTID, NodeId = @NODEID WHERE QueryId = @QUERYID";

        /// <summary>
        /// Gets the delete statement.
        /// </summary>
        protected override string DeleteStatement =>
            $"DELETE FROM {TenantContext.Current.TenantName}.Query WHERE QueryId = @QUERYID";

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valQueryId">The valQueryId.</param>
        public static void AddQueryIdParameter(DatabaseHandler dataHandler, string valQueryId)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Size = 36,
                Value = valQueryId,
                ParameterName = "QUERYID"
            };
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valParentId">The valParentId.</param>
        public static void AddParentIdParameter(DatabaseHandler dataHandler, string valParentId)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Size = 36,
                Value = valParentId,
                ParameterName = "PARENTID"
            };
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valIsAnd">The valIsAnd.</param>
        public static void AddIsAndParameter(DatabaseHandler dataHandler, bool valIsAnd)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.Boolean,
                Direction = ParameterDirection.Input,
                Value = valIsAnd,
                ParameterName = "ISAND"
            };
            dataHandler.Command.Parameters.Add(param);
        }
        
        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valNodeId">The valNodeId.</param>
        public static void AddNodeIdParameter(DatabaseHandler dataHandler, string valNodeId)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Size = 36,
                Value = valNodeId,
                ParameterName = "NODEID"
            };
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        protected override void AppendSqlParameters(DatabaseHandler databaseHandler, DatabaseObject databaseObject,
            DataMode mode)
        {
            var entity = databaseObject as Query;
            AddQueryIdParameter(databaseHandler, entity.QueryId);
            AddParentIdParameter(databaseHandler, entity.ParentId);
            AddNodeIdParameter(databaseHandler, entity.NodeId);
            AddIsAndParameter(databaseHandler, entity.IsAnd);
        }

        /// <summary>
        /// Create object from the data reader provided.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="populateObject">The populate object.</param>
        /// <returns>A data object.</returns>
        protected override Query CreateObject(DbDataReader reader, bool populateObject)
        {
            var entity = new Query
            {
                QueryId = reader.GetFieldValue<string>(reader.GetOrdinal("QueryId")),
                ParentId = reader.GetFieldValue<string>(reader.GetOrdinal("ParentId")),
                NodeId = reader.GetFieldValue<string>(reader.GetOrdinal("NodeId")),
                IsAnd = reader.GetFieldValue<bool>(reader.GetOrdinal("IsAnd")),
                IsNew = false
            };

            return entity;
        }

        /// <summary>
        /// Create object asynchronously from the data reader provided.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="populateObject">The populate object.</param>
        /// <returns>A data object.</returns>
        protected override async Task<Query> CreateObjectAsync(DbDataReader reader, bool populateObject)
        {
            // Make sure to check if the reader is async-capable
            var entity = new Query
            {
                QueryId = (await reader.GetFieldValueAsync<Guid>(reader.GetOrdinal("QueryId"))).ToString(),
                ParentId = await reader.GetFieldValueAsync<string>(reader.GetOrdinal("ParentId")),
                NodeId = await reader.GetFieldValueAsync<string>(reader.GetOrdinal("NodeId")),
                IsAnd = await reader.GetFieldValueAsync<bool>(reader.GetOrdinal("IsAnd")),
                IsNew = false
            };

            return entity;
        }
    }
}
