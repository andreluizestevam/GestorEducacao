//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE OPERACIONAL DE CURSOS
// OBJETIVO: CADASTRO DE MENSALIDADES ANO BASE
// DATA DE CRIAÇÃO: 22/11/2016
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+----------------------------------------
// 22/11/2016| Aex Ribeiro da Silva       | Criei a funcionalidade
// ----------+----------------------------+----------------------------------------

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
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
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2115_MensalidadesAnoBase
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

            if (Page.IsPostBack) return;

            CarregaAnos();
            CarregaModalidades();
            CarregaSerieCurso();

        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            try
            {
                int coAno = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;
                int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
                int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
                decimal? valorManha = txtValorManha.Text != "" ? Decimal.Parse(txtValorManha.Text) : 0;
                decimal? valorTarde = txtValorTarde.Text != "" ? Decimal.Parse(txtValorTarde.Text) : 0;
                decimal? valorNoite = txtValorNoite.Text != "" ? Decimal.Parse(txtValorNoite.Text) : 0;
                decimal? valorIntegral = txtValorIntegral.Text != "" ? Decimal.Parse(txtValorIntegral.Text) : 0;
                decimal? valorEspecial = txtValorEspecial.Text != "" ? Decimal.Parse(txtValorEspecial.Text) : 0;

                TB430_MENSA_ANOBASE tb430 = RetornaEntidade();

                tb430.CO_ANO_BASE = coAno;
                tb430.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                tb430.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie);
                tb430.VALOR_MANHA = valorManha;
                tb430.VALOR_TARDE = valorTarde;
                tb430.VALOR_NOITE = valorNoite;
                tb430.VALOR_INTEGRAL = valorIntegral;
                tb430.VALOR_ESPECIAL = valorEspecial;
                //TB430_MENSA_ANOBASE.SaveOrUpdate(tb430,true);

                CurrentPadraoCadastros.CurrentEntity = tb430;
                //AuxiliPagina.RedirecionaParaPaginaSucesso("Cadastro Efetuado com Sucesso", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                //AuxiliPagina.EnvioMensagemSucesso(this, "Mensalidades por Ano Base Cadastradas com Sucesso.");
                
            }
            catch (Exception e)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não foi possível cadastrar.");
            };
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() {

            TB430_MENSA_ANOBASE tb430 = RetornaEntidade();

            CarregaAnos();
            ddlAno.SelectedValue = QueryStringAuxili.QueryStringValor(QueryStrings.Ano)+"  ";
            CarregaModalidades();
            ddlModalidade.SelectedValue = QueryStringAuxili.QueryStringValor(QueryStrings.CoModuCur);
            CarregaSerieCurso();
            ddlSerieCurso.SelectedValue = QueryStringAuxili.QueryStringValor(QueryStrings.CoCur);
            txtValorManha.Text = tb430.VALOR_MANHA.ToString();
            txtValorTarde.Text = tb430.VALOR_TARDE.ToString();
            txtValorNoite.Text = tb430.VALOR_NOITE.ToString();
            txtValorIntegral.Text = tb430.VALOR_INTEGRAL.ToString();
            txtValorEspecial.Text = tb430.VALOR_ESPECIAL.ToString();

        }
               
        #endregion

        #region Métodos

        private TB430_MENSA_ANOBASE RetornaEntidade()
        {
            TB430_MENSA_ANOBASE tb430 = TB430_MENSA_ANOBASE.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb430 == null) ? new TB430_MENSA_ANOBASE() : tb430;
        }

        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            ddlAno.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                 select new { tb43.CO_ANO_GRADE }).Distinct().OrderByDescending(g => g.CO_ANO_GRADE);

            //ddlAno.DataSource = from re in resultado
            //                    select new { CO_ANO_GRADE = re.CO_ANO_GRADE.Trim() };


            ddlAno.DataTextField = "CO_ANO_GRADE";
            ddlAno.DataValueField = "CO_ANO_GRADE";
            ddlAno.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaModalidades()
        {
            int ano = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;

            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                //ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);
                AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, false);
            }
            else
            {
                var res = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                           join mo in TB44_MODULO.RetornaTodosRegistros() on rm.CO_MODU_CUR equals mo.CO_MODU_CUR
                           where rm.CO_COL_RESP == LoginAuxili.CO_COL
                           && rm.CO_ANO_REF == ano
                           select new
                           {
                               mo.DE_MODU_CUR,
                               rm.CO_MODU_CUR
                           }).Distinct();

                ddlModalidade.DataTextField = "DE_MODU_CUR";
                ddlModalidade.DataValueField = "CO_MODU_CUR";
                ddlModalidade.DataSource = res;
                ddlModalidade.DataBind();

                if (res.Count() != 1)
                    ddlModalidade.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries, verifica se o usuário logado  é professor.
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string anoGrade = ddlAno.SelectedValue;
            int ano = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;

            //if (modalidade != 0)
            //{
            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                //ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                //                            where tb01.CO_MODU_CUR == modalidade
                //                            join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb01.CO_CUR equals tb43.CO_CUR
                //                            where tb43.CO_ANO_GRADE == anoGrade && tb43.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR
                //                            select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy(c => c.NO_CUR);

                //ddlSerieCurso.DataTextField = "NO_CUR";
                //ddlSerieCurso.DataValueField = "CO_CUR";
                //ddlSerieCurso.DataBind();
                //ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
                AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, LoginAuxili.CO_EMP, false);
            }
            else
            {
                var res = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                           join c in TB01_CURSO.RetornaTodosRegistros() on rm.CO_CUR equals c.CO_CUR
                           join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on rm.CO_CUR equals tb43.CO_CUR
                           where rm.CO_COL_RESP == LoginAuxili.CO_COL
                           && rm.CO_MODU_CUR == modalidade
                           && rm.CO_ANO_REF == ano
                           select new
                           {
                               c.NO_CUR,
                               rm.CO_CUR
                           }).Distinct();

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataSource = res;
                ddlSerieCurso.DataBind();

                if (res.Count() != 1)
                    ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
            }
            //}
        }
        #endregion

        #region Eventos componentes

        protected void ddlAno_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaModalidades();
            CarregaSerieCurso();

        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
        }

        #endregion
    }
}
