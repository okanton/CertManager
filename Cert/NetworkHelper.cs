using System.Net;
using System.Net.NetworkInformation;

namespace Сerts
{
    class NetworkHelper
    {
        public static bool CheckNwConnectoin()
        {
            Ping ping = new Ping();
            PingReply pingStatus = ping.Send(IPAddress.Parse("10.168.0.10"));
            return (pingStatus.Status == IPStatus.Success) ? true : false;
        }
    }
}
