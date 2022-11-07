using C2BR.GestorEducacao.BusinessEntities.Auxiliar;
using C2BR.GestorEducacao.UI.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace C2BR.GestorEducacao.UI.Componentes
{
    public partial class Gestante : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                C2BR.GestorEducacao.UI.Object.TBS478_ATEND_GESTANTE_BO BO = new C2BR.GestorEducacao.UI.Object.TBS478_ATEND_GESTANTE_BO();
                BO = (C2BR.GestorEducacao.UI.Object.TBS478_ATEND_GESTANTE_BO)Session["TBS478_ATEND_GESTANTE_BO"];

                if (BO != null)
                {
                    tbaltura.Text = BO.AUTURA_RA;
                    if (!String.IsNullOrEmpty(tbaltura.Text))
                        tbaltura.Enabled = false;

                    tbpeso.Text = BO.PESO;
                    if (!String.IsNullOrEmpty(tbpeso.Text))
                        tbpeso.Enabled = false;

                    tbpa.Text = BO.PA;
                    if (!String.IsNullOrEmpty(tbpa.Text))
                        tbpa.Enabled = false;

                    tbbcf.Text = BO.BCF;
                    if (!String.IsNullOrEmpty(tbbcf.Text))
                        tbbcf.Enabled = false;

                    tbsaturacao.Text = BO.SATURACAO;
                    if (!String.IsNullOrEmpty(tbsaturacao.Text))
                        tbsaturacao.Enabled = false;

                    tbglicemia.Text = BO.GLICEMIA;
                    if (!String.IsNullOrEmpty(tbglicemia.Text))
                        tbglicemia.Enabled = false;

                    ddlleitura.SelectedValue = BO.LEITURAGLICEMICA;
                    if (ddlleitura.SelectedIndex > 0)
                        ddlleitura.Enabled = false;
                }
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //string TEMP = 
            TBS478_ATEND_GESTANTE_BO BO = new TBS478_ATEND_GESTANTE_BO();
            //TBS478_ATEND_GESTANTE_BUSINESS insere = new TBS478_ATEND_GESTANTE_BUSINESS();
            BO.AUTURA_RA = tbaltura.Text;
            BO.AUTURA_RPN = tbautura.Text;
            BO.BCF = tbbcf.Text;
            BO.CO_ALUNO = 0;
            BO.COD_GESTANTE = 0;
            BO.DPP = Convert.ToDateTime(tbdpp.Text);
            BO.DT_REGISTRO = tbdataregistro.Text;
            BO.DUM = Convert.ToDateTime(tbdum.Text);
            BO.EDMA = ddledma.SelectedValue;
            BO.ID_ATEND_GESTANTE = 0;
            BO.IDADE_GESTANTE = tbidadegestante.Text;
            BO.IMC = tbimc.Text;
            BO.MF = tbmf.Text;
            BO.OBS_ANTRO = tbobsantropometria.Text;
            BO.OBS_COMPLEMENTO = tbobservacaocomplemento.Text;
            BO.OBS_DUM = tbobsdum.Text;
            BO.OBS_MF = tbobsmf.Text;
            BO.PC = tbpc.Text;
            BO.PESO = tbpeso.Text;
            BO.PP = tbpp.Text;
            BO.TIPO_REG = ddltiporegistro.SelectedValue;
            BO.PA = tbpa.Text;
            BO.SATURACAO = tbsaturacao.Text;
            BO.GLICEMIA = tbglicemia.Text;
            BO.LEITURAGLICEMICA = ddlleitura.SelectedValue;
            Session["TBS478_ATEND_GESTANTE_BO"] = BO;
            //insere.InsereTBS478(BO);
        }
    }
}