//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: DESEMPENHO, ÍNDICES E ESTATÍSTICAS
// SUBMÓDULO: ÍNDICES E INDICADORES (PEDAGÓGICO)
// OBJETIVO:  RELAÇÃO RESUMO DE EVASÃO ESCOLAR (FALTAS) POR SÉRIE OU MATÉRIA (SÉRIE/GLOBAL) NO ANO LETIVO
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
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using System.Runtime.InteropServices;
using System.IO;
using System.ServiceModel;
using System.Configuration;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F9000_CtrlResultados.F9020_IndicesCtrlPedagogico.F9029_Relatorios
{
    public partial class MapaEstatisEvasaoEscolar : System.Web.UI.Page
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
                CarregaUnidades();
                CarregaAnos();
                CarregaModalidade();

                if (ddlTipo.SelectedValue.ToString() == "S")
                {
                    liMateria.Visible = false;
                    liSerie.Visible = true;
                    CarregaSerieCurso();
                }
                else
                {
                    liSerie.Visible = false;
                    liMateria.Visible = true;
                    CarregaMaterias();
                }
            }
        }

//====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
//--------> Variáveis obrigatórias para gerar o Relatório
            string strIDSessao, strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio;
            int lRetorno;

//--------> Variáveis de parâmetro do Relatório
            string strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_MAT, strP_DDL_SEL, strP_DT_INI, strP_DT_FIM;

            var varRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), WRAuxiliares.URLRelatorioWeb);
            IRelatorioWeb lIRelatorioWeb;

            strIDSessao = Session.SessionID.ToString();
            strParametrosRelatorio = null;
            strIdentFunc = WRAuxiliares.IdentFunc;
            strCaminhoRelatorioGerado = HttpRuntime.AppDomainAppPath + "TMP_Relatorios\\" + strIDSessao + "\\";
            strNomeRelatorio = WRAuxiliares.GeraNomeRelatorio("RelEvasaoEscolar");

//--------> Criação da Pasta
            if (!Directory.Exists(@strCaminhoRelatorioGerado))
                Directory.CreateDirectory(@strCaminhoRelatorioGerado);

//--------> Inicializa as variáveis
            strP_CO_EMP = null;
            strP_CO_MODU_CUR = null;
            strP_CO_CUR = null;
            strP_CO_MAT = null;
            strP_DDL_SEL = null;
            strP_DT_INI = null;
            strP_DT_FIM = null;

//--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = ddlUnidade.SelectedValue;
            strP_CO_MODU_CUR = ddlModalidade.SelectedValue;
            strP_CO_CUR = ddlSerieCurso.SelectedValue;
            if (ddlTipo.SelectedValue != "S")
            {
                strP_CO_MAT = ddlMateria.SelectedValue;   
            }            
            strP_DDL_SEL = ddlTipo.SelectedValue;
            strP_DT_INI = ddlAnoReferIni.SelectedValue;
            strP_DT_FIM = ddlAnoReferFim.SelectedValue;
            
            if (ddlTipo.SelectedValue == "S")
                strParametrosRelatorio += "( Módulo: " + ddlModalidade.SelectedItem.ToString() + " - Série: " + ddlSerieCurso.SelectedItem.ToString() + " )";                
            else
                strParametrosRelatorio += "( Módulo: " + ddlModalidade.SelectedItem.ToString() + " - Disciplina: " + ddlMateria.SelectedItem.ToString() + " )";                       

            lIRelatorioWeb = varRelatorioWeb.CreateChannel();

            lRetorno = lIRelatorioWeb.RelEvasaoEscolar(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_MAT, strP_DDL_SEL, strP_DT_INI, strP_DT_FIM);

            string strURL = WRAuxiliares.RetornaCaminhoRelatorioPDF("/TMP_Relatorios/" + strIDSessao + "/") + strNomeRelatorio;
            Session["URLRelatorio"] = strURL;
            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);

            varRelatorioWeb.Close();
        
        }        
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidades()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos de Referência
        /// </summary>
        private void CarregaAnos()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            if (coEmp != 0)
            {
                ddlAnoReferIni.DataSource = (from tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                                             where tb48.TB25_EMPRESA.CO_EMP == coEmp
                                             select new { tb48.CO_ANO_MES_MAT }).Distinct().OrderByDescending( g => g.CO_ANO_MES_MAT );

                ddlAnoReferIni.DataTextField = "CO_ANO_MES_MAT";
                ddlAnoReferIni.DataValueField = "CO_ANO_MES_MAT";
                ddlAnoReferIni.DataBind();

                ddlAnoReferFim.DataSource = (from tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                                             where tb48.TB25_EMPRESA.CO_EMP == coEmp
                                             select new { tb48.CO_ANO_MES_MAT }).Distinct().OrderByDescending( g => g.CO_ANO_MES_MAT );

                ddlAnoReferFim.DataTextField = "CO_ANO_MES_MAT";
                ddlAnoReferFim.DataValueField = "CO_ANO_MES_MAT";
                ddlAnoReferFim.DataBind();
            }
            else
            {
                ddlAnoReferIni.Items.Clear();
                ddlAnoReferFim.Items.Clear();
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidade()
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
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            if (modalidade != 0)
            {
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                                            join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb01.CO_CUR equals tb43.CO_CUR
                                            where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb01.CO_EMP == coEmp
                                            select new { tb01.NO_CUR, tb01.CO_CUR }).Distinct().OrderBy( c => c.NO_CUR );

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();

                ddlSerieCurso.Items.Insert(0, new ListItem("Todos", "T"));
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Matérias
        /// </summary>
        private void CarregaMaterias()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            if (modalidade != 0)
            {
                ddlMateria.DataSource = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                                         join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                         where tb02.CO_MODU_CUR == modalidade && tb02.CO_EMP == coEmp
                                         select new { tb02.ID_MATERIA, tb107.NO_MATERIA }).Distinct().OrderBy( m => m.NO_MATERIA );

                ddlMateria.DataTextField = "NO_MATERIA";
                ddlMateria.DataValueField = "ID_MATERIA";
                ddlMateria.DataBind();
            }
        }
        #endregion   
     
        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipo.SelectedValue.ToString() == "S")
            {
                liSerie.Visible = true;
                liMateria.Visible = false;
                CarregaSerieCurso();
            }
            else
            {
                liSerie.Visible = false;
                liMateria.Visible = true;
                CarregaMaterias();
            }
        }

        protected void ddlUnidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaAnos();
            CarregaModalidade();
            if (ddlTipo.SelectedValue == "S")
            {
                liSerie.Visible = true;
                liMateria.Visible = false;
                CarregaSerieCurso();
            }
            else
            {
                liSerie.Visible = false;
                liMateria.Visible = true;
                CarregaMaterias();
            }
        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipo.SelectedValue == "S")
            {
                liSerie.Visible = true;
                liMateria.Visible = false;
                CarregaSerieCurso();
            }
            else
            {
                liSerie.Visible = false;
                liMateria.Visible = true;
                CarregaMaterias();
            }
        }
    }
}
