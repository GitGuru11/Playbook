using System.Data;
using Playbook.Business.Model;
using Playbook.Data.ClickHouse;
using System.Data.Common;
using ClickHouse.Client.ADO.Parameters;
using Playbook.Common.Tenant;

namespace Playbook.Bussiness.Model
{
    /// <summary>
    /// Factory base class for ObjectFieldFactory.
    /// </summary>
    public partial class ObjectFieldFactoryBase : DataObjectFactory<ObjectField>
    {
        //Fields
        public const string FIELD_NAME_ID = "Id";
        
        public const string FIELD_NAME_OBJECTTYPEID = "ObjectTypeId";

        public const string FIELD_NAME_NAME = "Name";

        public const string FIELD_NAME_LABEL = "Label";
        
        public const string FIELD_NAME_FIELD_TYPE = "FieldType";

        public const string FIELD_NAME_ISDELETED = "IsDeleted";

        public const string FIELD_NAME_CREATEDAT = "CreatedAt";

        public const string FIELD_NAME_UPDATEDAT = "UpdatedAt";

        // Parameters
        public const string PARAMETER_NAME_ID = "@ID";
        
        public const string PARAMETER_NAME_OBJECTTYPEID = "@OBJECTTYPEID";

        public const string PARAMETER_NAME_NAME = "@NAME";

        public const string PARAMETER_NAME_LABEL = "@LABEL";
        
        public const string PARAMETER_NAME_FIELD_TYPE = "@FIELDTYPE";

        public const string PARAMETER_NAME_ISDELETED = "@ISDELETED";

        public const string PARAMETER_NAME_CREATEDAT = "@CREATEDAT";

        public const string PARAMETER_NAME_UPDATEDAT = "@UPDATEDAT";

        /// <summary>
        /// Gets the table name.
        /// </summary>
        public override string TableName
        {
            get { return "ObjectField "; }
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
            $"SELECT Id, ObjectTypeId, Name, Label, FieldType, OptionId, IsDeleted, CreatedAt, UpdatedAt FROM {TenantContext.Current.TenantName}.ObjectField";

        /// <summary>
        /// Gets the insert statement.
        /// </summary>
        protected override string InsertStatement =>
            $"INSERT INTO {TenantContext.Current.TenantName}.ObjectField (Id, ObjectTypeId, Name, Label, FieldType, OptionId, IsDeleted, CreatedAt, UpdatedAt) VALUES (@ID, @OBJECTTYPEID, @NAME, @LABEL,  @FIELDTYPE, @OPTIONID, @ISDELETED, @CREATEDAT, @UPDATEDAT)";

        /// <summary>
        /// Gets the update statement.
        /// </summary>
        protected override string UpdateStatement => 
                    $"ALTER TABLE {TenantContext.Current.TenantName}.ObjectField UPDATE ObjectTypeId = @OBJECTTYPEID, Name = @NAME, Label = @LABEL, FieldType = @FIELDTYPE, OptionId = @OPTIONID, IsDeleted = @ISDELETED, CreatedAt = @CREATEDAT, UpdatedAt = @UPDATEDAT WHERE Id = @ID";

        /// <summary>
        /// Gets the delete statment.
        /// </summary>
        protected override string DeleteStatement => $"ALTER TABLE {TenantContext.Current.TenantName}.ObjectField FROM IsDeleted = true WHERE Id = @ID";

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
        /// <param name="valObjectTypeId">The valObjectTypeId .</param>
        public static void AddObjectTypeIdParameter(DatabaseHandler dataHandler, string valObjectTypeId)
        {
            var param = new ClickHouseDbParameter();
            param.DbType = DbType.String;
            param.Direction = ParameterDirection.Input;
            param.Size = 36;
            param.Value = valObjectTypeId;
            param.IsNullable = true;
            param.ParameterName = "OBJECTTYPEID";
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
        /// <param name="valLabel">The valDescription .</param>
        public static void AddLabelParameter(DatabaseHandler dataHandler, string valLabel)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Size = 1024,
                Value = valLabel,
                ParameterName = "LABEL"
            };
            dataHandler.Command.Parameters.Add(param);
        }
        
        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valFieldType">The valFieldType .</param>
        public static void AddFieldTypeParameter(DatabaseHandler dataHandler, string valFieldType)
        {
            var param = new ClickHouseDbParameter();
            param.DbType = DbType.String;
            param.Direction = ParameterDirection.Input;
            param.Size = 1024;
            param.Value = valFieldType;
            param.IsNullable = true;
            param.ParameterName = "FIELDTYPE";
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valOptionId">The valOptionId .</param>
        public static void AddOptionIdParameter(DatabaseHandler dataHandler, string valOptionId)
        {
            var param = new ClickHouseDbParameter();
            param.DbType = DbType.String;
            param.Direction = ParameterDirection.Input;
            param.Size = 36;
            param.Value = valOptionId;
            param.IsNullable = true;
            param.ParameterName = "OPTIONID";
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
            var entity = databaseObject as ObjectField;
            AddIdParameter(databaseHandler, entity.Id);
            AddObjectTypeIdParameter(databaseHandler, entity.ObjectTypeId);
            AddNameParameter(databaseHandler, entity.Name);
            AddLabelParameter(databaseHandler, entity.Label);
            AddFieldTypeParameter(databaseHandler, entity.FieldType);
            AddOptionIdParameter(databaseHandler, entity.OptionId);
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
        protected override ObjectField CreateObject(DbDataReader reader, bool populateObject)
        {
            var entity = new ObjectField();
            entity.Id = reader.GetFieldValue<string>(reader.GetOrdinal("Id"));
            entity.ObjectTypeId = reader.GetFieldValue<string>(reader.GetOrdinal("ObjectTypeId"));
            entity.Name = reader.GetFieldValue<string>(reader.GetOrdinal("Name"));
            entity.Label = reader.GetFieldValue<string>(reader.GetOrdinal("Label"));
            entity.FieldType = reader.GetFieldValue<string>(reader.GetOrdinal("FieldType"));
            entity.OptionId = reader.GetFieldValue<string>(reader.GetOrdinal("OptionId"));
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
        protected override async Task<ObjectField> CreateObjectAsync(DbDataReader reader, bool populateObject)
        {
            var entity = new ObjectField();
            entity.Id = (await reader.GetFieldValueAsync<Guid>(reader.GetOrdinal("Id"))).ToString();
            entity.ObjectTypeId = (await reader.GetFieldValueAsync<Guid>(reader.GetOrdinal("ObjectTypeId"))).ToString();
            entity.Name = await reader.GetFieldValueAsync<string>(reader.GetOrdinal("Name"));
            entity.Label = await reader.GetFieldValueAsync<string>(reader.GetOrdinal("Label"));
            entity.FieldType = (await reader.GetFieldValueAsync<string>(reader.GetOrdinal("FieldType"))).ToString();
            entity.OptionId = (await reader.GetFieldValueAsync<Guid>(reader.GetOrdinal("OptionId"))).ToString();
            entity.IsDeleted = await reader.GetFieldValueAsync<bool>(reader.GetOrdinal("IsDeleted"));
            entity.CreatedAt = await reader.GetFieldValueAsync<DateTime>(reader.GetOrdinal("CreatedAt"));
            entity.UpdatedAt = await reader.GetFieldValueAsync<DateTime>(reader.GetOrdinal("UpdatedAt"));
            entity.IsNew = false;
            return entity;
        }
    }
}

