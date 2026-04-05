using System.Net;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp
{
    public class Jaguar
    {
        private readonly HttpListener _listener;
        private string? home;

        public Jaguar(HttpListener listiner)
        {
            this._listener = listiner;
        }
        

        public async Task StartApp(string port)
        {
            this.home = $"http://localhost:{port}/";

            _listener.Prefixes.Add(this.home); //Define a porta do servidor
            _listener.Start(); //Inicia o servidor

            Console.WriteLine($"Rodando em {this.home}"); //Exibe mensagem ao servidor

            while (true) //Mantem o servidor conectado
            {
                var context = await _listener.GetContextAsync(); //Aguarda uma requisição
                await RequestSettings(context); //Processa a requisição
            }
        }

        private async Task RequestSettings(HttpListenerContext context)
        {
            var request = context.Request; //Guarda os dados de entrada
            var response = context.Response; //Guarda dados de saída

            //Exibe o Endpoint
            var endpoint = request.Url!;
            Console.WriteLine($"Endpoint solicitado: {endpoint.AbsolutePath}");


            //Verifica o conteudo do Body
            var body = await new StreamReader(request.InputStream, Encoding.UTF8).ReadToEndAsync(); //Transforma o body recebido em bytes em texto
            Console.WriteLine($"Dados recebidos: {body}");


            //Criar Classe para executar Rotas
            if (endpoint.AbsolutePath == "/")
            {
                await RequestResponse.ResponseHTML("index.html", response);
                response.Close();
                return;

            }
            else if(endpoint.AbsolutePath == "/login")
            {
                RequestResponse.SendMessage("Tela de Login", response);
                response.Close();
                return;
            }
            else if (endpoint.AbsolutePath == "/cadastro")
            {
                RequestResponse.SendMessage("Tela de Cadastro", response);
                response.Close();
                return;
            }
            else if (endpoint.AbsolutePath == "/dashboard")
            {
                RequestResponse.SendMessage("Tela de Dashboard", response);
                response.Close();
                return;
            }

            RequestResponse.SendErrorMessage(response);
            response.Close(); //Fecha requisição
        }
    }



}

