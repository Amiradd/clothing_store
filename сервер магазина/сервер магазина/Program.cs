using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace сервер_магазина
{
    class Program
    {
        static List<Product> basket;

        static void Main(string[] args)
        {
            basket = new List<Product>();
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            int port = 8888;
            TcpListener server = new TcpListener(localAddr, port);
            server.Start();
            Console.WriteLine("Сервер запущен!");

            while (true)
            {
                try
                {
                    // Подключение клиента
                    TcpClient client = server.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();
                    // Обмен данными
                    try
                    {
                        if (stream.CanRead)
                        {
                            byte[] myReadBuffer = new byte[1024];
                            StringBuilder myCompleteMessage = new StringBuilder();
                            int numberOfBytesRead = 0;
                            do
                            {
                                numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);
                                myCompleteMessage.AppendFormat("{0}", Encoding.UTF8.GetString(myReadBuffer, 0, numberOfBytesRead));
                            }
                            while (stream.DataAvailable);
                            Byte[] responseData = switcher(myCompleteMessage.ToString());
                            stream.Write(responseData, 0, responseData.Length);
                        }
                    }
                    finally
                    {
                        stream.Close();
                        client.Close();
                    }
                }
                catch
                {
                    server.Stop();
                    break;
                }
            }
        }

        static Byte[] switcher(string mes)
        {
            string[] cmd = mes.Split(new char[] { '@' });
            string[] value = cmd[1].Split(new char[] { '#' });

            switch (cmd[0])
            {
                case "add":
                    {
                        if (basket == null)
                            basket = new List<Product>();
                        Console.WriteLine("положили в корзину :" + value[0] + "\n");
                        basket.Add(new Product(value[0], Convert.ToInt32(value[1])));
                    }
                    break;
                case "view":
                    {
                        if (basket == null)
                            basket = new List<Product>();
                        string str = "";
                         
                        foreach (var item in basket)
                        {
                            str += $"{item.title}   Цена:{item.cost.ToString()}@";
                        }
                        return (Encoding.UTF8.GetBytes(str));
                    }
                case "delall":
                    {
                        string str = "";

                        foreach (var item in basket)
                        {
                            str += $"{item.title}   Цена:{item.cost.ToString()}@";
                        }
                        Console.WriteLine("приобрели :" + str + "\n");
                        basket = null;
                    }
                    break;
                case "price":
                    {
                        int price = 0;

                        foreach (var item in basket)
                        {
                            price += Convert.ToInt32(item.cost);
                        }
                        return (Encoding.UTF8.GetBytes(price.ToString()));
                    }
                case "check":
                    {
                        if (basket == null)
                        {
                            return (Encoding.UTF8.GetBytes("no"));
                        }
                        return (Encoding.UTF8.GetBytes("yes"));
                    }
                default:
                    break;
            }
            return (Encoding.UTF8.GetBytes(""));
        }
    }
}
