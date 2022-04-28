﻿using System.Diagnostics;

namespace ImGuiSharp.Generator.Helpers;

public class Timer : IDisposable
{
    private readonly string    _message;
    private readonly Stopwatch _stopwatch;

    public Timer(string message = "")
    {
        _message   = message;
        _stopwatch = new Stopwatch();
        _stopwatch.Start();
    }

    public void Dispose()
    {
        _stopwatch.Stop();

        Console.WriteLine($"|{_message}| Took: {_stopwatch.ElapsedMilliseconds}ms");
    }
}