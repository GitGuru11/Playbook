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
    public partial class PlaybookNodeFactoryBase : DataObjectFactory<PlaybookNode>
    {
        // Fields
        public const string FIELD_NAME_NODEID = "NodeId";
        public const string FIELD_NAME_NODETYPE = "NodeType";
        public const string FIELD_NAME_PARENTNODEID = "ParentNodeId";
        public const string FIELD_NAME_ORDER = "Order";
        public const string FIELD_NAME_PLAYBOOKID = "PlaybookId";
        public const string FIELD_NAME_NODENAME = "NodeName";
        public const string FIELD_NAME_DELAY = "Delay";
        public const string FIELD_NAME_DELAYTYPE = "DelayType";
        public const string FIELD_NAME_ACTIONTYPE = "ActionType";
        public const string FIELD_NAME_VERSION = "Version";

        // Parameters
        public const string PARAMETER_NAME_NODEID = "@NODEID";
        public const string PARAMETER_NAME_NODETYPE = "@NODETYPE";
        public const string PARAMETER_NAME_PARENTNODEID = "@PARENTNODEID";
        public const string PARAMETER_NAME_ORDER = "@ORDER";
        public const string PARAMETER_NAME_PLAYBOOKID = "@PLAYBOOKID";
        public const string PARAMETER_NAME_NODENAME = "@NODENAME";
        public const string PARAMETER_NAME_DELAY = "@DELAY";
        public const string PARAMETER_NAME_DELAYTYPE = "@DELAYTYPE";
        public const string PARAMETER_NAME_ACTIONTYPE = "@ACTIONTYPE";
        public const string PARAMETER_NAME_VERSION= "@VERSION";

        /// <summary>
        /// Gets the table name.
        /// </summary>
        public override string TableName
        {
            get { return "PlaybookNode"; }
        }

        /// <summary>
        /// Gets the primary key name.
        /// </summary>
        public override string PrimaryKeyName
        {
            get { return "NodeId"; }
        }

        protected override string SelectAllStatement =>
            $"SELECT NodeId, NodeName, ParentNodeId, NodeType, ActionType, Delay, DelayType, Order, PlaybookId, Version FROM {TenantContext.Current.TenantName}.PlaybookNode";

        /// <summary>
        /// Gets the insert statement.
        /// </summary>
        protected override string InsertStatement =>
            $"INSERT INTO {TenantContext.Current.TenantName}.PlaybookNode (NodeId, NodeName, ParentNodeId, NodeType, ActionType, Delay, DelayType, Order, PlaybookId, Version) VALUES (@NODEID, @NODENAME, @PARENTNODEID, @NODETYPE, @ACTIONTYPE, @DELAY, @DELAYTYPE, @ORDER, @PLAYBOOKID, @VERSION)";

        /// <summary>
        /// Gets the update statement.
        /// </summary>
        protected override string UpdateStatement =>
            $"ALTER TABLE {TenantContext.Current.TenantName}.PlaybookNode UPDATE NodeName = @NODENAME, ParentNodeId = @PARENTNODEID, NodeType = @NODETYPE, ActionType = @ACTIONTYPE, Delay = @DELAY, DelayType = @DELAYTYPE, Order = @ORDER, PlaybookId = @PLAYBOOKID, Version = @VERSION WHERE NodeId = @NODEID";

        /// <summary>
        /// Gets the delete statement.
        /// </summary>
        protected override string DeleteStatement =>
            $"DELETE FROM {TenantContext.Current.TenantName}.PlaybookNode WHERE NodeId = @NODEID";

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
        /// Add SQL parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valNodeName">The valNodeName</param>
        public static void AddNodeNameParameter(DatabaseHandler dataHandler, string valNodeName)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Size = 1024,
                Value = valNodeName,
                IsNullable = true,
                ParameterName = "NODENAME"
            };
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valParentNodeId">The valParentNodeId.</param>
        public static void AddParentNodeIdParameter(DatabaseHandler dataHandler, string? valParentNodeId)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Size = 36,
                Value = valParentNodeId,
                ParameterName = "PARENTNODEID"
            };
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valNodeType">The valNodeType.</param>
        public static void AddNodeTypeParameter(DatabaseHandler dataHandler, string valNodeType)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Size = 36,
                Value = valNodeType,
                IsNullable = true,
                ParameterName = "NODETYPE"
            };
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valActionType">The valActionType.</param>
        public static void AddActionTypeParameter(DatabaseHandler dataHandler, string? valActionType)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Size = 36,
                Value = valActionType,
                IsNullable = true,
                ParameterName = "ACTIONTYPE"
            };
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valDelay">The valDelay.</param>
        public static void AddDelayParameter(DatabaseHandler dataHandler, long valDelay)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.Int64,
                Direction = ParameterDirection.Input,
                Value = valDelay,
                IsNullable = true,
                ParameterName = "DELAY"
            };
            dataHandler.Command.Parameters.Add(param);
        }
        
        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valDelayType">The valDelayType.</param>
        public static void AddDelayTypeParameter(DatabaseHandler dataHandler, string valDelayType)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Size = 36,
                Value = valDelayType,
                IsNullable = true,
                ParameterName = "DELAYTYPE"
            };
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valPlaybookId">The valDelayvalPlaybookIdype.</param>
        public static void AddPlaybookIdParameter(DatabaseHandler dataHandler, string valPlaybookId)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Size = 36,
                Value = valPlaybookId,
                ParameterName = "PLAYBOOKID"
            };
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valOrder">The valOrder.</param>
        public static void AddOrderParameter(DatabaseHandler dataHandler, int valOrder)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.Int32,
                Direction = ParameterDirection.Input,
                Value = valOrder,
                ParameterName = "ORDER"
            };
            dataHandler.Command.Parameters.Add(param);
        }
        
        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valVersion">The valVersion.</param>
        public static void AddVersionParameter(DatabaseHandler dataHandler, int valVersion)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.Int32,
                Direction = ParameterDirection.Input,
                Value = valVersion,
                ParameterName = "VERSION"
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
            var entity = databaseObject as PlaybookNode;
            AddNodeIdParameter(databaseHandler, entity.NodeId);
            AddNodeNameParameter(databaseHandler, entity.NodeName);
            AddParentNodeIdParameter(databaseHandler, entity.ParentNodeId);
            AddNodeTypeParameter(databaseHandler, entity.NodeType);
            AddActionTypeParameter(databaseHandler, entity.ActionType);
            AddDelayParameter(databaseHandler, entity.Delay);
            AddDelayTypeParameter(databaseHandler, entity.DelayType);
            AddPlaybookIdParameter(databaseHandler, entity.PlaybookId);
            AddOrderParameter(databaseHandler, entity.Order);
            AddVersionParameter(databaseHandler, entity.Version);
        }

        /// <summary>
        /// Create object from the data reader provided.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="populateObject">The populate object.</param>
        /// <returns>A data object.</returns>
        protected override PlaybookNode CreateObject(DbDataReader reader, bool populateObject)
        {
            var entity = new PlaybookNode
            {
                NodeId = reader.GetFieldValue<string>(reader.GetOrdinal("NodeId")),
                NodeName = reader.GetFieldValue<string>(reader.GetOrdinal("NodeName")),
                ParentNodeId = reader.GetFieldValue<string>(reader.GetOrdinal("ParentNodeId")),
                NodeType = reader.GetFieldValue<string>(reader.GetOrdinal("NodeType")),
                ActionType = reader.GetFieldValue<string>(reader.GetOrdinal("ActionType")),
                Delay = reader.GetFieldValue<Int64>(reader.GetOrdinal("Delay")),
                DelayType = reader.GetFieldValue<string>(reader.GetOrdinal("DelayType")),
                Order = reader.GetFieldValue<Int32>(reader.GetOrdinal("Order")),
                PlaybookId = reader.GetFieldValue<string>(reader.GetOrdinal("PlaybookId")),
                Version = reader.GetFieldValue<Int32>(reader.GetOrdinal("Version")),

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
        protected override async Task<PlaybookNode> CreateObjectAsync(DbDataReader reader, bool populateObject)
        {
            // Make sure to check if the reader is async-capable
            var entity = new PlaybookNode
            {
                NodeId = (await reader.GetFieldValueAsync<Guid>(reader.GetOrdinal("NodeId"))).ToString(),
                NodeName = await reader.GetFieldValueAsync<string>(reader.GetOrdinal("NodeName")),
                ParentNodeId = await reader.GetFieldValueAsync<string>(reader.GetOrdinal("ParentNodeId")),
                NodeType = await reader.GetFieldValueAsync<string>(reader.GetOrdinal("NodeType")),
                ActionType = await reader.GetFieldValueAsync<string>(reader.GetOrdinal("ActionType")),
                Delay = await reader.GetFieldValueAsync<Int64>(reader.GetOrdinal("Delay")),
                DelayType = await reader.GetFieldValueAsync<string>(reader.GetOrdinal("DelayType")),
                Order = await reader.GetFieldValueAsync<Int32>(reader.GetOrdinal("Order")),
                PlaybookId = await reader.GetFieldValueAsync<string>(reader.GetOrdinal("PlaybookId")),
                Version = await reader.GetFieldValueAsync<Int32>(reader.GetOrdinal("Version")),
                IsNew = false
            };

            return entity;
        }
    }
}
