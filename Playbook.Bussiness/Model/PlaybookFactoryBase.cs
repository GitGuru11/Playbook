using System.Data;
using Playbook.Data.ClickHouse;
using System.Data.Common;
using ClickHouse.Client.ADO.Parameters;
using Playbook.Common.Tenant;

namespace Playbook.Bussiness.Model
{
    /// <summary>
    /// Factory base class forPlayBookFactory.
    /// </summary>
    public partial class PlaybookFactoryBase : DataObjectFactory<Playbook>
    {

        //Fields
        public const string FIELD_NAME_ID = "Id";

        public const string FIELD_NAME_NAME = "Name";

        public const string FIELD_NAME_DESCRIPTION = "Description";

        public const string FIELD_NAME_OBJECTTYPEID = "ObjectTypeId";

        public const string FIELD_NAME_ENABLE = "Enable";

        public const string FIELD_NAME_LASTRUN = "LastRun";
        
        public const string FIELD_NAME_ISDELETED = "IsDeleted";

        public const string FIELD_NAME_CREATEDAT = "CreatedAt";

        public const string FIELD_NAME_UPDATEDAT = "UpdatedAt";

        // Parameters
        public const string PARAMETER_NAME_ID = "@ID";

        public const string PARAMETER_NAME_NAME = "@NAME";

        public const string PARAMETER_NAME_DESCRIPTION = "@DESCRIPTION";

        public const string PARAMETER_NAME_OBJECTTYPEID = "@OBJECTTYPEID";

        public const string PARAMETER_NAME_ENABLE = "@ENABLE";

        public const string PARAMETER_NAME_LASTRUN = "@LASTRUN";
        
        public const string PARAMETER_NAME_ISDELETED = "@ISDELETED";

        public const string PARAMETER_NAME_CREATEDAT = "@CREATEDAT";

        public const string PARAMETER_NAME_UPDATEDAT = "@UPDATEDAT";

        /// <summary>
        /// Gets the table name.
        /// </summary>
        public override string TableName
        {
            get { return "Playbook "; }
        }

        /// <summary>
        /// Gets the primar key name.
        /// </summary>
        public override string PrimaryKeyName
        {
            get { return "Id"; }
        }


        /// <summary>
        /// Gets the select all statement.
        /// </summary>
        protected override string SelectAllStatement =>
            $"SELECT Id, Name, Description, ObjectTypeId, Enable, LastRun, IsDeleted, CreatedAt, UpdatedAt FROM {TenantContext.Current.TenantName}.Playbook";

        /// <summary>
        /// Gets the insert statement.
        /// </summary>
        protected override string InsertStatement =>
            $"INSERT INTO {TenantContext.Current.TenantName}.Playbook (Id, Name, Description, ObjectTypeId, Enable, LastRun, IsDeleted, CreatedAt, UpdatedAt) VALUES (@ID, @NAME, @DESCRIPTION, @OBJECTTYPEID, @ENABLE, @LASTRUN, @ISDELETED, @CREATEDAT, @UPDATEDAT)";

        /// <summary>
        /// Gets the update statement.
        /// </summary>
        protected override string UpdateStatement => 
                    $"ALTER TABLE {TenantContext.Current.TenantName}.Playbook UPDATE Name = @NAME, Description = @DESCRIPTION, ObjectTypeId = @OBJECTTYPEID, Enable = @ENABLE, LastRun = @LASTRUN, IsDeleted = @ISDELETED, CreatedAt = @CREATEDAT, UpdatedAt = @UPDATEDAT WHERE Id = @ID";

