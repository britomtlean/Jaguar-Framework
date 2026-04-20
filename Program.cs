using System.Net;
using JaguarFramework;
using JaguarFramework.Context;
using JaguarFramework.Services;
using JaguarFramework.Models;
using System.Text.Json;

var listener = new HttpListener();
var app = new Jaguar(listener);

var db = new UsuarioService();

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


app.Post<Usuario>("/create-user", async (body) =>
{
    {
        var response = await db.CreateUser(body);
        return response;
    }

});


await app.StartApp(port);
