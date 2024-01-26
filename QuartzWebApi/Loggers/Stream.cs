﻿using System;
using System.Text;
using Microsoft.Extensions.Logging;

namespace QuartzWebApi.Loggers;

/// <summary>
///     Writes log information to a stream
/// </summary>
public class Stream : ILogger, IDisposable
{
    #region Fields
    private System.IO.Stream _stream;
    private string _instanceId;
    #endregion

    #region Constructors
    /// <summary>
    ///     Logs information to the given <paramref name="stream" />
    /// </summary>
    /// <param name="stream"></param>
    public Stream(System.IO.Stream stream)
    {
        _stream = stream;
    }
    #endregion

    #region BeginScope
    public IDisposable BeginScope<TState>(TState state)
    {
        _instanceId = state?.ToString();
        return null;
    }
    #endregion

    #region IsEnabled
    /// <summary>
    ///     Will always return <c>true</c>
    /// </summary>
    /// <param name="logLevel"></param>
    /// <returns></returns>
    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }
    #endregion

    #region Log
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
        Func<TState, Exception, string> formatter)
    {
        var message = $"{formatter(state, exception)}";

        if (_stream == null || !_stream.CanWrite) return;
        var line =
            $"{DateTime.Now:yyyy-MM-ddTHH:mm:ss.fff}{(_instanceId != null ? $" - {_instanceId}" : string.Empty)} - {message}{Environment.NewLine}";
        var bytes = Encoding.UTF8.GetBytes(line);
        _stream.Write(bytes, 0, bytes.Length);
        _stream.Flush();
    }
    #endregion

    #region Dispose
    public void Dispose()
    {
        if (_stream == null)
            return;

        _stream.Dispose();
        _stream = null;
    }
    #endregion
}