namespace C2BR.GestorEducacao.Bios
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
            this.tabCtrlMain = new System.Windows.Forms.TabControl();
            this.tabDados = new System.Windows.Forms.TabPage();
            this.grupoBios = new System.Windows.Forms.GroupBox();
            this.txtBiosVersao = new System.Windows.Forms.TextBox();
            this.txtBiosSoftware = new System.Windows.Forms.TextBox();
            this.txtBiosNome = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tabCtrlMain.SuspendLayout();
            this.tabDados.SuspendLayout();
            this.grupoBios.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 151);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(498, 10);
            this.panel1.TabIndex = 1;
            // 
            // tabCtrlMain
            // 
            this.tabCtrlMain.Controls.Add(this.tabDados);
            this.tabCtrlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrlMain.Location = new System.Drawing.Point(0, 0);
            this.tabCtrlMain.Name = "tabCtrlMain";
            this.tabCtrlMain.SelectedIndex = 0;
            this.tabCtrlMain.Size = new System.Drawing.Size(498, 151);
            this.tabCtrlMain.TabIndex = 0;
            // 
            // tabDados
            // 
            this.tabDados.Controls.Add(this.grupoBios);
            this.tabDados.Location = new System.Drawing.Point(4, 22);
            this.tabDados.Name = "tabDados";
            this.tabDados.Padding = new System.Windows.Forms.Padding(3);
            this.tabDados.Size = new System.Drawing.Size(490, 125);
            this.tabDados.TabIndex = 0;
            this.tabDados.Text = "Dados da BIOS";
            this.tabDados.UseVisualStyleBackColor = true;
            // 
            // grupoBios
            // 
            this.grupoBios.Controls.Add(this.txtBiosVersao);
            this.grupoBios.Controls.Add(this.txtBiosSoftware);
            this.grupoBios.Controls.Add(this.txtBiosNome);
            this.grupoBios.Controls.Add(this.label10);
            this.grupoBios.Controls.Add(this.label8);
            this.grupoBios.Controls.Add(this.label9);
            this.grupoBios.Location = new System.Drawing.Point(8, 6);
            this.grupoBios.Name = "grupoBios";
            this.grupoBios.Size = new System.Drawing.Size(471, 112);
            this.grupoBios.TabIndex = 4;
            this.grupoBios.TabStop = false;
            this.grupoBios.Text = "Dados BIOS";
            // 
            // txtBiosVersao
            // 
            this.txtBiosVersao.Location = new System.Drawing.Point(121, 75);
            this.txtBiosVersao.Name = "txtBiosVersao";
            this.txtBiosVersao.Size = new System.Drawing.Size(328, 20);
            this.txtBiosVersao.TabIndex = 4;
            // 
            // txtBiosSoftware
            // 
            this.txtBiosSoftware.Location = new System.Drawing.Point(121, 49);
            this.txtBiosSoftware.Name = "txtBiosSoftware";
            this.txtBiosSoftware.Size = new System.Drawing.Size(328, 20);
            this.txtBiosSoftware.TabIndex = 3;
            // 
            // txtBiosNome
            // 
            this.txtBiosNome.Location = new System.Drawing.Point(121, 23);
            this.txtBiosNome.Name = "txtBiosNome";
            this.txtBiosNome.Size = new System.Drawing.Size(328, 20);
            this.txtBiosNome.TabIndex = 2;
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
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 161);
            this.Controls.Add(this.tabCtrlMain);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.ShowIcon = false;
            this.Text = "BIOS";
            this.tabCtrlMain.ResumeLayout(false);
            this.tabDados.ResumeLayout(false);
            this.grupoBios.ResumeLayout(false);
            this.grupoBios.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabCtrlMain;
        private System.Windows.Forms.TabPage tabDados;
        private System.Windows.Forms.GroupBox grupoBios;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtBiosVersao;
        private System.Windows.Forms.TextBox txtBiosSoftware;
        private System.Windows.Forms.TextBox txtBiosNome;

    }
}