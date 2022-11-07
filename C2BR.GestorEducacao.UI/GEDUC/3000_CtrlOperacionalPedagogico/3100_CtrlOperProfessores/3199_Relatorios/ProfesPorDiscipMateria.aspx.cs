//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: GESTÃO DE DADOS DE PROFESSORES
// OBJETIVO: RELAÇÃO DE PROFESSORES POR MATÉRIA
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3100_CtrlOperProfessores.F3199_Relatorios
{
    public partial class ProfesPorDiscipMateria : System.Web.UI.Page
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
                CarregaAno();
                CarregaModalidade();
                CarregaSerie();
                CarregaTurma();
                CarregaMaterias();
            }
        }

//====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
//--------> Variáveis obrigatórias para gerar o Relatório
            string strIDSessao, strIdentFunc, strNomeCliente, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio;
            int lRetorno;

//--------> Variáveis de parâmetro do Relatório
            string strP_CO_EMP, strP_ID_MATERIA, strP_CLASSI;

            var varRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), WRAuxiliares.URLRelatorioWeb);
            IRelatorioWeb lIRelatorioWeb;            

            strIDSessao = Session.SessionID.ToString();
            strParametrosRelatorio = null;
            strIdentFunc = WRAuxiliares.IdentFunc;            
            strCaminhoRelatorioGerado = HttpRuntime.AppDomainAppPath + "TMP_Relatorios\\" + strIDSessao + "\\";
            strNomeRelatorio = WRAuxiliares.GeraNomeRelatorio("RelMapaDisciplinaProf");

//--------> Criação da Pasta
            if (!Directory.Exists(@strCaminhoRelatorioGerado))
                Directory.CreateDirectory(@strCaminhoRelatorioGerado);

//--------> Inicializa as variáveis
            strP_CO_EMP = null;
            strP_ID_MATERIA = null;
            strP_CLASSI = null;

//--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = ddlUnidade.SelectedValue;
            strP_ID_MATERIA = ddlMateria.SelectedValue;
            strP_CLASSI = ddlTipo.SelectedValue;           

            strParametrosRelatorio = "Unidade: " + ddlUnidade.SelectedItem.ToString() + " - Matéria: " + ddlMateria.SelectedItem.ToString();

            lIRelatorioWeb = varRelatorioWeb.CreateChannel();

            //Barao
            if (LoginAuxili.ORG_NUMERO_CNPJ == 09014296000174)
            {
                lRetorno = lIRelatorioWeb.RelMapaDisciplinaProfBarao(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_ID_MATERIA, strP_CLASSI);
            }
            else
                lRetorno = lIRelatorioWeb.RelMapaDisciplinaProf(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_ID_MATERIA, strP_CLASSI);

            string strURL = WRAuxiliares.RetornaCaminhoRelatorioPDF("/TMP_Relatorios/" + strIDSessao + "/") + strNomeRelatorio;
            Session["URLRelatorio"] = strURL;
            //AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);

            varRelatorioWeb.Close();
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidades()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, false);
        }

        /// <summary>
        /// Carrega todos os anos com matrículas do sistema
        /// </summary>
        private void CarregaAno()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaAnoGrdCurs(ddlAno, coEmp, false);
        }

        /// <summary>
        /// Carrega todos as modalidades disponíveis no sistema
        /// </summary>
        private void CarregaModalidade()
        {
            AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Carrega todas as série de acordo com a modalidade
        /// </summary>
        private void CarregaSerie()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            AuxiliCarregamentos.carregaSeriesCursos(ddlSerie, modalidade, coEmp, true);
        }

        /// <summary>
        /// Carrega todos os anos de acordo com a modalidade, série e ano
        /// </summary>
        private void CarregaTurma()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerie.SelectedValue != "" ? int.Parse(ddlSerie.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaTurmas(ddlTurma, coEmp, modalidade, serie, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Matérias
        /// </summary>
        private void CarregaMaterias()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerie.SelectedValue != "" ? int.Parse(ddlSerie.SelectedValue) : 0;
            string ano = ddlAno.SelectedValue;
            AuxiliCarregamentos.CarregaMateriasGradeCurso(ddlMateria, LoginAuxili.CO_EMP, modalidade, serie, ano, true);
        }
        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaModalidade();
            CarregaSerie();
            CarregaTurma();
            CarregaMaterias();
        }

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaModalidade();
            CarregaSerie();
            CarregaTurma();
            CarregaMaterias();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerie();
        }

        protected void ddlSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
        }
    }
}
