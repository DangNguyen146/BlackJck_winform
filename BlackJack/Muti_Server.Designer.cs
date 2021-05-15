namespace BlackJack
{
    partial class Muti_Server
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
            this.listMess = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // listMess
            // 
            this.listMess.HideSelection = false;
            this.listMess.Location = new System.Drawing.Point(235, 234);
            this.listMess.Name = "listMess";
            this.listMess.Size = new System.Drawing.Size(503, 203);
            this.listMess.TabIndex = 0;
            this.listMess.UseCompatibleStateImageBehavior = false;
            this.listMess.View = System.Windows.Forms.View.List;
            // 
            // Muti_Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::BlackJack.Properties.Resources.FormMain_1x;
            this.ClientSize = new System.Drawing.Size(969, 560);
            this.Controls.Add(this.listMess);
            this.Name = "Muti_Server";
            this.Text = "Muti_Server";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Muti_Server_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listMess;
    }
}