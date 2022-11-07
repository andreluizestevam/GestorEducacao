namespace C2BR.GestorEducacao.LicenseManager
{
    partial class Main
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnCopiar = new System.Windows.Forms.Button();
            this.btnGerar = new System.Windows.Forms.Button();
            this.tabCtrlMain = new System.Windows.Forms.TabControl();
            this.tabDados = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dtFim = new System.Windows.Forms.DateTimePicker();
            this.dtInicio = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtContrato = new System.Windows.Forms.MaskedTextBox();
            this.dtContrato = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCpf = new System.Windows.Forms.MaskedTextBox();
            this.dtNascContato = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCnpj = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabLicenca = new System.Windows.Forms.TabPage();
            this.txtLicenca = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtXml = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtCertificado = new System.Windows.Forms.TextBox();
            this.grupoBios = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtBiosNome = new System.Windows.Forms.TextBox();
            this.txtBiosSoftware = new System.Windows.Forms.TextBox();
            this.txtBiosVersao = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.tabCtrlMain.SuspendLayout();
            this.tabDados.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabLicenca.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.grupoBios.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.btnCopiar);
            this.panel1.Controls.Add(this.btnGerar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 353);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(498, 35);
            this.panel1.TabIndex = 1;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(12, 6);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(135, 23);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "&Exportar Certificado";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnCopiar
            // 
            this.btnCopiar.Enabled = false;
            this.btnCopiar.Location = new System.Drawing.Point(210, 6);
            this.btnCopiar.Name = "btnCopiar";
            this.btnCopiar.Size = new System.Drawing.Size(135, 23);
            this.btnCopiar.TabIndex = 1;
            this.btnCopiar.Text = "&Copiar Texto";
            this.btnCopiar.UseVisualStyleBackColor = true;
            this.btnCopiar.Click += new System.EventHandler(this.btnCopiar_Click);
            // 
            // btnGerar
            // 
            this.btnGerar.Location = new System.Drawing.Point(351, 6);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(135, 23);
            this.btnGerar.TabIndex = 0;
            this.btnGerar.Text = "&Gerar Licença";
            this.btnGerar.UseVisualStyleBackColor = true;
            this.btnGerar.Click += new System.EventHandler(this.btnGerar_Click);
            // 
            // tabCtrlMain
            // 
            this.tabCtrlMain.Controls.Add(this.tabDados);
            this.tabCtrlMain.Controls.Add(this.tabLicenca);
            this.tabCtrlMain.Controls.Add(this.tabPage1);
            this.tabCtrlMain.Controls.Add(this.tabPage2);
            this.tabCtrlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrlMain.Location = new System.Drawing.Point(0, 0);
            this.tabCtrlMain.Name = "tabCtrlMain";
            this.tabCtrlMain.SelectedIndex = 0;
            this.tabCtrlMain.Size = new System.Drawing.Size(498, 353);
            this.tabCtrlMain.TabIndex = 0;
            this.tabCtrlMain.SelectedIndexChanged += new System.EventHandler(this.tabCtrlMain_SelectedIndexChanged);
            // 
            // tabDados
            // 
            this.tabDados.Controls.Add(this.checkBox1);
            this.tabDados.Controls.Add(this.grupoBios);
            this.tabDados.Controls.Add(this.groupBox3);
            this.tabDados.Controls.Add(this.groupBox2);
            this.tabDados.Controls.Add(this.groupBox1);
            this.tabDados.Controls.Add(this.txtCnpj);
            this.tabDados.Controls.Add(this.label1);
            this.tabDados.Location = new System.Drawing.Point(4, 22);
            this.tabDados.Name = "tabDados";
            this.tabDados.Padding = new System.Windows.Forms.Padding(3);
            this.tabDados.Size = new System.Drawing.Size(490, 327);
            this.tabDados.TabIndex = 0;
            this.tabDados.Text = "Dados da Licença";
            this.tabDados.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dtFim);
            this.groupBox3.Controls.Add(this.dtInicio);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(8, 114);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(474, 50);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Validade da Licença";
            // 
            // dtFim
            // 
            this.dtFim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFim.Location = new System.Drawing.Point(247, 19);
            this.dtFim.Name = "dtFim";
            this.dtFim.Size = new System.Drawing.Size(160, 20);
            this.dtFim.TabIndex = 1;
            // 
            // dtInicio
            // 
            this.dtInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtInicio.Location = new System.Drawing.Point(49, 19);
            this.dtInicio.Name = "dtInicio";
            this.dtInicio.Size = new System.Drawing.Size(160, 20);
            this.dtInicio.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(215, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Fim:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Início:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtContrato);
            this.groupBox2.Controls.Add(this.dtContrato);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(266, 32);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(216, 76);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Dados do Contrato";
            // 
            // txtContrato
            // 
            this.txtContrato.Location = new System.Drawing.Point(59, 19);
            this.txtContrato.Name = "txtContrato";
            this.txtContrato.Size = new System.Drawing.Size(147, 20);
            this.txtContrato.TabIndex = 0;
            this.txtContrato.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // dtContrato
            // 
            this.dtContrato.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtContrato.Location = new System.Drawing.Point(59, 45);
            this.dtContrato.Name = "dtContrato";
            this.dtContrato.Size = new System.Drawing.Size(147, 20);
            this.dtContrato.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Data:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Número:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCpf);
            this.groupBox1.Controls.Add(this.dtNascContato);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(8, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 76);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dados do Contato";
            // 
            // txtCpf
            // 
            this.txtCpf.Location = new System.Drawing.Point(95, 19);
            this.txtCpf.Mask = "000,000,000-00";
            this.txtCpf.Name = "txtCpf";
            this.txtCpf.Size = new System.Drawing.Size(151, 20);
            this.txtCpf.TabIndex = 0;
            this.txtCpf.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // dtNascContato
            // 
            this.dtNascContato.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtNascContato.Location = new System.Drawing.Point(95, 45);
            this.dtNascContato.Name = "dtNascContato";
            this.dtNascContato.Size = new System.Drawing.Size(151, 20);
            this.dtNascContato.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Dt. Nascimento:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "CPF:";
            // 
            // txtCnpj
            // 
            this.txtCnpj.Location = new System.Drawing.Point(51, 6);
            this.txtCnpj.Mask = "00,000,000/0000-00";
            this.txtCnpj.Name = "txtCnpj";
            this.txtCnpj.Size = new System.Drawing.Size(145, 20);
            this.txtCnpj.TabIndex = 0;
            this.txtCnpj.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "CNPJ:";
            // 
            // tabLicenca
            // 
            this.tabLicenca.Controls.Add(this.txtLicenca);
            this.tabLicenca.Location = new System.Drawing.Point(4, 22);
            this.tabLicenca.Name = "tabLicenca";
            this.tabLicenca.Padding = new System.Windows.Forms.Padding(3);
            this.tabLicenca.Size = new System.Drawing.Size(490, 175);
            this.tabLicenca.TabIndex = 1;
            this.tabLicenca.Text = "Licença Criptografada";
            this.tabLicenca.UseVisualStyleBackColor = true;
            // 
            // txtLicenca
            // 
            this.txtLicenca.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLicenca.Location = new System.Drawing.Point(3, 3);
            this.txtLicenca.Multiline = true;
            this.txtLicenca.Name = "txtLicenca";
            this.txtLicenca.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLicenca.Size = new System.Drawing.Size(484, 169);
            this.txtLicenca.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtXml);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(490, 175);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "XML";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtXml
            // 
            this.txtXml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtXml.Location = new System.Drawing.Point(3, 3);
            this.txtXml.Name = "txtXml";
            this.txtXml.ReadOnly = true;
            this.txtXml.Size = new System.Drawing.Size(484, 169);
            this.txtXml.TabIndex = 0;
            this.txtXml.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtCertificado);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(490, 175);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "Certificado";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtCertificado
            // 
            this.txtCertificado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCertificado.Location = new System.Drawing.Point(3, 3);
            this.txtCertificado.Multiline = true;
            this.txtCertificado.Name = "txtCertificado";
            this.txtCertificado.ReadOnly = true;
            this.txtCertificado.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCertificado.Size = new System.Drawing.Size(484, 169);
            this.txtCertificado.TabIndex = 0;
            // 
            // grupoBios
            // 
            this.grupoBios.Controls.Add(this.txtBiosVersao);
            this.grupoBios.Controls.Add(this.txtBiosSoftware);
            this.grupoBios.Controls.Add(this.txtBiosNome);
            this.grupoBios.Controls.Add(this.label10);
            this.grupoBios.Controls.Add(this.label8);
            this.grupoBios.Controls.Add(this.label9);
            this.grupoBios.Location = new System.Drawing.Point(11, 209);
            this.grupoBios.Name = "grupoBios";
            this.grupoBios.Size = new System.Drawing.Size(471, 112);
            this.grupoBios.TabIndex = 4;
            this.grupoBios.TabStop = false;
            this.grupoBios.Text = "Dados BIOS";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "SoftwareElementID:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Nome:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 75);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Versão:";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(17, 186);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(104, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Instalação Local";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // txtBiosNome
            // 
            this.txtBiosNome.Location = new System.Drawing.Point(121, 23);
            this.txtBiosNome.Name = "txtBiosNome";
            this.txtBiosNome.Size = new System.Drawing.Size(328, 20);
            this.txtBiosNome.TabIndex = 2;
            // 
            // txtBiosSoftware
            // 
            this.txtBiosSoftware.Location = new System.Drawing.Point(121, 49);
            this.txtBiosSoftware.Name = "txtBiosSoftware";
            this.txtBiosSoftware.Size = new System.Drawing.Size(328, 20);
            this.txtBiosSoftware.TabIndex = 3;
            // 
            // txtBiosVersao
            // 
            this.txtBiosVersao.Location = new System.Drawing.Point(121, 75);
            this.txtBiosVersao.Name = "txtBiosVersao";
            this.txtBiosVersao.Size = new System.Drawing.Size(328, 20);
            this.txtBiosVersao.TabIndex = 4;
            // 
            // Main
            // 
            this.AcceptButton = this.btnGerar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 388);
            this.Controls.Add(this.tabCtrlMain);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.ShowIcon = false;
            this.Text = "Gerador de Licenças";
            this.panel1.ResumeLayout(false);
            this.tabCtrlMain.ResumeLayout(false);
            this.tabDados.ResumeLayout(false);
            this.tabDados.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabLicenca.ResumeLayout(false);
            this.tabLicenca.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.grupoBios.ResumeLayout(false);
            this.grupoBios.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabCtrlMain;
        private System.Windows.Forms.TabPage tabDados;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.MaskedTextBox txtContrato;
        private System.Windows.Forms.DateTimePicker dtContrato;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MaskedTextBox txtCpf;
        private System.Windows.Forms.DateTimePicker dtNascContato;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox txtCnpj;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabLicenca;
        private System.Windows.Forms.Button btnGerar;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DateTimePicker dtFim;
        private System.Windows.Forms.DateTimePicker dtInicio;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtLicenca;
        private System.Windows.Forms.Button btnCopiar;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtCertificado;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.RichTextBox txtXml;
        private System.Windows.Forms.GroupBox grupoBios;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox txtBiosVersao;
        private System.Windows.Forms.TextBox txtBiosSoftware;
        private System.Windows.Forms.TextBox txtBiosNome;

    }
}