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
    public partial class Multi_Client : Form
    {
        public Multi_Client()
        {
            InitializeComponent();
            Connect();
        }
          

        private void btnSend_Click(object sender, EventArgs e)
        {
            Send();
            if ((txtName.Text == string.Empty))
            {
                return;
            }
        }
        //kết nối

        Player user;
        
        IPEndPoint IP;
        Socket client;
        bool isChat = false;
        int tempPlayer;
        void Connect()  
        {
            user = new Player();
            user.setLoaiNguoi(1);

            IP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            client.Connect(IP);
            


            try
            {
                client.Connect(IP);
            }
            catch
            {
            }

            Thread listen = new Thread(Receive);
            
            listen.IsBackground = true;
            listen.Start();
        }
        //đống kết nối
        void Close()
        {
            client.Close();
        }
        //gởi tin
        void Send()
        {
            if ((txtMess.Text != string.Empty) || (txtName.Text != string.Empty))
            {
                if ((txtName.Text == string.Empty))
                {
                    MessageBox.Show("vui lòng nhập tên");
                    return;
                }
                string temp = ("01:"+txtName.Text + ": " + txtMess.Text).ToString();
                client.Send(Serialize(temp));
            }
            else if(txtMess.Visible==false)
            {
                string temp = "00:";
                client.Send(Serialize(temp));
            }
        }
        //nhận tin
        void Receive()
        {
            while (true)
            {
                try
                {
                   
                    byte[] data = new byte[1024 * 5000];
                    client.Receive(data);

                    string message = Deserialize(data).ToString();

                    switch (message.Substring(0, 3))
                    {
                        case "00:":
                            {
                                this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                                {
                                    waitingLoading.Visible = false;
                                    listMess.Visible = true;
                                    label1.Visible = true;
                                    label2.Visible = true;
                                    txtMess.Visible = true;
                                    txtName.Visible = true;
                                    btnSend.Visible = true;
                                    btnDan.Visible = false;
                                    label4.Visible = false;
                                });
                                break;
                            }
                        case "01:":
                            {
                                AddMessage(message.Substring(3));
                                break;
                            }
                        case "02:":   //2: wait
                            {
                                this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                                {
                                    waitingLoading.Visible=true;
                                });
                               
                                break;
                            }
                        case "03:":
                            {
                                this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                                {
                                    waitingLoading.Visible = false;
                                    btnRut.Visible = true;
                                    label4.Visible = true;
                                    if (Int32.Parse(label4.Text.Trim()) >= 16)
                                        btnDan.Visible = true;

                                });
                                break;
                            }
                        
                        case "94:":
                            {
                                Card card = new Card();
                                card.AddIdCard(message.Substring(3));
                                user.addCard(card);
                                DrawCard(card, user.getNumberCard(1));
                                client.Send(Serialize("11:2"));
                                this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                                {
                                    label4.Text = (user.getSum()).ToString();
                                    waitingLoading.Visible = true;
                                });
                               
                                break;
                            }
                        case "98:":
                            {
                                if (txtPlayer1.Text == message.Substring(3))
                                    this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                                    {
                                        pBc1.Visible = true;
                                    });
                                else if (txtPlayer2.Text == message.Substring(3))
                                    this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                                    {
                                        pBc2.Visible = true;
                                    });
                                else if (txtPlayer3.Text == message.Substring(3))
                                    this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                                    {
                                        pBc3.Visible = true;
                                    });
                                else if (txtPlayer4.Text == message.Substring(3))
                                    this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                                    {
                                        pBc4.Visible = true;
                                    });
                                break;
                            }
                        case "04:":
                            {
                                Card card = new Card();
                                card.AddIdCard(message.Substring(3));
                                user.addCard(card);
                                DrawCard(card, user.getNumberCard(1));
                                client.Send(Serialize("11:" + user.getNumberCard().ToString()));
                                this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                                {
                                    label4.Text = (user.getSum()).ToString();
                                    if (Int32.Parse(label4.Text.Trim()) >= 16 || user.getNumberCard()==5)
                                        btnDan.Visible = true;
                                });
                                
                                break;
                            }
                        case "09:":
                            {
                                AddMessage(message.Substring(3));
                                this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                                {
                                    waitingLoading.Visible = false;
                                    btnDan.Visible = true;
                                });
                                client.Send(Serialize("20:"));
                                break;
                            }
                        case "20:":
                            {
                                int temp = Int32.Parse(message.Substring(3,1));
                                string temp2 = message.Substring(4);
                                this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                                {
                                    if (temp >= 2)
                                    {
                                        player1.Visible = true;
                                        txtPlayer1.Visible = true;
                                        txtPlayer1.Text = temp2.Substring(0, 15);
                                        pbplayer11.Visible = true;
                                        pbplayer12.Visible = true;
                                    }
                                    if (temp >= 3)
                                    {
                                        temp2 = temp2.Substring(16);
                                        player2.Visible = true;
                                        txtPlayer2.Visible = true;
                                        txtPlayer2.Text = temp2.Substring(0, 15);
                                        pbplayer21.Visible = true;
                                        pbplayer22.Visible = true;
                                    }
                                    if (temp >= 4)
                                    {
                                        temp2 = temp2.Substring(16);
                                        player3.Visible = true;
                                        txtPlayer3.Visible = true;
                                        txtPlayer3.Text = temp2.Substring(0, 15);
                                        pbplayer31.Visible = true;
                                        pbplayer32.Visible = true;
                                    }
                                    if (temp >= 5)
                                    {
                                        temp2 = temp2.Substring(16);
                                        player4.Visible = true;
                                        txtPlayer4.Visible = true;
                                        txtPlayer4.Text = temp2.Substring(0, 15);
                                        pbplayer41.Visible = true;
                                        pbplayer42.Visible = true;
                                    }
                                });
                                break;
                            }
                        case "21:":
                            {
                                string temp = message.Substring(3);
                                this.Invoke((MethodInvoker)delegate {
                                    if (temp != null)
                                    {
                                        if (temp.Substring(0,15) == txtPlayer1.Text)
                                        {
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 3)
                                                pbplayer13.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 4)
                                                pbplayer14.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 5)
                                                pbplayer15.Visible = true;
                                        }
                                        else if (temp.Substring(0, 15) == txtPlayer2.Text)
                                        {
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 3)
                                                pbplayer23.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 4)
                                                pbplayer24.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 5)
                                                pbplayer25.Visible = true;
                                        }
                                        else if (temp.Substring(0, 15) == txtPlayer3.Text)
                                        {
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 3)
                                                pbplayer33.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 4)
                                                pbplayer34.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 5)
                                                pbplayer35.Visible = true;
                                        }
                                        else if (temp.Substring(0, 15) == txtPlayer4.Text)
                                        {
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 3)
                                                pbplayer43.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 4)
                                                pbplayer44.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 5)
                                                pbplayer45.Visible = true;
                                        }
                                    }
                                    if (temp.Substring(16)!="")
                                    {
                                        temp = temp.Substring(16);
                                        if (temp.Substring(0, 15) == txtPlayer1.Text)
                                        {
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 3)
                                                pbplayer13.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 4)
                                                pbplayer14.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 5)
                                                pbplayer15.Visible = true;
                                        }
                                        else if (temp.Substring(0, 15) == txtPlayer2.Text)
                                        {
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 3)
                                                pbplayer23.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 4)
                                                pbplayer24.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 5)
                                                pbplayer25.Visible = true;
                                        }
                                        else if (temp.Substring(0, 15) == txtPlayer3.Text)
                                        {
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 3)
                                                pbplayer33.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 4)
                                                pbplayer34.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 5)
                                                pbplayer35.Visible = true;
                                        }
                                        else if (temp.Substring(0, 15) == txtPlayer4.Text)
                                        {
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 3)
                                                pbplayer43.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 4)
                                                pbplayer44.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 5)
                                                pbplayer45.Visible = true;
                                        }
                                    }
                                    if (temp.Substring(16) != "")
                                    {
                                        temp = temp.Substring(16);
                                        if (temp.Substring(0, 15) == txtPlayer1.Text)
                                        {
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 3)
                                                pbplayer13.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 4)
                                                pbplayer14.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 5)
                                                pbplayer15.Visible = true;
                                        }
                                        else if (temp.Substring(0, 15) == txtPlayer2.Text)
                                        {
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 3)
                                                pbplayer23.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 4)
                                                pbplayer24.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 5)
                                                pbplayer25.Visible = true;
                                        }
                                        else if (temp.Substring(0, 15) == txtPlayer3.Text)
                                        {
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 3)
                                                pbplayer33.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 4)
                                                pbplayer34.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 5)
                                                pbplayer35.Visible = true;
                                        }
                                        else if (temp.Substring(0, 15) == txtPlayer4.Text)
                                        {
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 3)
                                                pbplayer43.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 4)
                                                pbplayer44.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 5)
                                                pbplayer45.Visible = true;
                                        }
                                    }
                                    if (temp.Substring(16) != "")
                                    {
                                        temp = temp.Substring(16);
                                        if (temp.Substring(0, 15) == txtPlayer1.Text)
                                        {
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 3)
                                                pbplayer13.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 4)
                                                pbplayer14.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 5)
                                                pbplayer15.Visible = true;
                                        }
                                        else if (temp.Substring(0, 15) == txtPlayer2.Text)
                                        {
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 3)
                                                pbplayer23.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 4)
                                                pbplayer24.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 5)
                                                pbplayer25.Visible = true;
                                        }
                                        else if (temp.Substring(0, 15) == txtPlayer3.Text)
                                        {
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 3)
                                                pbplayer33.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 4)
                                                pbplayer34.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 5)
                                                pbplayer35.Visible = true;
                                        }
                                        else if (temp.Substring(0, 15) == txtPlayer4.Text)
                                        {
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 3)
                                                pbplayer43.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 4)
                                                pbplayer44.Visible = true;
                                            if (Int32.Parse(temp.Substring(15, 1)) >= 5)
                                                pbplayer45.Visible = true;
                                        }
                                    }
                                });
                                
                                break;
                            }
                        case "30:":
                            {
                                this.Invoke((MethodInvoker)delegate {
                                    btnRut.Visible = true;
                                    label4.Visible = true;
                                    waitingLoading.Visible = false;
                                    if (txtPlayer1.Text != "")
                                        ckplayer1.Visible = true;
                                    if (txtPlayer2.Text != "")
                                        ckplayer2.Visible = true;
                                    if (txtPlayer3.Text != "")
                                        ckplayer3.Visible = true;
                                    if (txtPlayer4.Text != "")
                                        ckplayer4.Visible = true;
                                });
                                    break;
                            }
                        case "31:":
                            {
                                string temp = message.Substring(3);
                                this.Invoke((MethodInvoker)delegate {
                                    waitingLoading.Visible = false;
                                    ketQua.Visible = true;
                                    if (temp == "01")
                                    {
                                        ketQua.Text = "Thắng";
                                    }
                                    if (temp == "00")
                                    {
                                        ketQua.Text = "Hòa";
                                    }
                                    if (temp == "-1")
                                    {
                                        ketQua.Text = "Thua";
                                    }
                                });
                                temp = user.getAllCard();
                                client.Send(Serialize("36:"+temp));
                                break;
                            }
                        case "36:":
                            {
                                string temp = message.Substring(3);
                                Card cards = new Card();
                                int i = 1;
                                while (temp != "")
                                {
                                    cards.AddIdCard(temp.Substring(0, 2));
                                    DrawCardCai(cards, i, 1);
                                    i++;
                                    temp = temp.Substring(2);
                                }
                                break;
                            }
                        case "41:":
                            {
                                string temp = user.getAllCard();
                                client.Send(Serialize("41:"+ temp));
                                break;
                            }
                        case "42:":
                            {
                                string temp = user.getAllCard();
                                client.Send(Serialize("42:" + temp));
                                break;
                            }
                        case "43:":
                            {
                                string temp = user.getAllCard();
                                client.Send(Serialize("43:" + temp)) ;
                                break;
                            }
                        case "44:":
                            {
                                string temp = user.getAllCard();
                                client.Send(Serialize("44:" + temp));
                                break;
                            }
                        case "51:":
                            {
                                string temp = message.Substring(3);
                                Card cards = new Card();
                                int i = 1;
                                while (temp != "")
                                {
                                    cards.AddIdCard(temp.Substring(0, 2));
                                    DrawCardCai(cards, i, 1);
                                    i++;
                                    temp = temp.Substring(2);
                                }
                                break;
                            }
                        case "52:":
                            {
                                string temp = message.Substring(3);
                                int i = 1;
                                Card card = new Card();
                                while (temp != "")
                                {
                                    card.AddIdCard(temp.Substring(0, 2));
                                    DrawCardCai(card, i, 2);
                                    i++;
                                    temp = temp.Substring(2);
                                }
                                break;
                            }
                        case "53:":
                            {
                                string temp = message.Substring(3);
                                int i = 1;
                                Card card = new Card();
                                while (temp != "")
                                {
                                    card.AddIdCard(temp.Substring(0, 2));
                                    DrawCardCai(card, i, 3);
                                    i++;
                                    temp = temp.Substring(2);
                                }
                                break;
                            }
                        case "54:":
                            {
                                string temp = message.Substring(3);
                                int i = 1;
                                Card card = new Card();
                                while (temp != "")
                                {
                                    card.AddIdCard(temp.Substring(0, 2));
                                    DrawCardCai(card, i, 4);
                                    i++;
                                    temp = temp.Substring(2);
                                }
                                break;
                            }
                        case "55:":
                            {
                                string temp = message.Substring(3);
                                int i = 1;
                                Card card = new Card();
                                if (temp != null)
                                {
                                    if (temp.Substring(0, 15) == txtPlayer1.Text)
                                    {
                                        temp = temp.Substring(15);
                                        while (temp != "")
                                        {
                                            card.AddIdCard(temp.Substring(0, 2));
                                            DrawCardCai(card, i, 1);
                                            i++;
                                            temp = temp.Substring(2);
                                        }
                                    }
                                    else if (temp.Substring(0, 15) == txtPlayer2.Text)
                                    {
                                        temp = temp.Substring(15);
                                        while (temp != "")
                                        {
                                            card.AddIdCard(temp.Substring(0, 2));
                                            DrawCardCai(card, i, 2);
                                            i++;
                                            temp = temp.Substring(2);
                                        }
                                    }
                                    else if (temp.Substring(0, 15) == txtPlayer3.Text)
                                    {
                                        temp = temp.Substring(15);
                                        while (temp != "")
                                        {
                                            card.AddIdCard(temp.Substring(0, 2));
                                            DrawCardCai(card, i, 3);
                                            i++;
                                            temp = temp.Substring(2);
                                        }
                                    }
                                    else if (temp.Substring(0, 15) == txtPlayer4.Text)
                                    {
                                        temp = temp.Substring(15);
                                        while (temp != "")
                                        {
                                            card.AddIdCard(temp.Substring(0, 2));
                                            DrawCardCai(card, i, 4);
                                            i++;
                                            temp = temp.Substring(2);
                                        }
                                    }
                                }
                                break;
                            }
                        case "61:":
                            {
                                string temp = message.Substring(3);
                                this.Invoke((MethodInvoker)delegate {
                                    ckplayer1.Visible = false;
                                    ketQuaPlayer1.Visible = true;
                                    if (temp == "01")
                                    {
                                        ketQuaPlayer1.Text = "Thắng";
                                    }
                                    if (temp == "00")
                                    {
                                        ketQuaPlayer1.Text = "Hòa";
                                    }
                                    if (temp == "-1")
                                    {
                                        ketQuaPlayer1.Text = "Thua";
                                    }
                                });
                                
                                break;
                            }
                        case "62:":
                            {
                                this.Invoke((MethodInvoker)delegate {
                                    ckplayer2.Visible = false;
                                    ketQuaPlayer2.Visible = true;
                                    if (message.Substring(3) == "01")
                                    {
                                        ketQuaPlayer2.Text = "Thắng";
                                    }
                                    if (message.Substring(3) == "00")
                                    {
                                        ketQuaPlayer2.Text = "Hòa";
                                    }
                                    if (message.Substring(3) == "-1")
                                    {
                                        ketQuaPlayer2.Text = "Thua";
                                    }
                                });
                                break;
                            }
                        case "63:":
                            {
                                this.Invoke((MethodInvoker)delegate {
                                    ckplayer3.Visible = false;
                                    ketQuaPlayer3.Visible = true;
                                    if (message.Substring(3) == "01")
                                    {
                                        ketQuaPlayer3.Text = "Thắng";
                                    }
                                    if (message.Substring(3) == "00")
                                    {
                                        ketQuaPlayer3.Text = "Hòa";
                                    }
                                    if (message.Substring(3) == "-1")
                                    {
                                        ketQuaPlayer3.Text = "Thua";
                                    }
                                });
                                break;
                            }
                        case "64:":
                            {
                                this.Invoke((MethodInvoker)delegate {
                                    ckplayer4.Visible = false;
                                    ketQuaPlayer4.Visible = true;
                                    if (message.Substring(3) == "01")
                                    {
                                        ketQuaPlayer4.Text = "Thắng";
                                    }
                                    if (message.Substring(3) == "00")
                                    {
                                        ketQuaPlayer4.Text = "Hòa";
                                    }
                                    if (message.Substring(3) == "-1")
                                    {
                                        ketQuaPlayer4.Text = "Thua";
                                    }
                                });
                                break;
                            }
                        default:
                            break;
                    }
                }
                catch
                {
                    client.Close();
                }
            }

        }
        void AddMessage(string s)
        {
            listMess.Text += (s + "\n").ToString();
            txtMess.Clear();
        }
        byte[] Serialize(   object obj)
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
        private void Client_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Send();
            this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
            {
                waitingLoading.Visible = true;
                label3.Visible = false;
                label4.Visible = false;
                btnDan.Visible = false;
            });
        }
        private void DrawCard(Card card, int num)
        {
            PictureBox pb = new PictureBox();
            pb.Visible = false;
            switch (card.getIdCard())
            {
                case "1C":
                    pb.Image = BlackJack.Properties.Resources._1C;
                    break;
                case "1R":
                    pb.Image = BlackJack.Properties.Resources._1R;
                    break;
                case "1H":
                    pb.Image = BlackJack.Properties.Resources._1H;
                    break;
                case "1B":
                    pb.Image = BlackJack.Properties.Resources._1B;
                    break;
                case "2C":
                    pb.Image = BlackJack.Properties.Resources._2C;
                    break;
                case "2R":
                    pb.Image = BlackJack.Properties.Resources._2R;
                    break;
                case "2H":
                    pb.Image = BlackJack.Properties.Resources._2H;
                    break;
                case "2B":
                    pb.Image = BlackJack.Properties.Resources._2B;
                    break;
                case "3C":
                    pb.Image = BlackJack.Properties.Resources._3C;
                    break;
                case "3R":
                    pb.Image = BlackJack.Properties.Resources._3R;
                    break;
                case "3H":
                    pb.Image = BlackJack.Properties.Resources._3H;
                    break;
                case "3B":
                    pb.Image = BlackJack.Properties.Resources._3B;
                    break;
                case "4C":
                    pb.Image = BlackJack.Properties.Resources._4C;
                    break;
                case "4R":
                    pb.Image = BlackJack.Properties.Resources._4R;
                    break;
                case "4H":
                    pb.Image = BlackJack.Properties.Resources._4H;
                    break;
                case "4B":
                    pb.Image = BlackJack.Properties.Resources._4B;
                    break;
                case "5C":
                    pb.Image = BlackJack.Properties.Resources._5C;
                    break;
                case "5R":
                    pb.Image = BlackJack.Properties.Resources._5R;
                    break;
                case "5H":
                    pb.Image = BlackJack.Properties.Resources._5H;
                    break;
                case "5B":
                    pb.Image = BlackJack.Properties.Resources._5B;
                    break;
                case "6C":
                    pb.Image = BlackJack.Properties.Resources._6C;
                    break;
                case "6R":
                    pb.Image = BlackJack.Properties.Resources._6R;
                    break;
                case "6H":
                    pb.Image = BlackJack.Properties.Resources._6H;
                    break;
                case "6B":
                    pb.Image = BlackJack.Properties.Resources._6B;
                    break;
                case "7C":
                    pb.Image = BlackJack.Properties.Resources._7C;
                    break;
                case "7R":
                    pb.Image = BlackJack.Properties.Resources._7R;
                    break;
                case "7H":
                    pb.Image = BlackJack.Properties.Resources._7H;
                    break;
                case "7B":
                    pb.Image = BlackJack.Properties.Resources._7B;
                    break;
                case "8C":
                    pb.Image = BlackJack.Properties.Resources._8C;
                    break;
                case "8R":
                    pb.Image = BlackJack.Properties.Resources._8R;
                    break;
                case "8H":
                    pb.Image = BlackJack.Properties.Resources._8H;
                    break;
                case "8B":
                    pb.Image = BlackJack.Properties.Resources._8B;
                    break;
                case "9C":
                    pb.Image = BlackJack.Properties.Resources._9C;
                    break;
                case "9R":
                    pb.Image = BlackJack.Properties.Resources._9R;
                    break;
                case "9H":
                    pb.Image = BlackJack.Properties.Resources._9H;
                    break;
                case "9B":
                    pb.Image = BlackJack.Properties.Resources._9B;
                    break;
                case "10C":
                    pb.Image = BlackJack.Properties.Resources._10C;
                    break;
                case "10R":
                    pb.Image = BlackJack.Properties.Resources._10R;
                    break;
                case "10H":
                    pb.Image = BlackJack.Properties.Resources._10H;
                    break;
                case "10B":
                    pb.Image = BlackJack.Properties.Resources._10B;
                    break;
                case "JC":
                    pb.Image = BlackJack.Properties.Resources.JC;
                    break;
                case "JR":
                    pb.Image = BlackJack.Properties.Resources.JR;
                    break;
                case "JH":
                    pb.Image = BlackJack.Properties.Resources.JH;
                    break;
                case "JB":
                    pb.Image = BlackJack.Properties.Resources.JB;
                    break;
                case "QC":
                    pb.Image = BlackJack.Properties.Resources.QC;
                    break;
                case "QR":
                    pb.Image = BlackJack.Properties.Resources.QR;
                    break;
                case "QH":
                    pb.Image = BlackJack.Properties.Resources.QH;
                    break;
                case "QB":
                    pb.Image = BlackJack.Properties.Resources.QB;
                    break;
                case "KC":
                    pb.Image = BlackJack.Properties.Resources.KC;
                    break;
                case "KR":
                    pb.Image = BlackJack.Properties.Resources.KR;
                    break;
                case "KH":
                    pb.Image = BlackJack.Properties.Resources.KH;
                    break;
                case "KB":
                    pb.Image = BlackJack.Properties.Resources.KB;
                    break;
                default:
                    pb.Image = BlackJack.Properties.Resources.PP;
                    break;
            }
            switch (num)
            {
                case 1:
                    this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                    {
                        pictureBox6.Image = pb.Image;
                        pictureBox6.Visible = true;
                    });
                    break;
                case 2:
                    this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                    {
                        pictureBox7.Image = pb.Image;
                        pictureBox7.Visible = true;
                    });
                    break;
                case 3:
                    this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                    {
                        pictureBox8.Image = pb.Image;
                        pictureBox8.Visible = true;
                    });
                    break;
                case 4:
                    this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                    {
                        pictureBox9.Image = pb.Image;
                        pictureBox9.Visible = true;
                    });
                    break;
                case 5:
                    this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                    {
                        pictureBox10.Image = pb.Image;
                        pictureBox10.Visible = true;
                    });
                    break;
            }
        }
        private void DrawCardCai(Card card, int num, int player)
        {
            PictureBox pb = new PictureBox();
            switch (card.getIdCard())
            {
                case "1C":
                    pb.Image = BlackJack.Properties.Resources._1C;
                    break;
                case "1R":
                    pb.Image = BlackJack.Properties.Resources._1R;
                    break;
                case "1H":
                    pb.Image = BlackJack.Properties.Resources._1H;
                    break;
                case "1B":
                    pb.Image = BlackJack.Properties.Resources._1B;
                    break;
                case "2C":
                    pb.Image = BlackJack.Properties.Resources._2C;
                    break;
                case "2R":
                    pb.Image = BlackJack.Properties.Resources._2R;
                    break;
                case "2H":
                    pb.Image = BlackJack.Properties.Resources._2H;
                    break;
                case "2B":
                    pb.Image = BlackJack.Properties.Resources._2B;
                    break;
                case "3C":
                    pb.Image = BlackJack.Properties.Resources._3C;
                    break;
                case "3R":
                    pb.Image = BlackJack.Properties.Resources._3R;
                    break;
                case "3H":
                    pb.Image = BlackJack.Properties.Resources._3H;
                    break;
                case "3B":
                    pb.Image = BlackJack.Properties.Resources._3B;
                    break;
                case "4C":
                    pb.Image = BlackJack.Properties.Resources._4C;
                    break;
                case "4R":
                    pb.Image = BlackJack.Properties.Resources._4R;
                    break;
                case "4H":
                    pb.Image = BlackJack.Properties.Resources._4H;
                    break;
                case "4B":
                    pb.Image = BlackJack.Properties.Resources._4B;
                    break;
                case "5C":
                    pb.Image = BlackJack.Properties.Resources._5C;
                    break;
                case "5R":
                    pb.Image = BlackJack.Properties.Resources._5R;
                    break;
                case "5H":
                    pb.Image = BlackJack.Properties.Resources._5H;
                    break;
                case "5B":
                    pb.Image = BlackJack.Properties.Resources._5B;
                    break;
                case "6C":
                    pb.Image = BlackJack.Properties.Resources._6C;
                    break;
                case "6R":
                    pb.Image = BlackJack.Properties.Resources._6R;
                    break;
                case "6H":
                    pb.Image = BlackJack.Properties.Resources._6H;
                    break;
                case "6B":
                    pb.Image = BlackJack.Properties.Resources._6B;
                    break;
                case "7C":
                    pb.Image = BlackJack.Properties.Resources._7C;
                    break;
                case "7R":
                    pb.Image = BlackJack.Properties.Resources._7R;
                    break;
                case "7H":
                    pb.Image = BlackJack.Properties.Resources._7H;
                    break;
                case "7B":
                    pb.Image = BlackJack.Properties.Resources._7B;
                    break;
                case "8C":
                    pb.Image = BlackJack.Properties.Resources._8C;
                    break;
                case "8R":
                    pb.Image = BlackJack.Properties.Resources._8R;
                    break;
                case "8H":
                    pb.Image = BlackJack.Properties.Resources._8H;
                    break;
                case "8B":
                    pb.Image = BlackJack.Properties.Resources._8B;
                    break;
                case "9C":
                    pb.Image = BlackJack.Properties.Resources._9C;
                    break;
                case "9R":
                    pb.Image = BlackJack.Properties.Resources._9R;
                    break;
                case "9H":
                    pb.Image = BlackJack.Properties.Resources._9H;
                    break;
                case "9B":
                    pb.Image = BlackJack.Properties.Resources._9B;
                    break;
                case "10C":
                    pb.Image = BlackJack.Properties.Resources._10C;
                    break;
                case "10R":
                    pb.Image = BlackJack.Properties.Resources._10R;
                    break;
                case "10H":
                    pb.Image = BlackJack.Properties.Resources._10H;
                    break;
                case "10B":
                    pb.Image = BlackJack.Properties.Resources._10B;
                    break;
                case "JC":
                    pb.Image = BlackJack.Properties.Resources.JC;
                    break;
                case "JR":
                    pb.Image = BlackJack.Properties.Resources.JR;
                    break;
                case "JH":
                    pb.Image = BlackJack.Properties.Resources.JH;
                    break;
                case "JB":
                    pb.Image = BlackJack.Properties.Resources.JB;
                    break;
                case "QC":
                    pb.Image = BlackJack.Properties.Resources.QC;
                    break;
                case "QR":
                    pb.Image = BlackJack.Properties.Resources.QR;
                    break;
                case "QH":
                    pb.Image = BlackJack.Properties.Resources.QH;
                    break;
                case "QB":
                    pb.Image = BlackJack.Properties.Resources.QB;
                    break;
                case "KC":
                    pb.Image = BlackJack.Properties.Resources.KC;
                    break;
                case "KR":
                    pb.Image = BlackJack.Properties.Resources.KR;
                    break;
                case "KH":
                    pb.Image = BlackJack.Properties.Resources.KH;
                    break;
                case "KB":
                    pb.Image = BlackJack.Properties.Resources.KB;
                    break;
                default:
                    pb.Image = BlackJack.Properties.Resources.PP;
                    break;
            }
            if (player == 1)
                switch (num)
                {
                    case 1:
                        this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                        {
                            pbplayer11.Image = pb.Image;
                        });
                        break;
                    case 2:
                        this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                        {
                            pbplayer12.Image = pb.Image;
                        });
                        break;
                    case 3:
                        this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                        {
                            pbplayer13.Image = pb.Image;
                        });
                        break;
                    case 4:
                        this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                        {
                            pbplayer14.Image = pb.Image;
                        });
                        break;
                    case 5:
                        this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                        {
                            pbplayer15.Image = pb.Image;
                        });
                        break;

                }
            else if (player == 2)
                switch (num)
                {
                    case 1:
                        this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                        {
                            pbplayer21.Image = pb.Image;
                        });
                        break;
                    case 2:
                        this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                        {
                            pbplayer22.Image = pb.Image;
                        });
                        break;
                    case 3:
                        this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                        {
                            pbplayer23.Image = pb.Image;
                        });
                        break;
                    case 4:
                        this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                        {
                            pbplayer24.Image = pb.Image;
                        });
                        break;
                    case 5:
                        this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                        {
                            pbplayer25.Image = pb.Image;
                        });
                        break;
                }
            else if (player == 3)
                switch (num)
                {
                    case 1:
                        this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                        {
                            pbplayer31.Image = pb.Image;
                        });
                        break;
                    case 2:
                        this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                        {
                            pbplayer32.Image = pb.Image;
                        });
                        break;
                    case 3:
                        this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                        {
                            pbplayer33.Image = pb.Image;
                        });
                        break;
                    case 4:
                        this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                        {
                            pbplayer34.Image = pb.Image;
                        });
                        break;
                    case 5:
                        this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                        {
                            pbplayer35.Image = pb.Image;
                        });
                        break;
                }
            else if (player == 4)
                switch (num)
                {
                    case 1:
                        this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                        {
                            pbplayer41.Image = pb.Image;
                        });
                        break;
                    case 2:
                        this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                        {
                            pbplayer42.Image = pb.Image;
                        });
                        break;
                    case 3:
                        this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                        {
                            pbplayer43.Image = pb.Image;
                        });
                        break;
                    case 4:
                        this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                        {
                            pbplayer44.Image = pb.Image;
                        });
                        break;
                    case 5:
                        this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                        {
                            pbplayer45.Image = pb.Image;
                        });
                        break;
                }

        }
        private void btnRut_Click(object sender, EventArgs e)
        {
            client.Send(Serialize("04:"));
            this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
            {
                if (user.getNumberCard() == 4)
                {
                        btnRut.Visible = false;
                }
            });
        }

        private void btnDan_Click(object sender, EventArgs e)
        {
            client.Send(Serialize("05:" + user.getSum().ToString()));
            client.Send(Serialize("12:"));
            this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
            {
                waitingLoading.Visible = true;
                btnRut.Visible = false;
                btnDan.Visible = false;
            });
           
        }

        private void ckplayer1_Click(object sender, EventArgs e)
        {
            string text = txtPlayer1.Text;
            client.Send(Serialize("31:" + text));
            Thread.Sleep(20);
            text =  user.getAllCard();
            client.Send(Serialize("35:"+text));
        }

        private void ckplayer2_Click(object sender, EventArgs e)
        {
            string text = txtPlayer2.Text;
            client.Send(Serialize("32:" + text));
            Thread.Sleep(20);
            text = user.getAllCard();
            client.Send(Serialize("35:" + text));
        }

        private void ckplayer3_Click(object sender, EventArgs e)
        {
            string text = txtPlayer3.Text;
            client.Send(Serialize("33:" + text));
            Thread.Sleep(20);
            text = user.getAllCard();
            client.Send(Serialize("35:" + text));
        }

        private void ckplayer4_Click(object sender, EventArgs e)
        {
            string text = txtPlayer4.Text;
            client.Send(Serialize("34:" + text));
            Thread.Sleep(20);
            text = user.getAllCard();
            client.Send(Serialize("35:" + text));
        }
    }
}
