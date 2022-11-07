//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE DIPLOMAS E CERTIFICADOS
// OBJETIVO: CADASTRO DE DIPLOMAS
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
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2111_ContrDiplomaCertificado.CadastroDiploma
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

            txtNire.Enabled = ddlModalidade.Enabled = ddlSerieCurso.Enabled = ddlAluno.Enabled = txtDataSolicitacao.Enabled = false;

            CarregaSolicitacao();
            CarregaAluno();
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaColaborador();

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                var varLstDiploma = (from iTb16 in TB16_DIPLOMA.RetornaTodosRegistros()
                                     select iTb16).ToList();

                string ultCodDiplo = (varLstDiploma == null || varLstDiploma.Count() == 0) ? "1" : (varLstDiploma.ToList().Last().CO_DIPLOMA + 1).ToString();

                txtNumeroProcessoEscolar.Text = LoginAuxili.CO_EMP.ToString() + "." + DateTime.Now.Year + DateTime.Now.Month.ToString("00") + "." + ultCodDiplo.ToString().PadLeft(6, '0');
            }

            int coDiploma = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

            var qtdTb210 = (from tb210 in TB210_HIST_DIPLOMA.RetornaTodosRegistros()
                            where tb210.TB16_DIPLOMA.CO_DIPLOMA == coDiploma
                            select new { tb210.CO_HIST_DIPLOMA }).Count();

            if (qtdTb210 > 0)
            {
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
                    AuxiliPagina.EnvioMensagemErro(this, "Registro não pode ser excluido");
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB16_DIPLOMA tb16 = null;

            int coDiploma = 0;
            if (hdCodDip.Value != "")
            {
                coDiploma = Convert.ToInt32(hdCodDip.Value);
                tb16 = RetornaEntidade(coDiploma);
            }

            var qtdTb210 = (from tb210 in TB210_HIST_DIPLOMA.RetornaTodosRegistros()
                            where tb210.TB16_DIPLOMA.CO_DIPLOMA == coDiploma
                            select new { tb210.CO_HIST_DIPLOMA }).Count();

            if (qtdTb210 > 0)
            {
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
                    AuxiliPagina.EnvioMensagemErro(this, "Registro não pode ser excluido");
            }            

            int idSolicDip = ddlSol.SelectedValue != "" ? Convert.ToInt32(ddlSol.SelectedValue) : 0;

            if (tb16 == null)
            {
                tb16 = new TB16_DIPLOMA();

                var refAluno = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(ddlAluno.SelectedValue));

                tb16.TB211_SOLIC_DIPLOMA = TB211_SOLIC_DIPLOMA.RetornaPelaChavePrimaria(idSolicDip);
                tb16.TB07_ALUNO = refAluno;
                refAluno.TB25_EMPRESA1Reference.Load();
                tb16.TB25_EMPRESA = refAluno.TB25_EMPRESA1;
                tb16.CO_CUR = int.Parse(ddlSerieCurso.SelectedValue);                
            }

            tb16.NU_PROC_ESC_DIP = txtNumeroProcessoEscolar.Text != "" ? (double?)double.Parse(txtNumeroProcessoEscolar.Text.Replace(".", "")) : null;            
            tb16.NU_LIVR_ESC_DIP = txtNumeroLivroProcEscolar.Text != "" ? (int?)int.Parse(txtNumeroLivroProcEscolar.Text.Replace(".", "")) : null;
            tb16.NU_PAGI_ESC_DIP = txtNumeroPaginaProcEscolar.Text != "" ? (int?)int.Parse(txtNumeroPaginaProcEscolar.Text.Replace(".", "")) : null;
            tb16.DT_REGI_ESC_DIP = txtDataProcEscolar.Text != "" ? (DateTime?)DateTime.Parse(txtDataProcEscolar.Text.Replace(".", "")) : null;
            tb16.NU_PROC_MEC_DIP = txtNumeroProcessoMec.Text != "" ? (double?)double.Parse(txtNumeroProcessoMec.Text.Replace(".", "")) : null;
            tb16.NU_LIVR_MEC_DIP = txtNumeroLivroProcMec.Text != "" ? (int?)int.Parse(txtNumeroLivroProcMec.Text.Replace(".", "")) : null;
            tb16.NU_PAGI_MEC_DIP = txtNumeroPaginaProcMec.Text != "" ? (int?)int.Parse(txtNumeroPaginaProcMec.Text.Replace(".", "")) : null;
            tb16.DT_REGI_MEC_DIP = txtDataProcMec.Text != "" ? (DateTime?)DateTime.Parse(txtDataProcMec.Text) : null;
            tb16.DT_INIC_CURS_DIP = DateTime.Parse(txtDataInicio.Text);
            tb16.DT_TERM_CURS_DIP = DateTime.Parse(txtDataFim.Text);
            tb16.DT_SOLI_DIP = DateTime.Parse(txtDataSolicitacao.Text);
            tb16.DT_ENTR_DIP = txtDataEntrega.Text != "" ? (DateTime?)DateTime.Parse(txtDataEntrega.Text) : null;
            tb16.DT_PART_ENC = txtDataFimSerie.Text != "" ? (DateTime?)DateTime.Parse(txtDataFimSerie.Text) : null;
            tb16.DT_COLACAO_GRAU = txtDataColacaoGrau.Text != "" ? (DateTime?)DateTime.Parse(txtDataColacaoGrau.Text) : null;
            tb16.CO_ANO_SEM_CURSO = txtAnoSemestreConclusao.Text.Replace("/", "");
            tb16.NU_CARGA_HOR_CURSO = txtCargaHorariaSerie.Text != "" ? (decimal?)decimal.Parse(txtCargaHorariaSerie.Text) : null;
            tb16.NU_CARGA_HOR_CUMP = txtCargaHorariaCumprida.Text != "" ? (decimal?)decimal.Parse(txtCargaHorariaCumprida.Text) : null;
            tb16.OBS_HISTORICO = txtObservacao.Text.Trim().Length > 200 ? txtObservacao.Text.Trim().Substring(0, 200) : txtObservacao.Text.Trim();
            tb16.CO_COLA_EMIS_DIP = LoginAuxili.CO_COL;
            
            TB16_DIPLOMA.SaveOrUpdate(tb16, true);

            AuxiliPagina.RedirecionaParaPaginaSucesso("Cadastro Efetuado com Sucesso", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
        }        
        #endregion

        #region Métodos

        /// <summary>
        /// Método que faz referência a outro método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB16_DIPLOMA tb16 = RetornaEntidade(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));

            hdCodDip.Value = QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id);

            CarregaDiploma(tb16);
            ddlSol.Enabled = false;
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <param name="coDip">Id do diploma</param>
        /// <returns>Entidade TB16_DIPLOMA</returns>
        private TB16_DIPLOMA RetornaEntidade(int coDip)
        {
            return TB16_DIPLOMA.RetornaPelaChavePrimaria(coDip);
        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega informações do diploma
        /// </summary>
        /// <param name="tb16">Entidade TB16_DIPLOMA</param>
        private void CarregaDiploma(TB16_DIPLOMA tb16)
        {
            if (tb16 != null)
            {
                CarregaAluno();
                tb16.TB07_ALUNOReference.Load();
                ddlAluno.SelectedValue = tb16.TB07_ALUNO.CO_ALU.ToString();
                CarregaNire();

                ddlModalidade.SelectedValue = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                                               where tb01.CO_CUR == tb16.CO_CUR && tb01.CO_EMP == LoginAuxili.CO_EMP
                                               select new { tb01.CO_MODU_CUR }).FirstOrDefault().CO_MODU_CUR.ToString();

                tb16.TB211_SOLIC_DIPLOMAReference.Load();

                ddlSol.SelectedValue = tb16.TB211_SOLIC_DIPLOMA.ID_SOLIC_DIPLOMA.ToString();

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

                string coDiploma = tb16.CO_DIPLOMA.ToString();

                if (coDiploma.Length < 6)
                {
                    int difQtdeCarac = 6 - coDiploma.Length;

                    for (int i = 0; i < difQtdeCarac; i++)
                    {
                        coDiploma = "0" + coDiploma;
                    }
                }

                txtNumeroProcessoEscolar.Text = LoginAuxili.CO_EMP.ToString() + "." + tb16.DT_TERM_CURS_DIP.Year + (tb16.DT_TERM_CURS_DIP.Month.ToString().Length == 1 ? "0" + tb16.DT_TERM_CURS_DIP.Month.ToString() : tb16.DT_TERM_CURS_DIP.Month.ToString()) + "." + coDiploma;
            }
        }

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
        /// Método que carrega o dropdown de Solicitações
        /// </summary>
        private void CarregaSolicitacao()
        {
            ddlSol.DataSource = (from tb211 in TB211_SOLIC_DIPLOMA.RetornaTodosRegistros()
                                 join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb211.CO_CUR equals tb01.CO_CUR
                                 where tb211.TB07_ALUNO.CO_EMP == LoginAuxili.CO_EMP && tb211.CO_STATUS == "A"
                                 select new { DESCRICAO = tb211.TB07_ALUNO.NO_ALU + " - " + tb01.NO_CUR, tb211.ID_SOLIC_DIPLOMA }).OrderBy( s => s.DESCRICAO );

            ddlSol.DataTextField = "DESCRICAO";
            ddlSol.DataValueField = "ID_SOLIC_DIPLOMA";
            ddlSol.DataBind();

            ddlSol.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega a informação do NIRE do aluno
        /// </summary>
        private void CarregaNire()
        {
            int coAlu = Convert.ToInt32(ddlAluno.SelectedValue);

            txtNire.Text = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                            where tb07.CO_ALU == coAlu 
                            select new { tb07.NU_NIRE }).FirstOrDefault().NU_NIRE.ToString();
        }
        #endregion

        protected void ddlAluno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaNire();
        }        

        protected void ddlSol_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idSolicDiploma = Convert.ToInt32(ddlSol.SelectedValue);

            var tb211 = TB211_SOLIC_DIPLOMA.RetornaPelaChavePrimaria(idSolicDiploma);

            tb211.TB07_ALUNOReference.Load();

            txtDataSolicitacao.Text = tb211.DT_SOLIC.ToString("dd/MM/yyyy");
            ddlAluno.SelectedValue = tb211.TB07_ALUNO.CO_ALU.ToString();
            CarregaNire();

            ddlModalidade.SelectedValue = tb211.CO_MODU_CUR.ToString();
            CarregaSerieCurso();
            ddlSerieCurso.SelectedValue = tb211.CO_CUR.ToString();

            TB16_DIPLOMA tb16 = (from lTb16 in TB16_DIPLOMA.RetornaTodosRegistros()
                                where lTb16.TB211_SOLIC_DIPLOMA.ID_SOLIC_DIPLOMA == idSolicDiploma
                                select lTb16).FirstOrDefault();

            if (tb16 != null)
            {
                hdCodDip.Value = tb16.CO_DIPLOMA.ToString();
                CarregaDiploma(tb16);
            }
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