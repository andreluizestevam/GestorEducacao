//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// OBJETIVO: AGENDA DE CONTATOS
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
    public partial class AgendaContatos : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaUnidade();
                VerificaTipoUnid();
            }
        }                      
        #endregion     
 
        #region Métodos

        /// <summary>
        /// Verifica o tipo da unidade e adequa os campos necessários
        /// </summary>
        private void VerificaTipoUnid()
        {
            switch (LoginAuxili.CO_TIPO_UNID)
            {
                case "PGS":
                    ddlTpContato.Items.Clear();
                    ddlTpContato.Items.Insert(0, new ListItem("Fornecedor", "C"));
                    ddlTpContato.Items.Insert(0, new ListItem("Responsável", "R"));
                    ddlTpContato.Items.Insert(0, new ListItem("Paciente", "A"));
                    ddlTpContato.Items.Insert(0, new ListItem("Profissional Saúde", "P"));
                    ddlTpContato.Items.Insert(0, new ListItem("Funcionário", "F"));
                    break;
                case "PGE":
                default:
                    ddlTpContato.Items.Clear();
                    ddlTpContato.Items.Insert(0, new ListItem("Fornecedor", "C"));
                    ddlTpContato.Items.Insert(0, new ListItem("Responsável", "R"));
                    ddlTpContato.Items.Insert(0, new ListItem("Aluno", "A"));
                    ddlTpContato.Items.Insert(0, new ListItem("Professor(a)", "P"));
                    ddlTpContato.Items.Insert(0, new ListItem("Funcionário", "F"));
                    break;
            }
        }

        /// <summary>
        /// Carrega a grid de acordo com o tipo do contato
        /// </summary>
        /// <param name="tpContato">Tipo do contato ("A"luno, "F"uncionário, "P"rofessor, "R"esponsável e Forne"C"edor)</param>
        private void CarregaGrid(string tpContato)
        {
            int coEmp = ddlUnidade.SelectedValue != "T" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            if (tpContato == "A")
            {
                grdAgendaContatos.DataSource = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                                where (txtContato.Text != "" ? tb07.NO_ALU.Contains(txtContato.Text) : txtContato.Text == "")
                                                && (coEmp != 0 ? tb07.CO_EMP == coEmp : coEmp == 0)
                                                select new
                                                {
                                                    CONTATO = (tb07.NO_ALU).ToUpper(), EMAIL = "", TRABALHO = "-",
                                                    CELULAR = tb07.NU_TELE_CELU_ALU != null && tb07.NU_TELE_CELU_ALU != "" ? tb07.NU_TELE_CELU_ALU.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                                                    TELEFONE = tb07.NU_TELE_RESI_ALU != null && tb07.NU_TELE_RESI_ALU != "" ? tb07.NU_TELE_RESI_ALU.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : ""
                                                }).OrderBy( a => a.CONTATO );
            }
            else if (tpContato == "F")
            {
                grdAgendaContatos.DataSource = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                                where tb03.FLA_PROFESSOR == "N"
                                                && (txtContato.Text != "" ? tb03.NO_COL.Contains(txtContato.Text) : txtContato.Text == "")
                                                && (coEmp != 0 ? tb03.CO_EMP == coEmp : coEmp == 0)
                                                select new
                                                {
                                                    CONTATO = (tb03.NO_COL).ToUpper(), EMAIL = tb03.CO_EMAI_COL,
                                                    CELULAR = tb03.NU_TELE_CELU_COL != null && tb03.NU_TELE_CELU_COL != "" ? tb03.NU_TELE_CELU_COL.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                                                    TELEFONE = tb03.NU_TELE_RESI_COL != null && tb03.NU_TELE_RESI_COL != "" ? tb03.NU_TELE_RESI_COL.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                                                    TRABALHO = tb03.NU_TELE_COME_COL != null && tb03.NU_TELE_COME_COL != "" ? tb03.NU_TELE_COME_COL.Insert(0, "(").Insert(3, ") ").Insert(9, "-") + " / " + tb03.NU_RAMA_COME_COL : "-"
                                                }).OrderBy( f => f.CONTATO );
            }
            else if (tpContato == "P")
            {
                grdAgendaContatos.DataSource = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                                where tb03.FLA_PROFESSOR == "S"
                                                && (txtContato.Text != "" ? tb03.NO_COL.Contains(txtContato.Text) : txtContato.Text == "")
                                                && (coEmp != 0 ? tb03.CO_EMP == coEmp : coEmp == 0)
                                                select new
                                                {
                                                    CONTATO = (tb03.NO_COL).ToUpper(), EMAIL = tb03.CO_EMAI_COL,
                                                    CELULAR = tb03.NU_TELE_CELU_COL != null && tb03.NU_TELE_CELU_COL != "" ? tb03.NU_TELE_CELU_COL.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                                                    TELEFONE = tb03.NU_TELE_RESI_COL != null && tb03.NU_TELE_RESI_COL != "" ? tb03.NU_TELE_RESI_COL.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                                                    TRABALHO = tb03.NU_TELE_COME_COL != null && tb03.NU_TELE_COME_COL != "" ? tb03.NU_TELE_COME_COL.Insert(0, "(").Insert(3, ") ").Insert(9, "-") + " / " + tb03.NU_RAMA_COME_COL : "-"
                                                }).OrderBy( f => f.CONTATO );
            }
            else if (tpContato == "R")
            {
                grdAgendaContatos.DataSource = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                                where (txtContato.Text != "" ? tb108.NO_RESP.Contains(txtContato.Text) : txtContato.Text == "")
                                                select new
                                                {
                                                    CONTATO = (tb108.NO_RESP).ToUpper(), EMAIL = tb108.DES_EMAIL_RESP,
                                                    CELULAR = tb108.NU_TELE_CELU_RESP != null && tb108.NU_TELE_CELU_RESP != "" ? tb108.NU_TELE_CELU_RESP.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                                                    TELEFONE = tb108.NU_TELE_RESI_RESP != null && tb108.NU_TELE_RESI_RESP != "" ? tb108.NU_TELE_RESI_RESP.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                                                    TRABALHO = tb108.NU_TELE_COME_RESP != null && tb108.NU_TELE_COME_RESP != "" ? tb108.NU_TELE_RESI_RESP.Insert(0, "(").Insert(3, ") ").Insert(9, "-") + " / " + tb108.NU_RAMA_COME_RESP : "-"
                                                }).OrderBy( r => r.CONTATO );
            }
            else
            {
                grdAgendaContatos.DataSource = (from tb41 in TB41_FORNEC.RetornaTodosRegistros()
                                                where (txtContato.Text != "" ? tb41.NO_FAN_FOR.Contains(txtContato.Text) : txtContato.Text == "")
                                                select new
                                                {
                                                    CONTATO = (tb41.NO_FAN_FOR).ToUpper(), EMAIL = tb41.DE_EMAIL_CLI,
                                                    CELULAR = tb41.CO_CEL_FORN != null && tb41.CO_CEL_FORN != "" ? tb41.CO_CEL_FORN.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                                                    TELEFONE = tb41.CO_TEL2_FORN != null && tb41.CO_TEL2_FORN != "" ? tb41.CO_TEL2_FORN.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                                                    TRABALHO = tb41.CO_TEL1_FORN != null && tb41.CO_TEL1_FORN != "" ? tb41.CO_TEL1_FORN.Insert(0, "(").Insert(3, ") ").Insert(9, "-") + " / " + tb41.CO_RAMA_TEL1_FORN : "-"
                                                }).OrderBy(f => f.CONTATO);
            }

            grdAgendaContatos.DataBind();
        }

        /// <summary>
        /// Carrega o dropdown de Unidades
        /// </summary>
        private void CarregaUnidade()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();

            ddlUnidade.Items.Insert(0, new ListItem("Todas", "T"));
        }
        #endregion

        protected void grdAgendaContatos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
//--------> Criação do estilo das linhas da GRID
            if (e.Row.DataItem != null)
            {
                e.Row.Attributes.Add("onMouseOver", "this.style.backgroundColor='#DDDDDD'; this.style.cursor='hand';");
                e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor='#ffffff'");
            }
        }        

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

//--------> Chamada ao método que carrega a grid de acordo com o tipo de contato
            CarregaGrid(ddlTpContato.SelectedValue);
        } 
    }
}