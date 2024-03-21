using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using TraceRoute.Models;

namespace TraceRoute
{
    public static class TraceRoute
    {
        private const string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        public static async Task<TraceMod> GetTraceRoute(string url)
        {
            Stopwatch sw = Stopwatch.StartNew();
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions(1, true);
            TraceMod tr = new TraceMod();
            try
            {
                IPAddress iPAddress = Dns.GetHostEntry(url).AddressList[0];
            }
            catch (Exception)
            {
                bool valid = ValidateIPv4(url);
                if (!valid)
                {
                    tr.url = "invalid";
                    return tr;
                }
                tr.url = url;
            }
            tr.url = url;
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 200;
            for (int i = 1; i <= 30; i++)
            {
                TraceResults results = new TraceResults();
                
                bool success = false;
                long[] ipArr = new long[3];
                for (var j = 0; j < 3; j++)
                {
                    sw.Reset();
                    sw.Start();
                    PingReply reply = pingSender.Send(url, timeout, buffer, options);
                    if (reply.Status == IPStatus.TimedOut)
                    {
                        results.ip = "Request Timed Out";
                        continue;
                    }
                    ipArr[j] = sw.ElapsedMilliseconds;
                    results.ip = reply.Address.ToString();
                    if (reply.Status == IPStatus.Success)
                    {
                        success = true;
                    }
                }
                results.ping1 = ipArr[0];
                results.ping2 = ipArr[1];   
                results.ping3 = ipArr[2];
                
                results.hop = i;
                tr.traceResults.Add(results);
                if (success)
                {
                    tr.isComplete = true;
                    break;
                }
                options.Ttl++;
            }
            return tr;
        }
        public static void PrintTrace(TraceMod trace)
        {
            foreach(var line in trace.traceResults)
            {
                Console.WriteLine($"{line.hop} | {line.ping1} | {line.ping2} | {line.ping3} | {line.ip}");
            }
            if (trace.isComplete) Console.WriteLine("Ping Complete");
        }
        public static bool ValidateIPv4(string ip)
        {
            if(ip == null) return false;

            string[] ipSplit = ip.Split(".");
            if(ipSplit.Length != 4)
            {
                return false;
            }
            foreach(var val in ipSplit)
            { 
                byte oct;
                bool validNum = byte.TryParse(val, out oct);
                if(!validNum) return false;
            }
            return true;
        }
    }
}
