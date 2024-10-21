# Error handling
## General
Good error handling is necessary to ensure that your application can handle unexpected (and expected) situations. If we don't do this, there is a good chance that the application will crash, resulting in a bad user experience for our customers.

<p>&nbsp;</p>

:hand: <span style="color:black"> **Do log exceptions with the LogLevel.Error level** </span>
<div style="background-color:steelblue;padding:20px;color:white">
Why? Normally in a production environment we only record the log levels **warning** and **error**. Other levels are filtered out and will therefore never show the true reason for a crash.
We log these exceptions with the intention of finding out the reason for a software error as quickly as possible.
</div>

<p>&nbsp;</p>

``` csharp
public sealed partial class Example
{
    private readonly ILogger<Example> _logger;

    public Example(ILogger<Example> logger)
    {
        _logger = logger;
    }

    private static void Method()
    {
        try
        {
            // call a method that may throw an exception.
        }
        catch (Exception ex)
        {
            LogException(ex.Message);
            throw;
        }
    }

    [LoggerMessage(1, LogLevel.Error, "Operation ? failed: {exceptionMessage}")]
    partial void LogException(ILogger logger, string exceptionMessage);
}
```


<p>&nbsp;</p>

:hand: <span style="color:black"> **Do not throw the same exception** </span>
<div style="background-color:steelblue;padding:20px;color:white">
Why? This will remove the current stack trace.
</div>

<p>&nbsp;</p>

``` csharp
// This violates the rule !
public sealed partial class Example
{
    private static void Method()
    {
        try
        {
            // call a method that may throw an exception.
        }
        catch (Exception ex)
        {
            // throw ex resets the stack trace.
            throw ex;
        }
    }
}
```

``` csharp
public sealed partial class Example
{
    private static void Method()
    {
        try
        {
            // call a method that may throw an exception.
        }
        catch (Exception ex)
        {
            // this keeps the stack trace intact.
            throw;
        }
    }
}
 ```

