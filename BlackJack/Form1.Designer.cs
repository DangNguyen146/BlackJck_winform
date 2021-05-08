namespace BlackJack
{
    partial class Form1
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
            this.btnSinglePlayer = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialRaisedButton2 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.SuspendLayout();
            // 
            // btnSinglePlayer
            // 
            this.btnSinglePlayer.AutoSize = true;
            this.btnSinglePlayer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSinglePlayer.Depth = 0;
            this.btnSinglePlayer.Icon = null;
            this.btnSinglePlayer.Location = new System.Drawing.Point(241, 254);
            this.btnSinglePlayer.MaximumSize = new System.Drawing.Size(199, 39);
            this.btnSinglePlayer.MinimumSize = new System.Drawing.Size(199, 39);
            this.btnSinglePlayer.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSinglePlayer.Name = "btnSinglePlayer";
            this.btnSinglePlayer.Primary = true;
            this.btnSinglePlayer.Size = new System.Drawing.Size(199, 39);
            this.btnSinglePlayer.TabIndex = 0;
            this.btnSinglePlayer.Text = "PLAY SINGLE ";
            this.btnSinglePlayer.UseVisualStyleBackColor = true;
            this.btnSinglePlayer.Click += new System.EventHandler(this.btnSinglePlayer_Click);
            // 
            // materialRaisedButton2
            // 
            this.materialRaisedButton2.AutoSize = true;
            this.materialRaisedButton2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialRaisedButton2.Depth = 0;
            this.materialRaisedButton2.Icon = null;
            this.materialRaisedButton2.Location = new System.Drawing.Point(524, 254);
            this.materialRaisedButton2.MaximumSize = new System.Drawing.Size(199, 39);
            this.materialRaisedButton2.MinimumSize = new System.Drawing.Size(199, 39);
            this.materialRaisedButton2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButton2.Name = "materialRaisedButton2";
            this.materialRaisedButton2.Primary = true;
            this.materialRaisedButton2.Size = new System.Drawing.Size(199, 39);
            this.materialRaisedButton2.TabIndex = 1;
            this.materialRaisedButton2.Text = "MUTIPLAYER";
            this.materialRaisedButton2.UseVisualStyleBackColor = true;
            this.materialRaisedButton2.Click += new System.EventHandler(this.materialRaisedButton2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::BlackJack.Properties.Resources.FormMain_1x;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(972, 550);
            this.Controls.Add(this.materialRaisedButton2);
            this.Controls.Add(this.btnSinglePlayer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Location = new System.Drawing.Point(988, 600);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialRaisedButton btnSinglePlayer;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButton2;
    }
}

