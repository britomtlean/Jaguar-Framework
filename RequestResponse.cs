using System.Net;
using System.Text;


namespace ConsoleApp
{
    public class RequestResponse
    {
        public static async Task ResponseHTML(string path, HttpListenerResponse response)
        {
            var html = await File.ReadAllTextAsync(path);
            //Console.WriteLine(html);
            var buffer = Encoding.UTF8.GetBytes(html); //Converte HTML em byte

            response.StatusCode = 200;
            response.ContentType = "text/html";
            response.ContentLength64 = buffer.Length;
            await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
            response.Close(); //Fecha requisição

        }

        public static void SendMessage(string message, HttpListenerResponse response)
        {

            var buffer = Encoding.UTF8.GetBytes(message); //Gera uma resposta
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
