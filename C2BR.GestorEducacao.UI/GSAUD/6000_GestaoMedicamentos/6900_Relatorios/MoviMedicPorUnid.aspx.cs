//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: GESTÃO DE DADOS DE ALUNOS
// OBJETIVO: RELAÇÃO DE ALUNOS - ANIVERSARIANTES
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
using C2BR.GestorEducacao.Reports.GSAUD._6000_GestaoMedicamentos._6900;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GSAUD._6000_GestaoMedicamentos._6900_Relatorios
{
    public partial class MoviMedicPorUnid : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaUnidade();
                CarregaMedic();
                CarregaRegiao();

                
                ddlArea.Items.Insert(0, new ListItem("Todos", "0"));
                ddlSubArea.Items.Insert(0, new ListItem("Todos", "0"));
                ddlMedic.Items.Insert(0, new ListItem("Todos", "0"));
            }
        }

        #endregion
        
        #region Carregamentos
        /// <summary>
        /// Método que carrega as informações de solicitações na grid grdSoli
        /// </summary>
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {

            string parametros;
            string infos;
            int coEmp, lRetorno, regiao, area, subarea, medic, unidSolic;
            string dataIni, dataFim, Periodo, noUnidSoli, noMedic, noReg, noArea, noSubare;
            

            coEmp = LoginAuxili.CO_EMP;
            unidSolic = int.Parse(ddlUnidSolic.SelectedValue);
            regiao = int.Parse(ddlReg.SelectedValue);
            area = int.Parse(ddlArea.SelectedValue);
            subarea = int.Parse(ddlSubArea.SelectedValue);
            medic = int.Parse(ddlMedic.SelectedValue);
            dataIni = IniPeri.Text;
            dataFim = FimPeri.Text;


            noUnidSoli = (unidSolic != 0 ? TB25_EMPRESA.RetornaPelaChavePrimaria(unidSolic).NO_FANTAS_EMP : "Todos");
            noMedic = (medic != 0 ? TB90_PRODUTO.RetornaPelaChavePrimaria(medic, LoginAuxili.CO_EMP).NO_PROD : "Todos");
            noReg = (regiao != 0 ? TB906_REGIAO.RetornaPelaChavePrimaria(regiao).NM_REGIAO : "Todos");
            noArea = (area != 0 ? TB907_AREA.RetornaPelaChavePrimaria(area).NM_AREA : "Todos");
            noSubare = (subarea != 0 ? TB908_SUBAREA.RetornaPelaChavePrimaria(subarea).NM_SUBAREA : "Todos");
            Periodo = dataIni + " à " + dataFim;

            parametros = "( Unidade: " + noUnidSoli.ToUpper() + " - Medicamento: " + noMedic.ToUpper() + " - Região: " + noReg.ToUpper() + " - Área: " + noArea.ToUpper() +" - Subárea: " + noSubare.ToUpper() + " - Período: " + Periodo.ToUpper() + " )";
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptMoviMedicPorUnid fpcb = new RptMoviMedicPorUnid();
            lRetorno = fpcb.InitReport(parametros, infos, unidSolic, coEmp, medic, regiao, area, subarea, dataIni, dataFim);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }


        protected void CarregaUnidade() {

            var res = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                       select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP });

            ddlUnidSolic.DataTextField = "NO_FANTAS_EMP";
            ddlUnidSolic.DataValueField = "CO_EMP";

            ddlUnidSolic.DataSource = res;
            ddlUnidSolic.DataBind();

            ddlUnidSolic.Items.Insert(0, new ListItem("Todos", "0"));

            ddlUnidSolic.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        protected void CarregaMedic()
        {
            var res = (from tb90 in TB90_PRODUTO.RetornaTodosRegistros()
                       select new { tb90.CO_PROD, tb90.NO_PROD });

            ddlMedic.DataTextField = "NO_PROD";
            ddlMedic.DataValueField = "CO_PROD";

            ddlMedic.DataSource = res;
            ddlMedic.DataBind();

            //ddlMedic.Items.Insert(0, new ListItem("Todos", "0"));
        }

        protected void CarregaRegiao()
        {

            var res = (from tb906 in TB906_REGIAO.RetornaTodosRegistros()
                       select new { tb906.ID_REGIAO, tb906.NM_REGIAO });

            ddlReg.DataTextField = "NM_REGIAO";
            ddlReg.DataValueField = "ID_REGIAO";

            ddlReg.DataSource = res;
            ddlReg.DataBind();

            ddlReg.Items.Insert(0, new ListItem("Todos", "0"));

        }

        protected void CarregaArea()
        {

            int valsel = int.Parse(ddlReg.SelectedValue);

                var res = (from tb907 in TB907_AREA.RetornaTodosRegistros()
                           where (ddlReg.SelectedValue != "0" ? tb907.TB906_REGIAO.ID_REGIAO == valsel : 0 == 0)
                           select new { tb907.ID_AREA, tb907.NM_AREA, });

                ddlArea.DataTextField = "NM_AREA";
                ddlArea.DataValueField = "ID_AREA";

                ddlArea.DataSource = res;
                ddlArea.DataBind();

                ddlArea.Items.Insert(0, new ListItem("Todos", "0"));

            
        }
        protected void CarregaSubArea()
        {
            int valsel2 = int.Parse(ddlArea.SelectedValue);

            var res = (from tb908 in TB908_SUBAREA.RetornaTodosRegistros()
                       where (ddlArea.SelectedValue != "0" ? tb908.TB907_AREA.ID_AREA == valsel2 : 0 == 0)
                       select new { tb908.ID_SUBAREA, tb908.NM_SUBAREA });

            ddlSubArea.DataTextField = "NM_SUBAREA";
            ddlSubArea.DataValueField = "ID_SUBAREA";

            ddlSubArea.DataSource = res;
            ddlSubArea.DataBind();

            ddlSubArea.Items.Insert(0, new ListItem("Todos", "0"));

        }

        #endregion

        #region Funções de campo

        protected void ddlReg_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaArea();
        }

        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubArea();
        }

        #endregion
    }
}