//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: GESTÃO DE DADOS DE ALUNOS
// OBJETIVO: RELAÇÃO DE MAPA DE DISTRIBUIÇÃO DE ALUNOS - CARACTERÍSTICA
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
using System.Runtime.InteropServices;
using System.IO;
using System.ServiceModel;
using System.Configuration;
using Resources;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3699_Relatorios
{
    public partial class MapaDistriAlunosCaracteristicas : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }         

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

//--------> Criação das instâncias utilizadas
            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaInicial();

                liModalidade.Visible = liSerie.Visible = false;
            }
        }

        /// <summary>
        /// Abre a página do relatório
        /// </summary>
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            UpdateProgress1.EnableViewState = true;
            string strP_CO_EMP = null;
            string strP_CO_MODU_CUR = null;
            string strP_CO_CUR = null;
            string strP_CO_ANO_REF = null;
            string strP_CO_TIPO = null;
            string strP_CO_UF = null;
            int strP_CO_CID = 0;
            string infos = null;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = ddlUnidade.SelectedValue;
            strP_CO_MODU_CUR = ddlModalidade.SelectedValue == "" ? "0" : ddlModalidade.SelectedValue;
            strP_CO_CUR = ddlSerieCurso.SelectedValue == "" ? "0" : ddlSerieCurso.SelectedValue;
            strP_CO_ANO_REF = ddlAnoRefer.SelectedValue == "" ? "0" : ddlAnoRefer.SelectedValue;
            strP_CO_TIPO = rblTipo.SelectedValue;
            strP_CO_UF = ddlEstado.SelectedValue == "" ? "0" : ddlEstado.SelectedValue;
            strP_CO_CID = ddlCidade.SelectedValue == "" ? 0 : int.Parse(ddlCidade.SelectedValue);
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            string para1 = "( Unidade: " + ddlUnidade.SelectedItem + " - Tipo: " + rblTipo.SelectedItem;
            
            switch (strP_CO_TIPO)
            {
                case "S":
                    para1 += " - Ano referência: " + ddlAnoRefer.SelectedItem.ToString() + " - Módulo: " + ddlModalidade.SelectedItem.ToString() + " - Série: " + ddlSerieCurso.SelectedItem.ToString() + ")";
                    break;
                case "A":
                    para1 += " - Ano referência: " + ddlAnoRefer.SelectedItem.ToString() + ")";
                    break;
                case "":
                    para1 += " - Ano referência: " + ddlAnoRefer.SelectedItem.ToString() + " - Estado: " + ddlEstado.SelectedItem.ToString() + " - Cidade: " + ddlCidade.SelectedItem.ToString() + ")";
                    break;
            }

            string strParametrosRelatorio = para1;


            RptMapaDistriAlunosCaracteristicas rpt = new RptMapaDistriAlunosCaracteristicas();

            int lRetorno = rpt.InitReport(strParametrosRelatorio, LoginAuxili.CO_EMP, infos, int.Parse(ddlUnidade.SelectedValue), strP_CO_ANO_REF, int.Parse(strP_CO_MODU_CUR), int.Parse(strP_CO_CUR), strP_CO_TIPO, strP_CO_UF, strP_CO_CID);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
            UpdateProgress1.EnableViewState = false;
        }

        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidades()
        {
            ddlUnidade.Items.Clear();
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();
            ddlUnidade.Items.Insert(0, new ListItem("Selecione",""));
            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();

            rblTipo.Items.Clear();
            liAnoRefer.Visible = liModalidade.Visible = liSerie.Visible = false;

        }

        /// <summary>
        /// Carrega todos os tipos
        /// </summary>
        private void CarregaTipos()
        {
            rblTipo.Items.Clear();

            rblTipo.Items.Insert(0, new ListItem("Ano Letino", "A"));
            rblTipo.Items.Insert(1, new ListItem("Bairro", "B"));
            rblTipo.Items.Insert(2, new ListItem("Série", "S"));

            liAnoRefer.Visible = liModalidade.Visible = liSerie.Visible = false;
            liEstado.Visible = liCidade.Visible = false;
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            ddlAnoRefer.Items.Clear();
            ddlAnoRefer.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                      where tb08.TB25_EMPRESA.CO_EMP == coEmp
                                      && tb08.CO_SIT_MAT == "A"
                                      select new { tb08.CO_ANO_MES_MAT }).DistinctBy(d=>d.CO_ANO_MES_MAT).OrderByDescending(g => g.CO_ANO_MES_MAT);

            ddlAnoRefer.DataTextField = "CO_ANO_MES_MAT";
            ddlAnoRefer.DataValueField = "CO_ANO_MES_MAT";
            ddlAnoRefer.DataBind();
            ddlAnoRefer.Items.Insert(0, new ListItem("Todos", "-1"));
            ddlAnoRefer.Items.Insert(0, new ListItem("Selecione",""));
            ddlAnoRefer.SelectedValue = "";

            if(liModalidade.Visible)
                ddlModalidade.Items.Clear();
            if(liSerie.Visible)
                ddlSerieCurso.Items.Clear();
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            string coAno = ddlAnoRefer.SelectedValue != "" ? ddlAnoRefer.SelectedValue : "0";
            ddlModalidade.Items.Clear();
            ddlModalidade.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                        join tb44 in TB44_MODULO.RetornaTodosRegistros() on tb08.TB44_MODULO.CO_MODU_CUR equals tb44.CO_MODU_CUR
                                        where tb08.CO_EMP == coEmp
                                        && tb08.CO_SIT_MAT == "A"
                                        && (coAno == "-1" ? 0 == 0 : tb08.CO_ANO_MES_MAT == coAno)
                                        select new
                                        {
                                            tb44.DE_MODU_CUR, tb44.CO_MODU_CUR
                                        }
                                            ).DistinctBy(d=>d.CO_MODU_CUR);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Selecione",""));
            ddlModalidade.SelectedValue = "";

            if(liSerie.Visible)
                ddlSerieCurso.Items.Clear();
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        /// <param name="coModuCur">Id da modalidade</param>
        private void CarregaSerieCurso()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            string coAno = ddlAnoRefer.SelectedValue == "" ? "0" : ddlAnoRefer.SelectedValue;
            int coMod = ddlModalidade.SelectedValue == "" ? 0 : int.Parse(ddlModalidade.SelectedValue);
            ddlSerieCurso.Items.Clear();
            if (coMod != 0)
            {
                ddlSerieCurso.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                            join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb01.CO_CUR
                                            where tb08.CO_EMP == coEmp
                                            && tb08.CO_SIT_MAT == "A"
                                            && (coAno == "-1" ? 0==0 : tb08.CO_ANO_MES_MAT == coAno)
                                            && (coMod == -1 ? 0==0 : tb08.TB44_MODULO.CO_MODU_CUR == coMod)
                                            select new { tb01.NO_CUR, tb01.CO_CUR }).DistinctBy(d=>d.CO_CUR).OrderBy( c => c.NO_CUR );

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();

                ddlSerieCurso.Items.Insert(0, new ListItem("Todos", "-1"));
                ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
                ddlSerieCurso.SelectedValue = "";

            }
        }

        /// <summary>
        /// Carrega todos os estados matriculados
        /// </summary>
        private void CarregarEstados()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            string coAno = ddlAnoRefer.SelectedValue == "" ? "0" : ddlAnoRefer.SelectedValue;
            ddlEstado.Items.Clear();
            ddlEstado.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                    join tb74 in TB74_UF.RetornaTodosRegistros() on tb08.TB07_ALUNO.CO_ESTA_ALU equals tb74.CODUF
                                    where tb08.CO_SIT_MAT == "A"
                                    && tb08.CO_EMP == coEmp
                                    && (coAno == "-1" ? 0==0 : tb08.CO_ANO_MES_MAT == coAno)
                                    select new
                                    {
                                        tb74.CODUF,
                                        tb74.DESCRICAOUF
                                    }).DistinctBy(d=>d.CODUF);
            ddlEstado.DataTextField = "DESCRICAOUF";
            ddlEstado.DataValueField = "CODUF";
            ddlEstado.DataBind();

            ddlEstado.Items.Insert(0, new ListItem("Selecione",""));
            ddlEstado.SelectedValue = "";

            ddlCidade.Items.Clear();
        }

        /// <summary>
        /// Carrega todos as cidades matriculadas
        /// </summary>
        private void CarregarCidades()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            string coAno = ddlAnoRefer.SelectedValue == "" ? "0" : ddlAnoRefer.SelectedValue;
            string coUf = ddlEstado.SelectedValue == "" ? "0" : ddlEstado.SelectedValue;
            ddlCidade.Items.Clear();
            ddlCidade.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                    join tb904 in TB904_CIDADE.RetornaTodosRegistros() on tb08.TB07_ALUNO.TB905_BAIRRO.CO_CIDADE equals tb904.CO_CIDADE
                                    where tb08.CO_SIT_MAT == "A"
                                    && tb08.CO_EMP == coEmp
                                    && (coAno == "-1" ? 0 == 0 : tb08.CO_ANO_MES_MAT == coAno)
                                    && tb08.TB07_ALUNO.CO_ESTA_ALU == coUf
                                    select new
                                    {
                                        tb904.CO_CIDADE,
                                        tb904.NO_CIDADE
                                    }).DistinctBy(d=>d.CO_CIDADE);
            ddlCidade.DataTextField = "NO_CIDADE";
            ddlCidade.DataValueField = "CO_CIDADE";
            ddlCidade.DataBind();

            ddlCidade.Items.Insert(0, new ListItem("Selecione", ""));
            ddlCidade.SelectedValue = "";

        }
        #endregion

        #region Eventos de componentes

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(((DropDownList)sender).SelectedValue != "")
                CarregaSerieCurso();
        }

        protected void ddlUnidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregaTipos();
        }

        protected void ddlAnoRefer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
            {
                if(liModalidade.Visible)
                {
                    CarregaModalidades();
                }
                else if (liEstado.Visible)
                {
                    CarregarEstados();
                }
            }
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregarCidades();
        }

        protected void rblTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((RadioButtonList)sender).SelectedValue != "")
            {
                CarregaFiltros();
            }
        }
        
        #endregion

        #region Metodos personalizados
        private string CarregaFiltros(Boolean carregar = false)
        {
            switch (rblTipo.SelectedValue)
            {
                case "S":
                    liAnoRefer.Visible = liModalidade.Visible = liSerie.Visible = true;
                    liEstado.Visible = liCidade.Visible = false;
                    lblParametroNome.Visible = true;
                    lblParametroNome.Text = "Parametros de filtro da Série:";
                    CarregaAnos();
                    break;
                case "A":
                    liAnoRefer.Visible = true;
                    liModalidade.Visible = liSerie.Visible = false;
                    liEstado.Visible = liCidade.Visible = false;
                    lblParametroNome.Visible = true;
                    lblParametroNome.Text = "Parametros de filtro do Ano:";
                    CarregaAnos();
                    break;
                case "B":
                    liModalidade.Visible = liSerie.Visible = false;
                    liAnoRefer.Visible = liEstado.Visible = liCidade.Visible = true;
                    lblParametroNome.Visible = true;
                    lblParametroNome.Text = "Parametros de filtro do Bairro:";
                    CarregaAnos();
                    break;
            }
            return rblTipo.SelectedValue;
        }

        private void CarregaInicial()
        {
            CarregaUnidades();
            CarregaTipos();
            rblTipo.SelectedIndex = 0;
            CarregaFiltros();
            ddlAnoRefer.SelectedIndex = 1;
            
        }

        #endregion

    }
}
