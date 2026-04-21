using System.Net;
using System.Text;
using System.Text.Json;
using RazorLight;

namespace JaguarFramework
{
    public class RequestResponse
    {
        public static async Task ResponseHTML(HttpListenerResponse response)
        {
            var engine = new RazorLightEngineBuilder() //Cria engine
                .UseFileSystemProject(Directory.GetCurrentDirectory())
                .UseMemoryCachingProvider()
                .Build();

            string message = "Hello Jaguar";

            string html = await engine.CompileRenderAsync( //Compila para html
                "index.cshtml",
                message
            );

            //Console.WriteLine(html);

            var buffer = Encoding.UTF8.GetBytes(html); //Converte HTML em byte

            response.StatusCode = 200;
            response.ContentType = "text/html";
            response.ContentLength64 = buffer.Length;
            await response.OutputStream.WriteAsync(buffer, 0, buffer.Length); //Envia resposta HTTP em BYTE
            response.Close(); //Fecha requisição

        }


        //////////////////////////

        public static async Task LoginPage(HttpListenerResponse response)
        {
            var engine = new RazorLightEngineBuilder() //Cria engine
                .UseFileSystemProject(Directory.GetCurrentDirectory())
                .UseMemoryCachingProvider()
                .Build();

            string message = "Hello Jaguar";

            string html = await engine.CompileRenderAsync( //Compila para html
                "pages/login.cshtml",
                message
            );

            //Console.WriteLine(html);

            var buffer = Encoding.UTF8.GetBytes(html); //Converte HTML em byte

            response.StatusCode = 200;
            response.ContentType = "text/html";
            response.ContentLength64 = buffer.Length;
            await response.OutputStream.WriteAsync(buffer, 0, buffer.Length); //Envia resposta HTTP em BYTE
            response.Close(); //Fecha requisição

        }

        /////

        public static async Task RegisterPage(HttpListenerResponse response)
        {
            var engine = new RazorLightEngineBuilder() //Cria engine
                .UseFileSystemProject(Directory.GetCurrentDirectory())
                .UseMemoryCachingProvider()
                .Build();

            string message = "Hello Jaguar";

            string html = await engine.CompileRenderAsync( //Compila para html
                "pages/register.cshtml",
                message
            );

            //Console.WriteLine(html);

            var buffer = Encoding.UTF8.GetBytes(html); //Converte HTML em byte

            response.StatusCode = 200;
            response.ContentType = "text/html";
            response.ContentLength64 = buffer.Length;
            await response.OutputStream.WriteAsync(buffer, 0, buffer.Length); //Envia resposta HTTP em BYTE
            response.Close(); //Fecha requisição

        }

        ////

        public static void SendMessage(Object message, HttpListenerResponse response)
        {

            string stringJSON = JsonSerializer.Serialize(message).ToString(); //Transforma em JSON e converte em STRING
            Console.WriteLine($"Dados enviados: {stringJSON}");

            var buffer = Encoding.UTF8.GetBytes(stringJSON); //Converte STRING em BYTE
            response.StatusCode = 200;
            response.OutputStream.Write(buffer, 0, buffer.Length); //Envia Resposta
            response.Close(); //Fecha requisição
        }

        public static void SendErrorMessage(HttpListenerResponse response)
        {
            var buffer = Encoding.UTF8.GetBytes("Not Found"); //Gera uma resposta
            response.StatusCode = 404;
            response.OutputStream.Write(buffer, 0, buffer.Length); //Envia Resposta
            response.Close(); //Fecha requisição
        }
    }
}
