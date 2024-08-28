using MongoDB.Driver;

namespace Playbook.Data.Mongo;
public class MongoHandler
{

    /// <summary>
    /// Gets and sets the connection string.
    /// </summary>
    public static string ConnectionString { get; set; }

    /// <summary>
    /// Gets and sets the mongo connection.
    /// </summary>
    public MongoClient? Connection { get; set; }


    /// <summary>
    /// Gets and sets the tenant database.
    /// </summary>
    public IMongoDatabase Database { get; set; }


    /// <summary>
    /// Gets and sets the tenant name.
    /// </summary>
    public string Tenant { get; set; }

    /// <summary>
    /// MongoHandler
    /// </summary>
    /// <param name="database"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public MongoHandler(string database)
    {
        if (string.IsNullOrEmpty(database))
            throw new ArgumentNullException(nameof(database));
        try
        {
            string connectionString = ConnectionString;
            this.Tenant = database;
            if (this.Connection == null)
                this.Connection = new MongoClient(connectionString);

            this.Database = this.Connection.GetDatabase(database);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.ToString());
        }
    }

    /// <summary>
    /// MongoHandler
    /// </summary>
    /// <param name="database"></param>
    /// <param name="connectionString"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public MongoHandler(string database, string connectionString)
    {
        if (string.IsNullOrEmpty(database))
            throw new ArgumentNullException(nameof(database));
        if (string.IsNullOrEmpty(connectionString))
            throw new ArgumentNullException(nameof(connectionString));
        try
        {
            this.Tenant = database;
            if (this.Connection == null)
                this.Connection = new MongoClient(connectionString);
            this.Database = this.Connection.GetDatabase(database);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.ToString());
        }
    }
}

