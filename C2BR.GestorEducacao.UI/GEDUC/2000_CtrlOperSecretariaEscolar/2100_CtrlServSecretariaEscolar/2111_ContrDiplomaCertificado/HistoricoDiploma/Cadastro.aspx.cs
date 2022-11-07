//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE DIPLOMAS E CERTIFICADOS
// OBJETIVO: HISTÓRICO DE DIPLOMAS
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
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2111_ContrDiplomaCertificado.HistoricoDiploma
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
            if (IsPostBack)
                return;

            CarregaAluno();
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaColaborador();
            CarregaDiploma();

            txtDataHis.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDataPrev.Text = DateTime.Now.AddDays(10).ToString("dd/MM/yyyy");
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB210_HIST_DIPLOMA tb210 = TB210_HIST_DIPLOMA.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));

            if (tb210 == null) 
            {
                tb210 = new TB210_HIST_DIPLOMA();

                int coDiploma = ddlDiploma.SelectedValue != "" ? int.Parse(ddlDiploma.SelectedValue) : 0;
                tb210.TB16_DIPLOMA = TB16_DIPLOMA.RetornaPelaChavePrimaria(coDiploma);
                tb210.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);
            }

            tb210.DE_HISTORICO = txtHis.Text;
            tb210.DE_OBS = txtObsHis.Text;
            tb210.DT_PREVISAO = Convert.ToDateTime(txtDataPrev.Text);
            tb210.DT_CADASTRO = Convert.ToDateTime(txtDataHis.Text);
            
            CurrentPadraoCadastros.CurrentEntity = tb210;
        }        
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB210_HIST_DIPLOMA tb210 = TB210_HIST_DIPLOMA.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));

            if (tb210 != null)
            {
                tb210.TB16_DIPLOMAReference.Load();
                ddlDiploma.SelectedValue = tb210.TB16_DIPLOMA.CO_DIPLOMA.ToString();

                CarregaCampos(tb210.TB16_DIPLOMA.CO_DIPLOMA);
                tb210.TB03_COLABORReference.Load();
                ddlColaborador.SelectedValue = tb210.TB03_COLABOR.CO_COL.ToString();
                txtDataHis.Text = tb210.DT_CADASTRO.ToString("dd/MM/yyyy");
                txtDataPrev.Text = tb210.DT_PREVISAO.Value.ToString("dd/MM/yyyy");
                txtObsHis.Text = tb210.DE_OBS;
                txtHis.Text = tb210.DE_HISTORICO;
            }                  
        }

        /// <summary>
        /// Método que preenche campos do diploma do formulário da funcionalidade
        /// </summary>
        /// <param name="codDipl">Id do diploma</param>
        private void CarregaCampos(int codDipl)
        {
            TB16_DIPLOMA tb16 = TB16_DIPLOMA.RetornaPelaChavePrimaria(codDipl);

            if (tb16 != null)
            {
                CarregaAluno();
                tb16.TB07_ALUNOReference.Load();
                ddlAluno.SelectedValue = tb16.TB07_ALUNO.CO_ALU.ToString();
                CarregaNire();
                ddlModalidade.SelectedValue = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                                               where tb01.CO_CUR == tb16.CO_CUR && tb01.CO_EMP == LoginAuxili.CO_EMP
                                               select new { tb01.CO_MODU_CUR }).FirstOrDefault().CO_MODU_CUR.ToString();

                CarregaSerieCurso();
                ddlSerieCurso.SelectedValue = tb16.CO_CUR.ToString();

                if (!String.IsNullOrEmpty(tb16.CO_ANO_SEM_CURSO))
                    txtAnoSemestreConclusao.Text = tb16.CO_ANO_SEM_CURSO.Substring(0, 4) + "/" + tb16.CO_ANO_SEM_CURSO.Substring(4, 2);

                txtDataColacaoGrau.Text = tb16.DT_COLACAO_GRAU != null ? tb16.DT_COLACAO_GRAU.Value.ToString("dd/MM/yyyy") : "";
                txtDataEntrega.Text = tb16.DT_ENTR_DIP != null ? tb16.DT_ENTR_DIP.Value.ToString("dd/MM/yyyy") : "";
                txtDataProcEscolar.Text = tb16.DT_REGI_ESC_DIP != null ? tb16.DT_REGI_ESC_DIP.Value.ToString("dd/MM/yyyy") : "";
                txtDataProcMec.Text = tb16.DT_REGI_MEC_DIP != null ? tb16.DT_REGI_MEC_DIP.Value.ToString("dd/MM/yyyy") : "";
                txtDataSolicitacao.Text = tb16.DT_SOLI_DIP.ToString("dd/MM/yyyy");
                txtDataInicio.Text = tb16.DT_INIC_CURS_DIP.ToString("dd/MM/yyyy");
                txtDataFim.Text = tb16.DT_TERM_CURS_DIP.ToString("dd/MM/yyyy");
                txtDataSolicitacao.Text = tb16.DT_SOLI_DIP.ToString("dd/MM/yyyy");
                txtDataFimSerie.Text = tb16.DT_PART_ENC != null ? tb16.DT_PART_ENC.Value.ToString("dd/MM/yyyy") : "";
                txtCargaHorariaCumprida.Text = tb16.NU_CARGA_HOR_CUMP.ToString();
                txtCargaHorariaSerie.Text = tb16.NU_CARGA_HOR_CURSO.ToString();
                txtNumeroLivroProcEscolar.Text = tb16.NU_LIVR_ESC_DIP.ToString();
                txtNumeroLivroProcMec.Text = tb16.NU_LIVR_MEC_DIP.ToString();
                txtNumeroPaginaProcEscolar.Text = tb16.NU_PAGI_ESC_DIP.ToString();
                txtNumeroPaginaProcMec.Text = tb16.NU_PAGI_MEC_DIP.ToString();
                txtNumeroProcessoEscolar.Text = tb16.NU_PROC_ESC_DIP.ToString();
                txtNumeroProcessoMec.Text = tb16.NU_PROC_MEC_DIP.ToString();
                txtObservacao.Text = tb16.OBS_HISTORICO != null ? tb16.OBS_HISTORICO.ToString() : "";

                string strCoDiploma = tb16.CO_DIPLOMA.ToString();

                if (strCoDiploma.Length < 6)
                {
                    int difCaracDiploma = 6 - strCoDiploma.Length;

                    for (int i = 0; i < difCaracDiploma; i++)
                    {
                        strCoDiploma = "0" + strCoDiploma;
                    }
                }
                txtNumeroProcessoEscolar.Text = LoginAuxili.CO_EMP.ToString() + "." + tb16.DT_TERM_CURS_DIP.Year + tb16.DT_TERM_CURS_DIP.Month.ToString("00") + "." + strCoDiploma;
            }            
        }
        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where( m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            if (modalidade != 0)
            {
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                            where tb01.CO_MODU_CUR == modalidade
                                            join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb01.CO_CUR equals tb43.CO_CUR
                                            where tb43.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR
                                            select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy( c => c.NO_CUR );

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
            }

            ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAluno()
        {
            ddlAluno.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                   where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                   select new { NO_ALU = tb08.TB07_ALUNO.NO_ALU.ToUpper(), tb08.CO_ALU }).OrderBy( m => m.NO_ALU );

            ddlAluno.DataTextField = "NO_ALU";
            ddlAluno.DataValueField = "CO_ALU";
            ddlAluno.DataBind();

            ddlAluno.Items.Insert(0, new ListItem("Selecione", ""));         
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

            ddlColaborador.SelectedValue = LoginAuxili.CO_COL.ToString();                       
        }        

        /// <summary>
        /// Método que carrega o dropdown de Diplomas
        /// </summary>
        private void CarregaDiploma()
        {
            var resultado = (from tb16 in TB16_DIPLOMA.RetornaTodosRegistros()
                             where tb16.TB07_ALUNO.CO_EMP == LoginAuxili.CO_EMP
                             select new { tb16.CO_DIPLOMA, tb16.DT_INIC_CURS_DIP, tb16.TB07_ALUNO.CO_EMP }).ToList();

            var resultado2 = (from result in resultado
                              select new
                              {
                                result.CO_DIPLOMA,
                                NU_DIPLOMA = result.CO_EMP.ToString() + "." + result.DT_INIC_CURS_DIP.Year.ToString() + result.DT_INIC_CURS_DIP.Month.ToString("00") 
                                + "." + result.CO_DIPLOMA.ToString("000000")
                              }).OrderBy( r => r.NU_DIPLOMA );


            ddlDiploma.DataSource = resultado2;
            ddlDiploma.DataTextField = "NU_DIPLOMA";
            ddlDiploma.DataValueField = "CO_DIPLOMA";
            ddlDiploma.DataBind();

            ddlDiploma.Items.Insert(0, new ListItem("Selecione", ""));         
        }

        /// <summary>
        /// Método que carrega o NIRE do Aluno selecionado
        /// </summary>
        private void CarregaNire()
        {
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;

            if (coAlu == 0)
                return;

            txtNire.Text = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                            where tb07.CO_ALU == coAlu
                            select new { tb07.NU_NIRE }).FirstOrDefault().NU_NIRE.ToString();
        }
        #endregion

        protected void ddlAluno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaNire();
        }

        protected void ddlDiploma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCampos(int.Parse(ddlDiploma.SelectedValue));
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaAluno();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
        }
    }
}