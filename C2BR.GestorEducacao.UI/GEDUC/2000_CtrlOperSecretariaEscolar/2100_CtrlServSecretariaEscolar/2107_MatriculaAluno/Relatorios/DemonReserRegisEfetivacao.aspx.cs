//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: MATRÍCULA NOVA PARA ALUNO
// OBJETIVO: RELAÇÃO DO DEMONSTRATIVO DE RESERVA/EFETIVAÇÃO
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

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2107_MatriculaAluno.Relatorios
{
    public partial class DemonReserRegisEfetivacao : System.Web.UI.Page
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
                liTurma.Visible = false;
                CarregaUnidades();
                CarregaModalidades();
                CarregaSerieCurso(null);
                CarregaTurma(null);
            }
        }

//====> Método que faz a chamada de outro método de acordo com o Tipo
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            if (ddlTipo.SelectedValue.ToString() == "R") // R - Reserva
                RelDemonstrativoReserva();
            else // E - Matrícula
                RelDemonstrativoMatricula();
        }

//====> Processo de Geração do Relatório
        void RelDemonstrativoReserva()
        {
//--------> Variáveis obrigatórias para gerar o Relatório
            string strIDSessao, strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio;
            int lRetorno;

//--------> Variáveis de parâmetro do Relatório
            string strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_DT_INI, strP_DT_FIM, strP_CO_SIT_MAT;

            var varRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), WRAuxiliares.URLRelatorioWeb);
            IRelatorioWeb lIRelatorioWeb;

            strIDSessao = Session.SessionID.ToString();
            strParametrosRelatorio = null;
            strIdentFunc = WRAuxiliares.IdentFunc;
            strCaminhoRelatorioGerado = HttpRuntime.AppDomainAppPath + "TMP_Relatorios\\" + strIDSessao + "\\";
            strNomeRelatorio = WRAuxiliares.GeraNomeRelatorio("RelDemonstrativoReserva");

//--------> Criação da Pasta
            if (!Directory.Exists(@strCaminhoRelatorioGerado))
                Directory.CreateDirectory(@strCaminhoRelatorioGerado);

//--------> Inicializa as variáveis
            strP_CO_EMP = null;
            strP_CO_MODU_CUR = null;
            strP_CO_CUR = null;
            strP_DT_INI = null;
            strP_DT_FIM = null;
            strP_CO_SIT_MAT = null;

//--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = ddlUnidade.SelectedValue;
            strP_CO_MODU_CUR = ddlModalidade.SelectedValue;
            strP_CO_CUR = ddlSerieCurso.SelectedValue;
            strP_DT_INI = txtDataPeriodoIni.Text;
            strP_DT_FIM = txtDataPeriodoFim.Text;
            strP_CO_SIT_MAT = ddlSituacao.SelectedValue;

            strParametrosRelatorio += "( Unidade de Ensino: " + ddlUnidade.SelectedItem.ToString();
            strParametrosRelatorio += " - Período: " + txtDataPeriodoIni.Text + " à " + txtDataPeriodoFim.Text;
            strParametrosRelatorio += " - Tipo: " + ddlTipo.SelectedItem.ToString();
            strParametrosRelatorio += " - Módulo: " + ddlModalidade.SelectedItem.ToString();
            strParametrosRelatorio += " - Série: " + ddlSerieCurso.SelectedItem.ToString();
            strParametrosRelatorio += " - Situação: " + ddlSituacao.SelectedItem.ToString() + " )";

            lIRelatorioWeb = varRelatorioWeb.CreateChannel();

            lRetorno = lIRelatorioWeb.RelDemonstrativoReserva(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_DT_INI, strP_DT_FIM, strP_CO_SIT_MAT);

            string strURL = WRAuxiliares.RetornaCaminhoRelatorioPDF("/TMP_Relatorios/" + strIDSessao + "/") + strNomeRelatorio;
            Session["URLRelatorio"] = strURL;
            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);

            varRelatorioWeb.Close();
        }

//====> Processo de Geração do Relatório
        void RelDemonstrativoMatricula()
        {
//--------> Variáveis obrigatórias para gerar o Relatório
            string strIDSessao, strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio;
            int lRetorno;

//--------> Variáveis de parâmetro do Relatório
            string strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_DT_INI, strP_DT_FIM, strP_CO_SIT_MAT;

            var varRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), WRAuxiliares.URLRelatorioWeb);
            IRelatorioWeb lIRelatorioWeb;

            strIDSessao = Session.SessionID.ToString();
            strParametrosRelatorio = null;
            strIdentFunc = WRAuxiliares.IdentFunc;
            strCaminhoRelatorioGerado = HttpRuntime.AppDomainAppPath + "TMP_Relatorios\\" + strIDSessao + "\\";
            strNomeRelatorio = WRAuxiliares.GeraNomeRelatorio("RelDemonstrativoMatricula");

