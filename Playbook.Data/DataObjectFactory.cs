namespace Playbook.Data.ClickHouse
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Instrumentation;
    using System.Data.Common;

    /// <summary>
    /// The data object factory is the base class for accessing data objects in the database.
    /// It depends heavily on the <see cref="DatabaseHandler"/> class and is referenced in the automated code creation process.
    /// </summary>
    /// <typeparam name="T">
    /// The type of Data Object Factory.
    /// </typeparam>
    public abstract class DataObjectFactory<T> where T : IDisposable
    {

        #region Fields        

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Finalizes an instance of the <see cref="DataObjectFactory{T}"/> class. 
        /// </summary>
        ~DataObjectFactory()
        {
            this.Dispose();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets TableName.
        /// </summary>
        public abstract string TableName { get; }

        /// <summary>
        /// Gets TableName.
        /// </summary>
        public abstract string PrimaryKeyName { get; }


        /// <summary>
        /// Gets SelectAllStatement.
        /// </summary>
        protected abstract string SelectAllStatement { get; }

        /// <summary>
        /// Gets InsertStatement.
        /// </summary>
        protected abstract string InsertStatement { get; }

        /// <summary>
        /// Gets UpdateStatement.
        /// </summary>
        protected abstract string UpdateStatement { get; }

        /// <summary>
        /// Gets DeleteStatement.s
        /// </summary>
        protected abstract string DeleteStatement { get; }

        #endregion

        #region Properties        

        /// <summary>
        /// Gets or sets DatabaseHandler.
        /// </summary>
        protected DatabaseHandler DatabaseHandler { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Has AutoIncrement PK.
        /// </summary>
        protected virtual bool HasAutoIncrementPk { get; set; }
        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The create data handler.
        /// </summary>
        /// <returns>
        /// A Data Handler
        /// </returns>
        public virtual DatabaseHandler CreateDatabaseHandler()
        {
            this.DatabaseHandler = new DatabaseHandler();
            return this.DatabaseHandler;
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Search for the records in the database based on a data handler and return as an array list.
        /// </summary>
        /// <param name="currentDatabaseHandler">
        /// The data handler.
        /// </param>
        /// <returns>
        /// An ArrayList of the objects from the table.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws an exception if the find was unable to get the data from the database.
        /// </exception>
        public List<T> Find(DatabaseHandler dbHandler)
        {
            try
            {
                dbHandler.ExecuteReader();
                return this.BuildList(dbHandler, false);
            }
            catch (Exception e)
            {
                throw new Exception("Error occured in Find()", e);
            }
            finally
            {
                dbHandler.Dispose();
            }
        }

        /// <summary>
        /// Search for the records in the database based on a data handler and return as an array list.
        /// </summary>
        /// <param name="currentDatabaseHandler">
        /// The data handler.
        /// </param>
        /// <returns>
        /// An ArrayList of the objects from the table.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws an exception if the find was unable to get the data from the database.
        /// </exception>
        public async Task<List<T>> FindAsync(DatabaseHandler dbHandler)
        {
            try
            {
                await dbHandler.ExecuteReaderAsync();
                return await this.BuildListAsync(dbHandler, false);
            }
            catch (Exception e)
            {
                throw new Exception("Error occured in FindAsync()", e);
            }
            finally
            {
                await dbHandler.DisposeAsync();
            }
        }

        /// <summary>
        /// Search the database based on the loaded data handler for a paged selection of records and return as an array list of data objects.
        /// </summary>
        /// <param name="colList">
        /// The col list.
        /// </param>
        /// <param name="pageNumber">
        /// The page number.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <returns>
        /// An ArrayList of the objects from the table.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws an exception if the find was unable to get the data from the database.
        /// </exception>
        public virtual List<T> Find(List<SortDefinition> colList, int pageNumber, int pageSize)
        {
            if (this.DatabaseHandler == null)
            {
                throw new NullReferenceException(nameof(DatabaseHandler));
            }
            try
            {
                // Add the sort order
                if (!this.DatabaseHandler.Command.CommandText.Contains("ORDER BY"))
                {
                    this.DatabaseHandler.Command.CommandText += Sorter.Columns(colList) == string.Empty
                                                        ? string.Empty
                                                        : " " + Sorter.Columns(colList);
                }
                var startRecord = (pageNumber - 1) * pageSize;
                this.DatabaseHandler.Command.CommandText += string.Format(" OFFSET {0} LIMIT {1}", startRecord, pageSize);
                return this.Find(this.DatabaseHandler);

            }
            catch (Exception e)
            {
                throw new Exception("Find with paging failed", e);
            }
            finally
            {
                this.DatabaseHandler.CloseDatabaseConnection();
            }
        }

        /// <summary>
        /// Search the database based on the loaded data handler for a paged selection of records and return as an array list of data objects.
        /// </summary>
        /// <param name="colList">
        /// The col list.
        /// </param>
        /// <param name="pageNumber">
        /// The page number.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <returns>
        /// An ArrayList of the objects from the table.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws an exception if the find was unable to get the data from the database.
        /// </exception>
        public virtual async Task<List<T>> FindAsync(List<SortDefinition> colList, int pageNumber, int pageSize)
        {
            if (this.DatabaseHandler == null)
            {
                throw new NullReferenceException(nameof(DatabaseHandler));
            }
            //TODO:SACHIN
            ////this.DatabaseHandler.QueryHelper.AddOrderByFields(colList);
            ////this.DatabaseHandler.QueryHelper.Statement += this.dataHandler.QueryHelper.BuildPageStatement(pageNumber, pageSize);
            ////this.DatabaseHandler.Command.CommandText = this.DatabaseHandler.QueryHelper.Statement;
            try
            {
                // Add the sort order
                if (!this.DatabaseHandler.Command.CommandText.Contains("ORDER BY"))
                {
                    this.DatabaseHandler.Command.CommandText += Sorter.Columns(colList) == string.Empty
                                                        ? string.Empty
                                                        : " " + Sorter.Columns(colList);
                }
                var startRecord = (pageNumber - 1) * pageSize;
                this.DatabaseHandler.Command.CommandText += string.Format(" OFFSET {0} LIMIT {1}", startRecord, pageSize);
                return await this.FindAsync(this.DatabaseHandler);
            }
            catch (Exception e)
            {
                throw new Exception("FindAsync with paging failed", e);
            }
            finally
            {
                await this.DatabaseHandler.CloseDatabaseConnectionAsync();
            }
        }


        /// <summary>
        /// Find all objects on the database.
        /// </summary>
        /// <returns>
        /// An array of data objects
        /// </returns>
        public virtual async Task<List<T>> FindAllObjectsAsync()
        {
            return await this.FindAllObjectsAsync(string.Empty);
        }

        /// <summary>
        /// The find all objects in the database using a specific ORDER BY SQL clause and return as an array list of data objects.
        /// </summary>
        /// <param name="orderByClause">
        /// The order by clause.
        /// </param>
        /// <returns>
        /// An array of data objects.
        /// </returns>
        public virtual async Task<List<T>> FindAllObjectsAsync(string orderByClause)
        {
            return await this.FindAllObjectsAsync(orderByClause, 0);
        }

        /// <summary>
        /// The find the number of records  using a specific ORDER BY SQL clause and return as an array list of data objects.
        /// </summary>
        /// <param name="orderByClause">
        /// The order by clause.
        /// </param>
        /// <param name="resultLimit">
        /// The result limit.
        /// </param>
        /// <returns>
        /// An array of data objects
        /// </returns>
        /// <exception cref="Exception">
        /// An error if the method fails to access the database correctly.
        /// </exception>
        public virtual async Task<List<T>> FindAllObjectsAsync(string orderByClause, int resultLimit)
        {
            try
            {
                if (this.DatabaseHandler == null)
                {
                    this.CreateDatabaseHandler();
                }

                var colList = new List<SortDefinition>();
                if (!string.IsNullOrEmpty(orderByClause))
                {
                    colList.Add(new SortDefinition(orderByClause, SortOrder.Ascending));
                    this.DatabaseHandler.Command.CommandText = this.SelectAllStatement + " " + Sorter.Columns(colList);
                }
                else
                {
                    this.DatabaseHandler.Command.CommandText = this.SelectAllStatement;
                }

                if (resultLimit > 0)
                {
                    //TODO:SACHIN
                    //this.Command.ExecuteAsDataSet(0, resultLimit, this.TableName);
                    //return this.Command(this.dataHandler, false);
                    return new List<T>();
                }
                else
                {
                    return await this.FindAsync(this.DatabaseHandler);
                }
            }
            catch (Exception e)
            {
                throw new Exception("FindAllObjectsAsync Failed", e);
            }
            finally
            {
                if (this.DatabaseHandler != null)
                {
                    this.DatabaseHandler.Dispose();
                    this.DatabaseHandler = null;
                }
            }
        }

        /// <summary>
        /// Save the provided data object in the database automatically INSERTing or UPDATing based on the data objects state (New or Modified).
        /// </summary>
        /// <param name="dataObject">
        /// The data object.
        /// </param>
        /// <returns>
        /// A value indicating whether the data object was successfully saved.
        /// </returns>
        public virtual bool Save(DatabaseObject dataObject)
        {
            this.CreateDatabaseHandler();
            try
            {
                DataMode mode = DataMode.Insert;
                if (dataObject.IsNew)
                {
                    this.DatabaseHandler.Command.CommandText = this.InsertStatement;
                    this.AppendSqlParameters(this.DatabaseHandler, dataObject, mode);
                    var obj = this.DatabaseHandler.ExecuteScalar();
                    dataObject.PrimaryKey = obj;
                }
                else
                {
                    mode = DataMode.Update;
                    this.DatabaseHandler.Command.CommandText = this.UpdateStatement;
                    this.AppendSqlParameters(this.DatabaseHandler, dataObject, mode);
                    this.DatabaseHandler.ExecuteScalar();
                }
                dataObject.IsModified = false;
                dataObject.IsNew = false;
                return true;
            }

            catch (Exception e)
            {
                InstrumentationContext.Current.Exception("Data.ClickHouse.DataObjectFactory.Save", e, "Problem with ExecuteScalar");
                return false;
            }
            finally
            {
                this.DatabaseHandler.Dispose();
                this.DatabaseHandler = null;
            }
        }


        /// <summary>
        /// Save the provided data object in the database automatically INSERTing or UPDATing based on the data objects state (New or Modified).
        /// </summary>
        /// <param name="dataObject">
        /// The data object.
        /// </param>
        /// <returns>
        /// A value indicating whether the data object was successfully saved.
        /// </returns>
        public virtual async Task<bool> SaveAsync(DatabaseObject dataObject, CancellationToken cancellationToken = default(CancellationToken))
        {
            this.CreateDatabaseHandler();
            try
            {
                DataMode mode = DataMode.Insert;
                if (dataObject.IsNew)
                {


                    // ClickHouse
                    this.DatabaseHandler.Command.CommandText = this.InsertStatement;

                    this.AppendSqlParameters(this.DatabaseHandler, dataObject, mode);
                    await this.DatabaseHandler.ExecuteScalarAsync(cancellationToken);
                    dataObject.PrimaryKey = dataObject.PrimaryKey;
                }
                else
                {
                    mode = DataMode.Update;
                    this.DatabaseHandler.Command.CommandText = this.UpdateStatement;
                    this.AppendSqlParameters(this.DatabaseHandler, dataObject, mode);
                    await this.DatabaseHandler.ExecuteScalarAsync(cancellationToken);
                }
                dataObject.IsModified = false;
                dataObject.IsNew = false;
                return true;
            }

            catch (Exception e)
            {
                InstrumentationContext.Current.Exception("Data.ClickHouse.DataObjectFactory.Save", e, "Problem with ExecuteScalar");
                return false;
            }
            finally
            {
                this.DatabaseHandler.Dispose();
                this.DatabaseHandler = null;
            }
        }

        /// <summary>
        /// Deletes the data object from the database.
        /// </summary>
        /// <param name="dataObject">
        /// The data object.
        /// </param>
        /// <returns>
        /// A value indicating whether the delete was successful or not
        /// </returns>
        public virtual bool Delete(DatabaseObject dataObject)
        {
            if (this.DatabaseHandler == null)
            {
                this.CreateDatabaseHandler();
            }

            this.DatabaseHandler.Command.CommandText = this.DeleteStatement;
            this.DatabaseHandler.Command.CommandType = CommandType.Text;
            this.DatabaseHandler.Command.Parameters.Clear();
            this.AppendSqlParameters(this.DatabaseHandler, dataObject, DataMode.Delete);
            try
            {
                this.DatabaseHandler.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                this.DatabaseHandler.Dispose();
                this.DatabaseHandler = null;
            }
        }

        /// <summary>
        /// Deletes the data object from the database.
        /// </summary>
        /// <param name="dataObject">
        /// The data object.
        /// </param>
        /// <returns>
        /// A value indicating whether the delete was successful or not
        /// </returns>
        public async virtual Task<bool> DeleteAsync(DatabaseObject dataObject)
        {
            if (this.DatabaseHandler == null)
            {
                this.CreateDatabaseHandler();
            }

            this.DatabaseHandler.Command.CommandText = this.DeleteStatement;
            this.DatabaseHandler.Command.CommandType = CommandType.Text;
            this.DatabaseHandler.Command.Parameters.Clear();
            this.AppendSqlParameters(this.DatabaseHandler, dataObject, DataMode.Delete);
            try
            {
                await this.DatabaseHandler.ExecuteNonQueryAsync(default(CancellationToken));
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                await this.DatabaseHandler.DisposeAsync();
                this.DatabaseHandler = null;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The append SQL parameters to the data handler provided
        /// </summary>
        /// <param name="databaseHandler">
        /// The data handler.
        /// </param>
        /// <param name="databaseObject">
        /// The data object.
        /// </param>
        /// <param name="mode">
        /// The mode.
        /// </param>
        protected abstract void AppendSqlParameters(DatabaseHandler databaseHandler, DatabaseObject databaseObject, DataMode mode);

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
        protected abstract T CreateObject(DbDataReader reader, bool populateObject);

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
        protected abstract Task<T> CreateObjectAsync(DbDataReader reader, bool populateObject);

        /// <summary>
        /// The build array.
        /// </summary>
        /// <param name="currentDatabaseHandler">
        /// The data handler.
        /// </param>
        /// <param name="closeConnection">
        /// The close connection.
        /// </param>
        /// <returns>
        /// An array of data objects from the database.
        /// </returns>
        /// <exception cref="Exception">
        /// An error if the method was unable to read from the database successfully.
        /// </exception>
        private List<T> BuildList(DatabaseHandler dbHandler, bool closeConnection)
        {
            List<T> list = new List<T>();
            try
            {
                while (dbHandler.DataReader.Read())
                {
                    list.Add(this.CreateObject(dbHandler.DataReader, true));
                }
                InstrumentationContext.Current.Debug("Data.ClickHouse.DataObjectFactory.BuildList", string.Format("DatabaseHandler collected {0} record(s)", list.Count));

                dbHandler.DataReader.Close();
            }
            catch (Exception e)
            {
                InstrumentationContext.Current.Exception("Data.ClickHouse.DataObjectFactory.BuildList", e, "Error occrued in DatabaseObjectFactory building the list");
                throw;
            }
            finally
            {
                dbHandler.Dispose();
            }
            return list;
        }


        /// <summary>
        /// The build array.
        /// </summary>
        /// <param name="currentDatabaseHandler">
        /// The data handler.
        /// </param>
        /// <param name="closeConnection">
        /// The close connection.
        /// </param>
        /// <returns>
        /// An array of data objects from the database.
        /// </returns>
        /// <exception cref="Exception">
        /// An error if the method was unable to read from the database successfully.
        /// </exception>
        public async Task<List<T>> BuildListAsync(DatabaseHandler dbHandler, bool closeConnection)
        {
            List<T> list = new List<T>();
            try
            {
                while (await dbHandler.DataReader.ReadAsync())
                {
                    list.Add(await this.CreateObjectAsync(dbHandler.DataReader, true));
                }
                InstrumentationContext.Current.Debug("Data.ClickHouse.DataObjectFactory.BuildList", string.Format("DatabaseHandler collected {0} record(s)", list.Count));

                await dbHandler.DataReader.CloseAsync();
            }
            catch (Exception e)
            {
                InstrumentationContext.Current.Exception("Data.ClickHouse.DataObjectFactory.BuildList", e, "Error occrued in DatabaseObjectFactory building the list");
                throw;
            }
            finally
            {
                await dbHandler.DisposeAsync();
            }
            return list;
        }

        #endregion
    }
}