// See https://aka.ms/new-console-template for more information
using TraceRoute;
using TraceRoute.Models;


string domain = "";


while (domain.ToLower() != "q")
{
    Console.WriteLine("Enter a domain or ip address");
    domain = Console.ReadLine();
    var tr = TraceRoute.TraceRoute.GetTraceRoute(domain);

    int i = 1;

    if (tr.Result.url == "invalid")
    {
        Console.WriteLine("invalid IP!");
    }

    if (tr.Result != null)
    {
        foreach (var line in tr.Result.traceResults)
        {
            Console.WriteLine($"{line.hop} | {line.ping1} | {line.ping2} | {line.ping3} | {line.ip}");
        }
        if (tr.Result.isComplete) Console.WriteLine("Ping Complete!");
    }
}
