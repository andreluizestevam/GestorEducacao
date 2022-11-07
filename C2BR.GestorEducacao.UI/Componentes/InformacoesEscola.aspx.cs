//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// OBJETIVO: INFORMAÇÕES DA ESCOLA
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas 
using System;
using System.Collections.Generic;
using System.Linq;
using Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Linq.Expressions;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Web.Security;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Componentes
{
    public partial class InformacoesEscola : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
//------------> Chamada do método de preenchimento do formulário da funcionalidade
                CarregaFormulario();
            }
        }

//====> Método de preenchimento do formulário da funcionalidade
        private void CarregaFormulario()
        {
            TB25_EMPRESA tb25 = RetornaEntidade();

            tb25.TB24_TPEMPRESAReference.Load();

            tb25.TB162_CLAS_INSTReference.Load();

            tb25.ImageReference.Load();

            if (tb25.Image != null)
            {
                imgUnid.ImageUrl = "~/LerImagem.ashx?idimg=" + tb25.Image.ImageId;
            }

            txtNome.Text = tb25.NO_FANTAS_EMP;
            txtRazaoSocial.Text = tb25.NO_RAZSOC_EMP;
            txtSigla.Text = tb25.sigla;
            txtCNPJ.Text = tb25.CO_CPFCGC_EMP;

            ddlTipoUnidade.DataSource = from tb24 in TB24_TPEMPRESA.RetornaTodosRegistros()
                                        where tb24.CO_TIPOEMP == tb25.TB24_TPEMPRESA.CO_TIPOEMP
                                        select new { tb24.CO_TIPOEMP, tb24.NO_TIPOEMP };

            ddlTipoUnidade.DataTextField = "NO_TIPOEMP";
            ddlTipoUnidade.DataValueField = "CO_TIPOEMP";
            ddlTipoUnidade.DataBind();

            ddlClassificacao.DataSource = from tb162 in TB162_CLAS_INST.RetornaTodosRegistros()
                                          where tb162.CO_CLAS == tb25.TB162_CLAS_INST.CO_CLAS
                                          select new { tb162.CO_CLAS, tb162.NO_CLAS };

            ddlClassificacao.DataTextField = "NO_CLAS";
            ddlClassificacao.DataValueField = "CO_CLAS";
            ddlClassificacao.DataBind();

            txtMEC.Text = tb25.CO_REG_MEC;
            txtINEP.Text = tb25.NU_INEP.HasValue ? tb25.NU_INEP.ToString() : "";
            txtNumDecreto.Text = tb25.NU_ATA.HasValue ? tb25.NU_ATA.ToString() : "";
            txtDtDecreto.Text = tb25.DT_ATA.HasValue ? tb25.DT_ATA.Value.ToString("dd/MM/yyyy") : "";

            if (tb25.CO_TEL1_EMP != null)
                txtTelefone.Text = tb25.CO_TEL1_EMP.ToString();

            if (tb25.CO_TEL2_EMP != null)
                txtTelefone2.Text = tb25.CO_TEL2_EMP.ToString();

            if (tb25.CO_FAX_EMP != null)
                txtFax.Text = tb25.CO_FAX_EMP.ToString();

            txtWebSite.Text = tb25.NO_WEB_EMP;
            txtEmail.Text = tb25.NO_EMAIL_EMP;
            txtCEP.Text = tb25.CO_CEP_EMP;
            txtLogradouro.Text = tb25.DE_END_EMP;
            txtComplemento.Text = tb25.DE_COM_ENDE_EMP;
            ddlStatus.SelectedValue = tb25.CO_SIT_EMP;
            txtDtStatus.Text = tb25.DT_SIT_EMP.ToString("dd/MM/yyyy");
            txtDtCadastro.Text = tb25.DT_CAD_EMP.ToString("dd/MM/yyyy");
            txtInscEstadual.Text = tb25.CO_INS_ESTA_EMP;
            txtInscMunicipal.Text = tb25.CO_INS_MUNI_EMP;
            
            int? coNucleo = tb25.CO_NUCLEO != null ? (int?)tb25.CO_NUCLEO : 0;

            ddlNucleo.DataSource = from tbNucleoInst in TB_NUCLEO_INST.RetornaTodosRegistros()
                                   where tbNucleoInst.CO_NUCLEO == coNucleo
                                    select new { tbNucleoInst.NO_SIGLA_NUCLEO, tbNucleoInst.CO_NUCLEO };

            ddlNucleo.DataTextField = "NO_SIGLA_NUCLEO";
            ddlNucleo.DataValueField = "CO_NUCLEO";
            ddlNucleo.DataBind();

            txtObservacao.Text = tb25.DE_OBS_EMP;
            ddlMatriz.SelectedValue = tb25.FLA_UNID_GESTORA;

            ddlUF.DataSource = from tb74 in TB74_UF.RetornaTodosRegistros()
                               where tb74.CODUF == tb25.CO_UF_EMP
                               select new { tb74.CODUF };

            ddlUF.DataTextField = "CODUF";
            ddlUF.DataValueField = "CODUF";
            ddlUF.DataBind();

            ddlCidade.DataSource = from tb904 in TB904_CIDADE.RetornaTodosRegistros()
                                   where tb904.CO_CIDADE == tb25.CO_CIDADE
                                   select new { tb904.CO_CIDADE, tb904.NO_CIDADE };

            ddlCidade.DataTextField = "NO_CIDADE";
            ddlCidade.DataValueField = "CO_CIDADE";
            ddlCidade.DataBind();

            ddlBairro.DataSource = from tb905 in TB905_BAIRRO.RetornaPelaCidade(tb25.CO_CIDADE)
                                   where tb905.CO_BAIRRO == tb25.CO_BAIRRO
                                   select new { tb905.NO_BAIRRO, tb905.CO_BAIRRO };

            ddlBairro.DataTextField = "NO_BAIRRO";
            ddlBairro.DataValueField = "CO_BAIRRO";
            ddlBairro.DataBind();
        }

//====> Método de retorno da entidade informada
        private TB25_EMPRESA RetornaEntidade()
        {
            return TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
        }
        #endregion
    }
}