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
        string ipclient;
        string tempCai = "";
        
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
                        ipclient = IP;
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
                                        Thread.Sleep(100);
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
                                        Thread.Sleep(100);
                                    }
                                    
                                    foreach (Socket item in clientList) //đợi
                                    {
                                        item.Send(Serialize("02:"));
                                    }
                                    Thread.Sleep(100);
                                    clientList[0].Send(Serialize("09: Bạn là cái ---> Rút cuoi")); /// thang 0 đc rút
                                }
                              
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
                                    if (item != null)
                                    {
                                        if (item == client)
                                        {
                                            Card card = new Card();
                                            card = boBai.getCard();
                                            AddMessage("04:" + card.getIdCard());
                                            item.Send(Serialize("04:"+card.getIdCard()));
                                        }
                                    }
                                }
                                break;
                            }
                        case "05:":
                            {
                                AddMessage("05:" + client.RemoteEndPoint.ToString());
                                var dap = new SqlDataAdapter("update CLIENT SET SUMCARD = "+message.Substring(3)+" WHERE IP='" + IP + "'", conn);
                                var table = new DataTable();
                                dap.Fill(table);

                                tempPlayer++;
                                if (tempPlayer < clientList.Count())
                                {
                                    clientList[tempPlayer].Send(Serialize("03:"));
                                }
                                else
                                {
                                    clientList[0].Send(Serialize("01:Tới lượt bạn"));
                                }
                                break;
                            }
                        case "11:":
                            {
                                AddMessage("update CLIENT SET NUMOFCARD = " + message.Substring(3) + " WHERE IP='" + IP + "'");
                                var dap = new SqlDataAdapter("update CLIENT SET NUMOFCARD = "+message.Substring(3)+" WHERE IP='"+IP+"'", conn);
                                
                                var table = new DataTable();
                                dap.Fill(table);
                                AddMessage("UPDATE CLIENT SET NUMOFCARD = " + message.Substring(3) + " WHERE IP='" + IP + "'");

                                foreach (Socket item in clientList)
                                {
                                    if (item != null && item != client)
                                    {
                                        AddMessage("21:" + IP + message.Substring(3,1));
                                        item.Send(Serialize("21:" +IP + message.Substring(3, 1)));
                                    }
                                }
                                break;
                            }
                        case "12:":
                            {
                                AddMessage("update CLIENT SET DAN = 1 WHERE IP='" + IP + "'");
                                var dap = new SqlDataAdapter("update CLIENT SET DAN = 1 WHERE IP='" + IP + "'", conn);
                                var table = new DataTable();
                                dap.Fill(table);

                                //CHECK DAN HET CHUA
                                AddMessage("SELECT COUNT(*) FROM Client WHERE DAN=1");
                                dap = new SqlDataAdapter("SELECT COUNT(*) FROM Client WHERE DAN=1", conn);
                                table = new DataTable();
                                dap.Fill(table);
                                if(Int32.Parse(table.Rows[0][0].ToString()) == clientList.Count())
                                {
                                    foreach (Socket item in clientList)
                                    {
                                        if (item != null)
                                        {
                                            AddMessage("01:" );
                                            item.Send(Serialize("01: Xét bài"));
                                        }
                                    }
                                    AddMessage("30:");
                                    clientList[0].Send(Serialize("30:"));
                                }

                                break;
                            }
                        case"20:":
                            {
                                //get so luong người chơi database
                                
                                foreach (Socket item in clientList)
                                {
                                    string temp = "";
                                    foreach (Socket i in clientList)
                                    {
                                        if (i != null && item != i)
                                            temp += i.RemoteEndPoint.ToString() + "2";
                                    }
                                    AddMessage("20:" + (tempSL).ToString() + temp);
                                    item.Send(Serialize("20:" + (tempSL).ToString() + temp));
                                    Thread.Sleep(100);
                                }
                                Thread.Sleep(100);
                                foreach (Socket item in clientList)
                                {
                                    string temp = clientList[0].RemoteEndPoint.ToString();
                                    AddMessage("98:" + temp);
                                    item.Send(Serialize("98:" + temp));
                                }
                                break;
                            }
                        case "31:":
                            {
                                int sumCai = 0;
                                int slCardCai = 0;
                                int sumUser = 0;
                                int slCardUser = 0;

                                AddMessage("SELECT SUMCARD FROM Client WHERE IP='" + IP + "'");
                                var dap = new SqlDataAdapter("SELECT SUMCARD FROM Client WHERE IP='" + IP + "'", conn);
                                var table = new DataTable();
                                dap.Fill(table);
                                sumCai = Int32.Parse(table.Rows[0][0].ToString());
                                AddMessage("SELECT NUMOFCARD FROM Client WHERE IP='" + IP + "'");
                                dap = new SqlDataAdapter("SELECT NUMOFCARD FROM Client WHERE IP='" + IP + "'", conn);
                                table = new DataTable();
                                dap.Fill(table);
                                slCardCai = Int32.Parse(table.Rows[0][0].ToString());

                                AddMessage("SELECT SUMCARD FROM Client WHERE IP='" + message.Substring(3) + "'");
                                dap = new SqlDataAdapter("SELECT SUMCARD FROM Client WHERE IP='" + message.Substring(3) + "'", conn);
                                table = new DataTable();
                                dap.Fill(table);
                                sumUser = Int32.Parse(table.Rows[0][0].ToString());
                                AddMessage("SELECT NUMOFCARD FROM Client WHERE IP='" + message.Substring(3) + "'");
                                dap = new SqlDataAdapter("SELECT NUMOFCARD FROM Client WHERE IP='" + message.Substring(3) + "'", conn);
                                table = new DataTable();
                                dap.Fill(table);
                                slCardUser = Int32.Parse(table.Rows[0][0].ToString());

                                int ketQua = (KetQua(sumCai, sumUser, slCardCai, slCardUser));
                                if (ketQua == 1)
                                {
                                    AddMessage("61:01");
                                    client.Send(Serialize("61:01"));
                                    foreach (Socket item in clientList)
                                    {
                                        if (item != null && item.RemoteEndPoint.ToString() == message.Substring(3))
                                        {
                                            AddMessage("31:-1");
                                            item.Send(Serialize("31:-1"));
                                        }
                                    }
                                }
                                if (ketQua == 0)
                                {
                                    client.Send(Serialize("61:00"));
                                    foreach (Socket item in clientList)
                                    {
                                        if (item != null && item.RemoteEndPoint.ToString() == message.Substring(3))
                                        {
                                            AddMessage("31:00");
                                            item.Send(Serialize("31:00"));
                                        }
                                    }
                                }
                                if (ketQua == -1)
                                {
                                    AddMessage("61:-1");
                                    client.Send(Serialize("61:-1"));
                                    foreach (Socket item in clientList)
                                    {
                                        if (item != null && item.RemoteEndPoint.ToString() == message.Substring(3))
                                        {
                                            AddMessage("31:01");
                                            item.Send(Serialize("31:01"));
                                        }
                                    }
                                }
                                Thread.Sleep(100);
                                foreach (Socket item in clientList)
                                {
                                    if (item != null && item.RemoteEndPoint.ToString() == message.Substring(3))
                                    {
                                        AddMessage("41:");
                                        item.Send(Serialize("41:"));
                                    }
                                }
                                Thread.Sleep(130);
                                break;
                            }
                        case "32:":
                            {
                                int sumCai = 0;
                                int slCardCai = 0;
                                int sumUser = 0;
                                int slCardUser = 0;

                                AddMessage("SELECT SUMCARD FROM Client WHERE IP='" + IP + "'");
                                var dap = new SqlDataAdapter("SELECT SUMCARD FROM Client WHERE IP='" + IP + "'", conn);
                                var table = new DataTable();
                                dap.Fill(table);
                                sumCai = Int32.Parse(table.Rows[0][0].ToString());
                                AddMessage("SELECT NUMOFCARD FROM Client WHERE IP='" + IP + "'");
                                dap = new SqlDataAdapter("SELECT NUMOFCARD FROM Client WHERE IP='" + IP + "'", conn);
                                table = new DataTable();
                                dap.Fill(table);
                                slCardCai = Int32.Parse(table.Rows[0][0].ToString());

                                AddMessage("SELECT SUMCARD FROM Client WHERE IP='" + message.Substring(3) + "'");
                                dap = new SqlDataAdapter("SELECT SUMCARD FROM Client WHERE IP='" + message.Substring(3) + "'", conn);
                                table = new DataTable();
                                dap.Fill(table);
                                sumUser = Int32.Parse(table.Rows[0][0].ToString());
                                AddMessage("SELECT NUMOFCARD FROM Client WHERE IP='" + message.Substring(3) + "'");
                                dap = new SqlDataAdapter("SELECT NUMOFCARD FROM Client WHERE IP='" + message.Substring(3) + "'", conn);
                                table = new DataTable();
                                dap.Fill(table);
                                slCardUser = Int32.Parse(table.Rows[0][0].ToString());

                                int ketQua = (KetQua(sumCai, sumUser, slCardCai, slCardUser));
                                if (ketQua == 1)
                                {
                                    AddMessage("62:01");
                                    client.Send(Serialize("62:01"));
                                    foreach (Socket item in clientList)
                                    {
                                        if (item != null && item.RemoteEndPoint.ToString() == message.Substring(3))
                                        {
                                            AddMessage("31:-1");
                                            item.Send(Serialize("31:-1"));
                                        }
                                    }
                                }
                                if (ketQua == 0)
                                {
                                    AddMessage("64:00");
                                    client.Send(Serialize("62:00"));
                                    foreach (Socket item in clientList)
                                    {
                                        if (item != null && item.RemoteEndPoint.ToString() == message.Substring(3))
                                        {
                                            AddMessage("31:00");
                                            item.Send(Serialize("31:00"));
                                        }
                                    }
                                }
                                if (ketQua == -1)
                                {
                                    AddMessage("62:-1");
                                    client.Send(Serialize("62:-1"));
                                    foreach (Socket item in clientList)
                                    {
                                        if (item != null && item.RemoteEndPoint.ToString() == message.Substring(3))
                                        {
                                            AddMessage("31:01");
                                            item.Send(Serialize("31:01"));
                                        }
                                    }
                                }

                                Thread.Sleep(100);
                                foreach (Socket item in clientList)
                                {
                                    if (item != null && item.RemoteEndPoint.ToString() == message.Substring(3))
                                    {
                                        AddMessage("42:");
                                        item.Send(Serialize("42:"));
                                    }
                                }
                                Thread.Sleep(130);
                                break;
                            }
                        case "33:":
                            {
                                int sumCai = 0;
                                int slCardCai = 0;
                                int sumUser = 0;
                                int slCardUser = 0;

                                AddMessage("SELECT SUMCARD FROM Client WHERE IP='" + IP + "'");
                                var dap = new SqlDataAdapter("SELECT SUMCARD FROM Client WHERE IP='" + IP + "'", conn);
                                var table = new DataTable();
                                dap.Fill(table);
                                sumCai = Int32.Parse(table.Rows[0][0].ToString());
                                AddMessage("SELECT NUMOFCARD FROM Client WHERE IP='" + IP + "'");
                                dap = new SqlDataAdapter("SELECT NUMOFCARD FROM Client WHERE IP='" + IP + "'", conn);
                                table = new DataTable();
                                dap.Fill(table);
                                slCardCai = Int32.Parse(table.Rows[0][0].ToString());

                                AddMessage("SELECT SUMCARD FROM Client WHERE IP='" + message.Substring(3) + "'");
                                dap = new SqlDataAdapter("SELECT SUMCARD FROM Client WHERE IP='" + message.Substring(3) + "'", conn);
                                table = new DataTable();
                                dap.Fill(table);
                                sumUser = Int32.Parse(table.Rows[0][0].ToString());
                                AddMessage("SELECT NUMOFCARD FROM Client WHERE IP='" + message.Substring(3) + "'");
                                dap = new SqlDataAdapter("SELECT NUMOFCARD FROM Client WHERE IP='" + message.Substring(3) + "'", conn);
                                table = new DataTable();
                                dap.Fill(table);
                                slCardUser = Int32.Parse(table.Rows[0][0].ToString());

                                int ketQua = (KetQua(sumCai, sumUser, slCardCai, slCardUser));
                                if (ketQua == 1)
                                {
                                    AddMessage("63:01");
                                    client.Send(Serialize("63:01"));
                                    foreach (Socket item in clientList)
                                    {
                                        if (item != null && item.RemoteEndPoint.ToString() == message.Substring(3))
                                        {
                                            AddMessage("31:-1");
                                            item.Send(Serialize("31:-1"));
                                        }
                                    }
                                }
                                if (ketQua == 0)
                                {
                                    AddMessage("63:00");
                                    client.Send(Serialize("63:00"));
                                    foreach (Socket item in clientList)
                                    {
                                        if (item != null && item.RemoteEndPoint.ToString() == message.Substring(3))
                                        {
                                            AddMessage("31:00");
                                            item.Send(Serialize("31:00"));
                                        }
                                    }
                                }
                                if (ketQua == -1)
                                {
                                    AddMessage("63:-1");
                                    client.Send(Serialize("63:-1"));
                                    foreach (Socket item in clientList)
                                    {
                                        if (item != null && item.RemoteEndPoint.ToString() == message.Substring(3))
                                        {
                                            AddMessage("31:01");
                                            item.Send(Serialize("31:01"));
                                        }
                                    }
                                }

                                Thread.Sleep(100);
                                foreach (Socket item in clientList)
                                {
                                    if (item != null && item.RemoteEndPoint.ToString() == message.Substring(3))
                                    {
                                        AddMessage("43:");
                                        item.Send(Serialize("43:"));
                                    }
                                }
                                Thread.Sleep(130);
                                break;
                            }
                        case "34:":
                            {
                                int sumCai = 0;
                                int slCardCai = 0;
                                int sumUser = 0;
                                int slCardUser = 0;

                                AddMessage("SELECT SUMCARD FROM Client WHERE IP='" + IP + "'");
                                var dap = new SqlDataAdapter("SELECT SUMCARD FROM Client WHERE IP='" + IP + "'", conn);
                                var table = new DataTable();
                                dap.Fill(table);
                                sumCai = Int32.Parse(table.Rows[0][0].ToString());
                                AddMessage("SELECT NUMOFCARD FROM Client WHERE IP='" + IP + "'");
                                dap = new SqlDataAdapter("SELECT NUMOFCARD FROM Client WHERE IP='" + IP + "'", conn);
                                table = new DataTable();
                                dap.Fill(table);
                                slCardCai = Int32.Parse(table.Rows[0][0].ToString());

                                AddMessage("SELECT SUMCARD FROM Client WHERE IP='" + message.Substring(3) + "'");
                                dap = new SqlDataAdapter("SELECT SUMCARD FROM Client WHERE IP='" + message.Substring(3) + "'", conn);
                                table = new DataTable();
                                dap.Fill(table);
                                sumUser = Int32.Parse(table.Rows[0][0].ToString());
                                AddMessage("SELECT NUMOFCARD FROM Client WHERE IP = '" + message.Substring(3) + "'");
                                dap = new SqlDataAdapter("SELECT NUMOFCARD FROM Client WHERE IP='" + message.Substring(3) + "'", conn);
                                table = new DataTable();
                                dap.Fill(table);
                                slCardUser = Int32.Parse(table.Rows[0][0].ToString());

                                int ketQua = (KetQua(sumCai, sumUser, slCardCai, slCardUser));
                                if (ketQua == 1)
                                {
                                    AddMessage("64:01");
                                    client.Send(Serialize("64:01"));
                                    foreach (Socket item in clientList)
                                    {
                                        if (item != null && item.RemoteEndPoint.ToString() == message.Substring(3))
                                        {
                                            AddMessage("31:-1");
                                            item.Send(Serialize("31:-1"));
                                        }
                                    }
                                }
                                if (ketQua == 0)
                                {
                                    AddMessage("64:00");
                                    client.Send(Serialize("64:00"));
                                    foreach (Socket item in clientList)
                                    {
                                        if (item != null && item.RemoteEndPoint.ToString() == message.Substring(3))
                                        {
                                            AddMessage("31:00");
                                            item.Send(Serialize("31:00"));
                                        }
                                    }
                                }
                                if (ketQua == -1)
                                {
                                    AddMessage("64:-1");
                                    client.Send(Serialize("64:-1"));
                                    foreach (Socket item in clientList)
                                    {
                                        if (item != null && item.RemoteEndPoint.ToString() == message.Substring(3))
                                        {
                                            AddMessage("31:01");
                                            item.Send(Serialize("31:01"));
                                        }
                                    }
                                }

                                Thread.Sleep(100);
                                foreach (Socket item in clientList)
                                {
                                    if (item != null && item.RemoteEndPoint.ToString() == message.Substring(3))
                                    {
                                        AddMessage("44:");
                                        item.Send(Serialize("44:"));
                                    }
                                }
                                Thread.Sleep(130);
                                break;
                            }
                        case "35:":
                            {
                                tempCai = message.Substring(3);
                                AddMessage(tempCai);
                                break;
                            }
                        case "36:":
                            {
                                client.Send(Serialize("36:"+ tempCai));
                                string temp = IP + message.Substring(3);
                                foreach (Socket item in clientList)
                                {
                                    if( item!= null && item != clientList[0] && item!= client)
                                    {
                                        item.Send(Serialize("55:"+ temp));
                                    }
                                }
                                break;
                            }
                        case "41:":
                            {
                                AddMessage("51:" + message.Substring(3));
                                clientList[0].Send(Serialize("51:" + message.Substring(3)));
                                    break;
                            }
                        case "42:":
                            {
                                AddMessage("51:" + message.Substring(3));
                                clientList[0].Send(Serialize("52:" + message.Substring(3)));
                                
                                break;
                            }
                        case "43:":
                            {
                                AddMessage("51:" + message.Substring(3));
                                clientList[0].Send(Serialize("53:" + message.Substring(3)));
                                
                                break;
                            }
                        case "44:":
                            {
                                AddMessage("51:" + message.Substring(3));
                                clientList[0].Send(Serialize("54:" + message.Substring(3)));
                                break;
                            }
                        default:
                            break;
                    }
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

        public int KetQua(int com, int user, int numCom, int numUser)
        {
            if (numCom == 5 && numUser == 5 && com <= 21 && user <= 21)
                return 0;
            else if (numCom == 5 && numUser != 5 && com <= 21)
                return 1;
            else if (numCom != 5 && numUser == 5 && user <= 21)
                return -1;
            else if (com == 21 && user == 21)
                return 0;
            else if (com == 21 && user != 21)
                return 1;
            else if (com != 21 && user == 21)
                return -1;
            else if (com > 21 && user > 21)
                return 0;
            else if (com > 21 && user < 21)
                return -1;
            else if (21 >= com && 21 >= user && com < user)
                return -1;
            else if (com < 21 && user > 21)
                return 1;
            else if (21 >= com && 21 >= user && com > user)
                return 1;
            else if (21 >= com && 21 >= user && com == user)
                return 0;
            return 0;
        }
    }
}
