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
            AddMessage((txtName.Text + ": " + txtMess.Text).ToString());
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
                string temp = ("1:"+txtName.Text + ": " + txtMess.Text).ToString();
                client.Send(Serialize(temp));
            }
            else if(txtMess.Visible==false)
            {
                string temp = "0:";
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

                    switch (message.Substring(0, 2))
                    {
                        case "0:":
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
                        case "1:":
                            {
                                AddMessage(message.Substring(2));
                                break;
                            }
                        case "2:":   //2: wait
                            {
                                this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                                {
                                    waitingLoading.Visible=true;
                                });
                                break;
                            }
                        case "3:":
                            {
                                this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                                {
                                    waitingLoading.Visible = false;
                                    btnRut.Visible = true;
                                    label4.Visible = true;
                                });
                                break;
                            }
                        case "9:":
                            {
                                this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                                {
                                    waitingLoading.Visible = false;
                                    btnDan.Visible = true;
                                    label4.Visible = true;
                                });
                                break;
                            }
                        case "4:":
                            {
                                Card card = new Card();
                                card.AddIdCard(message.Substring(2));
                                user.addCard(card);
                                DrawCard(card, user.getNumberCard(1));
                                this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                                {
                                    label4.Text = (user.getSum()).ToString();
                                    if (Int32.Parse(label4.Text.Trim()) > 16)
                                        btnDan.Visible = true;
                                });
                                break;
                            }


                        default:
                            break;
                    }
                    //AddMessage(message);
                }
                catch
                {
                    Close();
                }
            }

        }
        void AddMessage(string s)
        {
            listMess.Items.Add(new ListViewItem()
            {
                Text = s
            });
            txtMess.Clear();
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
                    if (card.getIdCard().Substring(0, card.getIdCard().Length == 2 ? 1 : 2) == "1")
                    {
                        radioButton1.Visible = true;
                        radioButton2.Visible = true;
                    }
                    pictureBox6.Image = pb.Image;
                    pictureBox6.Visible = true;
                    break;
                case 2:
                    if (card.getIdCard().Substring(0, card.getIdCard().Length == 2 ? 1 : 2) == "1")
                    {
                        radioButton3.Visible = true;
                        radioButton4.Visible = true;
                    }
                    pictureBox7.Image = pb.Image;
                    pictureBox7.Visible = true;
                    break;
                case 3:
                    if (card.getIdCard().Substring(0, card.getIdCard().Length == 2 ? 1 : 2) == "1")
                    {
                        radioButton5.Visible = true;
                        radioButton6.Visible = true;
                    }
                    pictureBox8.Image = pb.Image;
                    pictureBox8.Visible = true;
                    break;
                case 4:
                    if (card.getIdCard().Substring(0, card.getIdCard().Length == 2 ? 1 : 2) == "1")
                    {
                        radioButton7.Visible = true;
                        radioButton8.Visible = true;
                    }
                    pictureBox9.Image = pb.Image;
                    pictureBox9.Visible = true;
                    break;
                case 5:
                    if (card.getIdCard().Substring(0, card.getIdCard().Length == 2 ? 1 : 2) == "1")
                    {
                        radioButton9.Visible = true;
                        radioButton10.Visible = true;
                    }
                    pictureBox10.Image = pb.Image;
                    pictureBox10.Visible = true;
                    break;
                    }
        }

        private void btnRut_Click(object sender, EventArgs e)
        {
            client.Send(Serialize("4:"));
            if (user.getNumberCard() == 4)
            {
                this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
                {
                    btnRut.Visible = false;
                });
            }
            
        }

        private void btnDan_Click(object sender, EventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate            //How do I update the GUI from another thread?
            {
                waitingLoading.Visible = true;
                btnRut.Visible = false;
                btnDan.Visible = false;
            });
            client.Send(Serialize("5:"));
        }
    }
}
