using System.Net;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using JaguarFramework.Models;

namespace JaguarFramework
{
    public class Jaguar
    {
        private readonly HttpListener _listener;

        private List<Router> routers = new List<Router>();


        public Jaguar(HttpListener listiner)
        {
            this._listener = listiner;
        }

        public async Task StartApp(string port)
        {
            var host = $"http://localhost:{port}/";

            _listener.Prefixes.Add(host); //Define a porta do servidor
            _listener.Start(); //Inicia o servidor

            Console.WriteLine($"Rodando em {host}"); //Exibe mensagem ao servidor

            while (true) //Mantem o servidor conectado
            {
                var context = await _listener.GetContextAsync(); //Aguarda uma requisição
                await RequestSettings(context); //Processa a requisição
            }

        }

        private async Task RequestSettings(HttpListenerContext context)
        {
            var request = context.Request!; //Guarda os dados de entrada
            var response = context.Response!; //Guarda dados de saída

            //Exibe o Endpoint
            var endpoint = request.Url!.AbsolutePath;
            Console.WriteLine($"Endpoint solicitado: {endpoint}");

            //Exibe Método
            var method = request.HttpMethod;
            Console.WriteLine($"Método solicitado: {method}");

            //Verifica o conteudo do Body
            var body = await new StreamReader(request.InputStream, Encoding.UTF8).ReadToEndAsync(); //Transforma o body recebido em bytes em texto
            //Console.WriteLine(body); //Body recebido
            //Console.WriteLine(body?.GetType()); //string

            //HOME
            if (endpoint == "/" && method == "GET")
            {
                await RequestResponse.ResponseHTML(response);
                response.Close();
                return;
            }

                        //HOME
            if (endpoint == "/login" && method == "GET")
            {
                await RequestResponse.LoginPage(response);
                response.Close();
                return;
            }

            foreach (var router in this.routers)
            {
                if (endpoint == router.Endpoint && method == router.Method)
                {
                    var message = await router.Function(body); //recebe como string
                    RequestResponse.SendMessage(message, response);
                    return;
                }
            }


            /*
            //Criar Classe para executar Rotas
            if (endpoint == "/" && method == "GET")
            {
                await RequestResponse.ResponseHTML("index.html", response);
                response.Close();
                return;

            }
            else if (endpoint == "/login" && method == "GET")
            {
                RequestResponse.SendMessage("Tela de Login", response);
                response.Close();
                return;
            }
            else if (endpoint == "/cadastro")
            {
                RequestResponse.SendMessage("Tela de Cadastro", response);
                response.Close();
                return;
            }
            else if (endpoint == "/dashboard" && method == "GET")
            {
                RequestResponse.SendMessage("Tela de Dashboard", response);
                response.Close();
                return;
            }
            */


            RequestResponse.SendErrorMessage(response);
            response.Close(); //Fecha requisição
        }




        /////////////////////////// GET \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public void Get(string endpoint, Func<Object> function)
        {
            //Console.WriteLine("void function");

            Func<string, Task<Object>> GetFunction = async (body) =>
            {
                return function();
            };

            routers.Add(new Router("GET", endpoint, GetFunction));
        }



        public void Get(string endpoint, Func<Task<Object>> function)
        {
            //Console.WriteLine("async function");

            Func<string, Task<Object>> GetFunction = async (body) =>
            {
                return await function();
            };

            routers.Add(new Router("GET", endpoint, GetFunction));
        }



        /////////////////////////// /POST \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\


        public void Post<T>(string endpoint, Func<T, Task<Object>> function)
        {
            Func<string, Task<Object>> PostFunction = async (body) =>
            {
                var genericBody = JsonSerializer.Deserialize<T>(body); //Converte string ao Tipo Generico
                //Console.WriteLine(genericBody.GetType());
                //Console.WriteLine(user);
                return await function(genericBody);
            };

            var route = new Router(
                "POST",
                endpoint,
                PostFunction
            );

            routers.Add(route);
        }


        //////////////////////////////////////////////////////////////
    

        public void Put(string endpoint, Func<Object> function)
        {
           // routers.Add(new Router("PUT", endpoint, function));
        }
        public void Delete(string endpoint, Func<Object> function)
        {
          //  routers.Add(new Router("DELETE", endpoint, function));
        }

    }
}

