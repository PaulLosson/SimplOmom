namespace MOM_Santé
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Button button_find;
            this.textBox_Log = new System.Windows.Forms.TextBox();
            this.button_connexion = new System.Windows.Forms.Button();
            this.textBox_endpoint = new System.Windows.Forms.TextBox();
            button_find = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_find
            // 
            button_find.Location = new System.Drawing.Point(723, 109);
            button_find.Name = "button_find";
            button_find.Size = new System.Drawing.Size(94, 29);
            button_find.TabIndex = 2;
            button_find.Text = "Find OPs";
            button_find.UseVisualStyleBackColor = true;
            button_find.Click += new System.EventHandler(this.button_find_Click);
            // 
            // textBox_Log
            // 
            this.textBox_Log.Location = new System.Drawing.Point(114, 38);
            this.textBox_Log.Multiline = true;
            this.textBox_Log.Name = "textBox_Log";
            this.textBox_Log.ReadOnly = true;
            this.textBox_Log.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBox_Log.ShortcutsEnabled = false;
            this.textBox_Log.Size = new System.Drawing.Size(588, 354);
            this.textBox_Log.TabIndex = 0;
            this.textBox_Log.UseSystemPasswordChar = true;
            this.textBox_Log.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button_connexion
            // 
            this.button_connexion.BackColor = System.Drawing.Color.Red;
            this.button_connexion.Font = new System.Drawing.Font("Segoe UI Black", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button_connexion.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button_connexion.Location = new System.Drawing.Point(1499, 38);
            this.button_connexion.Name = "button_connexion";
            this.button_connexion.Size = new System.Drawing.Size(178, 47);
            this.button_connexion.TabIndex = 1;
            this.button_connexion.Text = "Déconnecté";
            this.button_connexion.UseVisualStyleBackColor = false;
            this.button_connexion.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox_endpoint
            // 
            this.textBox_endpoint.Location = new System.Drawing.Point(179, 5);
            this.textBox_endpoint.Name = "textBox_endpoint";
            this.textBox_endpoint.Size = new System.Drawing.Size(475, 27);
            this.textBox_endpoint.TabIndex = 3;
            this.textBox_endpoint.Text = "opc.tcp://EX0012.inetemotors.com:6011/LineMiddleware1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1733, 491);
            this.Controls.Add(this.textBox_endpoint);
            this.Controls.Add(button_find);
            this.Controls.Add(this.button_connexion);
            this.Controls.Add(this.textBox_Log);
            this.Name = "Form1";
            this.Text = "MOM_Santé";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBox_Log;
        private Button button_connexion;
        private TextBox textBox_endpoint;
    }
}