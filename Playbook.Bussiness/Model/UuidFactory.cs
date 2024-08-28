using Playbook.Data.ClickHouse;

namespace Playbook.Bussiness.Model
{
    public static class UuidFactory
    {
        public static string GetUuid()
        {
            using var dataHandler = new DatabaseHandler();
            dataHandler.Command.CommandText = "SELECT generateUUIDv4()";
            return dataHandler.ExecuteScalar().ToString()!;
        }

    }
}

