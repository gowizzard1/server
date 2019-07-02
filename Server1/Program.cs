using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Server1
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 8002;
            string ipAdrr = "127.0.0.1";
            Socket ServerListener = new Socket(AddressFamily
                .InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endp = new IPEndPoint(IPAddress.Parse(ipAdrr), port);
            ServerListener.Bind(endp);
            ServerListener.Listen(100);
            Console.WriteLine("Server Started");
            Console.WriteLine("Accept Connection from client");
            Socket ClientSocket = default(Socket);
            int counter = 0;
            Program p = new Program();
            while (true)
            {
                counter++;
                ClientSocket = ServerListener.Accept();
                Console.WriteLine(counter+"Clients Connected");
                Thread UserThread = new Thread(new ThreadStart(()=>p.User(ClientSocket)));
                UserThread.Start();

            }
        }
        public void User(Socket client)
        {
            while (true)
            {
                byte[] msg = new byte[1024];
                int size = client.Receive(msg);
                client.Send(msg, 0, size, SocketFlags.None);
                Console.WriteLine("Data from client-" + System.Text.Encoding.ASCII.GetString(msg, 0, size));
               string reverse = "";
                int Length = msg.Length - 1;
                while (Length >=0)
                {
                    reverse = reverse + msg[Length];
                    Length--;
                }
                //Console.WriteLine("Response from server-" + System.Text.Encoding.ASCII.GetString(reverse, 0, size));
            }
        }
    }
}
