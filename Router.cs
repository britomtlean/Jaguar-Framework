using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace JaguarFramework
{
    public class Router
    {
        public string Method { get; set; }
        public string Endpoint { get; set; }
        public Func<string?, Task<Object>> Function { get; set; }

        public Router(string method, string endpoint, Func<string?, Task<Object>> function)
        {
            this.Method = method;
            this.Endpoint = endpoint;
            this.Function = function;
        }
    }
}