        /// <summary>
        /// Gets the delete statment.
        /// </summary>
        protected override string DeleteStatement => $"ALTER TABLE {TenantContext.Current.TenantName}.Playbook FROM IsDeleted = true WHERE Id = @ID";

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valId">The valId .</param>
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
        /// <param name="valName">The valName .</param>
        public static void AddNameParameter(DatabaseHandler dataHandler, string valName)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Size = 1024,
                Value = valName,
                ParameterName = "NAME"
            };
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valDescription">The valDescription .</param>
        public static void AddDescriptionParameter(DatabaseHandler dataHandler, string valDescription)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Size = 1024,
                Value = valDescription,
                ParameterName = "DESCRIPTION"
            };
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
        /// <param name="valEnable">The valEnable .</param>
        public static void AddEnableParameter(DatabaseHandler dataHandler, bool valEnable)
        {
            var param = new ClickHouseDbParameter();
            param.DbType = DbType.Boolean;
            param.Direction = ParameterDirection.Input;
            param.Size = 4;
            param.Value = valEnable;
            param.ParameterName = "ENABLE";
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valLastRun">The valLastRun .</param>
        public static void AddLastRunParameter(DatabaseHandler dataHandler, DateTime valLastRun)
        {
            var param = new ClickHouseDbParameter();
            param.DbType = DbType.DateTime;
            param.Direction = ParameterDirection.Input;
            param.Size = 8;
            param.Value = valLastRun;
            param.ParameterName = "LASTRUN";
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valIsDeleted">The valIsDeleted .</param>
        public static void AddIsDeletedParameter(DatabaseHandler dataHandler, bool valIsDeleted)
        {
            var param = new ClickHouseDbParameter();
            param.DbType = DbType.Boolean;
            param.Direction = ParameterDirection.Input;
            param.Size = 4;
            param.Value = valIsDeleted;
            param.ParameterName = "ISDELETED";
            dataHandler.Command.Parameters.Add(param);
        }


        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valCreatedAt">The valCreatedAt .</param>
        public static void AddCreatedAtParameter(DatabaseHandler dataHandler, DateTime valCreatedAt)
        {
            var param = new ClickHouseDbParameter();
            param.DbType = DbType.DateTime;
            param.Direction = ParameterDirection.Input;
            param.Size = 8;
            param.Value = valCreatedAt;
            param.IsNullable = true;
            param.ParameterName = "CREATEDAT";
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valUpdatedAt">The valUpdatedAt .</param>
        public static void AddUpdatedAtParameter(DatabaseHandler dataHandler, DateTime valUpdatedAt)
        {

            var param = new ClickHouseDbParameter();
            param.DbType = DbType.DateTime;
            param.Direction = ParameterDirection.Input;
            param.Size = 8;
            param.Value = valUpdatedAt;
            param.ParameterName = "UPDATEDAT";
            dataHandler.Command.Parameters.Add(param);

        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        protected override void AppendSqlParameters(DatabaseHandler databaseHandler, DatabaseObject databaseObject,
            DataMode mode)
        {
            var entity = databaseObject as Playbook;
            AddIdParameter(databaseHandler, entity.Id);
            AddNameParameter(databaseHandler, entity.Name);
            AddDescriptionParameter(databaseHandler, entity.Description);
            AddObjectTypeIdParameter(databaseHandler, entity.ObjectTypeId);
            AddEnableParameter(databaseHandler, entity.Enable);
            AddLastRunParameter(databaseHandler, entity.LastRun);
            AddIsDeletedParameter(databaseHandler, entity.IsDeleted);
            AddCreatedAtParameter(databaseHandler, entity.CreatedAt);
            AddUpdatedAtParameter(databaseHandler, entity.UpdatedAt);
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
        protected override Playbook CreateObject(DbDataReader reader, bool populateObject)
        {
            var entity = new Playbook();
            entity.Id = reader.GetFieldValue<string>(reader.GetOrdinal("Id"));
            entity.Name = reader.GetFieldValue<string>(reader.GetOrdinal("Name"));
            entity.Description = reader.GetFieldValue<string>(reader.GetOrdinal("Description"));
            entity.ObjectTypeId = reader.GetFieldValue<int>(reader.GetOrdinal("ObjectTypeId"));
            entity.Enable = reader.GetFieldValue<bool>(reader.GetOrdinal("Enable"));
            entity.LastRun = reader.GetFieldValue<DateTime>(reader.GetOrdinal("LastRun"));
            entity.IsDeleted = reader.GetFieldValue<bool>(reader.GetOrdinal("IsDeleted"));
            entity.CreatedAt = reader.GetFieldValue<DateTime>(reader.GetOrdinal("CreatedAt"));
            entity.UpdatedAt = reader.GetFieldValue<DateTime>(reader.GetOrdinal("UpdatedAt"));
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
        protected override async Task<Playbook> CreateObjectAsync(DbDataReader reader, bool populateObject)
        {
            var entity = new Playbook();
            entity.Id = (await reader.GetFieldValueAsync<Guid>(reader.GetOrdinal("Id"))).ToString();
            entity.Name = await reader.GetFieldValueAsync<string>(reader.GetOrdinal("Name"));
            entity.Description = await reader.GetFieldValueAsync<string>(reader.GetOrdinal("Description"));
            entity.ObjectTypeId = await reader.GetFieldValueAsync<int>(reader.GetOrdinal("ObjectTypeId"));
            entity.Enable = await reader.GetFieldValueAsync<bool>(reader.GetOrdinal("Enable"));
            entity.LastRun = await reader.GetFieldValueAsync<DateTime>(reader.GetOrdinal("LastRun"));
            entity.IsDeleted = await reader.GetFieldValueAsync<bool>(reader.GetOrdinal("IsDeleted"));
            entity.CreatedAt = await reader.GetFieldValueAsync<DateTime>(reader.GetOrdinal("CreatedAt"));
            entity.UpdatedAt = await reader.GetFieldValueAsync<DateTime>(reader.GetOrdinal("UpdatedAt"));
            entity.IsNew = false;
            return entity;
        }
    }
}

