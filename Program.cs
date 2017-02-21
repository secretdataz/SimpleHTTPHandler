using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHTTPHandler
{
    class Program
    {
        static string text = File.ReadAllText("response.txt");
        static void Main(string[] args)
        {
            #region HTTP server
            var server = new HttpListener();
            server.Prefixes.Add("http://127.0.0.1/");
            server.Prefixes.Add("http://localhost/");

            server.Start();

            while(true)
            {
                var ctx = server.GetContext();
                var response = ctx.Response;
                byte[] buf = Encoding.UTF8.GetBytes(text);
                response.ContentLength64 = buf.Length;
                var stream = response.OutputStream;
                stream.Write(buf, 0, buf.Length);
                response.Close();
                Console.WriteLine("Served spoofed time.");
            }
            #endregion
        }
    }
}
