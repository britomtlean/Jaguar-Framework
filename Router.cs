using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Router
    {
        public string Method { get; set; }
        public string Endpoint { get; set; }
        public Func<Object> Function { get; set; }

        public Router(string method, string endpoint, Func<Object> function)
        {
            this.Method = method;
            this.Endpoint = endpoint;
            this.Function = function;
        }
    }
}
