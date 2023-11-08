namespace Defaults;

public struct Result<T>
{
    public T Value { get; set; } = default!;
    public Exception Exception { get; set; } = null!;

    private readonly bool _stateFailure;

    public Result(T value)
    {
        Value = value;
        _stateFailure = false;
    }

    public Result(Exception exception)
    {
        Exception = exception;
        _stateFailure = true;
    }

    public readonly bool EnsureSuccess() => !_stateFailure;

    public readonly void Handle(Action<T> onSuccess, Action<Exception> onException)
    {
        if (_stateFailure)
        {
            onException(Exception);
        }
        else
        {
            onSuccess(Value);
        }
    }

    public async readonly Task HandleAsync(Func<T, Task> onSuccess, Func<Exception, Task> onException)
    {
        if (_stateFailure)
        {
            await onException(Exception);
        }
        else
        {
            await onSuccess(Value);
        }
    }
}