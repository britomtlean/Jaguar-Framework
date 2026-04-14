using System.Net;
using JaguarFramework;
using JaguarFramework.Context;

var listener = new HttpListener();
var app = new Jaguar(listener);

var db = new MongoContextDb();

//.ENV
DotNetEnv.Env.Load();
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

app.Get("/users", async () =>
{
    var usuarios = await db.GetUsers();
    return usuarios;
});


await app.StartApp(port);
