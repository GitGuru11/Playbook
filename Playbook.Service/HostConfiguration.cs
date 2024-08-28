namespace Playbook.Service
{
    /// <summary>
    /// HostConfiguration helper functions.
    /// </summary>
    public class HostConfiguration
    {
        public static string? Operator { get; set; }
        public static string? ServiceName { get; set; }
        public static string? ServiceVersion { get; set; }
        public static string? Environment { get; set; }
        public static string? SSLCertificateThumbprint { get; set; }
        public static string? SSLCertificatePath { get; set; }

        public static string MachineName
        {
            get
            {
                return System.Environment.MachineName;
            }
        }

        public static string ApplicationId
        {
            get
            {
                return string.Format("{0}-{1}-{2}-{3}-{4}", MachineName, Operator, ServiceName, Environment, ServiceVersion);
            }
        }
    }
}
