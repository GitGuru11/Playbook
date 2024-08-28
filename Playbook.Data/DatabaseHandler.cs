using System.Data;
using System.Data.Common;
using ClickHouse.Client.ADO;
using ClickHouse.Client.ADO.Parameters;
using Playbook.Common.Instrumentation;

namespace Playbook.Data.ClickHouse
{
    public class DatabaseHandler : IDisposable
    {

        /// <summary>
        /// Gets and sets the connection string.
        /// </summary>
        public static string ConnectionString { get; set; }

        /// <summary>
        /// The data reader.
        /// </summary>
        private IDataReader dataReader;

        /// <summary>
        /// The data set.
        /// </summary>
        private DataSet dataSet;

        /// <summary>
        /// The data table.
        /// </summary>
        private DataTable dataTable;

        public ClickHouseCommand Command { get; set; }
        public DbDataReader DataReader { get; set; }
        public ClickHouseConnection Connection { get; set; }


        /// <summary>
        /// Gets DataSet.
        /// </summary>
        public DataSet DataSet
        {
            get
            {
                return this.dataSet;
            }
        }

        /// <summary>
        /// Gets DataTable.
        /// </summary>
        public DataTable DataTable
        {
            get
            {
                return this.dataTable;
            }
        }


        public DatabaseHandler()
        {
            this.CreateCommand();
        }


        public DatabaseHandler(string storedProcedure)
            : this()
        {
            this.Command.CommandText = storedProcedure;
            this.Command.CommandType = CommandType.StoredProcedure;
        }

        private void CreateCommand()
        {
            this.Command?.Dispose();
            this.Command = new ClickHouseCommand();
            this.Command.Parameters.Clear();
        }

        private void CreateConnection()
        {

            string connectionString = ConnectionString;

            if (this.Command == null)
            {
                throw new NullReferenceException("Command parameter is null");
            }

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            this.Command.Connection?.Dispose();
            this.Command.Connection = new ClickHouseConnection(connectionString);
        }

        private async Task OpenDatabaseConnectionAsync()
        {
            try
            {
                this.CreateConnection();
                await this.Command.Connection.OpenAsync();
                InstrumentationContext.Current.Debug("Data.ClickHouse.Databasehandler.OpenDatabaseConnectionAsync", string.Format("Database [{0}] successfully opened.", this.Command.Connection.Database));
            }
            catch (Exception exception)
            {
                InstrumentationContext.Current.Exception("Data.ClickHouse.Databasehandler.OpenDatabaseConnectionAsync",
                                                        InstrumentationLevel.Fatal,
                                                        exception,
                                                        "Error occured while opening database connection");
                await this.CloseDatabaseConnectionAsync();
                //throw new DataException("Error occured while opening database connection", exception);
            }
        }

        private void OpenDatabaseConnection()
        {
            try
            {
                this.CreateConnection();
                this.Command.Connection.Open();
                InstrumentationContext.Current.Debug("Data.ClickHouse.Databasehandler.OpenDatabaseConnection", string.Format("Database [{0}] successfully opened.", this.Command.Connection.Database));
            }
            catch (Exception exception)
            {
                InstrumentationContext.Current.Exception("Data.ClickHouse.Databasehandler.OpenDatabaseConnectionAsync",
                                                        InstrumentationLevel.Fatal,
                                                        exception,
                                                        "Error occured while opening database connection");
                this.CloseDatabaseConnection();
                // throw new DataException("Error opening database connection", exception);
            }
        }



        public void AddParameter(ClickHouseDbParameter param)
        {
            this.Command.Parameters.Add(param);
        }

        public void AddParameter(string parameterName, DbType dbType, ParameterDirection direction, int size, object value)
        {
            var param = new ClickHouseDbParameter();
            param.DbType = dbType;
            param.Direction = direction;
            param.Size = size;
            param.Value = value;
            this.AddParameter(param);
        }

        public void AddInParameter(string parameterName, DbType dbType, int size, object value)
        {
            this.AddParameter(parameterName, dbType, ParameterDirection.Input, size, value);
        }

        public void AddOutParameter(string parameterName, DbType databaseType, int size, object value)
        {
            this.AddParameter(parameterName, databaseType, ParameterDirection.Output, size, value);
        }

