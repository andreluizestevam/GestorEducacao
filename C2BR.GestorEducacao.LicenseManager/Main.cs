using System;
using System.Windows.Forms;
using C2BR.GestorEducacao.LicenseValidator;
using System.Collections.Generic;
using System.Management;

namespace C2BR.GestorEducacao.LicenseManager
{
    public partial class Main : Form
    {
        #region ctor

        public Main()
        {
            InitializeComponent();

            this.dtInicio.Value = DateTime.Now.Date;
            this.dtFim.Value = DateTime.Now.AddMonths(1).Date;
            this.txtCnpj.Select();


            /* 
             * Por padrão traz os dados da bios, para diminuir trabalho e para facilitar o teste (principalmente para facilitar os testes)
             * Na verdade só vai ter utilidade para os testes mesmo xP
            */
            
            //Classe para poder realizar busca de informações da bios
            ManagementObjectSearcher search = new ManagementObjectSearcher("Select * from Win32_BIOS");

            // Guarda resultado da busca
            ManagementObjectCollection collection = search.Get();

            foreach (ManagementObject obj in collection)
            {
                txtBiosNome.Text = obj["Name"].ToString().Trim();
                txtBiosSoftware.Text = obj["SoftwareElementID"].ToString().Trim();
                txtBiosVersao.Text = obj["Version"].ToString().Trim();
            }
        } 

        #endregion

        #region Gerar Licença

        private void btnGerar_Click(object sender, EventArgs e)
        {
            try
            {
                string biosName = "";
                string biosElementId = "";
                string biosVersion = "";

                if (grupoBios.Visible)
                {
                    biosName = txtBiosNome.Text.Trim();
                    biosElementId = txtBiosSoftware.Text.Trim();
                    biosVersion = txtBiosVersao.Text.Trim();
                }

                License lic = new License()
                    {
                        Cnpj = txtCnpj.Text,
                        ContatoCpf = txtCpf.Text,
                        ContatoDataNasc = dtNascContato.Value,
                        ContratoData = dtContrato.Value,
                        ContratoNumero = txtContrato.Text,
                        DataFim = dtFim.Value,
                        DataInicio = dtInicio.Value,
                        BiosName = biosName,
                        BiosElementId = biosElementId,
                        BiosVersion = biosVersion,
                        Local = checkBox1.Checked
                    };

                try
                {
                    Exception ex;
                    string xml;
                    if (!SerializableHelper.Serialize<License>(lic, out xml, out ex))
                        throw ex;

                    this.txtXml.Text = Signer.SignXml(xml);
                    if (string.IsNullOrEmpty(this.txtXml.Text))
                        return;

                    this.txtLicenca.Text = Signer.EncryptXml(this.txtXml.Text);
                    this.tabCtrlMain.SelectedIndex = 1;
                }
                catch { throw; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        } 

        #endregion

        #region Exportar Certificado

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                this.txtCertificado.Text = Signer.GetPublicKey();
                if (string.IsNullOrEmpty(this.txtCertificado.Text))
                    return;

                this.tabCtrlMain.SelectedIndex = 3;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        } 

        #endregion

        #region Copiar Conteúdo

        private void btnCopiar_Click(object sender, EventArgs e)
        {
            if (tabCtrlMain.SelectedIndex == 1)
            {
                Clipboard.SetData(DataFormats.Text, txtLicenca.Text);
            }
            else if (tabCtrlMain.SelectedIndex == 2)
            {
                Clipboard.SetData(DataFormats.Rtf, txtXml.Text);
            }
            else if (tabCtrlMain.SelectedIndex == 3)
            {
                Clipboard.SetData(DataFormats.Text, txtCertificado.Text);
            }

            MessageBox.Show("Conteúdo copiado para a área de transferência com sucesso.");
        } 

        #endregion

        #region Outros Eventos

        private void tabCtrlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnCopiar.Enabled = (tabCtrlMain.SelectedIndex == 1 || tabCtrlMain.SelectedIndex == 2
                || tabCtrlMain.SelectedIndex == 3);
        } 

        #endregion

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                grupoBios.Visible = false;
            }
            else
            {
                grupoBios.Visible = true;
            }

        }
    }
}
