using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace BlackJack
{
    public partial class MyPictureBox : PictureBox
    {
        public MyPictureBox()
        {
            //InitializeComponent();
        }

        public MyPictureBox(IContainer container)
        {
            container.Add(this);


            //InitializeComponent();
        }

        Point mdLoc;
        Point temp;
        protected override void OnMouseDown(MouseEventArgs e)
        {

            base.OnMouseDown(e);
            mdLoc = e.Location;

        temp = e.Location;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - mdLoc.X;
                this.Top += e.Y - mdLoc.Y;
            }
        }

    }
}
