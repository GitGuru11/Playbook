using System.Data;
using System.Data.Common;
using Playbook.Data.ClickHouse;
using ClickHouse.Client.ADO.Parameters;
using Playbook.Common.Tenant;
using JsonApiSerializer;
using Newtonsoft.Json;

namespace Playbook.Bussiness.Model
{
    /// <summary>
    /// Factory base class for PlaybookBranchFactoryBase.
    /// </summary>
    public partial class PlaybookBranchFactoryBase : DataObjectFactory<PlaybookBranch>
    {
        // Fields
        public const string FIELD_NAME_ID = "Id";
        public const string FIELD_NAME_Name = "Name";
        public const string FIELD_NAME_QUERYTYPE = "QueryType";
        public const string FIELD_NAME_VALIDATIONERRORS = "ValidationErrors";
        public const string FIELD_NAME_PARENTID = "ParentId";

        // Parameters
        public const string PARAMETER_NAME_ID = "@ID";
        public const string PARAMETER_NAME_NAME = "@NAME";
        public const string PARAMETER_NAME_QUERYTYPE = "@QUERYTYPE";
        public const string PARAMETER_NAME_VALIDATIONERRORS = "@VALIDATIONERRORS";
        public const string PARAMETER_NAME_PARENTID = "@PARENTID";

        /// <summary>
        /// Gets the table name.
        /// </summary>
        public override string TableName => "PlaybookBranch";

        /// <summary>
        /// Gets the primary key name.
        /// </summary>
        public override string PrimaryKeyName => "Id";

        protected override string SelectAllStatement =>
            $"SELECT Id, Name, QueryType, ValidationErrors, ParentId FROM {TenantContext.Current.TenantName}.PlaybookBranch";

        /// <summary>
        /// Gets the insert statement.
        /// </summary>
        protected override string InsertStatement =>
            $"INSERT INTO {TenantContext.Current.TenantName}.PlaybookBranch (Id, Name, QueryType, ValidationErrors, ParentId) VALUES (@ID, @NAME, @QUERYTYPE, @VALIDATIONERRORS, @PARENTID)";

        /// <summary>
        /// Gets the update statement.
        /// </summary>
        protected override string UpdateStatement =>
            $"ALTER TABLE {TenantContext.Current.TenantName}.PlaybookBranch UPDATE Name = @NAME, QueryType = @QUERYTYPE, ValidationErrors = @VALIDATIONERRORS, ParentId = @PARENTID  WHERE Id = @ID";

        /// <summary>
        /// Gets the delete statement.
        /// </summary>
        protected override string DeleteStatement =>
            $"DELETE FROM {TenantContext.Current.TenantName}.PlaybookBranch WHERE Id = @ID";

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valId">The valId.</param>
        public static void AddIdParameter(DatabaseHandler dataHandler, string valId)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Size = 36,
                Value = valId,
                ParameterName = "ID"
            };
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add SQL parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valName">The valName</param>
        public static void AddNameParameter(DatabaseHandler dataHandler, string valName)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Size = 1024,
                Value = valName,
                IsNullable = true,
                ParameterName = "NAME"
            };
            dataHandler.Command.Parameters.Add(param);
        }


        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valQueryType">The valNodeType.</param>
        public static void AddQueryTypeParameter(DatabaseHandler dataHandler, string valQueryType)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Size = 36,
                Value = valQueryType,
                IsNullable = true,
                ParameterName = "QUERYTYPE"
            };
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valValidationErrors">The valValidationErrors.</param>
        public static void AddValidationErrorsParameter(DatabaseHandler dataHandler, List<string>? valValidationErrors)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.Int32,
                Direction = ParameterDirection.Input,
                Value = JsonConvert.SerializeObject(valValidationErrors, new JsonApiSerializerSettings()),
                ParameterName = "VALIDATIONERRORS"
            };
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valParentId">The valParentId.</param>
        public static void AddParentIParameter(DatabaseHandler dataHandler, string valParentId)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Size = 36,
                Value = valParentId,
                IsNullable = true,
                ParameterName = "PARENTID"
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
            var entity = databaseObject as PlaybookBranch;
            AddIdParameter(databaseHandler, entity.Id);
            AddNameParameter(databaseHandler, entity.Name);
            //AddQueryTypeParameter(databaseHandler, entity.QueryType);
            //AddValidationErrorsParameter(databaseHandler, entity.ValidationErrors);
            //AddParentIParameter(databaseHandler, entity.ParentId);
        }

        /// <summary>
        /// Create object from the data reader provided.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="populateObject">The populate object.</param>
        /// <returns>A data object.</returns>
        protected override PlaybookBranch CreateObject(DbDataReader reader, bool populateObject)
        {
            var entity = new PlaybookBranch
            {
                Id = reader.GetFieldValue<string>(reader.GetOrdinal("Id")),
                Name = reader.GetFieldValue<string>(reader.GetOrdinal("Name")),
                //QueryType = reader.GetFieldValue<string>(reader.GetOrdinal("QueryType")),
                //ValidationErrors = JsonConvert.DeserializeObject<List<string>>(reader.GetFieldValue<string>(reader.GetOrdinal("ValidationErrors"))),
                //ParentId = reader.GetFieldValue<string>(reader.GetOrdinal("ParentId")),
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
        protected override async Task<PlaybookBranch> CreateObjectAsync(DbDataReader reader, bool populateObject)
        {
            // Make sure to check if the reader is async-capable
            var entity = new PlaybookBranch
            {
                Id = (await reader.GetFieldValueAsync<Guid>(reader.GetOrdinal("Id"))).ToString(),
                Name = await reader.GetFieldValueAsync<string>(reader.GetOrdinal("Name")),
                //QueryType = await reader.GetFieldValueAsync<string>(reader.GetOrdinal("QueryType")),
                //ValidationErrors = JsonConvert.DeserializeObject<List<string>>(await reader.GetFieldValueAsync<string>(reader.GetOrdinal("ValidationErrors"))),
                //ParentId = (await reader.GetFieldValueAsync<Guid>(reader.GetOrdinal("ParentId"))).ToString(),
                IsNew = false
            };

            return entity;
        }
    }
}
