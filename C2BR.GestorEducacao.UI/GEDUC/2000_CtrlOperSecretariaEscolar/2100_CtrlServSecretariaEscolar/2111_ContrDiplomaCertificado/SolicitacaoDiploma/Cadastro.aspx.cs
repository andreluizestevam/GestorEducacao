//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE DIPLOMAS E CERTIFICADOS
// OBJETIVO: SOLICITAÇÃO DE DIPLOMAS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2111_ContrDiplomaCertificado.SolicitacaoDiploma
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

//--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
        }        

        protected void Page_Load(object sender, EventArgs e)
        {
            string dataAtual = DateTime.Now.ToString("dd/MM/yyyy");

            if (IsPostBack)
                return;

            CarregaModalidades();
            txtDataPrev.Text = DateTime.Now.AddDays(10).ToString("dd/MM/yyyy");
            txtDataSol.Text = txtCadastro.Text = txtDataStatus.Text = dataAtual;
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
            int coCol = ddlColaborador.SelectedValue != "" ? int.Parse(ddlColaborador.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            var ocorMatricula = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                 where tb08.CO_ALU == coAlu && tb08.CO_CUR == serie && tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                 && tb08.CO_SIT_MAT == "F" && tb08.CO_STA_APROV == "A" && tb08.CO_STA_APROV_FREQ == "A"
                                 select new { tb08.CO_ALU_CAD }).Count();

            if (ocorMatricula > 0)
            {
                TB211_SOLIC_DIPLOMA tb211 = TB211_SOLIC_DIPLOMA.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));

                if (tb211 == null)
                {
                    tb211 = new TB211_SOLIC_DIPLOMA();
                    tb211.CO_MODU_CUR = modalidade;
                    tb211.CO_CUR = serie;
                    var refAluno = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                    tb211.TB07_ALUNO = refAluno;
                    refAluno.TB25_EMPRESA1Reference.Load();
                    tb211.TB25_EMPRESA = refAluno.TB25_EMPRESA1;
                    tb211.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(coCol);
                    tb211.TP_USU_SOLIC = ddlTipoUsu.SelectedValue;
                    tb211.CO_USU = ddlTipoUsu.SelectedValue == "A" ? int.Parse(ddlAlunoSolicitante.SelectedValue) : ddlTipoUsu.SelectedValue == "R" ? int.Parse(ddlResponsavel.SelectedValue) : (int?)null;

                    if (ddlTipoUsu.SelectedValue == "O")
                    {
                        tb211.CO_RG_RESP_SOLIC_DIPLOMA = txtRGS.Text;
                        tb211.NO_RESP_SOLIC_DIPLOMA = txtNomeS.Text;
                        tb211.NU_TELE_RESP_SOLIC_DIPLOMA = txtTelefoneS.Text;
                    }

                    tb211.DT_SOLIC = Convert.ToDateTime(txtDataSol.Text);
                    tb211.DT_CADASTRO = Convert.ToDateTime(txtCadastro.Text);                    
                }
                tb211.DE_OBS = txtObservacao.Text;
                tb211.DT_STATUS = Convert.ToDateTime(txtDataStatus.Text);
                tb211.CO_STATUS = ddlStatus.SelectedValue;

                CurrentPadraoCadastros.CurrentEntity = tb211;
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possível efetuar a solicitação de Diploma para o Aluno selecionado");
                return;
            }
        }        
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB211_SOLIC_DIPLOMA tb211 = TB211_SOLIC_DIPLOMA.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));

            if (tb211 != null)
            {
                ddlModalidade.SelectedValue = tb211.CO_MODU_CUR.ToString();
                CarregaSerieCurso();
                ddlSerieCurso.SelectedValue = tb211.CO_CUR.ToString();
                CarregaAlunoSerie();
                tb211.TB07_ALUNOReference.Load();
                ddlAluno.SelectedValue = tb211.TB07_ALUNO.CO_ALU.ToString();
                tb211.TB03_COLABORReference.Load();
                ddlColaborador.SelectedValue = tb211.TB03_COLABOR.CO_COL.ToString();
                ddlTipoUsu.SelectedValue = tb211.TP_USU_SOLIC;
                CarregaNire();

                if (ddlTipoUsu.SelectedValue == "R")
                {
                    liResponsavel.Visible = true;
                    liAlunoSol.Visible = liOutroNomeSol.Visible = liOutroRGSol.Visible = liOutroFoneSol.Visible = false;
                    CarregaResponsavel(ddlResponsavel);
                    ddlResponsavel.SelectedValue = tb211.CO_USU.ToString();
                }
                else if (ddlTipoUsu.SelectedValue == "A")
                {
                    liResponsavel.Visible = liOutroNomeSol.Visible = liOutroRGSol.Visible = liOutroFoneSol.Visible = false;
                    liAlunoSol.Visible = true;
                    CarregaAluno(ddlAlunoSolicitante);
                    ddlAlunoSolicitante.SelectedValue = tb211.CO_USU.ToString();
                }
                else
                {
                    liOutroNomeSol.Visible = liOutroRGSol.Visible = liOutroFoneSol.Visible = true;
                    liResponsavel.Visible = liAlunoSol.Visible = false;
                    txtNomeS.Text = tb211.NO_RESP_SOLIC_DIPLOMA;
                    txtRGS.Text = tb211.CO_RG_RESP_SOLIC_DIPLOMA.ToString();
                    txtTelefoneS.Text = tb211.NU_TELE_RESP_SOLIC_DIPLOMA.ToString();
                }

                txtDataSol.Text = tb211.DT_SOLIC.ToString("dd/MM/yyyy");
                txtCadastro.Text = tb211.DT_CADASTRO.ToString("dd/MM/yyyy");

                txtObservacao.Text = tb211.DE_OBS;
                txtDataStatus.Text = tb211.DT_STATUS.ToString("dd/MM/yyyy");
                ddlStatus.SelectedValue = tb211.CO_STATUS;

                ddlModalidade.Enabled = ddlSerieCurso.Enabled = ddlAluno.Enabled = ddlColaborador.Enabled = ddlTipoUsu.Enabled = false;
            }
        }
        #endregion

        #region Carregamento
        
        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.Items.Clear();
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where( m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlModalidade.SelectedValue = "0";

            ddlSerieCurso.Items.Clear();
            ddlAluno.Items.Clear();
            ddlTipoUsu.Items.Clear();
            ddlResponsavel.Items.Clear();
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "0" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            if (modalidade != 0)
            {
                ddlSerieCurso.Items.Clear();
                ddlSerieCurso.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                            where tb43.TB44_MODULO.CO_MODU_CUR == modalidade
                                            join tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb43.CO_CUR equals tb01.CO_CUR
                                            where tb01.FLA_DIPLOMA == "S"
                                            select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy( c => c.NO_CUR );

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
                ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", "0"));
                ddlSerieCurso.SelectedValue = "0";

                ddlAluno.Items.Clear();
                ddlTipoUsu.Items.Clear();
                ddlResponsavel.Items.Clear();
            }

            
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        /// <param name="ddl">DropDown de aluno</param>
        private void CarregaAluno(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                              where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                              select new { NO_ALU = tb08.TB07_ALUNO.NO_ALU.ToUpper(), tb08.CO_ALU }).Distinct().OrderBy( m => m.NO_ALU );

            ddl.DataTextField = "NO_ALU";
            ddl.DataValueField = "CO_ALU";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Selecione", "0"));
            ddl.SelectedValue = "0";

        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        /// <param name="ddl">DropDown de aluno</param>
        private void CarregaAlunoSerie()
        {
            int serie = ddlSerieCurso.SelectedValue != "0" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if (serie != 0)
            {
                ddlAluno.Items.Clear();
                ddlAluno.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                  where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP && tb08.CO_CUR == serie && tb08.CO_SIT_MAT == "F"
                                  select new { NO_ALU = tb08.TB07_ALUNO.NO_ALU.ToUpper(), tb08.CO_ALU }).Distinct().OrderBy( m => m.NO_ALU );

                ddlAluno.DataTextField = "NO_ALU";
                ddlAluno.DataValueField = "CO_ALU";
                ddlAluno.DataBind();

                ddlAluno.Items.Insert(0, new ListItem("Selecione", "0"));
                ddlAluno.SelectedValue = "0";

                ddlTipoUsu.Items.Clear();
                ddlResponsavel.Items.Clear();
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Responsáveis
        /// </summary>
        /// <param name="ddl">DropDown de responsáveis</param>
        private void CarregaResponsavel(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.DataSource = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                              join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on tb07.TB108_RESPONSAVEL.CO_RESP equals tb108.CO_RESP
                              where tb07.TB25_EMPRESA1.CO_EMP == LoginAuxili.CO_EMP
                              select new { NO_RESP = tb108.NO_RESP.ToUpper(), tb108.CO_RESP }).Distinct().OrderBy( r => r.NO_RESP );

            ddl.DataTextField = "NO_RESP";
            ddl.DataValueField = "CO_RESP";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Selecione", "0"));
            ddl.SelectedValue = "0";
        }

        /// <summary>
        /// Método que carrega o dropdown de Colaboradores
        /// </summary>
        private void CarregaColaborador()
        {
            ddlColaborador.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(LoginAuxili.CO_EMP)
                                         select new { NO_COL = tb03.NO_COL.ToUpper(), tb03.CO_COL }).OrderBy( c => c.NO_COL );

            ddlColaborador.DataTextField = "NO_COL";
            ddlColaborador.DataValueField = "CO_COL";
            ddlColaborador.DataBind();                  
        }

        /// <summary>
        /// Método que carrega o NIRE do Aluno selecionado
        /// </summary>
        private Boolean CarregaNire()
        {
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;

            if (coAlu == 0)
                return false;
            txtNire.Text = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                            where tb07.CO_ALU == coAlu
                            select new { tb07.NU_NIRE }).FirstOrDefault().NU_NIRE.ToString();
            if (txtNire.Text == "")
                return false;
            return true;
        }
        /// <summary>
        /// Carrega os tipos
        /// </summary>
        private void CarregaTipo() 
        {
            ddlTipoUsu.Items.Clear();
            ddlTipoUsu.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlTipoUsu.Items.Insert(1, new ListItem("Aluno", "A"));
            ddlTipoUsu.Items.Insert(2, new ListItem("Responsável", "R"));
            ddlTipoUsu.Items.Insert(3, new ListItem("Outros", "O"));
            ddlTipoUsu.SelectedValue = "0";

            ddlResponsavel.Items.Clear();
        }
        #endregion

        protected void ddlAluno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
            {
                if(CarregaNire())
                    CarregaTipo();
            }
        }

        protected void ddlTipoUsu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
            {
                CarregaColaborador();
                if (ddlTipoUsu.SelectedValue == "R")
                {
                    liResponsavel.Visible = true;
                    liAlunoSol.Visible = liOutroNomeSol.Visible = liOutroRGSol.Visible = liOutroFoneSol.Visible = false;
                    CarregaResponsavel(ddlResponsavel);
                }
                else if (ddlTipoUsu.SelectedValue == "A")
                {
                    liResponsavel.Visible = liOutroNomeSol.Visible = liOutroRGSol.Visible = liOutroFoneSol.Visible = false;
                    liAlunoSol.Visible = true;
                    CarregaAluno(ddlAlunoSolicitante);
                }
                else
                {
                    liOutroNomeSol.Visible = liOutroRGSol.Visible = liOutroFoneSol.Visible = true;
                    liResponsavel.Visible = liAlunoSol.Visible = false;
                }
            }
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(((DropDownList)sender).SelectedValue != "0")
                CarregaSerieCurso();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
                CarregaAlunoSerie();
        }
    }
}