using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Клиент_для_магазина
{
    public partial class Catalog : Form
    {
        public Catalog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Exchange("127.0.0.1", 8888, $"add@{label1.Text}#{label7.Text}");
            MessageBox.Show("Добавлено");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Exchange("127.0.0.1", 8888, $"add@{label2.Text}#{label10.Text}");
            MessageBox.Show("Добавлено");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Exchange("127.0.0.1", 8888, $"add@{label3.Text}#{label12.Text}");
            MessageBox.Show("Добавлено");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Exchange("127.0.0.1", 8888, $"add@{label4.Text}#{label14.Text}");
            MessageBox.Show("Добавлено");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Exchange("127.0.0.1", 8888, $"add@{label5.Text}#{label16.Text}");
            MessageBox.Show("Добавлено");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Exchange("127.0.0.1", 8888, $"add@{label6.Text}#{label18.Text}");
            MessageBox.Show("Добавлено");
        }

        static private string Exchange(string address, int port, string outMessage)
        {
            try
            {
                // Инициализация
                TcpClient client = new TcpClient(address, port);
                Byte[] data = Encoding.UTF8.GetBytes(outMessage);
                NetworkStream stream = client.GetStream();
                try
                {
                    // Отправка сообщения
                    stream.Write(data, 0, data.Length);
                    // Получение ответа
                    Byte[] readingData = new Byte[256];
                    String responseData = String.Empty;
                    StringBuilder completeMessage = new StringBuilder();
                    int numberOfBytesRead = 0;
                    do
                    {
                        numberOfBytesRead = stream.Read(readingData, 0, readingData.Length);
                        completeMessage.AppendFormat("{0}", Encoding.UTF8.GetString(readingData, 0, numberOfBytesRead));
                    }
                    while (stream.DataAvailable);
                    responseData = completeMessage.ToString();
                    return responseData;
                }
                finally
                {
                    stream.Close();
                    client.Close();
                }
            }
            catch (Exception)
            {
                return ("Ожидание сервера...");
            }

        }
    }
}
