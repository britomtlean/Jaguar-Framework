using System.Net;
using ConsoleApp;

var listener = new HttpListener();
var app = new Jaguar(listener);

DotNetEnv.Env.Load(); //.env
var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";

await app.StartApp(port);
