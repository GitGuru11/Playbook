namespace Playbook.Common.Instrumentation
{
    using System.Collections.Concurrent;
    using System.Threading;

    /// <summary>
    /// Call context
    /// </summary>
    /// <typeparam name="T">The type of the data being retrieved.</typeparam>
    public static class CallContext<T>
    {
        /// <summary>
        /// Call context state
        /// </summary>
        private static readonly ConcurrentDictionary<string, AsyncLocal<T>> State =
            new ConcurrentDictionary<string, AsyncLocal<T>>();

        public static void SetData(string name, T data)
        {
            State.GetOrAdd(name, _ => new AsyncLocal<T>()).Value = data;
        }

        public static T? GetData(string name)
        {
            return State.TryGetValue(name, out var data) ? data.Value : default(T);
        }

        public static void FreeNamedDataSlot(string name)
        {
            State.TryRemove(name, out var data);
        }
    }
}