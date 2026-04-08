using System.Net;
using ConsoleApp;
using Microsoft.VisualBasic;

var listener = new HttpListener();
var app = new Jaguar(listener);

DotNetEnv.Env.Load(); //.env
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";


app.Get("/jaguar/swagger", () =>
{
    var lista = new List<Dictionary<string, object>>
    {
        new Dictionary<string, object>
        {
            ["nome"] = "Leandro",
            ["idade"] = 30
        },

        new Dictionary<string, object>
        {
            ["nome"] = "Maria",
            ["idade"] = 25
        },

        new Dictionary<string, object>
        {
            ["nome"] = "Carlos",
            ["idade"] = 40
        }
    };

    return lista;
});

await app.StartApp(port);