//--------> Criação da Pasta
            if (!Directory.Exists(@strCaminhoRelatorioGerado))
                Directory.CreateDirectory(@strCaminhoRelatorioGerado);

//--------> Inicializa as variáveis
            strP_CO_EMP = null;
            strP_CO_MODU_CUR = null;
            strP_CO_CUR = null;
            strP_CO_TUR = null;
            strP_DT_INI = null;
            strP_DT_FIM = null;
            strP_CO_SIT_MAT = null;

//--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = ddlUnidade.SelectedValue;
            strP_CO_MODU_CUR = ddlModalidade.SelectedValue;
            strP_CO_CUR = ddlSerieCurso.SelectedValue;
            strP_CO_TUR = ddlTurma.SelectedValue;
            strP_DT_INI = txtDataPeriodoIni.Text;
            strP_DT_FIM = txtDataPeriodoFim.Text;
            strP_CO_SIT_MAT = ddlSituacao.SelectedValue;

            strParametrosRelatorio += "( Unidade de Ensino: " + ddlUnidade.SelectedItem.ToString();
            strParametrosRelatorio += " - Período: " + txtDataPeriodoIni.Text + " à " + txtDataPeriodoFim.Text;
            strParametrosRelatorio += " - Tipo: " + ddlTipo.SelectedItem.ToString();
            strParametrosRelatorio += " - Módulo: " + ddlModalidade.SelectedItem.ToString();
            strParametrosRelatorio += " - Série: " + ddlSerieCurso.SelectedItem.ToString();
            strParametrosRelatorio += " - Turma: " + ddlTurma.SelectedItem.ToString();
            strParametrosRelatorio += " - Situação: " + ddlSituacao.SelectedItem.ToString() + " )";

            lIRelatorioWeb = varRelatorioWeb.CreateChannel();

            lRetorno = lIRelatorioWeb.RelDemonstrativoMatricula(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_DT_INI, strP_DT_FIM, strP_CO_SIT_MAT);

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
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        /// <param name="coModuCur">Id da modalidade</param>
        private void CarregaTurma(int? coModuCur)
        {
            int modalidade;
            if (!String.IsNullOrEmpty(coModuCur.ToString()))
                modalidade = Convert.ToInt32(coModuCur);
            else
                modalidade = int.Parse(ddlModalidade.SelectedValue);

            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if (serie != 0)
            {
                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                                       select new { tb06.TB129_CADTURMAS.CO_SIGLA_TURMA, tb06.CO_TUR }).OrderBy( t => t.CO_SIGLA_TURMA );

                ddlTurma.DataTextField = "CO_SIGLA_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
            }
            else
                ddlTurma.Items.Clear();
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
        /// <param name="coModuCur">Id da modalidade</param>
        private void CarregaSerieCurso(int? coModuCur)
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            int modalidade;

            if (!String.IsNullOrEmpty(coModuCur.ToString()))
                modalidade = Convert.ToInt32(coModuCur);
            else
                modalidade = int.Parse(ddlModalidade.SelectedValue);

            ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(coEmp)
                                        where tb01.CO_MODU_CUR == modalidade
                                        join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(coEmp) on tb01.CO_CUR equals tb43.CO_CUR
                                        where tb43.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR
                                        select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy( c => c.NO_CUR );

            ddlSerieCurso.DataTextField = "NO_CUR";
            ddlSerieCurso.DataValueField = "CO_CUR";
            ddlSerieCurso.DataBind();
        }
        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso(int.Parse(ddlModalidade.SelectedValue));
            CarregaTurma(int.Parse(ddlModalidade.SelectedValue));
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma(int.Parse(ddlModalidade.SelectedValue));
        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipo.SelectedValue.ToString() == "R")
            {
                ddlSituacao.Items.Clear();
                ddlSituacao.Items.Add(new ListItem("TODOS", "T"));
                ddlSituacao.Items.Add(new ListItem("Em aberto", "A"));
                ddlSituacao.Items.Add(new ListItem("Inscrição", "I"));
                ddlSituacao.Items.Add(new ListItem("Matriculada", "M"));
                ddlSituacao.Items.Add(new ListItem("Cancelada", "C"));
                ddlSituacao.SelectedIndex = 0;
                liTurma.Visible = false;
            }
            else
            {
                ddlSituacao.Items.Clear();
                ddlSituacao.Items.Add(new ListItem("TODOS", "U"));
                ddlSituacao.Items.Add(new ListItem("Em aberto", "A"));
                ddlSituacao.Items.Add(new ListItem("Cancelada", "C"));
                ddlSituacao.Items.Add(new ListItem("Trancada", "T"));
                ddlSituacao.Items.Add(new ListItem("Finalizada", "F"));
                ddlSituacao.Items.Add(new ListItem("Pendente", "P"));
                ddlSituacao.SelectedIndex = 0;
                liTurma.Visible = true;
            }
        }

        protected void ddlUnidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaModalidades();
            CarregaSerieCurso(null);
            CarregaTurma(null);
        }   
    }
}
