namespace Playbook.Common.Instrumentation
{
    /// <summary>
    /// Instrumenation level.
    /// </summary>
    public enum InstrumentationLevel
    {
        Verbose = 0,

        Debug = 1,

        Information = 2,

        Warning = 3,

        Error = 4,

        Fatal = 5,

        Exception = 6,

        Important = 7,

    }

}

public static class InstrumentationLevelType
{
    /// <summary>
    /// Primary.
    /// </summary>
    public const string Regx = "VERBOSE,DEBUG,INFORMATION,WARNING,ERROR,FATAL,EXCEPTION,IMPORTANT,APISUCCESS,APIFAILURE";

    /// <summary>
    /// Primary.
    /// </summary>
    public const string Verbose = "VERBOSE";

    /// <summary>
    /// Dependent.
    /// </summary>
    public const string Debug = "DEBUG";

    /// <summary>
    /// Dependent.
    /// </summary>
    public const string Information = "INFORMATION";

    /// <summary>
    /// Dependent.
    /// </summary>
    public const string Warning = "WARNING";

    /// <summary>
    /// Dependent.
    /// </summary>
    public const string Error = "ERROR";
    /// <summary>
    /// Dependent.
    /// </summary>
    public const string Fatal = "FATAL";
    /// <summary>
    /// Dependent.
    /// </summary>
    public const string Exception = "EXCEPTION";
    /// <summary>
    /// Dependent.
    /// </summary>
    public const string Important = "IMPORTANT";

}

