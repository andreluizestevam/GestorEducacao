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

namespace C2BR.GestorEducacao.UI.GSAUD._6000_GestaoMedicamentos._6900_Relatorios
{
    public partial class MoviUnidPorMedic : System.Web.UI.Page
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
                CarregaRegiao();
                CarregaGrupoItem();

                ddlArea.Items.Insert(0, new ListItem("Todos", "0"));
                ddlSubArea.Items.Insert(0, new ListItem("Todos", "0"));

                ddlSubGrpItem.Items.Insert(0, new ListItem("Todos", "0"));
                ddlItem.Items.Insert(0, new ListItem("Todos", "0"));

                ddlGrpItem.Items.Insert(0, new ListItem("Todos", "0"));
            }
        }

        #endregion

        #region Carregamentos

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            string parametros;
            string infos;
            int coEmp, lRetorno, grpItem, subGrpItem, Item, Unid, regiao, area, subarea;
            string dataIni, dataFim, tipoRelatorio, Periodo, deGrpItem, noSubGrpItem, noItem, noUnid, noReg, noArea, noSubArea, noTipoRelat;


            coEmp = LoginAuxili.CO_EMP;
            grpItem = int.Parse(ddlGrpItem.SelectedValue);
            subGrpItem = int.Parse(ddlSubGrpItem.SelectedValue);
            Item = int.Parse(ddlSubGrpItem.SelectedValue);
            Unid = int.Parse(ddlUnid.SelectedValue);
            regiao = int.Parse(ddlReg.SelectedValue);
            area = int.Parse(ddlArea.SelectedValue);
            subarea = int.Parse(ddlSubArea.SelectedValue);
            dataIni = IniPeri.Text;
            dataFim = FimPeri.Text;
            tipoRelatorio = ddlTipoRelatorio.SelectedValue;


            deGrpItem = (grpItem != 0 ? TB260_GRUPO.RetornaPelaChavePrimaria(grpItem).NOM_GRUPO : "Todos");
            noSubGrpItem = (subGrpItem != 0 ? TB261_SUBGRUPO.RetornaPelaChavePrimaria(subGrpItem).NOM_SUBGRUPO : "Todos");
            noItem = (Item != 0 ? TB90_PRODUTO.RetornaPelaChavePrimaria(Item, Unid).NO_PROD : "Todos");
            noUnid = (Unid != 0 ? TB25_EMPRESA.RetornaPelaChavePrimaria(Unid).NO_FANTAS_EMP : "Todos");
            noReg = (regiao != 0 ? TB906_REGIAO.RetornaPelaChavePrimaria(regiao).NM_REGIAO : "Todos");
            noSubArea = (subarea != 0 ? TB908_SUBAREA.RetornaPelaChavePrimaria(subarea).NM_SUBAREA : "Todos");
            noArea = (area != 0 ? TB907_AREA.RetornaPelaChavePrimaria(area).NM_AREA : "Todos");
            noTipoRelat = (tipoRelatorio == "U" ? noTipoRelat = "Por Unidade" : noTipoRelat = "Por Medicamento");
            Periodo = dataIni + " à " + dataFim;

            parametros = "( Grupo do Item: " + deGrpItem.ToUpper() + " - Subgrupo: " + noSubGrpItem.ToUpper() + " - Item: " + noItem.ToUpper() + " - Unidade: " + noUnid.ToUpper() + " - Região: " + noReg.ToUpper() + " - Área: " + noArea.ToUpper() + " - Subárea: " + noSubArea + " - Período: " + Periodo.ToUpper() + " - Tipo de Relatório: " + noTipoRelat + " )";
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            //Reg = (regiao != 0 ? tb155_plan_matr.

            RptMoviUnidPorMedic fpcb = new RptMoviUnidPorMedic();
            lRetorno = fpcb.InitReport(parametros, infos, grpItem, subGrpItem, coEmp, Item, Unid, regiao, area, subarea, dataIni, dataFim, tipoRelatorio);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        protected void CarregaUnidade()
        {

            var res = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                       select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP });

            ddlUnid.DataTextField = "NO_FANTAS_EMP";
            ddlUnid.DataValueField = "CO_EMP";

            ddlUnid.DataSource = res;
            ddlUnid.DataBind();

            ddlUnid.Items.Insert(0, new ListItem("Todas", "0"));

            ddlUnid.SelectedValue = LoginAuxili.CO_EMP.ToString();
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

        protected void CarregaGrupoItem()
        {
            //int valsel3 = int.Parse(ddlGrpItem.SelectedValue);

            var res = (from tb260 in TB260_GRUPO.RetornaTodosRegistros()
                       select new { tb260.ID_GRUPO, tb260.NOM_GRUPO });

            ddlGrpItem.DataTextField = "NOM_GRUPO";
            ddlGrpItem.DataValueField = "ID_GRUPO";

            ddlGrpItem.DataSource = res;
            ddlGrpItem.DataBind();

            //ddlGrpItem.Items.Insert(0, new ListItem("Todos", "0"));
        }

        protected void CarregaSubGrpItem()
        {
            int valsel = int.Parse(ddlGrpItem.SelectedValue);

            var res = from tb261 in TB261_SUBGRUPO.RetornaTodosRegistros()
                      where (ddlGrpItem.SelectedValue != "0" ? tb261.TB260_GRUPO.ID_GRUPO == valsel : 0 == 0)
                      select new { tb261.ID_SUBGRUPO, tb261.NOM_SUBGRUPO};

            ddlSubGrpItem.DataTextField = "NOM_SUBGRUPO";
            ddlSubGrpItem.DataValueField = "ID_SUBGRUPO";

            ddlSubGrpItem.DataSource = res;
            ddlSubGrpItem.DataBind();

            ddlSubGrpItem.Items.Insert(0, new ListItem("Todos", "0"));

        }

        protected void CarregaItem()
        {
            int valsel = int.Parse(ddlSubGrpItem.SelectedValue);

            var res = from tb90 in TB90_PRODUTO.RetornaTodosRegistros()
                      where (ddlSubGrpItem.SelectedValue != "0" ? tb90.TB261_SUBGRUPO.ID_SUBGRUPO == valsel : 0 == 0)
                      select new { tb90.CO_PROD, tb90.NO_PROD };

            ddlItem.DataTextField = "NO_PROD";
            ddlItem.DataValueField = "CO_PROD";

            ddlItem.DataSource = res;
            ddlItem.DataBind();

            ddlItem.Items.Insert(0, new ListItem("Todos", "0"));
        }



        #endregion

        #region Funções de campo

        protected void ddlTipoRelatorio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoRelatorio.SelectedValue.ToString() == "U")
            {

            }
            else
            {
    
            }
        }

        protected void ddlGrpItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrpItem();
        }

        protected void ddlSubGrpItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaItem();
        }

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