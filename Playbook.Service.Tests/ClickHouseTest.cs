using System.Diagnostics;
using ClickHouse.Client.ADO;
using Playbook.Bussiness.Model;

namespace Playbook.Service.Tests;

[TestClass]
public class ClickHouseTest
{
    [TestMethod]
    public void BasicConnectivityTest()
    {

        try
        {
            var connection_string = "Host=wav26k5jhf.us-east-1.aws.clickhouse.cloud;Protocol=https;Port=8443;Username=tony;Password=EbtKn$&0z8#@5vlCcSE";
            var tenant = "camunda_ck_preprod";
            using (var connection = new ClickHouseConnection(connection_string))
            using (var clickHouseCommand = new ClickHouseCommand(connection))
            {
                clickHouseCommand.CommandText = string.Format("SELECT COUNT(*) FROM {0}.deals_history", tenant);
                connection.Open();
                var count = clickHouseCommand.ExecuteScalar();
                connection.Close();
                Trace.WriteLine("Count:" + count.ToString());
            }
        }
        catch (Exception ex)
        {

            Trace.WriteLine("Exception:" + ex.ToString());
            Assert.Fail();
        }
    }

    [TestMethod]
    public void GetUuidTest()
    {
        var uuid = UuidFactory.GetUuid();
        Assert.IsNotNull(uuid);

    }

}
