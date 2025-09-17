using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Timers;

/// <summary>
/// Watches for foreground (active) window changes. Uses SetWinEventHook
/// and falls back to polling if events aren't coming through.
/// Raises OnActiveWindowChanged(processName, pid, hwnd).
/// </summary>
public class ActiveWindowWatcher : IDisposable
{
    public event Action<string, int, IntPtr>? OnActiveWindowChanged;

    // WinEvent callback signature
    private delegate void WinEventDelegate(
        IntPtr hWinEventHook, uint eventType, IntPtr hwnd,
        int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

    // Keep a reference so the delegate isn't GC'ed
    private readonly WinEventDelegate _procDelegate;
    private IntPtr _hook = IntPtr.Zero;

    // Polling fallback
    private readonly System.Timers.Timer _pollTimer;
    private readonly int _pollIntervalMs;

    // bookkeeping to avoid duplicate notifications
    private IntPtr _lastHwnd = IntPtr.Zero;
    private uint _lastPid = 0;
    private string? _lastProcessName;
    private bool _receivedEvent = false;
    private DateTime _lastEventAt = DateTime.MinValue;

    // constants
    private const uint EVENT_SYSTEM_FOREGROUND = 0x0003;
    private const uint WINEVENT_OUTOFCONTEXT = 0x0000;

    public ActiveWindowWatcher(int pollIntervalMs = 250)
    {
        _procDelegate = WinEventProc;
        _pollIntervalMs = Math.Max(50, pollIntervalMs);
        _pollTimer = new System.Timers.Timer(_pollIntervalMs);
        _pollTimer.AutoReset = true;
        _pollTimer.Elapsed += PollTimer_Elapsed;
    }

    /// <summary>
    /// Start the watcher: installs the event hook and starts fallback polling.
    /// </summary>
    public void Start()
    {
        // install event hook (global)
        _hook = SetWinEventHook(
            EVENT_SYSTEM_FOREGROUND, EVENT_SYSTEM_FOREGROUND,
            IntPtr.Zero, _procDelegate, 0, 0, WINEVENT_OUTOFCONTEXT);

        // start the poll timer as a safety net
        _pollTimer.Start();
    }

    /// <summary>
    /// Stop the watcher and cleanup.
    /// </summary>
    public void Stop()
    {
        _pollTimer.Stop();

        if (_hook != IntPtr.Zero)
        {
            UnhookWinEvent(_hook);
            _hook = IntPtr.Zero;
        }
    }

    private void PollTimer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        // If we've recently received events, polling can still act as a safety
        // If no events received for a while, poll to detect active window.
        if (!_receivedEvent || (DateTime.UtcNow - _lastEventAt).TotalMilliseconds > Math.Max(1000, _pollIntervalMs * 4))
        {
            PollForegroundWindow();
        }
    }

    // This method is the WinEventProc callback
    private void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
    {
        _receivedEvent = true;
        _lastEventAt = DateTime.UtcNow;

        if (hwnd == IntPtr.Zero) return;

        if (TryGetProcessInfoFromHwnd(hwnd, out uint pid, out string? procName))
        {
            NotifyIfChanged(procName, (int)pid, hwnd);
        }
    }

    private void PollForegroundWindow()
    {
        IntPtr hwnd = GetForegroundWindow();
        if (hwnd == IntPtr.Zero) return;

        if (TryGetProcessInfoFromHwnd(hwnd, out uint pid, out string? procName))
        {
            NotifyIfChanged(procName, (int)pid, hwnd);
        }
    }

    private void NotifyIfChanged(string? procName, int pid, IntPtr hwnd)
    {
        if (string.IsNullOrEmpty(procName)) return;

        // Only notify when the process or hwnd actually changes
        if (hwnd != _lastHwnd || (uint)pid != _lastPid || procName != _lastProcessName)
        {
            _lastHwnd = hwnd;
            _lastPid = (uint)pid;
            _lastProcessName = procName;

            try
            {
                OnActiveWindowChanged?.Invoke(procName, pid, hwnd);
            }
            catch
            {
                // swallow subscriber exceptions
            }
        }
    }

    private bool TryGetProcessInfoFromHwnd(IntPtr hwnd, out uint pid, out string? processName)
    {
        pid = 0;
        processName = null;

        try
        {
            GetWindowThreadProcessId(hwnd, out pid);
            if (pid == 0) return false;

            var process = Process.GetProcessById((int)pid);
            processName = process.ProcessName;
            return true;
        }
        catch
        {
            return false;
        }
    }

    public void Dispose()
    {
        Stop();
        _pollTimer.Dispose();
    }

    // P/Invoke
    [DllImport("user32.dll")]
    private static extern IntPtr SetWinEventHook(
        uint eventMin, uint eventMax, IntPtr hmodWinEventProc,
        WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

    [DllImport("user32.dll")]
    private static extern bool UnhookWinEvent(IntPtr hWinEventHook);

    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();
}
