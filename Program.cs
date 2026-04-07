using System.Net;
using ConsoleApp;

var listener = new HttpListener();
var app = new Jaguar(listener);

DotNetEnv.Env.Load(); //.env
var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";

app.Get("/jaguar", () => Console.WriteLine("Hello Jaguar"));

await app.StartApp(port);
