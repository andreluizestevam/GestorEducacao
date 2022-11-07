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
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using System.ServiceModel;
using System.Configuration;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Library.Auxiliares
{
    public class AuxiliRelatorioTemporario
    {
        #region Variáveis

        ChannelFactory<IRelatorioWeb> cfIRelatorioWeb = new ChannelFactory<IRelatorioWeb>();
        string strIDSessao, strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio;

        #endregion

        #region Eventos

        #region Eventos (Declaração)

        public delegate void OnInicializaVarsRelatorioGeradoHandler();
        public event OnInicializaVarsRelatorioGeradoHandler OnBeforeReportBeginGenerated;

        public delegate void OnRelatorioGeradoHandler(Page currentPage);
        public event OnRelatorioGeradoHandler OnRelatorioGerado;

        #endregion

        /// <summary>
        /// Método que abre uma nova página now browser com o pdf do relatório gerado
        /// </summary>
        /// <param name="paginaCorrente">Página corrente</param>
        void AuxiliRelatorioTemporario_OnRelatorioGerado(Page paginaCorrente)
        {
            paginaCorrente.ClientScript.RegisterStartupScript(paginaCorrente.GetType(), "", "setTimeout(\"window.open('" + (C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares.RetornaCaminhoRelatorioPDF("/TMP_Relatorios/" + strIDSessao + "/") + strNomeRelatorio) + "', '" + new Random(DateTime.Now.Millisecond).Next().ToString() + "')\", 1000);", true);
            cfIRelatorioWeb.Close();
        }

        /// <summary>
        /// Método que inicializa as variáveis padrões para todos os relatórios
        /// </summary>
        void AuxiliRelatorioTemporario_OnInicializaVarsRelatorioGeradoHandler()
        {
            //--------> Inicializa os valores padrão para todos os relatorios
            //--------> Http SessionId atual, usada para gerar o relatorio
            strIDSessao = HttpContext.Current.Session.SessionID.ToString();
            //--------> Identificação da origem do relatorio
            strIdentFunc = "UE: 187 EF019 - MAT: 80309-9 - IP Origem: 172.16.0.52";
            //--------> Gera o caminho da pasta do relatorio
            strCaminhoRelatorioGerado = HttpRuntime.AppDomainAppPath + "TMP_Relatorios\\" + strIDSessao + "\\";
            //--------> Gera aleatoriamente o nome do relatório
            strNomeRelatorio = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares.GeraNomeRelatorio("Rel" + new Random(DateTime.Now.Millisecond).Next());

            //--------> Criação da Pasta
            if (!Directory.Exists(@strCaminhoRelatorioGerado))
                Directory.CreateDirectory(@strCaminhoRelatorioGerado);
        }

        #endregion

        #region Construtores

        public AuxiliRelatorioTemporario()
        {
            OnBeforeReportBeginGenerated += new OnInicializaVarsRelatorioGeradoHandler(AuxiliRelatorioTemporario_OnInicializaVarsRelatorioGeradoHandler);
            OnRelatorioGerado += new OnRelatorioGeradoHandler(AuxiliRelatorioTemporario_OnRelatorioGerado);
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método desenvolvido para redirecionar para um determinado módulo da solução de acordo com o ID passado (Meus atalhos)
        /// </summary>
        /// <param name="intIdModulo">Id do módulo</param>
        static void RedirecionaParaModulo(int intIdModulo)
        {
            ADMMODULO admModulo = ADMMODULO.RetornaTodosRegistros().Where(modulos => modulos.ideAdmModulo.Equals(intIdModulo)).FirstOrDefault();

            HttpContext.Current.Session[Resources.SessoesHttp.IdModuloCorrente] = intIdModulo;
            QueryStringAuxili.RedirecionaParaOperacao(QueryStrings.PaginaURLIFrame, (object)String.Format("{0}&ititle={1}", admModulo.nomURLModulo,
                                                                                                                       HttpContext.Current.Server.UrlEncode(admModulo.nomModulo_GEREN)));
        }

        /// <summary>
        /// Método para carregar os relatórios, fixamente, da antiga lógica de Informativos.
        /// </summary>
        /// <param name="paginaCorrente">Página corrente</param>
        /// <param name="strIdModulo">Id do módulo</param>
        public static void ExecutaAvisoRelatorio(Page paginaCorrente, string strIdModulo)
        {
            if (strIdModulo.Equals("8"))
                new AuxiliRelatorioTemporario().AvisosRelPlanRealizado(paginaCorrente, "187", "D", "D", "2010", "2011");
            else if (strIdModulo.Equals("44"))
                new AuxiliRelatorioTemporario().RelMapaSolicRealiz(paginaCorrente, "187", "2011");
            else if (strIdModulo.Equals("84"))
                new AuxiliRelatorioTemporario().RelMapaCaracteristicaMatricula(paginaCorrente, "187", "2009");
            else if (strIdModulo.Equals("111"))
                new AuxiliRelatorioTemporario().RelFinalAlunos(paginaCorrente, "187", "2009", "2009", "1", "16", "13", "N");
        }

        /// <summary>
        /// Método utilizado para gerar relatório ou abrir uma funcionalidade do sistema de acordo com o Id do módulo (lógica dos Meus atalhos)        
        /// </summary>
        /// <param name="paginaCorrente">Página corrente</param>
        /// <param name="strIdModulo">Id do módulo</param>
        public static void ExecutaRelatorio(Page paginaCorrente, string strIdModulo)
        {
            //--------> Gerenciais que abrem o relatório
            if (strIdModulo.Equals("6"))
                new AuxiliRelatorioTemporario().RelMapadePlanejAnualMatricula(paginaCorrente, "187", "1", "12", "2009");
            else if (strIdModulo.Equals("8"))
                new AuxiliRelatorioTemporario().RelPlanRealizado(paginaCorrente, "187", "D", "A", "2010", "2011");
            else if (strIdModulo.Equals("24"))
                new AuxiliRelatorioTemporario().RelCurvaABCFreqFunc(paginaCorrente, "187", "T", "01/01/2009", "31/12/2009", "P");
            else if (strIdModulo.Equals("83"))
                new AuxiliRelatorioTemporario().RelMatricEfetivadas(paginaCorrente, "187", "2009", "T", "Ensino Padrão", "16");
            else if (strIdModulo.Equals("84"))
                new AuxiliRelatorioTemporario().RelMapaCaracteristicaMatricula(paginaCorrente, "187", "2009");

//--------> Gerenciais que vão para a tela de Parametro
            else if (strIdModulo.Equals("13"))
                RedirecionaParaModulo(13);
            else if (strIdModulo.Equals("14"))
                RedirecionaParaModulo(14);
            else if (strIdModulo.Equals("16"))
                RedirecionaParaModulo(16);
            else if (strIdModulo.Equals("139"))
                RedirecionaParaModulo(139);
            else if (strIdModulo.Equals("146"))
                RedirecionaParaModulo(146);
        }

        /// <summary>
        /// Método que retorna o identificador do funcionário IP XXX.XXX.XXX.XXX - Unid/Matr: XXXXX/XX.XXX-X
        /// </summary>
        /// <param name="strIP">Nome do relatório</param>
        /// <returns>String com o identificador do funcionário</returns>
        public static string GeraIdentFuncionarioRelatorio(string strIP)
        {
            if (LoginAuxili.TIPO_USU != "R")
            {
                return "IP " + strIP + " - Unid/Matr: " + TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP).sigla + "/" + LoginAuxili.CO_MAT_COL.Insert(5, "-").Insert(2, ".") + " - Data de Emissão: " + DateTime.Now.ToString("dd/MM/yy") + " às " + DateTime.Now.ToString("HH:mm");

            }
            else
            {
                return "IP " + strIP + " - Responsável: " + TB108_RESPONSAVEL.RetornaPelaChavePrimaria(LoginAuxili.CO_RESP).NO_RESP + "/" + " - Data de Emissão: " + DateTime.Now.ToString("dd/MM/yy") + " às " + DateTime.Now.ToString("HH:mm");
            }
        }

        #region Relatórios Gerados

        public void RelMapadePlanejAnualMatricula(Page paginaCorrente, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_DPTO_CUR, string strP_CO_ANO_REF)
        {
            OnBeforeReportBeginGenerated();
            ChannelFactory<IRelatorioWeb> lcfIRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), "net.tcp://localhost:1234/C2BRDLLRelatorioWeb/");
            IRelatorioWeb varIRelatorioWeb = lcfIRelatorioWeb.CreateChannel();

            strParametrosRelatorio = "( Unidade: E.M.E.F. Leonilda Ravaglio Trevisan - Módulo: Ensino Padrão - Departamento: Departamento de Ensino Pedagógico - Ano Base: 2009 )";

            if (varIRelatorioWeb.RelMapadePlanejAnualMatricula(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_DPTO_CUR, strP_CO_ANO_REF).Equals(1))
                OnRelatorioGerado(paginaCorrente);

            lcfIRelatorioWeb.Close();
        }

        public void RelMeusAcessos(Page paginaCorrente, string strP_CO_EMP, string strP_CO_EMP_COL, string strP_CO_COL)
        {
            OnBeforeReportBeginGenerated();
            ChannelFactory<IRelatorioWeb> lcfIRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), "net.tcp://localhost:1234/C2BRDLLRelatorioWeb/");
            IRelatorioWeb varIRelatorioWeb = lcfIRelatorioWeb.CreateChannel();

            if (varIRelatorioWeb.RelMeusAcessos(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_EMP_COL, strP_CO_COL, "01/01/1900", DateTime.Now.ToShortDateString()).Equals(1))
                OnRelatorioGerado(paginaCorrente);

            lcfIRelatorioWeb.Close();
        }

        public void RelMinhasMsgs(Page paginaCorrente, string strP_CO_EMP, string strP_CO_EMP_COL, string strP_CO_COL)
        {
            OnBeforeReportBeginGenerated();
            ChannelFactory<IRelatorioWeb> lcfIRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), "net.tcp://localhost:1234/C2BRDLLRelatorioWeb/");
            IRelatorioWeb varIRelatorioWeb = lcfIRelatorioWeb.CreateChannel();

            if (varIRelatorioWeb.RelMinhasMsgs(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_EMP_COL, strP_CO_COL, "01/01/1900", DateTime.Now.ToShortDateString()).Equals(1))
                OnRelatorioGerado(paginaCorrente);

            lcfIRelatorioWeb.Close();
        }

        public void RelFicCadFuncionario(Page paginaCorrente, string strP_CO_EMP, string strP_CO_EMP_COL, string strP_CO_COL, string strP_FLA_PROFESSOR, string strP_TP_PONTO)
        {
            OnBeforeReportBeginGenerated();
            ChannelFactory<IRelatorioWeb> lcfIRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), "net.tcp://localhost:1234/C2BRDLLRelatorioWeb/");
            IRelatorioWeb varIRelatorioWeb = lcfIRelatorioWeb.CreateChannel();

            if (varIRelatorioWeb.RelFicCadFuncionario(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP_COL, strP_CO_COL, DateTime.Now.Year.ToString(), strP_FLA_PROFESSOR, strP_TP_PONTO).Equals(1))
                OnRelatorioGerado(paginaCorrente);

            lcfIRelatorioWeb.Close();
        }

        public void RelMinhaBibliot(Page paginaCorrente, string strP_CO_EMP, string strP_CO_EMP_COL, string strP_CO_COL)
        {
            OnBeforeReportBeginGenerated();
            ChannelFactory<IRelatorioWeb> lcfIRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), "net.tcp://localhost:1234/C2BRDLLRelatorioWeb/");
            IRelatorioWeb varIRelatorioWeb = lcfIRelatorioWeb.CreateChannel();

            if (varIRelatorioWeb.RelMinhaBibliot(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_EMP_COL, strP_CO_COL).Equals(1))
                OnRelatorioGerado(paginaCorrente);

            lcfIRelatorioWeb.Close();
        }

        public void RelAgendaContatos(Page paginaCorrente, string strP_CO_EMP, string strP_CO_COL)
        {
            OnBeforeReportBeginGenerated();
            ChannelFactory<IRelatorioWeb> lcfIRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), "net.tcp://localhost:1234/C2BRDLLRelatorioWeb/");
            IRelatorioWeb varIRelatorioWeb = lcfIRelatorioWeb.CreateChannel();

            if (varIRelatorioWeb.RelAgendaContatos(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_COL).Equals(1))
                OnRelatorioGerado(paginaCorrente);

            lcfIRelatorioWeb.Close();
        }

        public void RelPlanRealizado(Page paginaCorrente, string strP_CO_EMP, string strP_TP_CONTA, string strP_TP_RELATORIO, string strP_CO_ANO_INI, string strP_CO_ANO_FIM)
        {
            OnBeforeReportBeginGenerated();
            ChannelFactory<IRelatorioWeb> lcfIRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), "net.tcp://localhost:1234/C2BRDLLRelatorioWeb/");
            IRelatorioWeb varIRelatorioWeb = lcfIRelatorioWeb.CreateChannel();

            strParametrosRelatorio = "( Unidade: E.M.E.F. Leonilda Ravaglio Trevisan - Ano Base: 2009 - Visualização: Despesas - Tipo: Analítico )";

            if (varIRelatorioWeb.RelPlanRealizado(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_TP_CONTA, strP_TP_RELATORIO, strP_CO_ANO_INI, strP_CO_ANO_FIM).Equals(1))
                OnRelatorioGerado(paginaCorrente);

            lcfIRelatorioWeb.Close();
        }

        public void AvisosRelPlanRealizado(Page paginaCorrente, string strP_CO_EMP, string strP_TP_CONTA, string strP_TP_RELATORIO, string strP_CO_ANO_INI, string strP_CO_ANO_FIM)
        {
            OnBeforeReportBeginGenerated();
            ChannelFactory<IRelatorioWeb> lcfIRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), "net.tcp://localhost:1234/C2BRDLLRelatorioWeb/");
            IRelatorioWeb varIRelatorioWeb = lcfIRelatorioWeb.CreateChannel();

            strParametrosRelatorio = "( Unidade: E.M.E.F. Leonilda Ravaglio Trevisan - Ano Base: 2010 à 2011 - Visualização: Despesas - Tipo: Diferença )";

            if (varIRelatorioWeb.RelPlanRealizado(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_TP_CONTA, strP_TP_RELATORIO, strP_CO_ANO_INI, strP_CO_ANO_FIM).Equals(1))
                OnRelatorioGerado(paginaCorrente);

            lcfIRelatorioWeb.Close();
        }

        public void RelCustoFinFunc(Page paginaCorrente, string strP_CO_EMP, string strP_FLA_PROFESSOR, string strP_CO_SEXO_COL, string strP_tp_def)
        {
            OnBeforeReportBeginGenerated();
            ChannelFactory<IRelatorioWeb> lcfIRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), "net.tcp://localhost:1234/C2BRDLLRelatorioWeb/");
            IRelatorioWeb varIRelatorioWeb = lcfIRelatorioWeb.CreateChannel();

            strParametrosRelatorio = "( Unidade: E.M.E.F. Leonilda Ravaglio Trevisan - Categoria: Todos - Sexo: Todos - Deficiência: Todas )";

            if (varIRelatorioWeb.RelCustoFinFunc(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_EMP, strP_FLA_PROFESSOR, strP_CO_SEXO_COL, strP_tp_def).Equals(1))
                OnRelatorioGerado(paginaCorrente);

            lcfIRelatorioWeb.Close();
        }

        public void RelCurvaABCFreqFunc(Page paginaCorrente, string strP_CO_EMP, string strP_CO_FUN, string strP_DT_INI, string strP_DT_FIM, string strP_TP_PONTO)
        {
            OnBeforeReportBeginGenerated();
            ChannelFactory<IRelatorioWeb> lcfIRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), "net.tcp://localhost:1234/C2BRDLLRelatorioWeb/");
            IRelatorioWeb varIRelatorioWeb = lcfIRelatorioWeb.CreateChannel();

            strParametrosRelatorio = "( Unidade: E.M.E.F. Leonilda Ravaglio Trevisan - Categoria: Todos - Sexo: Todos - Deficiência: Todas )";

            if (varIRelatorioWeb.RelCurvaABCFreqFunc(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_FUN, strP_DT_INI, strP_DT_FIM, strP_TP_PONTO).Equals(1))
                OnRelatorioGerado(paginaCorrente);

            lcfIRelatorioWeb.Close();
        }

        public void RelMatricEfetivadas(Page paginaCorrente, string strP_CO_EMP, string strP_ANO_REFER, string strP_CO_MODU_CUR, string strP_DES_MODU_CUR, string strP_CO_CUR)
        {
            OnBeforeReportBeginGenerated();
            ChannelFactory<IRelatorioWeb> lcfIRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), "net.tcp://localhost:1234/C2BRDLLRelatorioWeb/");
            IRelatorioWeb varIRelatorioWeb = lcfIRelatorioWeb.CreateChannel();

            strParametrosRelatorio = "( Unidade: E.M.E.F. Leonilda Ravaglio Trevisan - Categoria: Todos - Sexo: Todos - Deficiência: Todas )";

            if (varIRelatorioWeb.RelMatricEfetivadas(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_ANO_REFER, strP_CO_MODU_CUR, strP_DES_MODU_CUR, strP_CO_CUR).Equals(1))
                OnRelatorioGerado(paginaCorrente);

            lcfIRelatorioWeb.Close();
        }

        public void RelMapaCaracteristicaMatricula(Page paginaCorrente, string strP_CO_EMP, string strP_CO_ANO_REF)
        {
            OnBeforeReportBeginGenerated();
            ChannelFactory<IRelatorioWeb> lcfIRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), "net.tcp://localhost:1234/C2BRDLLRelatorioWeb/");
            IRelatorioWeb varIRelatorioWeb = lcfIRelatorioWeb.CreateChannel();

            strParametrosRelatorio = "( Unidade: E.M.E.F. Leonilda Ravaglio Trevisan - Categoria: Todos - Sexo: Todos - Deficiência: Todas )";

            if (varIRelatorioWeb.RelMapaCaracteristicaMatricula(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_ANO_REF).Equals(1))
                OnRelatorioGerado(paginaCorrente);

            lcfIRelatorioWeb.Close();
        }

        public void RelMapaSolicRealiz(Page paginaCorrente, string strP_CO_EMP, string strP_CO_ANO_REFER)
        {
            OnBeforeReportBeginGenerated();
            ChannelFactory<IRelatorioWeb> lcfIRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), "net.tcp://localhost:1234/C2BRDLLRelatorioWeb/");
            IRelatorioWeb varIRelatorioWeb = lcfIRelatorioWeb.CreateChannel();

            if (varIRelatorioWeb.RelMapaSolicRealiz(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_ANO_REFER).Equals(1))
                OnRelatorioGerado(paginaCorrente);

            lcfIRelatorioWeb.Close();
        }

        public void RelFinalAlunos(Page paginaCorrente, string strP_CO_EMP, string strP_CO_ANO_REFER, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_Classificacao)
        {
            OnBeforeReportBeginGenerated();
            ChannelFactory<IRelatorioWeb> lcfIRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), "net.tcp://localhost:1234/C2BRDLLRelatorioWeb/");
            IRelatorioWeb varIRelatorioWeb = lcfIRelatorioWeb.CreateChannel();

            strParametrosRelatorio = "( Unidade: E.M.E.F. Leonilda Ravaglio Trevisan - Ano de Referência: 2009   - Módulo: Ensino Padrão - Tipo de Curso: 1º Ano - Turma: Turma 01-A/Manhã )";

            if (varIRelatorioWeb.RelFinalAlunos(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_Classificacao).Equals(1))
                OnRelatorioGerado(paginaCorrente);

            lcfIRelatorioWeb.Close();
        }
        #endregion

        #endregion
    }
}
