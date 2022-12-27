using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


public abstract class Monitor
{
    public abstract string RestServerHostName { get; }
    public abstract int RestServerPort { get; }

    private const long INTEGRITY_INTERVAL = 10;  // 초 단위

    protected abstract string TargetProcess { get; }

    private Thread _monitorring;

    private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

    /*
     * 서버, 프로그램, 패키지 다양한 환경에서 처리를 위해 추상 클래스로 작성 
     */
    public static Monitor New(string mode)
    {
        switch (mode)
        {
            case "server":
                return new ServerMonitor();

            default:
                throw new Exception($"Invalid mode: {mode}");
        }
    }

    public void Start()
    {
        if (_monitorring != null)
            throw new Exception("이미 실행중");

        _monitorring = new Thread(() =>
        {
            do
            {
                try
                {
                    Monitorring();
                }
                catch (Exception ex)
                {
                    // logger
                }
                for (int i = 0; i < 60 && _monitorring != null; i++)
                    Thread.Sleep(1000);
            }
            while (_monitorring != null);

        })
        {
            IsBackground = true
        };

        _monitorring.Start();
    }

    private void Monitorring()
    {
        long lastIntegrityChecked = 0;

        long lastProcessChecked = DateTime.Now.Ticks / 10_000;

        while(_monitorring!=null)
        {
            var now = DateTime.Now.Ticks / 10_000;

            if (Process.GetProcessesByName(TargetProcess).Length > 0)
            {
                // 무결성 검사 시작 및 
                lastProcessChecked = DateTime.Now.Ticks / 10_000;

                if (now > lastIntegrityChecked + INTEGRITY_INTERVAL * 1000)
                {
                    var started = lastIntegrityChecked == 0;

                    CheckIntegrity();

                    lastIntegrityChecked = now;
                }
            }
            else
            {
                lastIntegrityChecked = 0;
            }
        }
    }

    private void CheckIntegrity()
    {
        _logger.Info("watchdog 무결성 검사");

        var deleteList = new List<string>();
        var corruptedList = new List<string>();

        // 원본 무결성은 어디서 받아올것인가?
        // 

    }
}

