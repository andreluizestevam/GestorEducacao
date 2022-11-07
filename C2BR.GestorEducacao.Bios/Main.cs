using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Management;

namespace C2BR.GestorEducacao.Bios
{
    public partial class Main : Form
    {

        public Main()
        {
            InitializeComponent();
            
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

    }
}
