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
//15/05/2014| Maxwell Almeida            | Criação da página e Filtro para que possam ser conseguidas as informações de quantos plantonistas/atividade domiciliar, etc 
//          |                            | existem para cada Unidade/função/região/cidade, etc.

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
using C2BR.GestorEducacao.Reports.GSAUD._7000_ControleOperRH._7200_CtrlColaborPerson;

namespace C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7300_CtrlCadastralInforFunc._7399_Relatorios
{
    public partial class RelFuncParam : System.Web.UI.Page
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
                CarregaRegiao();
                CarregaUnidade();
                CarregaUnidadeContr();
                carregaUF();
                carregaGrpEspec();
                carregaClassFunc();
                carregaCategoria();

                ddlSubArea.Items.Insert(0, new ListItem("Todos", "0"));
                ddlArea.Items.Insert(0, new ListItem("Todos", "0"));
                ddlCidade.Items.Insert(0, new ListItem("Todos", "0"));
                ddlBairro.Items.Insert(0, new ListItem("Todos", "0"));
                ddlEspecia.Items.Insert(0, new ListItem("Todos", "0"));

            }
        }

        #endregion

        #region Carregamentos

        //====> Método que faz a chamada de outro método de acordo com o ddlTipoPesq
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            if (ddlTipoPesq.SelectedValue == "U" || ddlTipoPesq.SelectedValue == "U2")
                porUnidade();
            else if (ddlTipoPesq.SelectedValue == "F")
                porFuncao();
            else if (ddlTipoPesq.SelectedValue == "C")
                porCidadeBairro();
        }

        void porUnidade()
        {

            string parametros;
            string infos;
            int coEmp = LoginAuxili.CO_EMP;
            int lRetorno, UnidCadastro, UnidContrato, regiao, area, subarea, Cidade, Bairro, classFunc, categoria, especializa;
            string tipoRelatorio, noUnidCadastro, noUnidContrato, noRegiao, noArea, noSubarea, noUF, noCidade, noBairro, noClassFunc, noCategoria, noEspecializa, uf;

            UnidCadastro = int.Parse(ddlUnidCadastro.SelectedValue);
            UnidContrato = int.Parse(ddlUnidContrato.SelectedValue);
            regiao = int.Parse(ddlReg.SelectedValue);
            area = int.Parse(ddlArea.SelectedValue);
            subarea = int.Parse(ddlSubArea.SelectedValue);
            uf = ddlUF.SelectedValue;
            Cidade = int.Parse(ddlCidade.SelectedValue);
            Bairro = int.Parse(ddlBairro.SelectedValue);
            classFunc = int.Parse(ddlClassFuncion.SelectedValue);
            categoria = int.Parse(ddlCategoria.SelectedValue);
            especializa = int.Parse(ddlEspecia.SelectedValue);

            noUnidCadastro = (UnidCadastro != 0 ? TB25_EMPRESA.RetornaPelaChavePrimaria(UnidCadastro).NO_FANTAS_EMP : "Todos");
            noUnidContrato = (UnidContrato != 0 ? TB25_EMPRESA.RetornaPelaChavePrimaria(UnidCadastro).NO_FANTAS_EMP : "Todos");
            noRegiao = (regiao != 0 ? TB906_REGIAO.RetornaPelaChavePrimaria(regiao).NM_REGIAO : "Todos");
            noArea = (area != 0 ? TB907_AREA.RetornaPelaChavePrimaria(area).NM_AREA : "Todos");
            noSubarea = (subarea != 0 ? TB908_SUBAREA.RetornaPelaChavePrimaria(subarea).NM_SUBAREA : "Todos");
            noUF = (uf != "0" ? TB74_UF.RetornaPelaChavePrimaria(uf).CODUF : "Todos");
            noCidade = (Cidade != 0 ? TB904_CIDADE.RetornaPelaChavePrimaria(Cidade).NO_CIDADE : "Todos");
            noBairro = (Bairro != 0 ? TB905_BAIRRO.RetornaPelaChavePrimaria(Bairro).NO_BAIRRO : "Todos");
            noClassFunc = (classFunc != 0 ? TB128_FUNCA_FUNCI.RetornaPelaChavePrimaria(classFunc).NO_FUNCA_FUNCI : "Todos");
            noCategoria = (categoria != 0 ? TB127_CATEG_FUNCI.RetornaPelaChavePrimaria(categoria).NO_CATEG_FUNCI : "Todos");
            noEspecializa = (especializa != 0 ? TB115_GRUPO_ESPECIALIDADE.RetornaPelaChavePrimaria(especializa).DE_GRUPO_ESPECI : "Todos");
            tipoRelatorio = "Por Unidade";

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            //if (chkEmiteGraf.Checked == true)
            //{
            //    parametros = "( Unidade Cadastro: " + noUnidCadastro.ToUpper() + " - Unidade Contrato: " + noUnidContrato.ToUpper() + " - Região/Área: " + noRegiao.ToUpper()
            //    + " - Área: " + noArea.ToUpper() + " - Subarea: " + noSubarea + " - UF: " + noUF + " - Cidade: " + noCidade + " - Bairro: " + noBairro.ToUpper()
            //    + " - Classificação Funcional: " + noClassFunc.ToUpper() + " - Categoria: " + noCategoria.ToUpper() + " - Especialidade: " + noEspecializa.ToUpper() + " - Emite Gráficos: SIM )";

            //    RptRelFuncParamPorUnidGraf fpcb = new RptRelFuncParamPorUnidGraf();
            //    lRetorno = fpcb.InitReport(parametros, infos, coEmp, UnidCadastro, UnidContrato, regiao, area, subarea, uf, Cidade, Bairro, classFunc, categoria, especializa);
            //    Session["Report"] = fpcb;
            //    Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            //    AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
            //}
            //else
            //{
                parametros = "( Unidade Cadastro: " + noUnidCadastro.ToUpper() + " - Unidade Contrato: " + noUnidContrato.ToUpper() + " - Região/Área: " + noRegiao.ToUpper()
                    + " - Área: " + noArea.ToUpper() + " - Subarea: " + noSubarea + " - UF: " + noUF + " - Cidade: " + noCidade + " - Bairro: " + noBairro.ToUpper()
                    + " - Classificação Funcional: " + noClassFunc.ToUpper() + " - Categoria: " + noCategoria.ToUpper() + " - Especialidade: " + noEspecializa.ToUpper() + " - Emite Gráficos: NÃO )";

                RptRelFuncParamPorUnid fpcb = new RptRelFuncParamPorUnid();
                lRetorno = fpcb.InitReport(parametros, infos, coEmp, UnidCadastro, UnidContrato, regiao, area, subarea, uf, Cidade, Bairro, classFunc, categoria, especializa, (chkEmiteGraf.Checked ? true : false), (chkComRelatorio.Checked ? true : false), ddlTipoPesq.SelectedValue);
                Session["Report"] = fpcb;
                Session["URLRelatorio"] = "/GeducReportViewer.aspx";

                AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        void porFuncao()
        {
            string parametros;
            string infos;
            int coEmp = LoginAuxili.CO_EMP;
            int lRetorno, UnidCadastro, UnidContrato, regiao, area, subarea, Cidade, Bairro, classFunc, categoria, especializa;
            string tipoRelatorio, noUnidCadastro, noUnidContrato, noRegiao, noArea, noSubarea, noUF, noCidade, noBairro, noClassFunc, noCategoria, noEspecializa, uf;

            UnidCadastro = int.Parse(ddlUnidCadastro.SelectedValue);
            UnidContrato = int.Parse(ddlUnidContrato.SelectedValue);
            regiao = int.Parse(ddlReg.SelectedValue);
            area = int.Parse(ddlArea.SelectedValue);
            subarea = int.Parse(ddlSubArea.SelectedValue);
            uf = ddlUF.SelectedValue;
            Cidade = int.Parse(ddlCidade.SelectedValue);
            Bairro = int.Parse(ddlBairro.SelectedValue);
            classFunc = int.Parse(ddlClassFuncion.SelectedValue);
            categoria = int.Parse(ddlCategoria.SelectedValue);
            especializa = int.Parse(ddlEspecia.SelectedValue);

            noUnidCadastro = (UnidCadastro != 0 ? TB25_EMPRESA.RetornaPelaChavePrimaria(UnidCadastro).NO_FANTAS_EMP : "Todos");
            noUnidContrato = (UnidContrato != 0 ? TB25_EMPRESA.RetornaPelaChavePrimaria(UnidCadastro).NO_FANTAS_EMP : "Todos");
            noRegiao = (regiao != 0 ? TB906_REGIAO.RetornaPelaChavePrimaria(regiao).NM_REGIAO : "Todos");
            noArea = (area != 0 ? TB907_AREA.RetornaPelaChavePrimaria(area).NM_AREA : "Todos");
            noSubarea = (subarea != 0 ? TB908_SUBAREA.RetornaPelaChavePrimaria(subarea).NM_SUBAREA : "Todos");
            noUF = (uf != "0" ? TB74_UF.RetornaPelaChavePrimaria(uf).CODUF : "Todos");
            noCidade = (Cidade != 0 ? TB904_CIDADE.RetornaPelaChavePrimaria(Cidade).NO_CIDADE : "Todos");
            noBairro = (Bairro != 0 ? TB905_BAIRRO.RetornaPelaChavePrimaria(Bairro).NO_BAIRRO : "Todos");
            noClassFunc = (classFunc != 0 ? TB128_FUNCA_FUNCI.RetornaPelaChavePrimaria(classFunc).NO_FUNCA_FUNCI : "Todos");
            noCategoria = (categoria != 0 ? TB127_CATEG_FUNCI.RetornaPelaChavePrimaria(categoria).NO_CATEG_FUNCI : "Todos");
            noEspecializa = (especializa != 0 ? TB115_GRUPO_ESPECIALIDADE.RetornaPelaChavePrimaria(especializa).DE_GRUPO_ESPECI : "Todos");
            tipoRelatorio = "Por Função";

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            parametros = "( Unidade Cadastro: " + noUnidCadastro.ToUpper() + " - Unidade Contrato: " + noUnidContrato.ToUpper() + " - Região/Área: " + noRegiao.ToUpper()
                + " - Área: " + noArea.ToUpper() + " - Subarea: " + noSubarea + " - UF: " + noUF + " - Cidade: " + noCidade + " - Bairro: " + noBairro.ToUpper()
                + " - Classificação Funcional: " + noClassFunc.ToUpper() + " - Categoria: " + noCategoria.ToUpper() + " - Especialidade: " + noEspecializa.ToUpper() + " )";

            RptRelFuncParamPorFuncao fpcb = new RptRelFuncParamPorFuncao();
            lRetorno = fpcb.InitReport(parametros, infos, coEmp, UnidCadastro, UnidContrato, regiao, area, subarea, uf, Cidade, Bairro, classFunc, categoria, especializa, (chkEmiteGraf.Checked ? true : false), (chkComRelatorio.Checked ? true : false));
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        void porCidadeBairro()
        {
            string parametros;
            string infos;
            int coEmp = LoginAuxili.CO_EMP;
            int lRetorno, UnidCadastro, UnidContrato, regiao, area, subarea, Cidade, Bairro, classFunc, categoria, especializa;
            string tipoRelatorio, noUnidCadastro, noUnidContrato, noRegiao, noArea, noSubarea, noUF, noCidade, noBairro, noClassFunc, noCategoria, noEspecializa, uf;

            UnidCadastro = int.Parse(ddlUnidCadastro.SelectedValue);
            UnidContrato = int.Parse(ddlUnidContrato.SelectedValue);
            regiao = int.Parse(ddlReg.SelectedValue);
            area = int.Parse(ddlArea.SelectedValue);
            subarea = int.Parse(ddlSubArea.SelectedValue);
            uf = ddlUF.SelectedValue;
            Cidade = int.Parse(ddlCidade.SelectedValue);
            Bairro = int.Parse(ddlBairro.SelectedValue);
            classFunc = int.Parse(ddlClassFuncion.SelectedValue);
            categoria = int.Parse(ddlCategoria.SelectedValue);
            especializa = int.Parse(ddlEspecia.SelectedValue);

            noUnidCadastro = (UnidCadastro != 0 ? TB25_EMPRESA.RetornaPelaChavePrimaria(UnidCadastro).NO_FANTAS_EMP : "Todos");
            noUnidContrato = (UnidContrato != 0 ? TB25_EMPRESA.RetornaPelaChavePrimaria(UnidCadastro).NO_FANTAS_EMP : "Todos");
            noRegiao = (regiao != 0 ? TB906_REGIAO.RetornaPelaChavePrimaria(regiao).NM_REGIAO : "Todos");
            noArea = (area != 0 ? TB907_AREA.RetornaPelaChavePrimaria(area).NM_AREA : "Todos");
            noSubarea = (subarea != 0 ? TB908_SUBAREA.RetornaPelaChavePrimaria(subarea).NM_SUBAREA : "Todos");
            noUF = (uf != "0" ? TB74_UF.RetornaPelaChavePrimaria(uf).CODUF : "Todos");
            noCidade = (Cidade != 0 ? TB904_CIDADE.RetornaPelaChavePrimaria(Cidade).NO_CIDADE : "Todos");
            noBairro = (Bairro != 0 ? TB905_BAIRRO.RetornaPelaChavePrimaria(Bairro).NO_BAIRRO : "Todos");
            noClassFunc = (classFunc != 0 ? TB128_FUNCA_FUNCI.RetornaPelaChavePrimaria(classFunc).NO_FUNCA_FUNCI : "Todos");
            noCategoria = (categoria != 0 ? TB127_CATEG_FUNCI.RetornaPelaChavePrimaria(categoria).NO_CATEG_FUNCI : "Todos");
            noEspecializa = (especializa != 0 ? TB115_GRUPO_ESPECIALIDADE.RetornaPelaChavePrimaria(especializa).DE_GRUPO_ESPECI : "Todos");
            tipoRelatorio = "Por Cidade/Bairro";

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            parametros = "( Unidade Cadastro: " + noUnidCadastro.ToUpper() + " - Unidade Contrato: " + noUnidContrato.ToUpper() + " - Região/Área: " + noRegiao.ToUpper()
                + " - Área: " + noArea.ToUpper() + " - Subarea: " + noSubarea + " - UF: " + noUF + " - Cidade: " + noCidade + " - Bairro: " + noBairro.ToUpper()
                + " - Classificação Funcional: " + noClassFunc.ToUpper() + " - Categoria: " + noCategoria.ToUpper() + " - Especialidade: " + noEspecializa.ToUpper() + " )";

            RptRleFuncParamCid fpcb = new RptRleFuncParamCid();
            lRetorno = fpcb.InitReport(parametros, infos, coEmp, UnidCadastro, UnidContrato, regiao, area, subarea, uf, Cidade, Bairro, classFunc, categoria, especializa, (chkEmiteGraf.Checked ? true : false), (chkComRelatorio.Checked ? true : false));
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        protected void CarregaUnidade()
        {

            var res = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                       select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP });

            ddlUnidCadastro.DataTextField = "NO_FANTAS_EMP";
            ddlUnidCadastro.DataValueField = "CO_EMP";

            ddlUnidCadastro.DataSource = res;
            ddlUnidCadastro.DataBind();

            ddlUnidCadastro.Items.Insert(0, new ListItem("Todos", "0"));

            //ddlUnidCadastro.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        protected void CarregaUnidadeContr()
        {

            var res = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                       select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP });

            ddlUnidContrato.DataTextField = "NO_FANTAS_EMP";
            ddlUnidContrato.DataValueField = "CO_EMP";

            ddlUnidContrato.DataSource = res;
            ddlUnidContrato.DataBind();

            ddlUnidContrato.Items.Insert(0, new ListItem("Todos", "0"));
        }

        #region LOGISTICA

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

            int regiao = int.Parse(ddlReg.SelectedValue);

            if (regiao != 0)
            {

                var res = (from tb907 in TB907_AREA.RetornaTodosRegistros()
                           where (ddlReg.SelectedValue != "0" ? tb907.TB906_REGIAO.ID_REGIAO == regiao : 0 == 0)
                           select new { tb907.ID_AREA, tb907.NM_AREA, });

                ddlArea.DataTextField = "NM_AREA";
                ddlArea.DataValueField = "ID_AREA";

                ddlArea.DataSource = res;
                ddlArea.DataBind();

                ddlArea.Items.Insert(0, new ListItem("Todos", "0"));
            }
            else
            {
                ddlArea.Items.Clear();
                ddlArea.Items.Insert(0, new ListItem("Todos", "0"));
            }

        }

        protected void CarregaSubArea()
        {
            int area = int.Parse(ddlArea.SelectedValue);

            if (area != 0)
            {
                var res = (from tb908 in TB908_SUBAREA.RetornaTodosRegistros()
                           where (ddlArea.SelectedValue != "0" ? tb908.TB907_AREA.ID_AREA == area : 0 == 0)
                           select new { tb908.ID_SUBAREA, tb908.NM_SUBAREA });

                ddlSubArea.DataTextField = "NM_SUBAREA";
                ddlSubArea.DataValueField = "ID_SUBAREA";

                ddlSubArea.DataSource = res;
                ddlSubArea.DataBind();

                ddlSubArea.Items.Insert(0, new ListItem("Todos", "0"));
            }
            else
            {
                ddlSubArea.Items.Clear();
                ddlSubArea.Items.Insert(0, new ListItem("Todos", "0"));
            }
        }

        protected void carregaUF()
        {
            var res = (from tb74 in TB74_UF.RetornaTodosRegistros()
                       select new { tb74.CODUF });

            ddlUF.DataTextField = "CODUF";
            ddlUF.DataValueField = "CODUF";
            ddlUF.DataSource = res;
            ddlUF.DataBind();
            ddlUF.Items.Insert(0, new ListItem("Todos", "0"));
        }

        protected void carregaCidade()
        {
            string uf = ddlUF.SelectedValue;

            if (uf != "0")
            {
                var res = (from tb904 in TB904_CIDADE.RetornaTodosRegistros()
                           where (ddlUF.SelectedValue != "0" ? tb904.CO_UF == uf : 0 == 0)
                           select new { tb904.CO_CIDADE, tb904.NO_CIDADE });

                ddlCidade.DataTextField = "NO_CIDADE";
                ddlCidade.DataValueField = "CO_CIDADE";
                ddlCidade.DataSource = res;
                ddlCidade.DataBind();
                ddlCidade.Items.Insert(0, new ListItem("Todos", "0"));
            }
            else
            {
                ddlCidade.Items.Clear();
                ddlCidade.Items.Insert(0, new ListItem("Todos", "0"));
            }

        }

        protected void carregaBairro()
        {
            string uf = ddlUF.SelectedValue;
            int cidade = int.Parse(ddlCidade.SelectedValue);

            if ((uf != "0") && (cidade != 0))
            {
                var res = (from tb905 in TB905_BAIRRO.RetornaTodosRegistros()
                           where tb905.CO_UF == uf && tb905.CO_CIDADE == cidade
                           select new { tb905.CO_BAIRRO, tb905.NO_BAIRRO });

                ddlBairro.DataValueField = "CO_BAIRRO";
                ddlBairro.DataTextField = "NO_BAIRRO";
                ddlBairro.DataSource = res;
                ddlBairro.DataBind();
                ddlBairro.Items.Insert(0, new ListItem("Todos", "0"));
            }
            else
            {
                ddlBairro.Items.Clear();
                ddlBairro.Items.Insert(0, new ListItem("Todos", "0"));
            }
        }

        #endregion

        #region PARAMETROS

        private void carregaClassFunc()
        {
            var res = (from tb128 in TB128_FUNCA_FUNCI.RetornaTodosRegistros()
                       select new { tb128.ID_FUNCA_FUNCI, tb128.NO_FUNCA_FUNCI });

            ddlClassFuncion.DataTextField = "NO_FUNCA_FUNCI";
            ddlClassFuncion.DataValueField = "ID_FUNCA_FUNCI";
            ddlClassFuncion.DataSource = res;
            ddlClassFuncion.DataBind();
            ddlClassFuncion.Items.Insert(0, new ListItem("Todos", "0"));
        }

        private void carregaCategoria()
        {
            var res = (from tb127 in TB127_CATEG_FUNCI.RetornaTodosRegistros()
                       select new { tb127.ID_CATEG_FUNCI, tb127.NO_CATEG_FUNCI });

            ddlCategoria.DataTextField = "NO_CATEG_FUNCI";
            ddlCategoria.DataValueField = "ID_CATEG_FUNCI";
            ddlCategoria.DataSource = res;
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0, new ListItem("Todos", "0"));
        }

        protected void carregaGrpEspec()
        {
            var res = (from tb115 in TB115_GRUPO_ESPECIALIDADE.RetornaTodosRegistros()
                       select new { tb115.ID_GRUPO_ESPECI, tb115.DE_GRUPO_ESPECI });

            ddlGrpEspec.DataValueField = "ID_GRUPO_ESPECI";
            ddlGrpEspec.DataTextField = "DE_GRUPO_ESPECI";
            ddlGrpEspec.DataSource = res;
            ddlGrpEspec.DataBind();
            ddlGrpEspec.Items.Insert(0, new ListItem("Todos", "0"));
        }

        protected void carregaEspec()
        {
            int grupoEsp = int.Parse(ddlGrpEspec.SelectedValue);

            if (grupoEsp != 0)
            {

                var res = (from tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros()
                           where (tb63.TB115_GRUPO_ESPECIALIDADE.ID_GRUPO_ESPECI == grupoEsp)
                           select new { tb63.CO_ESPEC, tb63.NO_ESPECIALIDADE });

                ddlEspecia.DataTextField = "NO_ESPECIALIDADE";
                ddlEspecia.DataValueField = "CO_ESPEC";
                ddlEspecia.DataSource = res;
                ddlEspecia.DataBind();
                ddlEspecia.Items.Insert(0, new ListItem("Todos", "0"));
            }
            else
            {
                ddlEspecia.Items.Clear();
                ddlEspecia.Items.Insert(0, new ListItem("Todos", "0"));
            }
        }

        #endregion

        #endregion

        #region Funções de Campo

        protected void ddlGrpEspec_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaEspec();
        }

        protected void ddlUF_SelectedIndexChanged(object sender, EventArgs e)
        {
            carregaCidade();
            carregaBairro();
        }

        protected void ddlCidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            carregaBairro();
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