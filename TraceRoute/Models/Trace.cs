using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceRoute.Models
{
    public class TraceMod
    {
        public string url { get; set; } = string.Empty;
        public bool isComplete { get; set; } = false;
        public List<TraceResults>? traceResults { get; set; } = new List<TraceResults>();

    }
}
