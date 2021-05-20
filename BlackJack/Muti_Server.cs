using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
            DataClient dataClient = new DataClient();
            dataClient.Show();
            Control.CheckForIllegalCrossThreadCalls = false;
            boBai = new BoBai();
            Connect();
        }
        SqlConnection conn = new SqlConnection("Data Source=BLACKSONIA\\SQLEXPRESS;Initial Catalog=Client;Integrated Security=True");
        

        private void Muti_Server_Load(object sender, EventArgs e)
        {
            conn.Open();

        }
        //public DataSet LoadData(string strLenh)
        //{
        //    DataSet ds = new DataSet();
        //    Create_Connect();
        //    SqlDataAdapter da = new SqlDataAdapter(strLenh, strConnect);
        //    da.Fill(ds);
        //    CloseConnect();
        //    return ds;
        //}

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

                        
                        var dap = new SqlDataAdapter("SELECT COUNT(IP) FROM CLIENT", conn);
                        var table = new DataTable();
                        dap.Fill(table);
                        string IP = client.RemoteEndPoint.ToString();   //Hiển thị ip client

                        string stringSql = "("+(Int32.Parse(table.Rows[0][0].ToString()) + 1).ToString()+", '"+IP+ "')";

                        dap = new SqlDataAdapter("insert into CLIENT (ID, IP) VALUES"+stringSql, conn);
                        table = new DataTable();
                        dap.Fill(table);
                        AddMessage(IP);
                        AddMessage("insert into CLIENT (ID, IP) VALUES" + stringSql);
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
                    string IP = client.RemoteEndPoint.ToString();
                    //00: start
                    //01: Mess
                    //02: đợi
                    //03: Đã rút
                    //04: Rut
                    //05: Dan
                    //06: Xet bai


                    //07:Số lượng người chơi vidu: 7:20213 --->2 người chơi 1-> có 2 lá 2-> có 2 lá
                    //08: Server hỏi client

                    //09: nhà cái


                    //30: Xét bài

                    //11: udate numbercard

                    //2*: Get vào SQLServer
                    //20: Get số lượng người chơi
                    //21: Lấy số lượng người chơi
                    //22: Lấy IP người chơi 1  21:1
                    //   2 Lấy IP người chơi 2
                    //   3 Lấy IP người chơi 3
                    //   4 Lấy IP người chơi 4
                    //   5 Lấy IP người chơi 5

                    //20: Truy xuất SQL Server


                    switch (message.Substring(0, 3))
                    {
                        case "00:":
                            {
                                tempSL++;
                                if (tempSL == clientList.Count())
                                {
                                    foreach (Socket item in clientList)
                                    {
                                        if (item != null)
                                        {
                                            message = "00:Start";
                                            AddMessage(item.RemoteEndPoint.ToString() + ": "+message);
                                            item.Send(Serialize(message));
                                        }
                                    }
                                    for (int i = 0; i < 2; i++)
                                    {
                                        foreach (Socket item in clientList)
                                        {
                                            if (item != null)
                                            {
                                                Card card = new Card();
                                                card = boBai.getCard();
                                                AddMessage("94:" + card.getIdCard());
                                                item.Send(Serialize("94:" + card.getIdCard()));
                                            }
                                        }
                                    }
                                    foreach (Socket item in clientList) 
                                    {
                                        if (item != null)
                                        {
                                            AddMessage("20:" + tempSL.ToString());
                                            client.Send(Serialize("20:" + tempSL.ToString()));
                                        }
                                    }
                                    foreach (Socket item in clientList) //đợi
                                    {
                                        item.Send(Serialize("02:"));
                                    }
                                }
                                clientList[0].Send(Serialize("09: Bạn là cái ---> Rút cuoi")); /// thang 0 đc rút
                              
                                break;
                            }
                        case "01:":
                            {
                                AddMessage(client.RemoteEndPoint.ToString() + ": " + message);
                                foreach (Socket item in clientList)
                                {
                                    if (item != null && item != client)
                                    {
                                        item.Send(Serialize(message));
                                    }
                                }
                                
                                break;
                            }
                        case "04:":
                            {
                                AddMessage("04:" + client.RemoteEndPoint.ToString());
                                
                                foreach (Socket item in clientList)
                                {
                                    if (item != null && item == client)
                                    {
                                        Card card = new Card();
                                        card = boBai.getCard();
                                        AddMessage("04:" + card.getIdCard());
                                        item.Send(Serialize("04:"+card.getIdCard()));
                                    }
                                }
                                break;
                            }
                        case "05:":
                            {
                                AddMessage("05:" + client.RemoteEndPoint.ToString());
                                
                                var dap = new SqlDataAdapter("update CLIENT SET SUMCARD="+message.Substring(3)+" WHERE IP='"+IP+"'", conn);
                                var table = new DataTable();
                                dap.Fill(table);
                                tempPlayer++;
                                if (tempPlayer < clientList.Count())
                                {
                                    clientList[tempPlayer].Send(Serialize("03:"));
                                }
                                break;
                            }
                        case "11:":
                            {
                                var dap = new SqlDataAdapter("update CLIENT SET NUMOFCARD = "+message.Substring(3)+" WHERE IP='"+IP+"'", conn);
                                var table = new DataTable();
                                dap.Fill(table);
                                AddMessage("UPDATE CLIENT SET NUMOFCARD = " + message.Substring(3) + " WHERE IP='" + IP + "'");

                                break;
                            }
                        case"20:":
                            {
                                //get so luong trung database
                                

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
                    //MessageBox.Show("Erro");
                    clientList.Remove(client);
                    client.Close();
                }
            }

        }
        void AddMessage(string s)
        {
            listMess.Text += (s+"\n").ToString();
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
            
            var dap = new SqlDataAdapter("DELETE FROM CLIENT", conn);
            var table = new DataTable();
            dap.Fill(table);


            Close();
            Form1 form = new Form1();
            form.Show();
        }
    }
}
