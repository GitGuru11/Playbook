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
    public partial class QueryConditionFactoryBase : DataObjectFactory<QueryCondition>
    {
        // Fields
        public const string FIELD_NAME_CONDITIONID = "ConditionId";
        public const string FIELD_NAME_FIELD = "Field";
        public const string FIELD_NAME_VALUE = "Value";
        public const string FIELD_NAME_OPERATOR = "Operator";
        public const string FIELD_NAME_QUERYID = "QueryId";
        public const string FIELD_NAME_NESTEDQUERYID = "NestedQueryId";

        // Parameters
        public const string PARAMETER_NAME_ID = "@CONDITIONID";
        public const string PARAMETER_NAME_FIELD = "@FIELD";
        public const string PARAMETER_NAME_VALUE = "@VALUE";
        public const string PARAMETER_NAME_OPERATOR = "@OPERATOR";
        public const string PARAMETER_NAME_QUERYID = "QUERYID";
        public const string PARAMETER_NAME_NESTEDQUERYID = "@NESTEDQUERYID";

        /// <summary>
        /// Gets the table name.
        /// </summary>
        public override string TableName => "QueryCondition";

        /// <summary>
        /// Gets the primary key name.
        /// </summary>
        public override string PrimaryKeyName => "Id";

        protected override string SelectAllStatement =>
            $"SELECT ConditionId, Field, Value, Operator, QueryId, NestedQueryId FROM {TenantContext.Current.TenantName}.QueryCondition";

        /// <summary>
        /// Gets the insert statement.
        /// </summary>
        protected override string InsertStatement =>
            $"INSERT INTO {TenantContext.Current.TenantName}.QueryCondition (ConditionId, Field, Value, Operator,  QueryId, NestedQueryId) VALUES (@CONDITIONID, @FIELD, @VALUE, @OPERATOR, @QUERYID, @NESTEDQUERYID)";

        /// <summary>
        /// Gets the update statement.
        /// </summary>
        protected override string UpdateStatement =>
            $"ALTER TABLE {TenantContext.Current.TenantName}.QueryCondition UPDATE Field = @FIELD, Value = @VALUE, Operator = @OPERATOR, QueryId = @QUERYID, NestedQueryId = @NESTEDQUERYID WHERE ConditionId = @CONDITIONID";

        /// <summary>
        /// Gets the delete statement.
        /// </summary>
        protected override string DeleteStatement =>
            $"DELETE FROM {TenantContext.Current.TenantName}.QueryCondition WHERE ConditionId = @CONDITIONID";

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valConditionId">The valConditionId.</param>
        public static void AddConditionIdParameter(DatabaseHandler dataHandler, string valConditionId)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Size = 36,
                Value = valConditionId,
                ParameterName = "CONDITIONID"
            };
            dataHandler.Command.Parameters.Add(param);
        }


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
        /// <param name="valNestedQueryId">The valNestedQueryId.</param>
        public static void AddNestedQueryIdParameter(DatabaseHandler dataHandler, string valNestedQueryId)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Size = 36,
                Value = valNestedQueryId,
                ParameterName = "NESTEDQUERYID"
            };
            dataHandler.Command.Parameters.Add(param);
        }


        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valField">The valFieldId.</param>
        public static void AddFieldParameter(DatabaseHandler dataHandler, string valField)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Size = 36,
                Value = valField,
                ParameterName = "FIELD"
            };
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valValue">The valValue.</param>
        public static void AddValueParameter(DatabaseHandler dataHandler, string valValue)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Size = 1024,
                Value = valValue,
                ParameterName = "VALUE"
            };
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valOperator">The valOperator.</param>
        public static void AddOperatorParameter(DatabaseHandler dataHandler, string valOperator)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Size = 36,
                Value = valOperator,
                ParameterName = "OPERATOR"
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
            var entity = databaseObject as QueryCondition;
            AddConditionIdParameter(databaseHandler, entity.ConditionId);
            AddQueryIdParameter(databaseHandler, entity.QueryId);
            AddNestedQueryIdParameter(databaseHandler, entity.NestedQueryId);
            AddFieldParameter(databaseHandler, entity.Field);
            AddValueParameter(databaseHandler, entity.Value);
            AddOperatorParameter(databaseHandler, entity.Operator);
        }

        /// <summary>
        /// Create object from the data reader provided.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="populateObject">The populate object.</param>
        /// <returns>A data object.</returns>
        protected override QueryCondition CreateObject(DbDataReader reader, bool populateObject)
        {
            var entity = new QueryCondition
            {
                ConditionId = reader.GetFieldValue<string>(reader.GetOrdinal("Id")),
                QueryId = reader.GetFieldValue<string>(reader.GetOrdinal("QueryId")),
                NestedQueryId = reader.GetFieldValue<string>(reader.GetOrdinal("NestedQueryId")),
                Field = reader.GetFieldValue<string>(reader.GetOrdinal("Field")),
                Value = reader.GetFieldValue<string>(reader.GetOrdinal("Value")),
                Operator = reader.GetFieldValue<string>(reader.GetOrdinal("Operator")),
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
        protected override async Task<QueryCondition> CreateObjectAsync(DbDataReader reader, bool populateObject)
        {
            // Make sure to check if the reader is async-capable
            var entity = new QueryCondition
            {
                ConditionId = (await reader.GetFieldValueAsync<Guid>(reader.GetOrdinal("ConditionId"))).ToString(),
                QueryId = await reader.GetFieldValueAsync<string>(reader.GetOrdinal("QueryId")),
                NestedQueryId = await reader.GetFieldValueAsync<string>(reader.GetOrdinal("NestedQueryId")),
                Field = await reader.GetFieldValueAsync<string>(reader.GetOrdinal("Field")),
                Value = await reader.GetFieldValueAsync<string>(reader.GetOrdinal("Value")),
                Operator = await reader.GetFieldValueAsync<string>(reader.GetOrdinal("Operator")),
                IsNew = false
            };

            return entity;
        }
    }
}
