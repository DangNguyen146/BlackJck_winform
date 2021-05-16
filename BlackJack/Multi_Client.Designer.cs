namespace BlackJack
{
    partial class Multi_Client
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSend = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtMess = new System.Windows.Forms.TextBox();
            this.listMess = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.waitingLoading = new System.Windows.Forms.Label();
            this.btnDan = new System.Windows.Forms.Label();
            this.btnRut = new System.Windows.Forms.Label();
            this.radioButton9 = new System.Windows.Forms.RadioButton();
            this.radioButton10 = new System.Windows.Forms.RadioButton();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.radioButton8 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.player1 = new System.Windows.Forms.Label();
            this.player2 = new System.Windows.Forms.Label();
            this.player3 = new System.Windows.Forms.Label();
            this.player4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.ForeColor = System.Drawing.Color.Black;
            this.btnSend.Location = new System.Drawing.Point(161, 549);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(47, 23);
            this.btnSend.TabIndex = 11;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Visible = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(12, 533);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Message";
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 494);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Your name";
            this.label1.Visible = false;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(15, 510);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(72, 20);
            this.txtName.TabIndex = 8;
            this.txtName.Visible = false;
            // 
            // txtMess
            // 
            this.txtMess.Location = new System.Drawing.Point(15, 549);
            this.txtMess.Multiline = true;
            this.txtMess.Name = "txtMess";
            this.txtMess.Size = new System.Drawing.Size(140, 20);
            this.txtMess.TabIndex = 7;
            this.txtMess.Visible = false;
            // 
            // listMess
            // 
            this.listMess.HideSelection = false;
            this.listMess.Location = new System.Drawing.Point(12, 440);
            this.listMess.Name = "listMess";
            this.listMess.Size = new System.Drawing.Size(196, 51);
            this.listMess.TabIndex = 6;
            this.listMess.UseCompatibleStateImageBehavior = false;
            this.listMess.View = System.Windows.Forms.View.List;
            this.listMess.Visible = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Image = global::BlackJack.Properties.Resources.Asset_2;
            this.label3.Location = new System.Drawing.Point(386, 299);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(219, 66);
            this.label3.TabIndex = 15;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // waitingLoading
            // 
            this.waitingLoading.BackColor = System.Drawing.Color.Transparent;
            this.waitingLoading.Image = global::BlackJack.Properties.Resources.giphy;
            this.waitingLoading.Location = new System.Drawing.Point(313, 234);
            this.waitingLoading.Name = "waitingLoading";
            this.waitingLoading.Size = new System.Drawing.Size(313, 204);
            this.waitingLoading.TabIndex = 16;
            this.waitingLoading.Visible = false;
            // 
            // btnDan
            // 
            this.btnDan.BackColor = System.Drawing.Color.Transparent;
            this.btnDan.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDan.ForeColor = System.Drawing.Color.White;
            this.btnDan.Image = global::BlackJack.Properties.Resources.DAN;
            this.btnDan.Location = new System.Drawing.Point(757, 372);
            this.btnDan.Name = "btnDan";
            this.btnDan.Size = new System.Drawing.Size(153, 66);
            this.btnDan.TabIndex = 49;
            this.btnDan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDan.Visible = false;
            this.btnDan.Click += new System.EventHandler(this.btnDan_Click);
            // 
            // btnRut
            // 
            this.btnRut.BackColor = System.Drawing.Color.Transparent;
            this.btnRut.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRut.ForeColor = System.Drawing.Color.White;
            this.btnRut.Image = global::BlackJack.Properties.Resources.rut1;
            this.btnRut.Location = new System.Drawing.Point(757, 306);
            this.btnRut.Name = "btnRut";
            this.btnRut.Size = new System.Drawing.Size(153, 66);
            this.btnRut.TabIndex = 48;
            this.btnRut.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnRut.Visible = false;
            this.btnRut.Click += new System.EventHandler(this.btnRut_Click);
            // 
            // radioButton9
            // 
            this.radioButton9.BackColor = System.Drawing.Color.Transparent;
            this.radioButton9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton9.ForeColor = System.Drawing.Color.OrangeRed;
            this.radioButton9.Location = new System.Drawing.Point(632, 426);
            this.radioButton9.Name = "radioButton9";
            this.radioButton9.Size = new System.Drawing.Size(47, 24);
            this.radioButton9.TabIndex = 47;
            this.radioButton9.TabStop = true;
            this.radioButton9.Text = "11";
            this.radioButton9.UseVisualStyleBackColor = false;
            this.radioButton9.Visible = false;
            // 
            // radioButton10
            // 
            this.radioButton10.BackColor = System.Drawing.Color.Transparent;
            this.radioButton10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton10.ForeColor = System.Drawing.Color.White;
            this.radioButton10.Location = new System.Drawing.Point(590, 426);
            this.radioButton10.Name = "radioButton10";
            this.radioButton10.Size = new System.Drawing.Size(36, 24);
            this.radioButton10.TabIndex = 46;
            this.radioButton10.TabStop = true;
            this.radioButton10.Text = "1";
            this.radioButton10.UseVisualStyleBackColor = false;
            this.radioButton10.Visible = false;
            // 
            // radioButton7
            // 
            this.radioButton7.BackColor = System.Drawing.Color.Transparent;
            this.radioButton7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton7.ForeColor = System.Drawing.Color.OrangeRed;
            this.radioButton7.Location = new System.Drawing.Point(558, 426);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(47, 24);
            this.radioButton7.TabIndex = 45;
            this.radioButton7.TabStop = true;
            this.radioButton7.Text = "11";
            this.radioButton7.UseVisualStyleBackColor = false;
            this.radioButton7.Visible = false;
            // 
            // radioButton8
            // 
            this.radioButton8.BackColor = System.Drawing.Color.Transparent;
            this.radioButton8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton8.ForeColor = System.Drawing.Color.White;
            this.radioButton8.Location = new System.Drawing.Point(516, 426);
            this.radioButton8.Name = "radioButton8";
            this.radioButton8.Size = new System.Drawing.Size(36, 24);
            this.radioButton8.TabIndex = 44;
            this.radioButton8.TabStop = true;
            this.radioButton8.Text = "1";
            this.radioButton8.UseVisualStyleBackColor = false;
            this.radioButton8.Visible = false;
            // 
            // radioButton5
            // 
            this.radioButton5.BackColor = System.Drawing.Color.Transparent;
            this.radioButton5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton5.ForeColor = System.Drawing.Color.OrangeRed;
            this.radioButton5.Location = new System.Drawing.Point(479, 426);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(47, 24);
            this.radioButton5.TabIndex = 43;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "11";
            this.radioButton5.UseVisualStyleBackColor = false;
            this.radioButton5.Visible = false;
            // 
            // radioButton6
            // 
            this.radioButton6.BackColor = System.Drawing.Color.Transparent;
            this.radioButton6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton6.ForeColor = System.Drawing.Color.White;
            this.radioButton6.Location = new System.Drawing.Point(437, 426);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(36, 24);
            this.radioButton6.TabIndex = 42;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "1";
            this.radioButton6.UseVisualStyleBackColor = false;
            this.radioButton6.Visible = false;
            // 
            // radioButton3
            // 
            this.radioButton3.BackColor = System.Drawing.Color.Transparent;
            this.radioButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton3.ForeColor = System.Drawing.Color.OrangeRed;
            this.radioButton3.Location = new System.Drawing.Point(404, 426);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(47, 24);
            this.radioButton3.TabIndex = 41;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "11";
            this.radioButton3.UseVisualStyleBackColor = false;
            this.radioButton3.Visible = false;
            // 
            // radioButton4
            // 
            this.radioButton4.BackColor = System.Drawing.Color.Transparent;
            this.radioButton4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton4.ForeColor = System.Drawing.Color.White;
            this.radioButton4.Location = new System.Drawing.Point(362, 426);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(36, 24);
            this.radioButton4.TabIndex = 40;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "1";
            this.radioButton4.UseVisualStyleBackColor = false;
            this.radioButton4.Visible = false;
            // 
            // radioButton2
            // 
            this.radioButton2.BackColor = System.Drawing.Color.Transparent;
            this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.ForeColor = System.Drawing.Color.OrangeRed;
            this.radioButton2.Location = new System.Drawing.Point(312, 428);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(47, 24);
            this.radioButton2.TabIndex = 39;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "11";
            this.radioButton2.UseVisualStyleBackColor = false;
            this.radioButton2.Visible = false;
            // 
            // radioButton1
            // 
            this.radioButton1.BackColor = System.Drawing.Color.Transparent;
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.ForeColor = System.Drawing.Color.White;
            this.radioButton1.Location = new System.Drawing.Point(270, 428);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(36, 24);
            this.radioButton1.TabIndex = 38;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "1";
            this.radioButton1.UseVisualStyleBackColor = false;
            this.radioButton1.Visible = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox6.Location = new System.Drawing.Point(288, 456);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(71, 96);
            this.pictureBox6.TabIndex = 37;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox7
            // 
            this.pictureBox7.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox7.Location = new System.Drawing.Point(365, 456);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(71, 96);
            this.pictureBox7.TabIndex = 36;
            this.pictureBox7.TabStop = false;
            // 
            // pictureBox8
            // 
            this.pictureBox8.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox8.Location = new System.Drawing.Point(442, 456);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(71, 96);
            this.pictureBox8.TabIndex = 35;
            this.pictureBox8.TabStop = false;
            // 
            // pictureBox9
            // 
            this.pictureBox9.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox9.Location = new System.Drawing.Point(519, 456);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(71, 96);
            this.pictureBox9.TabIndex = 34;
            this.pictureBox9.TabStop = false;
            // 
            // pictureBox10
            // 
            this.pictureBox10.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox10.Location = new System.Drawing.Point(596, 456);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(71, 96);
            this.pictureBox10.TabIndex = 33;
            this.pictureBox10.TabStop = false;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("SVN-A Love Of Thunder", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Image = global::BlackJack.Properties.Resources.BANGDIEM;
            this.label4.Location = new System.Drawing.Point(705, 456);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 90);
            this.label4.TabIndex = 27;
            this.label4.Text = "0";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.Visible = false;
            // 
            // player1
            // 
            this.player1.BackColor = System.Drawing.Color.Transparent;
            this.player1.Image = global::BlackJack.Properties.Resources._1;
            this.player1.Location = new System.Drawing.Point(31, 287);
            this.player1.Name = "player1";
            this.player1.Size = new System.Drawing.Size(158, 59);
            this.player1.TabIndex = 50;
            this.player1.Visible = false;
            // 
            // player2
            // 
            this.player2.BackColor = System.Drawing.Color.Transparent;
            this.player2.Image = global::BlackJack.Properties.Resources._2;
            this.player2.Location = new System.Drawing.Point(97, 127);
            this.player2.Name = "player2";
            this.player2.Size = new System.Drawing.Size(158, 59);
            this.player2.TabIndex = 51;
            this.player2.Visible = false;
            // 
            // player3
            // 
            this.player3.BackColor = System.Drawing.Color.Transparent;
            this.player3.Image = global::BlackJack.Properties.Resources._3;
            this.player3.Location = new System.Drawing.Point(629, 127);
            this.player3.Name = "player3";
            this.player3.Size = new System.Drawing.Size(149, 59);
            this.player3.TabIndex = 52;
            this.player3.Visible = false;
            // 
            // player4
            // 
            this.player4.BackColor = System.Drawing.Color.Transparent;
            this.player4.Image = global::BlackJack.Properties.Resources._4;
            this.player4.Location = new System.Drawing.Point(792, 234);
            this.player4.Name = "player4";
            this.player4.Size = new System.Drawing.Size(158, 59);
            this.player4.TabIndex = 53;
            this.player4.Visible = false;
            // 
            // Multi_Client
            // 
            this.AcceptButton = this.btnSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::BlackJack.Properties.Resources.BackGround_Table_1x;
            this.ClientSize = new System.Drawing.Size(972, 577);
            this.Controls.Add(this.player4);
            this.Controls.Add(this.player3);
            this.Controls.Add(this.player2);
            this.Controls.Add(this.player1);
            this.Controls.Add(this.btnDan);
            this.Controls.Add(this.btnRut);
            this.Controls.Add(this.radioButton9);
            this.Controls.Add(this.radioButton10);
            this.Controls.Add(this.radioButton7);
            this.Controls.Add(this.radioButton8);
            this.Controls.Add(this.radioButton5);
            this.Controls.Add(this.radioButton6);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton4);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.pictureBox7);
            this.Controls.Add(this.pictureBox8);
            this.Controls.Add(this.pictureBox9);
            this.Controls.Add(this.pictureBox10);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.listMess);
            this.Controls.Add(this.txtMess);
            this.Controls.Add(this.waitingLoading);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Multi_Client";
            this.Text = "Multi_Client";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Client_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtMess;
        private System.Windows.Forms.ListView listMess;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label waitingLoading;
        private System.Windows.Forms.Label btnDan;
        private System.Windows.Forms.Label btnRut;
        private System.Windows.Forms.RadioButton radioButton9;
        private System.Windows.Forms.RadioButton radioButton10;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.RadioButton radioButton8;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label player1;
        private System.Windows.Forms.Label player2;
        private System.Windows.Forms.Label player3;
        private System.Windows.Forms.Label player4;
    }
}