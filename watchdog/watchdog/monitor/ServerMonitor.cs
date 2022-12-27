using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class ServerMonitor :Monitor
{

    // 추후 외부에서 받아오도록 수정 
    public override string RestServerHostName => "127.0.0.1";
    public override int RestServerPort => 50005;

    protected override string TargetProcess => "Steam";

    public ServerMonitor()
    {
        
    }
}