        public async Task ExecuteReaderAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                await this.OpenDatabaseConnectionAsync();
                //LogDatabaseAction();
                this.DataReader = await this.Command.ExecuteReaderAsync(CommandBehavior.CloseConnection, cancellationToken);
            }
            catch (Exception exception)
            {
                InstrumentationContext.Current.Exception("Data.ClickHouse.Databasehandler.ExecuteReaderAsync",
                                                        InstrumentationLevel.Fatal,
                                                        exception,
                                                        "Error occured in ExecuteReaderAsync");
                await this.CloseDatabaseConnectionAsync();
                throw new DataException("Error occured in ExecuteReaderAsync.", exception);
            }
        }

        public void ExecuteReader()
        {
            try
            {
                this.OpenDatabaseConnection();
                this.DataReader = this.Command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception exception)
            {
                InstrumentationContext.Current.Exception("Data.ClickHouse.Databasehandler.ExecuteReader",
                                                        InstrumentationLevel.Fatal,
                                                        exception,
                                                        "Error occured in ExecuteReaderAsync");
                this.CloseDatabaseConnection();
                throw new DataException("Error occured in ExecuteReader.", exception);
            }

        }

        public async Task<object> ExecuteScalarAsync(CancellationToken cancellationToken)
        {
            try
            {
                await this.OpenDatabaseConnectionAsync();
                var obj = await this.Command.ExecuteScalarAsync(cancellationToken);
                await this.CloseDatabaseConnectionAsync();
                return obj;
            }
            catch (Exception exception)
            {

                InstrumentationContext.Current.Exception("Data.ClickHouse.Databasehandler.ExecuteScalarAsync",
                                                        InstrumentationLevel.Fatal,
                                                        exception,
                                                        "Error occured in ExecuteScalarAsync");
                this.CloseDatabaseConnection();
                throw new DataException("Error occured in ExecuteScalarAsync.", exception);
            }
        }

        public object ExecuteScalar()
        {
            try
            {
                this.OpenDatabaseConnection();
                var obj = this.Command.ExecuteScalar();
                this.CloseDatabaseConnection();
                return obj;
            }
            catch (Exception exception)
            {
                InstrumentationContext.Current.Exception("Data.ClickHouse.Databasehandler.ExecuteScalarAsync",
                                                        InstrumentationLevel.Fatal,
                                                        exception,
                                                        "Error occured in ExecuteScalar");
                this.CloseDatabaseConnection();
                throw new DataException("Error occured in ExecuteScalar.", exception);
            }
        }

        public async Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken)
        {
            try
            {
                await this.OpenDatabaseConnectionAsync();
                var obj = await this.Command.ExecuteNonQueryAsync(cancellationToken);
                await this.CloseDatabaseConnectionAsync();
                return obj;
            }
            catch (Exception exception)
            {
                InstrumentationContext.Current.Exception("Data.ClickHouse.Databasehandler.ExecuteNonQueryAsync",
                                                       InstrumentationLevel.Fatal,
                                                       exception,
                                                       "Error occured in ExecuteNonQueryAsync");
                this.CloseDatabaseConnection();
                throw new DataException("Error occured in ExecuteNonQueryAsync.", exception);
            }
        }

        public int ExecuteNonQuery()
        {
            try
            {
                this.OpenDatabaseConnection();
                var obj = this.Command.ExecuteNonQuery();
                this.CloseDatabaseConnection();
                return obj;
            }
            catch (Exception exception)
            {
                InstrumentationContext.Current.Exception("Data.ClickHouse.Databasehandler.ExecuteNonQuery",
                                                       InstrumentationLevel.Fatal,
                                                       exception,
                                                      "Error occured in ExecuteNonQuery");
                this.CloseDatabaseConnection();
                throw new DataException("Error occured in ExecuteNonQuery.", exception);
            }
        }



        public async Task CloseDatabaseConnectionAsync()
        {
            try
            {
                if (this.Command?.Connection?.State == ConnectionState.Open)
                {
                    await this.Command.Connection.CloseAsync();
                }
                InstrumentationContext.Current.Debug("Data.ClickHouse.Databasehandler.CloseDatabaseConnectionAsync", "Database successfully closed.");
            }
            catch (Exception exception)
            {
                InstrumentationContext.Current.Exception("Data.ClickHouse.Databasehandler.CloseDatabaseConnectionAsync",
                                                      InstrumentationLevel.Fatal,
                                                      exception,
                                                      "Error occured while closing databse connection.");
                throw new DataException("Error occured while closing databse connection.", exception);
            }
            finally
            {
                if (this.DataReader != null)
                {
                    this.DataReader.Dispose();
                    this.DataReader = null;
                }

                if (this.Command != null && this.Command.Connection != null)
                {
                    this.Command.Connection.Dispose();
                    this.Command.Connection = null;
                    this.Command.Dispose();
                    this.Command = null;

                }
            }
        }

        public void CloseDatabaseConnection()
        {
            try
            {
                if (this.Command?.Connection?.State == ConnectionState.Open)
                {
                    this.Command.Connection.Close();
                }
                InstrumentationContext.Current.Debug("Data.ClickHouse.Databasehandler.CloseDatabaseConnection", "Database successfully closed.");
            }
            catch (Exception exception)
            {
                InstrumentationContext.Current.Exception("Data.ClickHouse.Databasehandler.CloseDatabaseConnection",
                                                       InstrumentationLevel.Fatal,
                                                       exception,
                                                       "Error occured while closing databse connection.");
                throw new DataException("Error occured while closing databse connection.", exception);
            }
            finally
            {
                if (this.DataReader != null)
                {
                    this.DataReader.Dispose();
                    this.DataReader = null;
                }

                if (this.Command != null && this.Command.Connection != null)
                {
                    this.Command.Connection.Dispose();
                    this.Command.Connection = null;
                    this.Command.Dispose();
                    this.Command = null;

                }
            }
        }


        private void LogDatabaseAction()
        {
            InstrumentationContext.Current.Debug("Data.ClickHouse.Databasehandler.LogDatabaseAction", "Attempting SQL [" + this.Command.CommandText + "]");
            var ct = this.Command.CommandText;

            foreach (ClickHouseDbParameter parameter in this.Command.Parameters)
            {
                ct = ct.Replace(parameter.ParameterName, parameter.Value == null ? "NULL" : "'" + parameter.Value + "'");
            }

            InstrumentationContext.Current.Debug("Data.ClickHouse.Databasehandler.LogDatabaseAction", "Actual SQL [" + ct + "]");

            foreach (ClickHouseDbParameter param in this.Command.Parameters)
            {
                InstrumentationContext.Current.Debug("Data.ClickHouse.Databasehandler.LogDatabaseAction",
                    "Parameter [name:" + param.ParameterName + ", value:"
                    + (param.Value == null ? "null" : param.Value.ToString()) + "]");
            }
        }


        public void Dispose()
        {
            this.CloseDatabaseConnection();
        }

        public async Task DisposeAsync()
        {
            await this.CloseDatabaseConnectionAsync();
        }
    }
}
