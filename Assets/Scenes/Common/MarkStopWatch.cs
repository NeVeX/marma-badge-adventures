using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public static class MarkStopWatch 
{
    private static Stopwatch _stopwatch = new Stopwatch();
    
    public static void Restart()
    {
        _stopwatch.Restart();
    }

    public static void Stop()
    {
        _stopwatch.Stop();
    }

    public static long ElapsedMilliseconds()
    {
        return _stopwatch.ElapsedMilliseconds;
    }

    public static string Elapsed()
    {
        TimeSpan ts = _stopwatch.Elapsed;
        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds);
        return elapsedTime;
    }
}
