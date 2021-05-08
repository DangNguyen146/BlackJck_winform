using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace BlackJack
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            // Configure color schema
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Orange400, Primary.Orange500,
                Primary.Orange500, Accent.LightGreen200,
                TextShade.WHITE
            );
        }

        private void btnSinglePlayer_Click(object sender, EventArgs e)
        {
            SinglePlay singlePlay = new SinglePlay();
            singlePlay.Show();
            this.Hide();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            MainMutiplayer mainMutiplayer = new MainMutiplayer();
            this.Hide();
            mainMutiplayer.Show();
        }
    }
}
