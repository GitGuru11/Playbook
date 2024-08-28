using System.Data;
using Playbook.Data.ClickHouse;
using System.Data.Common;
using ClickHouse.Client.ADO.Parameters;
using Playbook.Common.Tenant;

namespace Playbook.Bussiness.Model
{
    /// <summary>
    /// Factory base class for PlaybookDefinitionFactory.
    /// </summary>
    public partial class PlaybookDefinitionFactoryBase : DataObjectFactory<PlaybookDefinition>
    {
        //Fields
        public const string FIELD_NAME_ID = "Id";

        public const string FIELD_NAME_IS_ACTIVE = "IsActive";

        public const string FIELD_NAME_VERSION_NUMBER = "VersionNumber";

        public const string FIELD_NAME_OBJECTTYPEID = "ObjectTypeId";

        // Parameters
        public const string PARAMETER_NAME_ID = "@ID";

        public const string PARAMETER_NAME_IS_ACTIVE = "@ISACTIVE";

        public const string PARAMETER_NAME_VERSION_NUMBER = "@VERSIONNUMBER";

        public const string PARAMETER_NAME_OBJECTTYPEID = "@OBJECTTYPEID";

        /// <summary>
        /// Gets the table name.
        /// </summary>
        public override string TableName => "PlaybookDefinition ";

        /// <summary>
        /// Gets the primar key name.
        /// </summary>
        public override string PrimaryKeyName => "Id";

        /// <summary>
        /// Gets the select all statement.
        /// </summary>
        protected override string SelectAllStatement =>
            $"SELECT Id, ParentId, IsActive, VersionNumber, ObjectTypeId FROM {TenantContext.Current.TenantName}.PlaybookDefinition";

        /// <summary>
        /// Gets the insert statement.
        /// </summary>
        protected override string InsertStatement =>
            $"INSERT INTO {TenantContext.Current.TenantName}.PlaybookDefinition (Id, IsActive, VersionNumber, ObjectTypeId) VALUES (@ID, @ISACTIVE, @VERSIONNUMBER, @OBJECTTYPEID)";

        /// <summary>
        /// Gets the update statement.
        /// </summary>
        protected override string UpdateStatement =>
            $"ALTER TABLE {TenantContext.Current.TenantName}.PlaybookDefinition UPDATE  IsActive = @ISACTIVE, VersionNumber = @VERSIONNUMBER, ObjectTypeId = @OBJECTTYPEID WHERE Id = @ID";

        /// <summary>
        /// Gets the delete statment.
        /// </summary>
        protected override string DeleteStatement => $"DELETE FROM {TenantContext.Current.TenantName}.PlaybookDefinition WHERE Id = @ID";

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
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valIsActive">The valIsActive.</param>
        public static void AddIsActiveParameter(DatabaseHandler dataHandler, bool valIsActive)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.Boolean,
                Direction = ParameterDirection.Input,
                Size = 4,
                Value = valIsActive,
                ParameterName = "ISACTIVE"
            };
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valVersionNumber">The valVersionNumber .</param>
        public static void AddVersionNumberAtParameter(DatabaseHandler dataHandler, int valVersionNumber)
        {
            var param = new ClickHouseDbParameter();
            param.DbType = DbType.Int32;
            param.Direction = ParameterDirection.Input;
            param.Value = valVersionNumber;
            param.ParameterName = "VERSIONNUMBER";
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valObjectTypeId">The valObjectTypeId .</param>
        public static void AddObjectTypeIdParameter(DatabaseHandler dataHandler, int valObjectTypeId)
        {
            var param = new ClickHouseDbParameter();
            param.DbType = DbType.Int32;
            param.Direction = ParameterDirection.Input;
            param.Value = valObjectTypeId;
            param.ParameterName = "OBJECTTYPEID";
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        protected override void AppendSqlParameters(DatabaseHandler databaseHandler, DatabaseObject databaseObject, DataMode mode)
        {
            var entity = databaseObject as PlaybookDefinition;
            if (mode == DataMode.Insert || mode == DataMode.Update)
            {
                AddIdParameter(databaseHandler, entity.Id);
                AddIsActiveParameter(databaseHandler, entity.IsActive);
                AddVersionNumberAtParameter(databaseHandler, entity.VersionNumber);
                AddObjectTypeIdParameter(databaseHandler, entity.ObjectTypeId);

            }
        }

        /// <summary>
        /// The create object from the data reader provided.
        /// </summary>
        /// <param name="reader">
        /// The reader.
        /// </param>
        /// <param name="populateObject">
        /// The populate object.
        /// </param>
        /// <returns>
        /// A data Object
        /// </returns>
        protected override PlaybookDefinition CreateObject(DbDataReader reader, bool populateObject)
        {
            var entity = new PlaybookDefinition();
            entity.Id = reader.GetFieldValue<string>(reader.GetOrdinal("Id"));
            entity.IsActive = reader.GetFieldValue<bool>(reader.GetOrdinal("IsActive"));
            entity.VersionNumber = reader.GetFieldValue<int>(reader.GetOrdinal("VersionNumber"));
            entity.ObjectTypeId = reader.GetFieldValue<int>(reader.GetOrdinal("ObjectTypeId"));
            entity.IsNew = false;
            return entity;
        }

        /// <summary>
        /// The create object from the data reader provided.
        /// </summary>
        /// <param name="reader">
        /// The reader.
        /// </param>
        /// <param name="populateObject">
        /// The populate object.
        /// </param>
        /// <returns>
        /// A data Object
        /// </returns>
        protected override async Task<PlaybookDefinition> CreateObjectAsync(DbDataReader reader, bool populateObject)
        {
            var entity = new PlaybookDefinition();
            entity.Id = (await reader.GetFieldValueAsync<Guid>(reader.GetOrdinal("Id"))).ToString();
            entity.IsActive = await reader.GetFieldValueAsync<bool>(reader.GetOrdinal("IsActive"));
            entity.VersionNumber = await reader.GetFieldValueAsync<int>(reader.GetOrdinal("VersionNumber"));
            entity.ObjectTypeId = await reader.GetFieldValueAsync<int>(reader.GetOrdinal("ObjectTypeId"));
            entity.IsNew = false;
            return entity;
        }
    }
}

