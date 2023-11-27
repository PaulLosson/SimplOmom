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
            components = new System.ComponentModel.Container();
            Button button_find;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            textBox_Log = new TextBox();
            button_connexion = new Button();
            textBox_endpoint = new TextBox();
            button_SaveParameters = new Button();
            button_LoadParameter = new Button();
            button_ChienLoup = new Button();
            button_depannage = new Button();
            textBox_OP_Input = new TextBox();
            textBox_DMC_Input = new TextBox();
            textBox_TRK_Input = new TextBox();
            checkedListBox_op_done = new CheckedListBox();
            label_OP_input = new Label();
            label_TRK_input = new Label();
            label_DMC_input = new Label();
            button1_Fichier = new Button();
            contextMenuStrip_menu = new ContextMenuStrip(components);
            saveToolStripMenuItem = new ToolStripMenuItem();
            loadToolStripMenuItem = new ToolStripMenuItem();
            parametersToolStripMenuItem = new ToolStripMenuItem();
            endpointToolStripMenuItem = new ToolStripMenuItem();
            autoConnectToolStripMenuItem = new ToolStripMenuItem();
            button_find = new Button();
            contextMenuStrip_menu.SuspendLayout();
            SuspendLayout();
            // 
            // button_find
            // 
            button_find.Location = new Point(1041, 36);
            button_find.Name = "button_find";
            button_find.Size = new Size(197, 29);
            button_find.TabIndex = 2;
            button_find.Text = "Find OPs";
            button_find.UseVisualStyleBackColor = true;
            button_find.Click += button_find_Click;
            // 
            // textBox_Log
            // 
            textBox_Log.Location = new Point(11, 80);
            textBox_Log.Multiline = true;
            textBox_Log.Name = "textBox_Log";
            textBox_Log.ReadOnly = true;
            textBox_Log.ScrollBars = ScrollBars.Horizontal;
            textBox_Log.ShortcutsEnabled = false;
            textBox_Log.Size = new Size(187, 289);
            textBox_Log.TabIndex = 0;
            textBox_Log.UseSystemPasswordChar = true;
            // 
            // button_connexion
            // 
            button_connexion.BackColor = Color.Red;
            button_connexion.Font = new Font("Segoe UI Black", 15F, FontStyle.Regular, GraphicsUnit.Point);
            button_connexion.ForeColor = SystemColors.ActiveCaptionText;
            button_connexion.Location = new Point(11, 16);
            button_connexion.Name = "button_connexion";
            button_connexion.Size = new Size(187, 47);
            button_connexion.TabIndex = 1;
            button_connexion.Text = "Déconnecté";
            button_connexion.UseVisualStyleBackColor = false;
            button_connexion.Click += button1_Click;
            // 
            // textBox_endpoint
            // 
            textBox_endpoint.Location = new Point(216, 36);
            textBox_endpoint.Name = "textBox_endpoint";
            textBox_endpoint.Size = new Size(475, 27);
            textBox_endpoint.TabIndex = 3;
            textBox_endpoint.Text = "opc.tcp://IP:PORT";
            // 
            // button_SaveParameters
            // 
            button_SaveParameters.Location = new Point(725, 36);
            button_SaveParameters.Name = "button_SaveParameters";
            button_SaveParameters.Size = new Size(94, 29);
            button_SaveParameters.TabIndex = 5;
            button_SaveParameters.Text = "Save";
            button_SaveParameters.UseVisualStyleBackColor = true;
            button_SaveParameters.Click += button_SaveParameters_Click;
            // 
            // button_LoadParameter
            // 
            button_LoadParameter.BackColor = SystemColors.Control;
            button_LoadParameter.Location = new Point(824, 36);
            button_LoadParameter.Name = "button_LoadParameter";
            button_LoadParameter.Size = new Size(94, 29);
            button_LoadParameter.TabIndex = 6;
            button_LoadParameter.Text = "Load";
            button_LoadParameter.UseVisualStyleBackColor = false;
            button_LoadParameter.Click += button_LoadParameter_Click;
            // 
            // button_ChienLoup
            // 
            button_ChienLoup.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button_ChienLoup.Location = new Point(11, 375);
            button_ChienLoup.Name = "button_ChienLoup";
            button_ChienLoup.Size = new Size(187, 45);
            button_ChienLoup.TabIndex = 7;
            button_ChienLoup.Text = "Recherche défauts";
            button_ChienLoup.UseVisualStyleBackColor = true;
            button_ChienLoup.Click += button_ChienLoup_Click;
            // 
            // button_depannage
            // 
            button_depannage.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button_depannage.Location = new Point(129, 701);
            button_depannage.Name = "button_depannage";
            button_depannage.Size = new Size(187, 45);
            button_depannage.TabIndex = 8;
            button_depannage.Text = "Depannage";
            button_depannage.UseVisualStyleBackColor = true;
            button_depannage.Click += button_depannage_Click;
            // 
            // textBox_OP_Input
            // 
            textBox_OP_Input.Location = new Point(129, 555);
            textBox_OP_Input.Name = "textBox_OP_Input";
            textBox_OP_Input.Size = new Size(74, 27);
            textBox_OP_Input.TabIndex = 9;
            // 
            // textBox_DMC_Input
            // 
            textBox_DMC_Input.Location = new Point(129, 620);
            textBox_DMC_Input.Name = "textBox_DMC_Input";
            textBox_DMC_Input.Size = new Size(374, 27);
            textBox_DMC_Input.TabIndex = 10;
            // 
            // textBox_TRK_Input
            // 
            textBox_TRK_Input.Location = new Point(129, 587);
            textBox_TRK_Input.Name = "textBox_TRK_Input";
            textBox_TRK_Input.Size = new Size(150, 27);
            textBox_TRK_Input.TabIndex = 11;
            // 
            // checkedListBox_op_done
            // 
            checkedListBox_op_done.FormattingEnabled = true;
            checkedListBox_op_done.Location = new Point(1087, 80);
            checkedListBox_op_done.Name = "checkedListBox_op_done";
            checkedListBox_op_done.Size = new Size(150, 796);
            checkedListBox_op_done.TabIndex = 12;
            // 
            // label_OP_input
            // 
            label_OP_input.AutoSize = true;
            label_OP_input.Location = new Point(29, 555);
            label_OP_input.Name = "label_OP_input";
            label_OP_input.Size = new Size(39, 20);
            label_OP_input.TabIndex = 13;
            label_OP_input.Text = "OP : ";
            // 
            // label_TRK_input
            // 
            label_TRK_input.AutoSize = true;
            label_TRK_input.Location = new Point(29, 589);
            label_TRK_input.Name = "label_TRK_input";
            label_TRK_input.Size = new Size(94, 20);
            label_TRK_input.TabIndex = 14;
            label_TRK_input.Text = "Tracking ID : ";
            // 
            // label_DMC_input
            // 
            label_DMC_input.AutoSize = true;
            label_DMC_input.Location = new Point(29, 623);
            label_DMC_input.Name = "label_DMC_input";
            label_DMC_input.Size = new Size(53, 20);
            label_DMC_input.TabIndex = 15;
            label_DMC_input.Text = "DMC : ";
            // 
            // button1_Fichier
            // 
            button1_Fichier.Location = new Point(924, 36);
            button1_Fichier.Name = "button1_Fichier";
            button1_Fichier.Size = new Size(93, 29);
            button1_Fichier.TabIndex = 16;
            button1_Fichier.Text = "Fichier";
            button1_Fichier.UseVisualStyleBackColor = true;
            button1_Fichier.Click += button1_Fichier_Click;
            // 
            // contextMenuStrip_menu
            // 
            contextMenuStrip_menu.ImageScalingSize = new Size(20, 20);
            contextMenuStrip_menu.Items.AddRange(new ToolStripItem[] { saveToolStripMenuItem, loadToolStripMenuItem, parametersToolStripMenuItem });
            contextMenuStrip_menu.Name = "contextMenuStrip1";
            contextMenuStrip_menu.Size = new Size(152, 76);
            contextMenuStrip_menu.Text = "Fichier";
            contextMenuStrip_menu.Opening += contextMenuStrip_menu_Opening;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(151, 24);
            saveToolStripMenuItem.Text = "Save";
            // 
            // loadToolStripMenuItem
            // 
            loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            loadToolStripMenuItem.Size = new Size(151, 24);
            loadToolStripMenuItem.Text = "Load";
            // 
            // parametersToolStripMenuItem
            // 
            parametersToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { endpointToolStripMenuItem, autoConnectToolStripMenuItem });
            parametersToolStripMenuItem.Name = "parametersToolStripMenuItem";
            parametersToolStripMenuItem.Size = new Size(151, 24);
            parametersToolStripMenuItem.Text = "Parameters";
            // 
            // endpointToolStripMenuItem
            // 
            endpointToolStripMenuItem.Name = "endpointToolStripMenuItem";
            endpointToolStripMenuItem.Size = new Size(224, 26);
            endpointToolStripMenuItem.Text = "Endpoint";
            // 
            // autoConnectToolStripMenuItem
            // 
            autoConnectToolStripMenuItem.Name = "autoConnectToolStripMenuItem";
            autoConnectToolStripMenuItem.Size = new Size(224, 26);
            autoConnectToolStripMenuItem.Text = "AutoConnect";
            autoConnectToolStripMenuItem.Click += autoConnectToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.DarkSlateGray;
            ClientSize = new Size(1259, 899);
            Controls.Add(button1_Fichier);
            Controls.Add(label_DMC_input);
            Controls.Add(label_TRK_input);
            Controls.Add(label_OP_input);
            Controls.Add(checkedListBox_op_done);
            Controls.Add(textBox_TRK_Input);
            Controls.Add(textBox_DMC_Input);
            Controls.Add(textBox_OP_Input);
            Controls.Add(button_depannage);
            Controls.Add(button_ChienLoup);
            Controls.Add(button_LoadParameter);
            Controls.Add(button_SaveParameters);
            Controls.Add(textBox_endpoint);
            Controls.Add(button_find);
            Controls.Add(button_connexion);
            Controls.Add(textBox_Log);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "MOM Santé : la supervision du MOM";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            contextMenuStrip_menu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox_Log;
        private Button button_connexion;
        private TextBox textBox_endpoint;
        private Button button_SaveParameters;
        private Button button_LoadParameter;
        private Button button_ChienLoup;
        private Button button_depannage;
        private TextBox textBox_OP_Input;
        private TextBox textBox_DMC_Input;
        private TextBox textBox_TRK_Input;
        private CheckedListBox checkedListBox_op_done;
        private Label label_OP_input;
        private Label label_TRK_input;
        private Label label_DMC_input;
        private Button button1_Fichier;
        private ContextMenuStrip contextMenuStrip_menu;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem parametersToolStripMenuItem;
        private ToolStripMenuItem endpointToolStripMenuItem;
        private ToolStripMenuItem autoConnectToolStripMenuItem;
    }
}