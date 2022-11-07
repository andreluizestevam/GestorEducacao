//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE PONTO DO COLABORADOR
// OBJETIVO: REGISTRO DE PLANTÃO
// DATA DE CRIAÇÃO: 20/05/2014
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
// 
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Data;
using System.ServiceModel;
using System.IO;
using System.Reflection;
using Resources;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3010_CtrlPedagogicoSeries;
using C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno;

namespace C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7100_CtrlPontoFuncional._7160_AssociaPlantoesColabor
{
    public partial class AssociaPlantoesColabor : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //txtDtIniResCons.Text = DateTime.Now.ToString("dd/MM/yyyy");

                carregaGrpEspec();
                CarregaUnidades(ddlUnidRealPlant);
                //CarregaUnidades(ddlUnidResCons);
                CarregaGridHorario(null);
                CarregaGridProfi();

                ddlEspMedResCons.Items.Insert(0, new ListItem("Todos", "0"));
            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //    --------> criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
        }

        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {

            ///Varre toda a gride de Solicitações e salva na tabela TB114_FARDMAT 
            foreach (GridViewRow linha in grdHorario.Rows)
            {
                CheckBox grHr = ((CheckBox)linha.Cells[0].FindControl("chkSelect2"));

                if (grHr.Checked)
                //if (((CheckBox)linha.Cells[0].FindControl("chkSelect2")).Checked)
                {
                    if (!string.IsNullOrEmpty(hdCoCOl.Value))
                    {
                        int cocol = int.Parse(hdCoCOl.Value);
                        HiddenField hdfPla = ((HiddenField)linha.Cells[0].FindControl("hidCoPla"));

                        var res = from tb154 in TB154_PLANT_COLABOR.RetornaPeloCoColPlantao(cocol, int.Parse(hdfPla.Value))
                                  select tb154;
                        
                        if (res.Count() == 0)
                        {
                            TB154_PLANT_COLABOR tb154 = new TB154_PLANT_COLABOR
                            {
                                CO_COL = cocol,
                                ID_TIPO_PLANT = int.Parse(hdfPla.Value)
                            };
                            GestorEntities.SaveOrUpdate(tb154);
                            CurrentPadraoCadastros.CurrentEntity = tb154;
                        }
                    }
                }
                else
                {
                    int cocol = int.Parse(hdCoCOl.Value);
                    HiddenField hdfPla = ((HiddenField)linha.Cells[0].FindControl("hidCoPla"));

                    //var res = from tb154 in TB154_PLANT_COLABOR.RetornaPeloCoColPlantao(cocol, int.Parse(hdfPla.Value))
                    //          select tb154;
                }
            }
        }

        #region Carregamentos

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
            int empCo = int.Parse(ddlUnidRealPlant.SelectedValue);

            if (grupoEsp != 0)
            {

                var res = (from tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros()
                           where (tb63.TB115_GRUPO_ESPECIALIDADE.ID_GRUPO_ESPECI == grupoEsp
                                    && tb63.CO_EMP == empCo)
                           select new { tb63.CO_ESPECIALIDADE, tb63.NO_ESPECIALIDADE }).Distinct();

                ddlEspMedResCons.DataTextField = "NO_ESPECIALIDADE";
                ddlEspMedResCons.DataValueField = "CO_ESPECIALIDADE";
                ddlEspMedResCons.DataSource = res;
                ddlEspMedResCons.DataBind();
                ddlEspMedResCons.Items.Insert(0, new ListItem("Todos", "0"));
            }
            else
            {
                ddlEspMedResCons.Items.Clear();
                ddlEspMedResCons.Items.Insert(0, new ListItem("Todos", "0"));
            }
        }

        private void CarregaUnidades(DropDownList ddl)
        {
            var res = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                       select new
                       {
                           tb25.CO_EMP,
                           tb25.NO_FANTAS_EMP
                       });

            ddl.DataValueField = "CO_EMP";
            ddl.DataTextField = "NO_FANTAS_EMP";

            ddl.DataSource = res;
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Todos", "0"));

            ddl.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        private void CarregaGridHorario(int? coC)
        {
            var res = (from tb153 in TB153_TIPO_PLANT.RetornaTodosRegistros()
                       //join tb154 in TB154_PLANT_COLABOR.RetornaTodosRegistros() on tb153.ID_TIPO_PLANT equals tb154.ID_TIPO_PLANT into l1
                       //from ls in l1.DefaultIfEmpty()
                       //where (coC != null ? ls.CO_COL == coC : 0 == 0)

                       select new HorarioSaida
                       {
                           co_Plan = tb153.ID_TIPO_PLANT,
                           sigla = tb153.CO_SIGLA_TIPO_PLANT,
                           hrIni = tb153.HR_INI_TIPO_PLANT,
                           cH = tb153.QT_HORAS,
                           seg = tb153.FL_SEGUN,
                           ter = tb153.FL_TERCA,
                           qua = tb153.FL_QUARTA,
                           qui = tb153.FL_QUINTA,
                           sex = tb153.FL_SEXTA,
                           sab = tb153.FL_SABAD,
                           dom = tb153.FL_DOMIN,
                           //chkpla = ls != null ? true : false
                       });
					   
                       //foreach(HorarioSaida li in res)
                       //{
                       //    var re
                       //     li.chkpla
                       //}



            //carregaOsPlantoes();

            grdHorario.DataSource = res;
            grdHorario.DataBind();
        }

        private void CarregaGridProfi()
        {
            int coEsp = int.Parse(ddlEspMedResCons.SelectedValue);
            int coEmp = int.Parse(ddlUnidRealPlant.SelectedValue);

            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb03.CO_EMP equals tb25.CO_EMP
                       join t63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb03.CO_ESPEC equals t63.CO_ESPEC into l1
                       from le in l1.DefaultIfEmpty()

                       where tb03.FL_PERM_PLANT == "S"
                       && (coEmp != 0 ? tb03.CO_EMP == coEmp : 0 == 0)
                       && (coEsp != 0 ? tb03.CO_ESPEC == coEsp : 0 == 0)


                       select new GrdProfiSaida
                       {
                           CO_COL = tb03.CO_COL,
                           NO_COL = tb03.NO_COL,
                           NO_EMP = tb25.sigla,
                           DE_ESP = le.NO_ESPECIALIDADE
                       });

            grdProfi.DataSource = res;
            grdProfi.DataBind();
        }


        #endregion


        #region Classes de saída

        public class HorarioSaida
        {
            public int co_Plan { get; set; }
            public string sigla { get; set; }
            public int cH { get; set; }
            public string ch
            {
                get
                {
                    return this.cH.ToString().PadLeft(2, '0');
                }
            }
            public string hrIni { get; set; }
            public string hrFim
            {
                get
                {
                    int h = int.Parse(this.hrIni.Substring(0, 2));
                    int m = int.Parse(this.hrIni.Substring(3, 2));
                    int th = 24;

                    h = h + cH;

                    if (h >= th)
                    {
                        h = h - th;
                    }
                    var h2 = h.ToString().PadLeft(2, '0');
                    var m2 = m.ToString().PadLeft(2, '0');
                    return h2 + ":" + m2;
                    //return h.ToString().PadLeft(2, '0');
                }
            }
            public string horario
            {
                get
                {
                    return this.hrIni + " - " + this.hrFim;
                }
            }

            public string seg { get; set; }
            public bool segV
            {
                get
                {
                    if (this.seg == "S")
                        return true;
                    else
                        return false;
                }
            }
            public string ter { get; set; }
            public bool terV
            {
                get
                {
                    if (this.ter == "S")
                        return true;
                    else
                        return false;
                }
            }
            public string qua { get; set; }
            public bool quaV
            {
                get
                {
                    if (this.qua == "S")
                        return true;
                    else
                        return false;
                }
            }
            public string qui { get; set; }
            public bool quiV
            {
                get
                {
                    if (this.qui == "S")
                        return true;
                    else
                        return false;
                }
            }
            public string sex { get; set; }
            public bool sexV
            {
                get
                {
                    if (this.sex == "S")
                        return true;
                    else
                        return false;
                }
            }
            public string sab { get; set; }
            public bool sabV
            {
                get
                {
                    if (this.sab == "S")
                        return true;
                    else
                        return false;
                }
            }
            public string dom { get; set; }
            public bool domV
            {
                get
                {
                    if (this.dom == "S")
                        return true;
                    else
                        return false;
                }
            }

            public bool chkpla { get; set; }
            //public bool chkplav
            //{
            //    get
            //    {
            //        if (this.chkpla == "s")
            //            return true;

            //        else
            //            return false;
            //    }
            //} 

        }

        public class GrdProfiSaida
        {
            public int CO_COL { get; set; }
            public string NO_COL { get; set; }
            public string NO_EMP { get; set; }
            public string DE_ESP { get; set; }
        }

        #endregion

        #region Métodos de campo

        protected void ddlUnidRealPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CarregaDepartamento();
            CarregaGridProfi();
        }

        protected void ddlGrpEspec_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaEspec();
            CarregaGridProfi();
        }

        protected void ddlEspMedResCons_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGridProfi();
        }


        // Desmarca todos os checkbox que não forem clicados
        protected void ckSelect_CheckedChange(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;
            int coCol = 0;

            // Valida se a grid de atividades possui algum registro
            if (grdProfi.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdProfi.Rows)
                {
                    chk = (CheckBox)linha.Cells[0].FindControl("ckSelect");

                    // Desmarca todos os registros menos o que foi clicado
                    if (chk.ClientID != atual.ClientID)
                    {
                        chk.Checked = false;
                    }
                    else
                    {
                        if (chk.Checked)
                        {
                            coCol = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCoCol")).Value);
                        }
                    }
                }
            }

            hdCoCOl.Value = coCol.ToString();
            carregaOsPlantoes();
            //CarregaGridHorario(int.Parse(hdCoCOl.Value));

            //    foreach (GridViewRow linha in grdHorario.Rows)
            //    {
            //        //CheckBox grHr = ((CheckBox)linha.Cells[0].FindControl("chkSelect2"));
            //        //CheckBox grCo = ((CheckBox)linha.Cells[0].FindControl(""));
            //        string cocol = hdCoCOl.Value;

            //        if(!string.IsNullOrEmpty(cocol))
            //        {
            //            int co2 = int.Parse(hdCoCOl.Value);
            //            HiddenField hdfPla = ((HiddenField)linha.Cells[0].FindControl("hidCoPla"));

            //            var res = from tb154 in TB154_PLANT_COLABOR.RetornaPeloCoColPlantao(co2, int.Parse(hdfPla.Value))
            //                      select tb154;

            //            HorarioSaida v = new HorarioSaida();
            //            if (res.Count() > 0)
            //                v.chkpla = "s";

            //            else
            //                v.chkpla = "n";
            //        }
            //        CarregaGridHorario();

            //}
        }
        #endregion

        private void carregaOsPlantoes()
        {
            foreach (GridViewRow linha in grdHorario.Rows)
            {
                //CheckBox grHr = ((CheckBox)linha.Cells[0].FindControl("chkSelect2"));
                //CheckBox grCo = ((CheckBox)linha.Cells[0].FindControl(""));
                string cocol = hdCoCOl.Value;

                if (!string.IsNullOrEmpty(cocol))
                {
                    int co2 = int.Parse(hdCoCOl.Value);
                    HiddenField hdfPla = ((HiddenField)linha.Cells[0].FindControl("hidCoPla"));

                    var res = from tb154 in TB154_PLANT_COLABOR.RetornaPeloCoColPlantao(co2, int.Parse(hdfPla.Value))
                              select tb154;

                    HorarioSaida v = new HorarioSaida();
                    if (res.Count() > 0)
                        v.chkpla = true;

                    else
                        v.chkpla = false;
                }
            }
        }

        #region funções de campo
        
        //protected void OnRowDataBound_grdHorario(object sender, GridViewRowEventArgs e)
        //{

        //    foreach (GridViewRow linha in grdHorario.Rows)
        //    {

        //        string cocol = hdCoCOl.Value;

        //        if (!string.IsNullOrEmpty(cocol))
        //        {
        //            int co2 = int.Parse(hdCoCOl.Value);
        //            HiddenField hdfPla = ((HiddenField)linha.Cells[0].FindControl("hidCoPla"));

        //            var res = from tb154 in TB154_PLANT_COLABOR.RetornaPeloCoColPlantao(co2, int.Parse(hdfPla.Value))
        //                      select tb154;

        //            if (res.Count() > 0)
        //            {
        //                int i = e.Row.RowIndex;
        //            }
                    
        //        }

        //    }
        //}

        #endregion

    }
}