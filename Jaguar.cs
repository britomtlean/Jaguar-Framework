using System.Net;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp
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
            Console.WriteLine($"Dados recebidos: {body}");

            //HOME
            if (endpoint == "/" && method == "GET")
            {
                await RequestResponse.ResponseHTML("index.html", response);
                response.Close();
                return;
            }

            foreach (var router in this.routers)
            {
                if (endpoint == router.Endpoint && method == router.Method)
                {
                    router.Function();
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

        //Routers Functions
        public void Get(string endpoint, Action function)
        {
            routers.Add( new Router("GET", endpoint, function));
        }
        public void Post(string endpoint, Action function)
        {
            routers.Add(new Router("POST", endpoint, function));
        }
        public void Put(string endpoint, Action function)
        {
            routers.Add(new Router("PUT", endpoint, function));
        }
        public void Delete(string endpoint, Action function)
        {
            routers.Add(new Router("DELETE", endpoint, function));
        }

    }
}

