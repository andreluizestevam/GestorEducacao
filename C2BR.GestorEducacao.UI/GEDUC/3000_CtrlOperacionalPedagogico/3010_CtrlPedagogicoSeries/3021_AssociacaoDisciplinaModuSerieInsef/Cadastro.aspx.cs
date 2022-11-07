//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE SÉRIES OU CURSOS
// OBJETIVO: ASSOCIAÇÃO DE DISCIPLINA A MODALIDADE/SÉRIE
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.F3021_AssociacaoDisciplinaModuSerieInsef
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
            string dataAtual = DateTime.Now.Date.ToString("dd/MM/yyyy");

            if (!IsPostBack)
            {
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaMaterias();
                CarregaSigla();
                txtDataInclusao.Text = dataAtual;
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                    txtDataSituacao.Text = dataAtual;
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;            
            int materia = ddlMateria.SelectedValue != "" ? int.Parse(ddlMateria.SelectedValue) : 0;

            DateTime dataRetorno = DateTime.Now;
            int intRetorno = 0;

//--------> Faz a verificação para saber se Modalidade, Série e Matéria já estão associados
            int verifOcorr = (from lTb02 in TB02_MATERIA.RetornaTodosRegistros()
                              where lTb02.CO_CUR == serie && lTb02.CO_MODU_CUR == modalidade && lTb02.CO_EMP == LoginAuxili.CO_EMP && lTb02.ID_MATERIA == materia
                              select new { lTb02.CO_CUR }).Count();

            if (verifOcorr > 0 && QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                AuxiliPagina.EnvioMensagemErro(this, "Já existe Associação para o Modalidade/Série com a Disciplina selecionada.");
                return;
            }
            else
            {
                TB02_MATERIA tb02 = RetornaEntidade();

                if (tb02 == null)
                {
                    tb02 = new TB02_MATERIA();
                    tb02.CO_EMP = LoginAuxili.CO_EMP;
                    tb02.CO_MODU_CUR = modalidade;
                    tb02.CO_CUR = serie;
                    tb02.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie);
                    tb02.ID_MATERIA = int.Parse(ddlMateria.SelectedValue);
                }
                else
                {
                    if ((tb02.ID_MATERIA != materia) || (QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCur) != serie) || (QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoModuCur) != modalidade))
                    {
                        verifOcorr = (from lTb02 in TB02_MATERIA.RetornaTodosRegistros()
                                      where lTb02.CO_CUR == serie && lTb02.CO_MODU_CUR == modalidade && lTb02.CO_EMP == LoginAuxili.CO_EMP && lTb02.ID_MATERIA == materia
                                      select new { lTb02.CO_CUR }).Count();
                        if (verifOcorr > 0)
                            AuxiliPagina.EnvioMensagemErro(this, "Já existe Associação para o Modalidade/Série com a Disciplina selecionada.");
                    }
                }

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    tb02.FL_INCLU_MAT = true;
                    tb02.FL_ALTER_MAT = false;
                }

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                {
                    tb02.FL_ALTER_MAT = true;
                }

                tb02.QT_CRED_MAT = int.TryParse(txtCreditos.Text, out intRetorno) ? (int?)intRetorno : null;
                tb02.QT_CARG_HORA_MAT = int.Parse(txtCargaHoraria.Text);
                tb02.DT_INCL_MAT = DateTime.TryParse(txtDataInclusao.Text, out dataRetorno) ? dataRetorno : DateTime.Now;
                tb02.DT_SITU_MAT = DateTime.TryParse(txtDataSituacao.Text, out dataRetorno) ? dataRetorno : DateTime.Now;
                tb02.CO_SITU_MAT = ddlSituacao.SelectedValue;
                tb02.FL_HISTOR_ESCOL = (chkImpHistorico.Checked ? "S" : "N");
                tb02.FL_TESTE_PROVA = (chkTesteProva.Checked ? "S" : "N");
                tb02.FL_TRABA       = (chkTrabalho.Checked   ? "S" : "N");
                tb02.FL_PROJE       = (chkProjeto.Checked    ? "S" : "N");
                tb02.FL_CONCE       = (chkConceito.Checked   ? "S" : "N");
                tb02.FL_AVALI_ESPEC = (chkAvaliacaoEspecifica.Checked  ? "S" : "N");
                tb02.FL_AVALI_GLOBA = (chkAvaliacaoGlobalizada.Checked ? "S" : "N");
                tb02.FL_SIMUL       = (chkSimulado.Checked   ? "S" : "N");
                tb02.FL_ATIVI_AVALI = (chkAtividadeAvaliativa.Checked  ? "S" : "N");
                tb02.FL_ATIVI_PRATI = (chkAtividadePratica.Checked     ? "S" : "N");
                tb02.FL_REDAC       = (chkRedacao.Checked    ? "S" : "N");
                tb02.VL_CALCU_MEDIA = (Decimal.Parse(txtCalcMedia.Text.Replace('.',',')));

                CurrentPadraoCadastros.CurrentEntity = tb02;

            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB02_MATERIA tb02 = RetornaEntidade();

            if (tb02 != null)
            {
                txtCargaHoraria.Text = tb02.QT_CARG_HORA_MAT.ToString();
                txtCreditos.Text = tb02.QT_CRED_MAT.ToString();
                txtDataInclusao.Text = tb02.DT_INCL_MAT.ToString("dd/MM/yyyy");
                txtDataSituacao.Text = tb02.DT_SITU_MAT.ToString("dd/MM/yyyy");
                ddlMateria.SelectedValue = tb02.ID_MATERIA.ToString();
                txtSigla.Text = TB107_CADMATERIAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, int.Parse(tb02.ID_MATERIA.ToString())).NO_SIGLA_MATERIA;
                ddlModalidade.SelectedValue = tb02.CO_MODU_CUR.ToString();
                CarregaSerieCurso();
                ddlSerieCurso.SelectedValue = tb02.CO_CUR.ToString();
                ddlSituacao.SelectedValue = tb02.CO_SITU_MAT;
                chkImpHistorico.Checked = (tb02.FL_HISTOR_ESCOL == "S" ? true : false);
                chkTesteProva.Checked           = (tb02.FL_TESTE_PROVA == "S" ? true : false);
                chkTrabalho.Checked             = (tb02.FL_TRABA == "S" ? true : false);
                chkProjeto.Checked              = (tb02.FL_PROJE == "S" ? true : false);
                chkConceito.Checked             = (tb02.FL_CONCE == "S" ? true : false);
                chkAvaliacaoEspecifica.Checked  = (tb02.FL_AVALI_ESPEC == "S" ? true : false);
                chkAvaliacaoGlobalizada.Checked = (tb02.FL_AVALI_GLOBA == "S" ? true : false);
                chkSimulado.Checked             = (tb02.FL_SIMUL == "S" ? true : false);
                chkAtividadeAvaliativa.Checked  = (tb02.FL_ATIVI_AVALI == "S" ? true : false);
                chkAtividadePratica.Checked     = (tb02.FL_ATIVI_PRATI == "S" ? true : false);
                chkRedacao.Checked              = (tb02.FL_REDAC == "S" ? true : false);
                txtCalcMedia.Text = (tb02.VL_CALCU_MEDIA < Decimal.Parse("10,00") ? '0' + tb02.VL_CALCU_MEDIA.ToString() : tb02.VL_CALCU_MEDIA.ToString());
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB02_MATERIA</returns>
        private TB02_MATERIA RetornaEntidade()
        {
            return TB02_MATERIA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoModuCur), 
              QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoMat), QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCur));
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

            ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                        where tb01.CO_MODU_CUR == modalidade
                                        select new { tb01.CO_CUR, tb01.CO_SIGL_CUR }).OrderBy(c => c.CO_SIGL_CUR);

            ddlSerieCurso.DataTextField = "CO_SIGL_CUR";
            ddlSerieCurso.DataValueField = "CO_CUR";
            ddlSerieCurso.DataBind();

            ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Matérias
        /// </summary>
        private void CarregaMaterias()
        {
            ddlMateria.DataSource = TB107_CADMATERIAS.RetornaPelaEmpresa(LoginAuxili.CO_EMP).OrderBy( c => c.NO_MATERIA );

            ddlMateria.DataTextField = "NO_MATERIA";
            ddlMateria.DataValueField = "ID_MATERIA";
            ddlMateria.DataBind();

            ddlMateria.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega sigla da Matéria Selecionada
        /// </summary>
        private void CarregaSigla()
        {
            int idMateria = ddlMateria.SelectedValue != "" ? int.Parse(ddlMateria.SelectedValue) : 0;

            if (idMateria != 0)
            {
                txtSigla.Text = (from tb107 in TB107_CADMATERIAS.RetornaTodosRegistros()
                                 where tb107.ID_MATERIA == idMateria
                                 select new { tb107.NO_SIGLA_MATERIA }).FirstOrDefault().NO_SIGLA_MATERIA.ToString();
            }
        }
        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
        }

        protected void ddlMateria_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSigla();
        }
    }
}
