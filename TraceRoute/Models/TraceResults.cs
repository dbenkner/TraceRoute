using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceRoute.Models
{
    public class TraceResults
    {
        public string ip {  get; set; } = string.Empty;
        public long ping1 { get; set; }
        public long ping2 { get; set; } 
        public long ping3 { get; set; }
        public int hop {  get; set; }
    }
}
