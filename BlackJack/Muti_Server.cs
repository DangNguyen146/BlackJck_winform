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
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            boBai = new BoBai();
            Connect();
        }

        BoBai boBai;
        IPEndPoint IP;
        Socket server;
        List<Socket> clientList;
        int tempSL = 0;
        int tempPlayer = 0;
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
                        if (clientList.Count == 5)
                        {
                            AddMessage("Đã đạt số client tối đa".ToString());
                            return;
                        }
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
                    //2: đợi
                    //3: Đã rút
                    //4: Rut
                    //5: Dan
                    //6: Xet bai

                    //7:Số lượng người chơi vidu: 7:20213 --->2 người chơi 1-> có 2 lá 2-> có 2 lá
                    //8: Server hỏi client

                    //9: nhà cái
                    
                    switch (message.Substring(0, 2))
                    {
                        case "0:":
                            {
                                tempSL++;
                                if (tempSL == clientList.Count())
                                {
                                    foreach (Socket item in clientList)
                                    {
                                        if (item != null)
                                        {
                                            message = "0:Start";
                                            AddMessage(item.RemoteEndPoint.ToString() + ": "+message);
                                            item.Send(Serialize(message));
                                        }
                                        Thread.Sleep(200);
                                    }
                                    for (int i = 0; i < 2; i++)
                                    {
                                        foreach (Socket item in clientList)
                                        {
                                            if (item != null)
                                            {
                                                Card card = new Card();
                                                card = boBai.getCard();
                                                AddMessage("4:" + card.getIdCard());
                                                item.Send(Serialize("4:" + card.getIdCard()));
                                            }
                                            Thread.Sleep(200);
                                        }
                                    }
                                    Thread.Sleep(200);
                                    foreach (Socket item in clientList) //đợi
                                    {
                                        item.Send(Serialize("2:"));
                                        Thread.Sleep(200);
                                    }
                                }
                                clientList[0].Send(Serialize("9: Bạn là cái ---> Rút cuoi")); /// thang 0 đc rút
                              
                                break;
                            }
                        case "1:":
                            {
                                AddMessage(client.RemoteEndPoint.ToString() + ": " + message);
                                foreach (Socket item in clientList)
                                {
                                    if (item != null && item != client)
                                    {
                                        item.Send(Serialize(message));
                                    }
                                    Thread.Sleep(200);
                                }
                                
                                break;
                            }
                        case "4:":
                            {
                                AddMessage("4:" + client.RemoteEndPoint.ToString());
                                
                                foreach (Socket item in clientList)
                                {
                                    if (item != null && item == client)
                                    {
                                        Card card = new Card();
                                        card = boBai.getCard();
                                        AddMessage("4:" + card.getIdCard());
                                        item.Send(Serialize("4:"+card.getIdCard()));
                                    }
                                    Thread.Sleep(200);
                                }
                                break;
                            }
                        case "5:":
                            {
                                AddMessage("5:" + client.RemoteEndPoint.ToString());
                                Thread.Sleep(100);
                                tempPlayer++;
                                if (tempPlayer < clientList.Count())
                                {
                                    clientList[tempPlayer].Send(Serialize("3:"));
                                }
                                break;
                            }
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

                    //AddMessage(message);
                }
                catch
                {
                    MessageBox.Show("Erro");
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
