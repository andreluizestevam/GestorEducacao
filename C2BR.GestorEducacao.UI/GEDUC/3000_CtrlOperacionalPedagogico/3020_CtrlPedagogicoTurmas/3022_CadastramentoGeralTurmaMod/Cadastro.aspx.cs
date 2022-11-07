//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE TURMAS POR SÉRIES/CURSOS
// OBJETIVO: CADASTRAMENTO GERAL DE TURMAS POR MODALIDADE
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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3020_CtrlPedagogicoTurmas.F3022_CadastramentoGeralTurmaMod
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
            CurrentPadraoCadastros.BarraCadastro.OnDelete += new Library.Componentes.BarraCadastro.OnDeleteHandler(BarraCadastro_OnDelete);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaDropDown();

                if (TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO).ORG_NUMERO_CNPJ != Decimal.Parse("11489849000133"))
                {
                    liTurmaAnterior.Visible = false;
                }
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }     

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            if (ddlTurmaAnterior.SelectedValue != "" && ddlProxTurmaMatr.SelectedValue != "")
            {
                if (ddlTurmaAnterior.SelectedValue == ddlProxTurmaMatr.SelectedValue)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Turma anterior e próxima turma de matrícula devem ser diferentes.");
                    return;
                }
            }

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                var ocoTurma = (from iTb129 in TB129_CADTURMAS.RetornaTodosRegistros()
                                where iTb129.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                && (iTb129.NO_TURMA == txtNO_TURMA.Text || iTb129.CO_SIGLA_TURMA == txtCO_SIGLA_TURMA.Text)
                                select iTb129).FirstOrDefault();

                if (ocoTurma != null)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Turma com o nome ou sigla informados já cadastrados.");
                    return;
                }
            }
            else if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {
                int idTurma = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

                var ocoTurma = (from iTb129 in TB129_CADTURMAS.RetornaTodosRegistros()
                                where iTb129.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP && iTb129.CO_TUR != idTurma
                                && (iTb129.NO_TURMA == txtNO_TURMA.Text || iTb129.CO_SIGLA_TURMA == txtCO_SIGLA_TURMA.Text)
                                select iTb129).FirstOrDefault();

                if (ocoTurma != null)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Turma com o nome ou sigla informados já cadastrados.");
                    return;
                }
            }

            TB129_CADTURMAS tb129 = RetornaEntidade();

            if (tb129.CO_TUR == 0)
            {
                tb129 = new TB129_CADTURMAS();
                tb129.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            }                       

            tb129.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(modalidade);
            tb129.NO_TURMA = txtNO_TURMA.Text.Trim();
            tb129.CO_SIGLA_TURMA = txtCO_SIGLA_TURMA.Text.Trim().ToUpper();
            tb129.CO_FLAG_MULTI_SERIE = ddlCO_FLAG_MULTI_SERIE.SelectedValue;
            tb129.TB248_UNIDADE_SALAS_AULA = ddlSalaAula.SelectedValue != "" ? TB248_UNIDADE_SALAS_AULA.RetornaPelaChavePrimaria(int.Parse(ddlSalaAula.SelectedValue)) : null;
            tb129.CO_FLAG_TIPO_TURMA = ddlCO_FLAG_TIPO_TURMA.SelectedValue;
            tb129.DT_STATUS_TURMA = DateTime.Parse(txtDT_STATUS_TURMA.Text);
            tb129.CO_STATUS_TURMA = ddlCO_STATUS_TURMA.SelectedValue;
            tb129.CO_EMP_UNID_CONT = int.Parse(ddlUnidadeContrato.SelectedValue);
            tb129.CO_TUR_REFER_ANTER = ddlTurmaAnterior.SelectedValue != "" ? (int?)int.Parse(ddlTurmaAnterior.SelectedValue) : null;
            tb129.CO_TUR_PROX_MATR = ddlProxTurmaMatr.SelectedValue != "" ? (int?)int.Parse(ddlProxTurmaMatr.SelectedValue) : null;
            tb129.FL_ATIVI_ENSIN_REMOT = chkEnsinoRemoto.Checked;
            tb129.DT_INICI_AULA = (dtIniAulas.Text != "" ? DateTime.Parse(dtIniAulas.Text) : (DateTime?)null);
            tb129.DT_TERMI_AULAS = (txtFimAulas.Text != "" ? DateTime.Parse(txtFimAulas.Text) : (DateTime?)null);

            if (GestorEntities.SaveOrUpdate(tb129, true) <= 0)
                CurrentPadraoCadastros.CurrentEntity = tb129;
            else
            {
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                {
                    var varTb06 = (from iTb06 in TB06_TURMAS.RetornaTodosRegistros()
                                   where iTb06.CO_TUR == tb129.CO_TUR
                                   select iTb06).ToList<TB06_TURMAS>();

                    foreach (TB06_TURMAS tb06 in varTb06)
                    {
                        tb06.FL_ALTER_TUR = true;
                    }

                    CurrentPadraoCadastros.CurrentEntity = tb129;
                }
                else
                    CurrentPadraoCadastros.CurrentEntity = tb129;
            }
        }

        void BarraCadastro_OnDelete(System.Data.Objects.DataClasses.EntityObject entity)
        {
            TB129_CADTURMAS tb129 = RetornaEntidade();

            if (GestorEntities.Delete(tb129) <= 0)
                AuxiliPagina.EnvioMensagemErro(this, "Erro ao excluir registro.");
            else
                AuxiliPagina.RedirecionaParaPaginaSucesso("Registro excluído com Sucesso", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB129_CADTURMAS tb129 = RetornaEntidade();

            if (tb129 != null)
            {
                tb129.TB248_UNIDADE_SALAS_AULAReference.Load();

                ddlModalidade.SelectedValue = tb129.TB44_MODULO.CO_MODU_CUR.ToString();
                txtNO_TURMA.Text = tb129.NO_TURMA;
                txtCO_SIGLA_TURMA.Text = tb129.CO_SIGLA_TURMA;
                ddlCO_STATUS_TURMA.SelectedValue = tb129.CO_STATUS_TURMA;
                ddlCO_FLAG_MULTI_SERIE.SelectedValue = tb129.CO_FLAG_MULTI_SERIE;
                txtDT_STATUS_TURMA.Text = tb129.DT_STATUS_TURMA.ToString("dd/MM/yyyy");
                ddlCO_FLAG_TIPO_TURMA.SelectedValue = tb129.CO_FLAG_TIPO_TURMA;
                ddlSalaAula.SelectedValue = tb129.TB248_UNIDADE_SALAS_AULA != null ? tb129.TB248_UNIDADE_SALAS_AULA.ID_SALA_AULA.ToString() : "";
                ddlUnidadeContrato.SelectedValue = tb129.CO_EMP_UNID_CONT.ToString();                
                ddlTurmaAnterior.SelectedValue = tb129.CO_TUR_REFER_ANTER != null ? tb129.CO_TUR_REFER_ANTER.Value.ToString() : null;
                ddlProxTurmaMatr.SelectedValue = tb129.CO_TUR_PROX_MATR != null ? tb129.CO_TUR_PROX_MATR.Value.ToString() : null;
                chkEnsinoRemoto.Checked = tb129.FL_ATIVI_ENSIN_REMOT;
                dtIniAulas.Text = tb129.DT_INICI_AULA.HasValue ? tb129.DT_INICI_AULA.Value.ToString() : "";
                txtFimAulas.Text = tb129.DT_TERMI_AULAS.HasValue ? tb129.DT_TERMI_AULAS.Value.ToString() : "";
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB129_CADTURMAS</returns>
        private TB129_CADTURMAS RetornaEntidade()
        {
            TB129_CADTURMAS tb129 = TB129_CADTURMAS.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb129 == null) ? new TB129_CADTURMAS() : tb129;
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método de carregamento dos DropDowns da modalidade, status da turma, turma multiserie e tipo de turma.
        /// </summary>
        private void CarregaDropDown()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where( m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Selecione", ""));

            ddlSalaAula.DataSource = (from tb248 in TB248_UNIDADE_SALAS_AULA.RetornaTodosRegistros()
                                      where tb248.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                      select new { tb248.ID_SALA_AULA, tb248.DE_SALA_AULA }).OrderBy(u => u.DE_SALA_AULA);

            ddlSalaAula.DataTextField = "DE_SALA_AULA";
            ddlSalaAula.DataValueField = "ID_SALA_AULA";
            ddlSalaAula.DataBind();

            ddlSalaAula.Items.Insert(0, new ListItem("", ""));

            ddlCO_STATUS_TURMA.Items.Add(new ListItem("Ativo", "A"));
            ddlCO_STATUS_TURMA.Items.Add(new ListItem("Inativo", "I"));

            ddlCO_FLAG_MULTI_SERIE.Items.Add(new ListItem("Sim", "S"));
            ddlCO_FLAG_MULTI_SERIE.Items.Add(new ListItem("Não", "N"));

            ddlCO_FLAG_TIPO_TURMA.Items.Add(new ListItem("Presencial", "P"));
            ddlCO_FLAG_TIPO_TURMA.Items.Add(new ListItem("Semi-Presencial", "S"));
            ddlCO_FLAG_TIPO_TURMA.Items.Add(new ListItem("Não Presencial", "N"));

            ddlUnidadeContrato.DataSource = from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                               where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                               select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP };

            ddlUnidadeContrato.DataTextField = "NO_FANTAS_EMP";
            ddlUnidadeContrato.DataValueField = "CO_EMP";
            ddlUnidadeContrato.DataBind();

            ddlUnidadeContrato.SelectedValue = LoginAuxili.CO_EMP.ToString();

            int idTurma = 0;
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {
                idTurma = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);
            }

            ddlTurmaAnterior.Items.Add(new ListItem("0T202", "1"));
            ddlTurmaAnterior.Items.Add(new ListItem("0T302", "2"));
            ddlTurmaAnterior.Items.Add(new ListItem("0T402", "3"));
            ddlTurmaAnterior.Items.Add(new ListItem("0T403", "4"));
            ddlTurmaAnterior.Items.Add(new ListItem("0M503", "5"));
            ddlTurmaAnterior.Items.Add(new ListItem("0T502", "6"));
            ddlTurmaAnterior.Items.Add(new ListItem("0T503", "7"));
            ddlTurmaAnterior.Items.Add(new ListItem("1M204", "8"));
            ddlTurmaAnterior.Items.Add(new ListItem("1T202", "9"));
            ddlTurmaAnterior.Items.Add(new ListItem("1T203", "10"));
            ddlTurmaAnterior.Items.Add(new ListItem("1T902", "11"));
            ddlTurmaAnterior.Items.Add(new ListItem("2M101", "12"));
            ddlTurmaAnterior.Items.Add(new ListItem("2M103", "13"));
            ddlTurmaAnterior.Items.Add(new ListItem("2M201", "14"));
            ddlTurmaAnterior.Items.Add(new ListItem("2M203", "15"));
            ddlTurmaAnterior.Items.Add(new ListItem("2M301", "16"));            

            ddlTurmaAnterior.Items.Insert(0, new ListItem("Selecione", ""));

            ddlProxTurmaMatr.DataSource = (from tb129 in TB129_CADTURMAS.RetornaTodosRegistros()
                                          where tb129.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                          && tb129.CO_TUR != idTurma
                                          select new { tb129.CO_TUR, tb129.NO_TURMA }).OrderBy(t => t.NO_TURMA);

            ddlProxTurmaMatr.DataTextField = "NO_TURMA";
            ddlProxTurmaMatr.DataValueField = "CO_TUR";
            ddlProxTurmaMatr.DataBind();

            ddlProxTurmaMatr.Items.Insert(0, new ListItem("Selecione", ""));

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                txtDT_STATUS_TURMA.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
        #endregion
    }
}