using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BlackJack
{
    public partial class Muti_Server : Form
    {
        public Muti_Server()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            Connect();
        }

        IPEndPoint IP;
        Socket server;
        List<Socket> clientList;
        int tempSL = 0;
        void Connect()
        {
            clientList = new List<Socket>();
            IP = new IPEndPoint(IPAddress.Any, 8080);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            server.Bind(IP);

            Thread Listen = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        server.Listen(100);
                        Socket client = server.Accept();
                        clientList.Add(client);

                        Thread receive = new Thread(Receive);
                        
                        receive.IsBackground = true;
                        receive.Start(client);


                        string IP = client.RemoteEndPoint.ToString();   //Hiển thị ip client
                        AddMessage(IP);
                    }
                }
                catch
                {
                    IP = new IPEndPoint(IPAddress.Any, 8080);
                    server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                }
            });
            Listen.IsBackground = true;
            Listen.Start();
        }
        //đống kết nối
        void Close()
        {
            server.Close();
        }

        //nhận tin
        void Receive(object obj)
        {
            Socket client = obj as Socket;
            while (true)
            {
                try
                {
                    byte[] data = new byte[1024 * 5000];
                    client.Receive(data);

                    string message = Deserialize(data).ToString();
                    //0: start
                    //1: Mess
                    //2: wait
                    //3: Duoc choi
                    //4: Rut
                    //5: Dan
                    //6: Xet bai
                    
                    switch (message.Substring(0, 2))
                    {
                        case "1:":
                            tempSL++;
                            if (tempSL == clientList.Count()){
                                foreach (Socket item in clientList)
                                {
                                    if (item != null)
                                    {
                                        message = "Start";
                                        item.Send(Serialize(message));
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    //if (message.Substring(0, 2) == "1:")
                    //{

                    //}
                    //else
                    //{
                    //    foreach (Socket item in clientList)
                    //    {
                    //        if (item != null && item != client)
                    //            item.Send(Serialize(message));
                    //    }
                    //}

                    AddMessage(message);
                }
                catch
                {
                    clientList.Remove(client);
                    client.Close();
                }
            }

        }
        void AddMessage(string s)
        {
            listMess.Items.Add(new ListViewItem()
            {
                Text = s
            });
        }
        byte[] Serialize(object obj)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(stream, obj);

            return stream.ToArray();
        }
        object Deserialize(byte[] data)
        {
            MemoryStream stream = new MemoryStream(data);
            BinaryFormatter formatter = new BinaryFormatter();

            return formatter.Deserialize(stream);
        }

        private void Muti_Server_FormClosed(object sender, FormClosedEventArgs e)
        {
            Close();
            SinglePlay singlePlay = new SinglePlay();
            singlePlay.Show();
        }

    }
}
