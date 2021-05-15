using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackJack
{
    public partial class SinglePlay : Form
    {
        public SinglePlay()
        {
            InitializeComponent();
        }

        BoBai boBai;
        Player com;
        Player user;
        private void label3_Click(object sender, EventArgs e)
        {
            label3.Visible = false;

            boBai = new BoBai();

            com = new Player();
            com.setLoaiNguoi(0);

            user = new Player();
            user.setLoaiNguoi(1);

            Card card = new Card();

            //card = boBai.getCard();
            //com.addCard(card);
            card = new Card();
            DrawCard(card, com.getLoaiNguoi(), 1);
            Thread.Sleep(100);

            card = boBai.getCard();
            user.addCard(card);
            DrawCard(card, user.getLoaiNguoi(), user.getNumberCard(1));
            Thread.Sleep(100);

            //card = boBai.getCard();
            //com.addCard(card);
            card = new Card();
            DrawCard(card, com.getLoaiNguoi(), 2);
            Thread.Sleep(100);

            card = boBai.getCard();
            user.addCard(card);
            DrawCard(card, user.getLoaiNguoi(), user.getNumberCard(1));
            Thread.Sleep(100);

            btnRut.Visible = true;

            label2.Text = (user.getSum()).ToString();
            label2.Visible = true;

            if (Int32.Parse(label2.Text.Trim()) > 16)
                btnDan.Visible = true;
        }
        private void btnRut_Click_1(object sender, EventArgs e)
        {
            if (user.getNumberCard() == 4)
            {
                btnRut.Enabled = false;
                btnRut.Visible = false;
            }
            Card card = new Card();
            card = boBai.getCard();

            user.addCard(card);
            label2.Text = (user.getSum()).ToString();

            DrawCard(card, user.getLoaiNguoi(), user.getNumberCard(1));
            if (Int32.Parse(label2.Text.Trim()) > 16)
                btnDan.Visible = true;
        }

        private void btnDan_Click_1(object sender, EventArgs e)
        {
            btnRut.Visible = false;
            btnDan.Visible = false;
            Card card = new Card();
            DrawCard(card, 0, 0);
            while (com.getSum() <= 16)
            {
                card = boBai.getCard();
                com.addCard(card);
                DrawCard(card, 0, com.getNumberCard(1));
                if (com.getSum() <= 16 && card.getIdCard().Substring(0, card.getIdCard().Length == 2 ? 1 : 2) == "1")
                {
                    com.getSum(1);
                }
            }
            KetQua(com.getSum(), user.getSum(0), com.getNumberCard(), user.getNumberCard());
        }

        private void DrawCard(Card card, int type, int num)
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
            if (type == 0)
            {
                switch (num)
                {
                    case 1:
                        pictureBox1.Image = pb.Image;
                        pictureBox1.Visible = true;
                        break;
                    case 2:
                        pictureBox2.Image = pb.Image;
                        pictureBox2.Visible = true;
                        break;
                    case 3:
                        pictureBox3.Image = pb.Image;
                        pictureBox3.Visible = true;
                        break;
                    case 4:
                        pictureBox4.Image = pb.Image;
                        pictureBox4.Visible = true;
                        break;
                    case 5:
                        pictureBox5.Image = pb.Image;
                        pictureBox5.Visible = true;
                        break;
                }
            }
            else
            {
                switch (num)
                {
                    case 1:
                        if(card.getIdCard().Substring(0, card.getIdCard().Length == 2 ? 1 : 2) =="1")
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


        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            int temp = int.Parse(label2.Text.Trim());
            temp += 1;
            
            label2.Text = (user.getSum(1)).ToString();
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            user.getSum(1);
            if (Int32.Parse(label2.Text.Trim()) > 16)
                btnDan.Visible = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            int temp = int.Parse(label2.Text.Trim());
            temp += 11;
            label2.Text = (user.getSum(11)).ToString();
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            user.getSum(11);
            if (Int32.Parse(label2.Text.Trim()) > 16)
                btnDan.Visible = true;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            int temp = int.Parse(label2.Text.Trim());
            temp += 1;
            label2.Text = (user.getSum(1)).ToString();
            radioButton3.Visible = false;
            radioButton4.Visible = false;
            user.getSum(1);
            if (Int32.Parse(label2.Text.Trim()) > 16)
                btnDan.Visible = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            int temp = int.Parse(label2.Text.Trim());
            temp += 11;
            label2.Text = (user.getSum(11)).ToString();
            radioButton3.Visible = false;
            radioButton4.Visible = false;
            user.getSum(11);
            if (Int32.Parse(label2.Text.Trim()) > 16)
                btnDan.Visible = true;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            int temp = int.Parse(label2.Text.Trim());
            temp += 1;
            label2.Text = (user.getSum(1)).ToString();
            radioButton5.Visible = false;
            radioButton6.Visible = false;
            user.getSum(1);
            if (Int32.Parse(label2.Text.Trim()) > 16)
                btnDan.Visible = true;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            int temp = int.Parse(label2.Text.Trim());
            temp += 11;
            label2.Text = (user.getSum(11)).ToString();
            radioButton5.Visible = false;
            radioButton6.Visible = false;
            user.getSum(11);
            if (Int32.Parse(label2.Text.Trim()) > 16)
                btnDan.Visible = true;
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            int temp = int.Parse(label2.Text.Trim());
            temp += 1;
            label2.Text = (user.getSum(1)).ToString();
            radioButton7.Visible = false;
            radioButton8.Visible = false;
            user.getSum(1);
            if (Int32.Parse(label2.Text.Trim()) > 16)
                btnDan.Visible = true;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            int temp = int.Parse(label2.Text.Trim());
            temp += 11;
            label2.Text = (user.getSum(11)).ToString();
            radioButton7.Visible = false;
            radioButton8.Visible = false;
            user.getSum(11);
            if (Int32.Parse(label2.Text.Trim()) > 16)
                btnDan.Visible = true;
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            int temp = int.Parse(label2.Text.Trim());
            temp += 1;
            label2.Text = (user.getSum(1)).ToString();
            radioButton9.Visible = false;
            radioButton10.Visible = false;
            user.getSum(1);
            if (Int32.Parse(label2.Text.Trim()) > 16)
                btnDan.Visible = true;
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            int temp = int.Parse(label2.Text.Trim());
            temp += 11;
            label2.Text = (user.getSum(11)).ToString();
            radioButton9.Visible = false;
            radioButton10.Visible = false;
            user.getSum(11);
            if (Int32.Parse(label2.Text.Trim()) > 16)
                btnDan.Visible = true;
        }

       
        public void KetQua(int com, int user, int numCom, int numUser)
        {
            if(numCom==5 && numUser==5 && com<=21 && user<=21)
                MessageBox.Show("Hòa");
            else if (numCom == 5 && numUser != 5 &&com<=21)
                MessageBox.Show("Bạn đã thua");
            else if (numCom != 5 && numUser == 5 &&user<=21)
                MessageBox.Show("Bạn thắng");

            else if (com == 21 && user != 21)
                MessageBox.Show("Bạn đã thua");
            else if (com != 21 && user == 21)
                MessageBox.Show("Bạn thắng");
            else if (com > 21 && user > 21)
                MessageBox.Show("Hòa");
            else if (com>21 && user<21)
                MessageBox.Show("Bạn thắng");

            else if (21 >= com && 21 >= user && com < user)
                MessageBox.Show("Bạn thắng");
            else if (com<21 && user>21)
                MessageBox.Show("Bạn đã thua");
            else if (21>=com && 21>=user && com>user)
                MessageBox.Show("Bạn đã thua");
            else if (21 >= com && 21 >= user && com == user)
                MessageBox.Show("Hòa");

            this.Close();
        }

        private void SinglePlay_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show();
        }
    }
}
