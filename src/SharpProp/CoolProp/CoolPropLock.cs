namespace SharpProp;

[ExcludeFromCodeCoverage]
internal static class CoolPropLock
{
    private static bool _callbacksRegistered;

    private static object SyncRoot { get; } = new();

    internal static void Invoke(Action action)
    {
        lock (SyncRoot)
        {
            EnsureCallbacksRegistered();
            action();
            SwigExceptions.ThrowPendingException();
        }
    }

    internal static TResult Invoke<TResult>(Func<TResult> action)
    {
        lock (SyncRoot)
        {
            EnsureCallbacksRegistered();
            var result = action();
            SwigExceptions.ThrowPendingException();
            return result;
        }
    }

    private static void EnsureCallbacksRegistered()
    {
        if (_callbacksRegistered)
        {
            return;
        }

        SwigStrings.RegisterStringCallback();
        SwigExceptions.RegisterExceptionCallbacks();
        _callbacksRegistered = true;
    }
}
